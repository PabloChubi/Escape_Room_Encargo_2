using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
 [SerializeField] private Transform camTransform;
    [SerializeField] public float mouseSensitivity = 200f;
    [SerializeField] Rigidbody rb;
    [SerializeField] float sfPlayerSpeed = 10f;
    [SerializeField] Vector3 playerMovement;
    [SerializeField] public float jumpForce = 0f;
    [SerializeField] bool isGrounded = true;
    
    

    private void Awake()
    {
        camTransform = GetComponentInChildren<Camera>().transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
 
    private void Update()
    {
            if(transform.position.y < 0)
        {
            transform.position = new Vector3(-16,1,-17);
        } 
    

        #region Movimiento de Player
        playerMovement.x = Input.GetAxis("Horizontal") * sfPlayerSpeed;
        playerMovement.y = Input.GetAxis("Vertical") * sfPlayerSpeed;

        transform.Translate(
            playerMovement.x * Time.deltaTime,
            0,
            playerMovement.y * Time.deltaTime);
        #endregion

        #region Camara
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(0, mouseX, 0);
        camTransform.Rotate(-mouseY, 0, 0);

        
        #endregion

     #region Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    #endregion
}
