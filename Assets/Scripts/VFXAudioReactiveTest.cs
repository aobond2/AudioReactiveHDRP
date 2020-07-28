using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;
using UnityEngine.VFX;

public class VFXAudioReactiveTest : MonoBehaviour
{
    //public int _band = 1;
    public float bandScale;
    public VisualEffect visualEffect;
    public float Band;
    public string bandParameter = "Band";
    float updatedBand;

    // Start is called before the first frame update
    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        updatedBand = AudioRe._audioBand[(int)Band] * bandScale;
        visualEffect.SetFloat (bandParameter, updatedBand);
    }
}
