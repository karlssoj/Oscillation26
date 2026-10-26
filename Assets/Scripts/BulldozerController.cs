using UnityEngine;
using UnityEngine.InputSystem;

public class BulldozerController : MonoBehaviour
{
    //constant speed for the bulldozer
    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.wKey.isPressed)
        {
            //Move forward with constant speed when pressing 'w'
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
