using UnityEngine;

public class WaveFunction : MonoBehaviour
{
    Mesh mesh;
    Vector3[] original;
 
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        original = mesh.vertices;           // a copy 
    }
    
    void Update()
    {
        Vector3[] v = mesh.vertices;
        for (int i = 0; i < v.Length; i++)
            v[i].y = Mathf.Sin(original[i].x + Time.time);
    
        mesh.vertices = v;                  // write back
        mesh.RecalculateNormals();
    }

}
