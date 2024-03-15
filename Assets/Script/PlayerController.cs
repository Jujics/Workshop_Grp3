using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    // Variables publiques pour ajuster le comportement du joueur
    public float movementSpeed = 15.0f;    // Vitesse de déplacement du joueur
    public float boostSp = 120.0f;         // Vitesse max de boost 
    public float basicSp = 15.0f;          // Vitesse du joueur
    public float jumpForce = 10.0f;         // Force de saut du joueur
    public float frollbo = 25.0f;           // Bonus de vitesse du joueur
    public double elec = 0;                 // Jauge d'electricitée
    public int n = 0;                       // Variable de comptage
    public bool isboosingout;               // Indique si le joueur accélère
    public bool isslowingout;               // Indique si le joueur ralentit
    public bool isboosingin;                // Placeholder, non utilisé dans ce script
    public CinemachineVirtualCamera vcam;   // Caméra virtuelle utilisée pour ajuster la vue
    public GameObject winui;                // Interface utilisateur de victoire
    public Mesh newMesh1;                   //mesh voiture 1
    public Mesh newMesh2;                   //mesh voiture 2
    public bool IsOnTurnLeft;               // Indique si le joueur est dans un virage vers la gauche
    public bool IsOnTurnRight;              // Indique si le joueur est dans un virage vers la droite
    public bool IsInFog;
    public int FogCount = 0;
    private Rigidbody rb;                   // Composant Rigidbody du joueur
    private bool isGrounded;                // Indique si le joueur est au sol
    private float horizontalInput;  
    private float verticalInput;
    private const float LengthMultiplier = 4f;  // Facteur multiplicateur pour la longueur du rayon de débogage

    private void Start()
    {
        Application.targetFrameRate = 60;
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        int agrs = PlayerPrefs.GetInt("CarSelection");
        Debug.Log("test" + agrs);
        if (agrs == 1)
        {
            meshFilter.mesh = newMesh1;
        }
        else if (agrs == 0)
        {
            meshFilter.mesh = newMesh2;
        }
        movementSpeed = 0f;
        // Initialisation du composant Rigidbody au démarrage
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(IsInFog)
        {
            RenderSettings.fogDensity = 0.1f;
            FogCount += 1;
            if (FogCount == 200)
            {
                RenderSettings.fogDensity = 0.001f;
                IsInFog = false;
            }
        }
        // Gestion de l'accélération du joueur
        if (isboosingout == true)
        {
            // Si la variable de comptage est inférieure à 100, augmenter la vitesse
            if (n <= 100)
            {
                movementSpeed += 0.5f;
                n += 1;
            }
            // Si la variable de comptage est entre 100 et 200, augmenter la vitesse plus lentement
            else if (n > 100 && n < 200)
            {
                movementSpeed += 0.2f;
                n += 1;
            }
            // Sinon, réinitialiser la variable de comptage et arrêter l'accélération
            else
            {
                n = 0;
                isboosingout = false;
            }
        }

        // Gestion de la décélération du joueur
        if (isboosingout == false && movementSpeed != basicSp)
        {
            float interpolationFactor = 1.5f;
            // Appliquer une interpolation linéaire pour décélérer en douceur
            movementSpeed = Mathf.Lerp(movementSpeed, basicSp, interpolationFactor * Time.deltaTime);
        }

        // Vérifier si le joueur est au sol
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Gestion du ralentissement du joueur
        if (isslowingout == true)
        {
            // Si la variable de comptage est inférieure à 100, ralentir la vitesse
            if (n <= 100)
            {
                movementSpeed += -0.2f;
                n += 1;
                vcam.m_Lens.FieldOfView -= 0.5f;
            }
            // Si la variable de comptage est entre 100 et 200, ralentir la vitesse plus lentement
            else if (n > 100 && n < 200)
            {
                movementSpeed += -0.05f;
                n += 1;
            }
            // Sinon, réinitialiser la variable de comptage et arrêter le ralentissement
            else
            {
                n = 0;
                isslowingout = false;
            }
        }

        // Gestion du retour à la vitesse normale après le ralentissement
        if (isslowingout == false && movementSpeed != basicSp)
        {
            float interpolationFactor = 0.8f;
            // Appliquer une interpolation linéaire pour revenir à la vitesse normale en douceur
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 60f, interpolationFactor * Time.deltaTime);
            movementSpeed = Mathf.Lerp(movementSpeed, basicSp, interpolationFactor * Time.deltaTime);
        }

        // Gérer les entrées du joueur
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("RightTriggerButton");

        // Gérer les entrées du clavier
        if (IsKeyboardUsed())
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
            {
                verticalInput = 1.0f;
            }
        }
        if (verticalInput < 0.1f)
        {
            float interpolationFactor = 3.0f;
            verticalInput = 1.0f;
            movementSpeed = Mathf.Lerp(movementSpeed, 0f, interpolationFactor * Time.deltaTime);
        }
        

        // Afficher les informations de débogage
        float rotationStep = 10.0f * Time.deltaTime;
        // Limiter l'entrée horizontale pour éviter des valeurs extrêmes
        Debug.Log("aaa"+transform.rotation.eulerAngles.x);
        if (horizontalInput > 0.70f || IsOnTurnLeft)
        {
            if (IsOnTurnLeft)
            {                
                horizontalInput = 0.70f;
                transform.Rotate(Vector3.up, rotationStep);
            }
            else 
            {
                horizontalInput = 0.70f;
            }
        }
        else if (horizontalInput < -0.70f || IsOnTurnRight)
        {
            if (IsOnTurnRight)
            {                
                horizontalInput = -0.70f;
                transform.Rotate(Vector3.up, -rotationStep);
            }
            else
            {
                horizontalInput = -0.70f;
            }
        }
        if (Mathf.Abs(transform.rotation.eulerAngles.x - 270f) > 1f && !IsOnTurnLeft && !IsOnTurnRight)
        {
            float targetAngle = 270f;
            float currentAngle = transform.rotation.eulerAngles.x;
            float newAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime);
            transform.rotation = Quaternion.Euler(newAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        Debug.Log("bbb"+transform.rotation.eulerAngles.x);


        // Limiter l'entrée verticale à une valeur minimale de 0
        verticalInput = Mathf.Max(0.0f, verticalInput);

        // Définir la direction du déplacement en fonction des entrées du joueur
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Appliquer la vélocité pour déplacer le joueur
        elecmanager();
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
    }

    public void elecmanager()
    {
        if (Input.GetAxis("BoostBtn") > 0 && elec > 0)
        {
            Debug.Log(elec);
            float interpolationFactor = 10.0f;
            movementSpeed = Mathf.Lerp(movementSpeed, boostSp, interpolationFactor * Time.deltaTime);
            elec -= 0.1;
        }
    }

    // Méthode vérifiant si le clavier est utilisé
    bool IsKeyboardUsed()
    {
        return Input.anyKey;
    }

    // Méthode appelée lorsqu'un objet entre en collision avec le joueur
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("winwall"))
        {
            winui.SetActive(true);
        }
        else if (other.gameObject.CompareTag("elecgiv"))
        {
            if (elec + 10 < 100)
            {
                elec += 10;
                other.gameObject.SetActive(false);
            }
            else if (elec + 10 > 100)
            {
                elec = 100;
                other.gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.CompareTag("GoLeft"))
        {
            IsOnTurnLeft = true;
        }
        else if (other.gameObject.CompareTag("GoRight"))
        {
            IsOnTurnRight = true;
        }
        else if (other.gameObject.CompareTag("Schroom"))
        {
            IsInFog = true;
        }
        else if (other.gameObject.CompareTag("GravitySwitch"))
        {
            Physics.gravity = new Vector3(0, -20, 0);
        }
        
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GoLeft"))
        {
            IsOnTurnLeft = false;
        }
        else if (other.gameObject.CompareTag("GoRight"))
        {
            IsOnTurnRight = false;  
        }
        else if (other.gameObject.CompareTag("GravitySwitch"))
        {
            Physics.gravity = new Vector3(0, -300, 0);
        }
    }

    // Méthode appelée lorsqu'il est nécessaire de dessiner des gizmos pour le débogage
    private void OnDrawGizmos()
    {
        // Obtenir les entrées du clavier pour dessiner les rayons de débogage
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        var playerpos = transform.position;

        // Dessiner un rayon de débogage
        Gizmos.color = Color.red;
        Gizmos.DrawRay(playerpos, moveDirection * LengthMultiplier);
    }
}
