using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band = 1;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(AudioRe._AmplitudeBuffer);
        if (_useBuffer && AudioRe._audioBand[_band] > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                                    (AudioRe._audioBandBuffer[_band] * _scaleMultiplier) + _startScale,
                                    AudioRe._AmplitudeBuffer * 5);//transform.localScale.z);
        }
        if (!_useBuffer && AudioRe._audioBand[_band] > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                                    (AudioRe._audioBand[_band] * _scaleMultiplier) + _startScale,
                                    transform.localScale.z);
        }

    }
}
