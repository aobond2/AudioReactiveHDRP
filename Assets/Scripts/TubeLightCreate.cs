using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
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

    Vector3 spawnAddition;

    public enum Direction {Side, Up };
    public Direction _spawnDirection;

    public Boolean _rotateObject = false;

    List<GameObject> tubeList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend_size = rend.bounds.size;
        float rend_sizeUsed = 0;
        Vector3 spawn_startL = new Vector3(0,0,0);

        if (_spawnDirection == Direction.Side)
        {
            rend_sizeUsed = rend_size.x;
            spawn_startL = new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3((rend_size.x) / 2, 0, 0);
        }

        else if (_spawnDirection == Direction.Up)
        {
            rend_sizeUsed = rend_size.y;
            spawn_startL = new Vector3(transform.position.x,0f,transform.position.z) - new Vector3(0, 0, 0);
        }

        rend_length = rend_sizeUsed;
        instance_number = (Mathf.RoundToInt(rend_length / cube_distance));
        Vector3 spawn_start = spawn_startL;

        AudioObject = prefab.GetComponent<ParamAudioObject>();
        rotateTube = prefab.GetComponent<RotateTube>();
        AudioObject._band = 1;
        tubes_number = 0;
        CountFrequency();
        AudioObject._objectNumber = 0;




        for (int i = 0; i < instance_number * 2; i++)
        {
            ChooseDirection(_spawnDirection);

            GameObject instantiatedGo = Instantiate(prefab, new Vector3(spawn_start.x, spawn_start.y, spawn_start.z) + spawnAddition, Quaternion.identity);

            if (_rotateObject == true)
            {
                instantiatedGo.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            }

            spawn_start += spawnAddition;

            i += 1;

            //Frequency assignment
            AudioObject._band += rounded_frequency;
            AudioObject.audioRe = _audioRe;
            AudioObject._objectNumber += 1;

            //Counting number of instance and add it to list
            tubes_number += 1;
            tubeList.Add(instantiatedGo);
            
        }

        RotateObject();   
        rend.enabled = false;
    }

    public Vector3 ChooseDirection (Direction dir)
    {
        if (dir == Direction.Side)
            spawnAddition = new Vector3(cube_distance, 0, 0);
        else if (dir == Direction.Up)
            spawnAddition = new Vector3(0, cube_distance, 0 );
        return spawnAddition;
    }

    void CountFrequency()
    {
        rounded_frequency = (64 / instance_number);
    }

    void RotateObject()
    {
        int numberObjectRotated = percentageObjectRotated * tubes_number / 100;
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
