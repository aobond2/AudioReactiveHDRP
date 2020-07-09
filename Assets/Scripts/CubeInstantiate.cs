using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInstantiate : MonoBehaviour
{
    public GameObject _sampleCubePrefab;
    public float _cubeDistance = 100f;
    public float _maxScale = 1000;
    public float _cubeSize = 1;
    GameObject[] _sampleCube = new GameObject[512];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, (-360f / 512f) * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * _cubeDistance;
            _sampleCube[i] = _instanceSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (_sampleCube != null)
            {
                _sampleCube[i].transform.localScale = new Vector3(_cubeSize , (AudioRe._samples[i] * _maxScale) + 2, _cubeSize);
            }
        }
    }
}
