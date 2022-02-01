using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterController : MonoBehaviour
{
    public float playerSpeed;
    public float playerJumpForce;
    public GameObject cam;
    public float mouseSensitivity;
    public float minX = -90.0f;
    public float maxX = 90.0f;
    public GameObject menu;
    public Animator animator;


    //inventroy section
   /*ammo in Gun*/ private int ammo;
    /*max ammo in gun*/  private int ammoInGun = 10;
    /*max ammo in bag */private int maxAmmoPickup = 20;
    /*ammo with player*/ private int ammoInBag;
    private int health;
    private int maxHealthPickup = 10;


    private Rigidbody rb;
    private bool isGrounded;
    private CapsuleCollider capsuleCollider;
    
    private Quaternion camRotation;
    private Quaternion playerRotation;
    
    private bool state;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        ammoInBag = maxAmmoPickup;
        ammo = ammoInGun;
        print("Ammo in Gun: " + ammo +"\n Ammo in Bag: "+  ammoInBag);
        health = maxHealthPickup;
        camRotation = cam.transform.localRotation;
        playerRotation = transform.localRotation;
        Cursor.visible = false;
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

       if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("Aiming", !animator.GetBool("Aiming"));
        }

        if (Input.GetMouseButtonDown(0) &&  !animator.GetBool("Fire"))
        {
            if(ammo > 0)
            {
                ammo = Mathf.Clamp(ammo-= 1, 0, ammoInGun);
                print("Ammo in Gun: " + ammo + "\n Ammo in Bag: " + ammoInBag);
                animator.SetTrigger("Fire");
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            animator.SetBool("WalkandFire", false);
            animator.SetBool("RunandFire", false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
           
            int ammoNeeded = ammoInGun - ammo; // 5 = 15 - 10
            Debug.Log("ammo Needed: " + ammoNeeded);
            if(ammoNeeded < ammoInBag && ammoNeeded>0)
            {
                ammoInBag -= ammoNeeded;
                animator.SetTrigger("Reloding");
                ammo += ammoNeeded;

                print("Ammo in Gun: " + ammo + "\n Ammo in Bag: " + ammoInBag);
            }
            //else if(ammoNeeded > ammo)
            //{
            //    ammo += ammoNeeded;
            //    print("Ammo in Gun: " + ammo + "\n Ammo in Bag: " + ammoInBag);
            //}
        }

    }

    void Pause()
    {
        menu.SetActive(false);
           
            state = Cursor.visible;
       // print("State: " + state);
            Cursor.visible = !state;
        //Cursor.visible = !Cursor.visible;

        if (Cursor.visible)
        {
            menu.SetActive(true);
            
            Cursor.lockState = CursorLockMode.Locked;
            print("HUAAAAAQ");
        }
            

        
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

        if (forwardMovement != 0 || horizontalMovement != 0 )
        {
            animator.SetTrigger("Walk");
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("WalkandFire");
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Run");
                if (Input.GetMouseButtonDown(0))
                {
                    //animator.SetBool("WalkandFire", false);
                    animator.SetTrigger("RunandFire");
                }
            }
            //if(Input.GetKeyUp(KeyCode.E))
            //{
            //    animator.SetBool("Run", false);
            //}
        }
        else
        {
            //animator.SetBool("Run", false);
            
        }
            
        
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ammunation" && ammoInBag < maxAmmoPickup)
        { 
            
            ammoInBag = Mathf.Clamp(ammoInBag +5, 0,maxAmmoPickup);
            print("Ammo in Gun: " + ammo + "\n Ammo in Bag: " + ammoInBag);
            Destroy(collision.gameObject);
            
        }
        else if (collision.gameObject.tag.Equals("Medkit") && health < maxHealthPickup)
        {
            health = Mathf.Clamp(health + 5, 0, maxHealthPickup);
            Debug.Log("Health: " + health);
            print("MEDIIC!!");
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("Lava") )
        {
            // playerdeath when health reaches zero need to apply
            health = Mathf.Clamp(health -= 5, 0, maxHealthPickup);
            Debug.Log("Health: " + health); 
        }
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
