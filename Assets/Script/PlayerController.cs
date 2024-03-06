using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 15.0f;
    public float rotationSpeed = 0.5f;
    public float maxRotationAngle = 30.0f;
    public float forwardSpeed = 10.0f;
    public CinemachineVirtualCamera vcam;
    public GameObject winui;
    public GameObject loseui;
    public int score = 100000;

    private Rigidbody rb;
    private bool isGrounded;
    private const float LengthMultiplier = 4f;
    private bool isBoostingOut;
    private bool isSlowingOut;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleBoostingOut();
        HandleSlowingOut();
        HandleRotationAndMovement();
        CheckGrounded();

        if (score <= -1)
        {
            loseui.SetActive(true);
        }
    }

    private void HandleBoostingOut()
    {
        if (isBoostingOut)
        {
            BoostOut();
        }
        else if (movementSpeed != 15.0f)
        {
            LerpToCenterRotation();
        }
    }

    private void BoostOut()
    {
        float interpolationFactor = 1.5f;
        movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, interpolationFactor * Time.deltaTime);
    }

    private void HandleSlowingOut()
    {
        if (isSlowingOut)
        {
            SlowOut();
        }
        else if (movementSpeed != 15.0f)
        {
            LerpToCenterRotation();
        }
    }

    private void SlowOut()
    {
        float interpolationFactor = 0.8f;
        vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 60f, interpolationFactor * Time.deltaTime);
        movementSpeed = Mathf.Lerp(movementSpeed, 15.0f, interpolationFactor * Time.deltaTime);
    }

    private void LerpToCenterRotation()
    {
        float lerpFactor = 0.1f;
        Quaternion centerRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, centerRotation, lerpFactor);
    }

    private void HandleRotationAndMovement()
    {
        float shiftInput = Input.GetKey(KeyCode.LeftShift) ? 1.0f : 0.0f;
        float leftInput = Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0.0f;
        float rightInput = Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0.0f;

        // Calculate forward movement based on left shift key
        Vector3 forwardMovement = Vector3.forward * shiftInput * forwardSpeed;

        // Calculate rotation based on left and right keys
        float targetRotationAngle = (rightInput - leftInput) * maxRotationAngle;
        float currentRotationAngle = transform.eulerAngles.y;
        float rotationAngle = Mathf.Clamp(targetRotationAngle, currentRotationAngle - maxRotationAngle, currentRotationAngle + maxRotationAngle);
        Quaternion rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

        // Apply rotation and movement to the player's transform
        transform.rotation = rotation;
        transform.Translate(forwardMovement * Time.deltaTime);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dmgin"))
        {
            score -= 100;
            isSlowingOut = true;
        }
        else if (other.CompareTag("froll"))
        {
            score += 100;
            isBoostingOut = true;
        }
        else if (other.CompareTag("froll") && other.CompareTag("dmgin"))
        {
            score -= 100;
            isSlowingOut = true;
        }
        else if (other.CompareTag("winwall"))
        {
            winui.SetActive(true);
        }
        else if (other.CompareTag("smscoreobj"))
        {
            score += 20;
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("bgscoreobj"))
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
