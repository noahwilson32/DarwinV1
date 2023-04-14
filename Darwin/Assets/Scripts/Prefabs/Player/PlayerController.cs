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

    public float jumpForce;

    public float gravity = -9.81f;

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
            if (yAxis <= 0.1f)
            {
                movement = transform.right * xAxis + transform.forward * zAxis;
                movement.y = 0;
            }
            else
            {
                movement.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
        else 
        {
            movement.y += gravity * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //InitGravit();
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
