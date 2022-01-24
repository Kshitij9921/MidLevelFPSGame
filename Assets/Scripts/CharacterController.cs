using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float playerSpeed;
    public float playerJumpForce;
    private Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerJumpMovement();

    }
    // Update is called once per frame
    void Update()
    {

        

    }

    void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed;
        float forwardMovement = Input.GetAxis("Vertical") * playerSpeed;

        transform.position += new Vector3(horizontalMovement, 0, forwardMovement);


    }

    void PlayerJumpMovement()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, playerJumpForce, 0);
        }
        
    }
}
