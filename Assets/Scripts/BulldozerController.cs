using UnityEngine;
using UnityEngine.InputSystem;

public class BulldozerController : MonoBehaviour
{
    //constant speed for the bulldozer
    public float speed = 1;
    public float rotationSpeed = 10;

    public float EnginePower = 10;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.wKey.isPressed)
        {
            //Move forward with constant speed when pressing 'w'
            rb.AddRelativeForce(0, 0, EnginePower);
        }

        if(Keyboard.current.sKey.isPressed)
        {
            //Move forward with constant speed when pressing 'w'
             rb.AddRelativeForce(0, 0, -EnginePower);
        }

        if(Keyboard.current.aKey.isPressed)
        {
            //Move forward with constant speed when pressing 'w'
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

        if(Keyboard.current.dKey.isPressed)
        {
            //Move forward with constant speed when pressing 'w'
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "wall")
            Debug.Log("YOU CRASHED");
    }

}
