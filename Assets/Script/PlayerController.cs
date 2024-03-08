using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    // Variables publiques pour ajuster le comportement du joueur
    public float movementSpeed = 15.0f;    // Vitesse de déplacement du joueur
    public float jumpForce = 10.0f;         // Force de saut du joueur
    public float boostBonus = 25.0f;        // Bonus de vitesse du joueur lors du boost
    public double electricity = 0;          // Jauge d'électricité
    public int boostCounter = 0;            // Variable de comptage pour le boost
    public bool isBoosting;                 // Indique si le joueur est en train d'accélérer
    public bool isSlowing;                  // Indique si le joueur est en train de ralentir
    public bool isBoostingIn;               // Placeholder, non utilisé dans ce script
    public CinemachineVirtualCamera vcam;   // Caméra virtuelle utilisée pour ajuster la vue
    public GameObject winUI;                // Interface utilisateur de victoire
    private Rigidbody rb;                   // Composant Rigidbody du joueur
    private bool isGrounded;                // Indique si le joueur est au sol
    private const float RayLengthMultiplier = 4f;  // Facteur multiplicateur pour la longueur du rayon de débogage

    private void Start()
    {
        // Initialisation de la vitesse de déplacement au démarrage
        movementSpeed = 0f;
        // Initialisation du composant Rigidbody au démarrage
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Gestion de l'accélération du joueur
        if (isBoosting)
        {
            // Si le compteur de boost est inférieur à 100, augmenter la vitesse
            if (boostCounter <= 100)
            {
                movementSpeed += 0.5f;
                boostCounter += 1;
            }
            // Si le compteur de boost est entre 100 et 200, augmenter la vitesse plus lentement
            else if (boostCounter > 100 && boostCounter < 200)
            {
                movementSpeed += 0.2f;
                boostCounter += 1;
            }
            // Sinon, réinitialiser le compteur de boost et arrêter l'accélération
            else
            {
                boostCounter = 0;
                isBoosting = false;
            }
        }

        // Gestion de la décélération du joueur
        if (!isBoosting && movementSpeed != 15.0f)
        {
            float interpolationFactor = 1.5f;
            // Appliquer une interpolation linéaire pour décélérer en douceur
            movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, interpolationFactor * Time.deltaTime);
        }

        // Vérifier si le joueur est au sol
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Gestion du ralentissement du joueur
        if (isSlowing)
        {
            // Si le compteur de ralentissement est inférieur à 100, ralentir la vitesse
            if (boostCounter <= 100)
            {
                movementSpeed += -0.01f;
                boostCounter += 1;
                vcam.m_Lens.FieldOfView -= 0.25f;
            }
            // Si le compteur de ralentissement est entre 100 et 200, ralentir la vitesse plus lentement
            else if (boostCounter > 100 && boostCounter < 200)
            {
                movementSpeed += -0.005f;
                boostCounter += 1;
            }
            // Sinon, réinitialiser le compteur de ralentissement et arrêter le ralentissement
            else
            {
                boostCounter = 0;
                isSlowing = false;
            }
        }

        // Gestion du retour à la vitesse normale après le ralentissement
        if (!isSlowing && movementSpeed != 15.0f)
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
        Debug.Log("Horizontal: " + horizontalInput + " Vertical: " + verticalInput);

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
        Debug.Log("Horizontal: " + horizontalInput + " Vertical: " + verticalInput);

        // Limiter l'entrée verticale à une valeur minimale de 0
        verticalInput = Mathf.Max(0.0f, verticalInput);

        // Définir la direction du déplacement en fonction des entrées du joueur
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Appliquer la vélocité pour déplacer le joueur
        ElectricityManager();
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
    }

    public void ElectricityManager()
    {
        if (Input.GetAxis("BoostBtn") > 0 && electricity > 0)
        {
            Debug.Log(electricity);
            float interpolationFactor = 10.0f;
            movementSpeed = Mathf.Lerp(movementSpeed, 120f, interpolationFactor * Time.deltaTime);
            electricity -= 0.02;
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
            winUI.SetActive(true);
        }
        else if (other.gameObject.CompareTag("electricitygiver"))
        {
            if (electricity + 10 < 100)
            {
                electricity += 10;
                other.gameObject.SetActive(false);
            }
            else if (electricity + 10 > 100)
            {
                electricity = 100;
                other.gameObject.SetActive(false);
            }
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
        Gizmos.DrawRay(playerpos, moveDirection * RayLengthMultiplier);
    }
}
