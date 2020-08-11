using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTube : MonoBehaviour
{
    public GameObject rotatedObject;
    public bool Rotated = false;
    public float xAngle, yAngle, zAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Rotate()
    {
        rotatedObject.transform.Rotate(Random.Range(-xAngle, xAngle), Random.Range(-yAngle, yAngle), Random.Range(-zAngle, zAngle), Space.World);
    }

    public void Rotate(float xAngle, float yAngle, float zAngle)
    {        
        rotatedObject.transform.Rotate(Random.Range(-xAngle, xAngle), Random.Range(-yAngle, yAngle), Random.Range(-zAngle, zAngle), Space.World);
    }


    // Update is called once per frame
    void Update()
    {
    }
}
