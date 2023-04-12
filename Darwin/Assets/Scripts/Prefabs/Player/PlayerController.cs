using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool isGrounded = false;

    [SerializeField]
    private Rigidbody myRigidbody;

    [SerializeField]
    private Vector3 movement;

    public float jumpForce = 7f;

    public float walkSpeed = 100f;

    public float sprintSpeed = 200f;
    

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");

        float yAxis = Input.GetAxisRaw("Jump");

        float zAxis = Input.GetAxisRaw("Vertical");

        //Debug.Log(yAxis);
        if (isGrounded)
        {
            if (yAxis == 0)
            {
                movement = new Vector3(xAxis, 0, zAxis).normalized;
            }
            else
            {
                movement = new Vector3(xAxis, (yAxis * jumpForce), zAxis).normalized;
            }
        }
        else 
        {
            movement = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z).normalized;
        }
    }

    void FixedUpdate()
    {
        InitGravit();
        MovePlayer(movement);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Terrain") 
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            isGrounded = false;
        }
    }

    public void InitGravit() 
    {
        myRigidbody.AddForce(Physics.gravity * 20f);
    }

    public void MovePlayer(Vector3 _movement) 
    {
        float moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        } else 
        {
            moveSpeed = walkSpeed;
        }

        myRigidbody.velocity = _movement * moveSpeed;
    }
}
