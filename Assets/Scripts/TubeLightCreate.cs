using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeLightCreate : MonoBehaviour
{
    Renderer rend;
    Vector3 rend_size;
    public float cube_distance = 1f;
    Vector3 spawn_start;
    public Transform prefab;
    float rend_length;
    int instance_number;
    ParamCube cube;
    

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend_size = rend.bounds.size;
        rend_length = rend_size.x;
        instance_number = (Mathf.RoundToInt(rend_length / cube_distance));
        Vector3 spawn_start = transform.position - new Vector3((rend_size.x) / 2, 0, 0);
        cube = prefab.GetComponent<ParamCube>();
        cube._band = 1;

        AssignBand();
        
        for (int i = 0; i < instance_number * 2; i++)
        {
            Instantiate(prefab, new Vector3 (spawn_start.x + cube_distance, 0, spawn_start.z), Quaternion.identity);
            spawn_start.x = spawn_start.x + cube_distance;
            i += 1;
            cube._band += 1;
        }

        DestroyImmediate(rend);
    }

    void AssignBand()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
