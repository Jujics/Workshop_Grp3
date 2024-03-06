using UnityEngine;
using Cinemachine;


public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 15.0f;
    public float jumpForce = 10.0f;
    public int score = 100000;
    public float frollbo = 25.0f;
    public int n = 0;
    public bool isboosingout;
    public bool isslowingout;
    public bool isboosingin;
    public CinemachineVirtualCamera vcam;
    public GameObject winui;
    public GameObject loseui;
    private Rigidbody rb;
    private bool isGrounded;
    private const float LengthMultiplier = 4f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isboosingout == true)
        {
            if (n <= 100)
            {
                movementSpeed += 0.5f;
                n += 1;
            }
            else if (n > 100 && n < 200)
            {
                movementSpeed += 0.2f;
                n += 1;
            }
            else
            {
                n = 0;
                isboosingout = false;
            }
        }
        if (isboosingout == false && movementSpeed != 15.0f)
        {
            float interpolationFactor = 1.5f;
            movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, interpolationFactor * Time.deltaTime);
        }
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if(score <= -1)
        {
            loseui.SetActive(true);
        }


        if (isslowingout == true)
        {
            if (n <= 100)
            {
                movementSpeed += -0.01f;
                n += 1;
                vcam.m_Lens.FieldOfView -= 0.25f;
            }
            else if (n > 100 && n < 200)
            {
                movementSpeed += -0.005f;
                n += 1;
            }
            else
            {
                n = 0;
                isslowingout = false;
            }
        }
        if (isslowingout == false && movementSpeed != 15.0f)
        {
            float interpolationFactor = 0.8f;
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 60f, interpolationFactor * Time.deltaTime);
            movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, interpolationFactor * Time.deltaTime);
        }
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        verticalInput = Mathf.Max(0.0f, verticalInput);
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
        Debug.Log(score);   
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("dmgin"))
        {
            score -= 100;
            isslowingout = true;
        }
        else if (other.gameObject.CompareTag("froll"))
        {
            score += 100;
            isboosingout = true;
        }
        else if (other.gameObject.CompareTag("froll")&& other.gameObject.CompareTag("dmgin"))
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

    private void OnDrawGizmos()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        var playerpos = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(playerpos, moveDirection * LengthMultiplier);
    }
}