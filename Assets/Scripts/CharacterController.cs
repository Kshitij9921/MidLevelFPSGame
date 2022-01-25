using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float playerSpeed;
    public float playerJumpForce;
    private Rigidbody rb;
    private bool isGrounded;
    private CapsuleCollider capsuleCollider;
    public GameObject cam;
    private Quaternion camRotation;
    private Quaternion playerRotation;
    public float mouseSensitivity;
    public float minX = -90.0f;
    public float maxX = 90.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        camRotation = cam.transform.localRotation;
        playerRotation = transform.localRotation;
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerJumpMovement();
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        camRotation = camRotation * Quaternion.Euler(-mouseY,0, 0);
        playerRotation = playerRotation * Quaternion.Euler(0,mouseX,0);  
        transform.localRotation = playerRotation;
        cam.transform.localRotation = camRotation;
        camRotation = ClampRotationXaxis(camRotation);

    }
    // Update is called once per frame
    void Update()
    {



    }

    bool PlayerGrounded()
    {
        RaycastHit hitInfo;

        if (Physics.SphereCast(transform.position, capsuleCollider.radius, Vector3.down, out hitInfo, ((capsuleCollider.height / 2.0f) - capsuleCollider.radius + 0.1f)))
        {
            //Debug.DrawRay(transform.position, hitInfo.point, Color.yellow);
            print("True!!");
            return true;
        }
        else
        {
            print("False");
            return false;
        }
    }

    void PlayerMovement()
    {

        float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed;
        float forwardMovement = Input.GetAxis("Vertical") * playerSpeed;

        //transform.position += new Vector3(horizontalMovement, 0, forwardMovement);
        transform.position += cam.transform.forward * forwardMovement + cam.transform.right * horizontalMovement;

    }

    void PlayerJumpMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerGrounded())
        {
            print("JUMP!!");
            rb.AddForce(0, playerJumpForce, 0);
            //rb.velocity = new Vector3(0,playerJumpForce,0);
        }

    }

    Quaternion ClampRotationXaxis(Quaternion value)
    {
        value.x/= value.w;
        value.y/= value.w;
        value.z/= value.w;
        value.w = 1.0f;
        float angleX = 2.0f * Mathf.Rad2Deg*Mathf.Atan(value.x);
        angleX = Mathf.Clamp(angleX,minX,maxX);
        value.x = Mathf.Tan(0.5f * Mathf.Deg2Rad*angleX);
        return value;
    }



    //public float playerSpeed;
    //Rigidbody rb;
    //public float playerJumpValue;
    //private bool isGrounded;
    //private CapsuleCollider capsuleCollider;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    capsuleCollider = GetComponent<CapsuleCollider>();
    //}

    //void Start()
    //{

    //}
    //void Update()
    //{

    //}

    //void FixedUpdate()
    //{
    //    PlayerMovement();
    //    PlayerJumpMovement();
    //}

    //bool PlayerGrounded()
    //{
    //    RaycastHit hitInfo;

    //    if (Physics.SphereCast(transform.position, capsuleCollider.radius, Vector3.down, out hitInfo, (capsuleCollider.height / 2.0f) - capsuleCollider.radius + 0.1f))
    //    {
    //        print("true statement");
    //        return true;

    //    }
    //    else
    //    {
    //        print("false statement");
    //        return false;
    //    }
    //}

    //void PlayerMovement()
    //{
    //    float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed;
    //    float forwardMovement = Input.GetAxis("Vertical") * playerSpeed;

    //    transform.position += new Vector3(horizontalMovement, 0, forwardMovement);
    //}

    //void PlayerJumpMovement()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && PlayerGrounded())
    //    {
    //        print("jump pressed");
    //        rb.AddForce(0, playerJumpValue, 0);
    //        //rb.velocity = new Vector3(0, playerJumpValue, 0);
    //    }
    //}
}
