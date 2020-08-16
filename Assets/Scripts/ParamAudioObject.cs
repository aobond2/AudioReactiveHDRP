using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamAudioObject : MonoBehaviour
{
    public AudioRe audioRe;
    public int _band = 1;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;
    public int _objectNumber = 0;

    public Transform _lightObject;
    public Transform _realLight;

    public float _emissiveValue;
    public float _lightIntensity;

    Material _lightMaterial;
    Light _myLight;

    public float _RealLightIntensity;

    bool _haveLight = false;

    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("LightObject"))
            {
                _lightObject = child;
                _lightMaterial = _lightObject.GetComponent<MeshRenderer>().materials[0];
                _emissiveValue = _lightMaterial.GetFloat("_EmissiveIntensity");
            }
            if (child.CompareTag("RealLight"))
            {
                _haveLight = true;

                _realLight = child;
                _myLight = _realLight.GetComponent<Light>();
                //_lightIntensity = _myLight
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (audioRe != null)
        {
            if (_useBuffer && audioRe._audioBand64[_band] > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x,
                                        (audioRe._audioBandBuffer64[_band] * _scaleMultiplier) + _startScale,
                                        transform.localScale.z);

                //Need to fix turning on off value
                //_lightMaterial.SetFloat("_EmissiveIntensity", audioRe._audioBandBuffer64[_band] * _emissiveValue);
                _lightMaterial.SetFloat("_AudioEmissiveControl", (audioRe._audioBandBuffer64[_band]));
                
                //set light intensity based on band
                if (_haveLight) 
                {
                    _myLight.intensity = (audioRe._audioBandBuffer64[_band]) * _RealLightIntensity;
                } 
            }
            if (!_useBuffer && audioRe._audioBand64[_band] > 0)
            {
                //transform.localScale = new Vector3(transform.localScale.x,
                //                        (audioRe._audioBand64[_band] * _scaleMultiplier) + _startScale,
                //                        (audioRe._Amplitude *2 )); //+ transform.localScale.z);
                //_lightMaterial.SetFloat("_EmissiveExposureWeight", 0);
            }
        }


    }
}
