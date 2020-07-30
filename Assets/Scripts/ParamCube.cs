using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public AudioRe audioRe;
    public int _band = 1;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;

    // Start is called before the first frame update
    void Start()
    {
        print(audioRe);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_useBuffer && audioRe._audioBand64[_band] > 0 && audioRe != null)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                                    (audioRe._audioBandBuffer64[_band] * _scaleMultiplier) + _startScale,
                                    transform.localScale.z);
        }
        if (!_useBuffer && audioRe._audioBand64[_band] > 0 && audioRe != null)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                                    (audioRe._audioBand64[_band] * _scaleMultiplier) + _startScale,
                                    transform.localScale.z);
        }

    }
}
