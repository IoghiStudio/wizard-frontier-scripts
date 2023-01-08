using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [SerializeField] Transform playerTransform = null;
    [SerializeField] float mouseSensitivity = 4.2f;
    [SerializeField] bool lockCamera = true;
    [SerializeField] float walkSpeed = 8;
    public Transform broomContainer;
    Rigidbody playerRb;
    public ParticleSystem exitBroomEffect;

    public float maxHealth = 100;
    public float maxMana = 100;
    public float health = 75;
    public float mana = 75;

    float cameraPitch = 0.0f;
    CharacterController controller = null;
    // Start is called before the first frame update
    void Start()
    {
        controller =GetComponent<CharacterController>();
        if(lockCamera)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMouseMovement();
    }
    public void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity * Time.deltaTime * 100;
        cameraPitch = Mathf.Clamp(cameraPitch, -50.0f, 30.0f);
        
        playerTransform.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity * Time.deltaTime * 100);
    }

    public void UpdateMouseMovement()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDir.Normalize();

        Vector3 velocity = (transform.forward * moveDir.y + transform.right * moveDir.x) * walkSpeed;

        controller.Move(velocity * Time.deltaTime);
    }
}