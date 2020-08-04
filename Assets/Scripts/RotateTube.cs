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
        //Rotated = false;


    }

    public void Rotate()
    {
        rotatedObject.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

    public void Rotate(float xAngle, float yAngle, float zAngle)
    {
        rotatedObject.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }


    // Update is called once per frame
    void Update()
    {
    }
}
