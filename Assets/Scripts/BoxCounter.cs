using UnityEngine;

public class BoxCounter : MonoBehaviour
{
    int BoxesInTargetZone = 0;


    // Update is called once per frame
    void Update()
    {
        if(BoxesInTargetZone >= 3)
            Debug.Log("MISSION COMPLETED!");    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "box")
        {
            BoxesInTargetZone++;   
            Debug.Log(BoxesInTargetZone); 
        }  
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "box")
        {
            BoxesInTargetZone--;    
        }  
    }
}
