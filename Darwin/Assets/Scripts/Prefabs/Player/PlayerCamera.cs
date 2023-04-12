using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float y;
    private float rotX;

    public float turnSpeed = 4f;

    public float minTurnAngle = -90f;
    public float maxTurnAngle = 90f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX = Input.GetAxis("Mouse Y") * turnSpeed;

        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
    }
    void FixedUpdate()
    {
        MouseAiming();
    }

    public void MouseAiming() 
    {
        this.transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        this.transform.Find("Capsule").transform.Find("Main Camera").transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
    }
}
