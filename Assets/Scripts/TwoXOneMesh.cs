using UnityEngine;

public class Two : MonoBehaviour
{
    Mesh mesh;
 
    void Start()
    {
 	    mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();   // remove the old vertices/triangles first

        mesh.vertices = new Vector3[] //the vertice positions of 1 square
        {
            new Vector3(0, 0, 0),   	//0
            new Vector3(1, 0, 0),   	//1
            new Vector3(2, 0, 0),   	//2
            new Vector3(0, 0, 1), 		//3
            new Vector3(1, 0, 1),   	//4
            new Vector3(2, 0, 1) 		//5
        };

        //Define the order of the triangle vertices 
	    mesh.triangles = new int[] { 0, 3, 1,  1, 3, 4,  1, 4, 2,  2, 4, 5 };
        mesh.RecalculateNormals();    
    }
}
