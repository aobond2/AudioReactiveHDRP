using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeLightCreate : MonoBehaviour
{
    public AudioRe _audioRe;
    Renderer rend;
    Vector3 rend_size;
    public float cube_distance = 1f;
    public GameObject prefab;
    float rend_length;
    int instance_number;
    ParamAudioObject AudioObject;
    int tubes_number;
    int rounded_frequency;
    RotateTube rotateTube;
    public int percentageObjectRotated;
    int numberObjectRotated;

    public List<GameObject> tubeList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend_size = rend.bounds.size;
        rend_length = rend_size.x;
        instance_number = (Mathf.RoundToInt(rend_length / cube_distance));
        Vector3 spawn_start = transform.position - new Vector3((rend_size.x) / 2, 0, 0);
        AudioObject = prefab.GetComponent<ParamAudioObject>();
        rotateTube = prefab.GetComponent<RotateTube>();
        AudioObject._band = 1;
        tubes_number = 0;
        CountFrequency();
        AudioObject._objectNumber = 0;


        for (int i = 0; i < instance_number * 2; i++)
        {
            GameObject instantiatedGo = Instantiate(prefab, new Vector3 (spawn_start.x + cube_distance, 0, spawn_start.z), Quaternion.identity);
            spawn_start.x = spawn_start.x + cube_distance;
            i += 1;
            AudioObject._band += rounded_frequency;
            AudioObject.audioRe = _audioRe;
            AudioObject._objectNumber += 1;
            tubes_number += 1;
            tubeList.Add(instantiatedGo);
            
        }

        RotateObject();   
        rend.enabled = false;
    }

    void CountFrequency()
    {
        rounded_frequency = (64 / instance_number);
    }

    void RotateObject()
    {
        Debug.Log("Rotation started.");

        int numberObjectRotated = percentageObjectRotated * tubes_number / 100;

        Debug.Log("Number Object Rotated : " + numberObjectRotated);

        for (int e = 0; e < numberObjectRotated; e++)
        {
            GameObject rotate = tubeList[UnityEngine.Random.Range(0, tubeList.Count)];            
            RotateTube tubeRotation = rotate.GetComponent<RotateTube>();
            if (tubeRotation.Rotated == false)
            {

                tubeRotation.Rotate();
                tubeRotation.Rotated = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
