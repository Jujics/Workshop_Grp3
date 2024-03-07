using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    // Variables publiques pour ajuster le comportement du joueur
    public float movementSpeed = 15.0f;    // Vitesse de déplacement du joueur
    public float jumpForce = 10.0f;         // Force de saut du joueur
    public int score = 100000;              // Score du joueur
    public float frollbo = 25.0f;           // Bonus de vitesse du joueur
    public int n = 0;                       // Variable de comptage
    public bool isboosingout;               // Indique si le joueur accélère
    public bool isslowingout;               // Indique si le joueur ralentit
    public bool isboosingin;                // Placeholder, non utilisé dans ce script
    public CinemachineVirtualCamera vcam;   // Caméra virtuelle utilisée pour ajuster la vue
    public GameObject winui;                // Interface utilisateur de victoire
    public GameObject loseui;               // Interface utilisateur de défaite

    private Rigidbody rb;                   // Composant Rigidbody du joueur
    private bool isGrounded;                // Indique si le joueur est au sol
    private const float LengthMultiplier = 4f;  // Facteur multiplicateur pour la longueur du rayon de débogage

    private void Start()
    {
        // Initialisation du composant Rigidbody au démarrage
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
        if (isboosingout == false && movementSpeed != 15.0f)
        {
            float interpolationFactor = 1.5f;
            // Appliquer une interpolation linéaire pour décélérer en douceur
            movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, interpolationFactor * Time.deltaTime);
        }

        // Vérifier si le joueur est au sol
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Gestion de la défaite si le score est inférieur à -1
        if (score <= -1)
        {
            loseui.SetActive(true);
        }

        // Gestion du ralentissement du joueur
        if (isslowingout == true)
        {
            // Si la variable de comptage est inférieure à 100, ralentir la vitesse
            if (n <= 100)
            {
                movementSpeed += -0.01f;
                n += 1;
                vcam.m_Lens.FieldOfView -= 0.25f;
            }
            // Si la variable de comptage est entre 100 et 200, ralentir la vitesse plus lentement
            else if (n > 100 && n < 200)
            {
                movementSpeed += -0.005f;
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
        if (isslowingout == false && movementSpeed != 15.0f)
        {
            float interpolationFactor = 0.8f;
            // Appliquer une interpolation linéaire pour revenir à la vitesse normale en douceur
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 60f, interpolationFactor * Time.deltaTime);
            movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, interpolationFactor * Time.deltaTime);
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
        Debug.Log("horizontale " + horizontalInput + " verticale " + verticalInput);

        // Limiter l'entrée horizontale pour éviter des valeurs extrêmes
        if (horizontalInput > 0.70f)
        {
            horizontalInput = 0.70f;
        }
        else if (horizontalInput < -0.70f)
        {
            horizontalInput = -0.70f;
        }

        // Afficher à nouveau les informations de débogage
        Debug.Log("horizontale " + horizontalInput + " verticale " + verticalInput);

        // Limiter l'entrée verticale à une valeur minimale de 0
        verticalInput = Mathf.Max(0.0f, verticalInput);

        // Définir la direction du déplacement en fonction des entrées du joueur
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Appliquer la vélocité pour déplacer le joueur
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
    }

    // Méthode vérifiant si le clavier est utilisé
    bool IsKeyboardUsed()
    {
        return Input.anyKey;
    }

    // Méthode appelée lorsqu'un objet entre en collision avec le joueur
    void OnTriggerEnter(Collider other)
    {
        // Gérer les collisions avec différents types d'objets
        if (other.gameObject.CompareTag("dmgin"))
        {
            score -= 100;
            isslowingout = true;
        }
        else if (other.gameObject.CompareTag("froll"))
        {
            score += 100;
            isboosingout = true;
        }
        else if (other.gameObject.CompareTag("froll") && other.gameObject.CompareTag("dmgin"))
        {
            score -= 100;
            isslowingout = true;
        }
        else if (other.gameObject.CompareTag("winwall"))
        {
            winui.SetActive(true);
        }
        else if (other.gameObject.CompareTag("smscoreobj"))
        {
            score += 20;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("bgscoreobj"))
        {
            score += 100;
            other.gameObject.SetActive(false);
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
