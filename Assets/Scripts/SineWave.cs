using UnityEngine;

public class SineWave : MonoBehaviour
{

    Vector3 Position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Position = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        Position.y = Mathf.Sin(Time.time + Position.x);
        transform.position = Position;
    }
}
