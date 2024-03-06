using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 15.0f;
    public float jumpForce = 10.0f;
    public int score = 100000;
    public float frollbo = 25.0f;

    private Rigidbody rb;
    private bool isGrounded;
    private const float LengthMultiplier = 4f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y , moveDirection.z * movementSpeed);;

        
        Debug.Log(score);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("dmgin"))
        {
            score -= 100;
        }
        else if (other.gameObject.CompareTag("froll"))
        {
            score += 100;

            while (movementSpeed < 25.0f)
            {
                movementSpeed = Mathf.Lerp(movementSpeed, 25.0f, Time.deltaTime);
            }

            while (movementSpeed > 15.0f)
            {
                movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, Time.deltaTime);
            }   
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