using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class AudioRe : MonoBehaviour
{
    AudioSource _audioSource;
    private float[] _samplesLeft = new float[512];
    private float[] _samplesRight = new float[512];

    private float[] _freqBand = new float[8];
    private float[] _bandBuffer = new float[8];
    public float BufferDecreaseDown = 0.005f;
    public float BufferDecreaseUp = 1.2f;
    private float[] _bufferDecrease = new float[8];
    private float[] _freqBandHighest = new float[8];

    //64 frequency bands
    private float[] _freqBand64 = new float[64];
    private float[] _bandBuffer64 = new float[64];
    public float BufferDecreaseDown64 = 0.005f;
    public float BufferDecreaseUp64 = 1.2f;
    private float[] _bufferDecrease64 = new float[64];
    private float[] _freqBandHighest64 = new float[64];

    [HideInInspector]
    public float[] _audioBand, _audioBandBuffer;

    [HideInInspector]
    public float[] _audioBand64, _audioBandBuffer64;

    [HideInInspector]
    public float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;
    public float _audioProfile;

    public enum _channel {Stereo, Left, Right};
    public _channel channel = new _channel();

    // Start is called before the first frame update
    void Start()
    {
        _audioBand = new float[8];
        _audioBandBuffer = new float[8];

        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];

        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        MakeFrequencyBands64();
        BandBuffer();
        BandBuffer64();
        CreateAudioBands();
        CreateAudioBands64();
        GetAmplitude();
    }

    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = audioProfile;
        }
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = Mathf.Clamp01 (_CurrentAmplitude / _AmplitudeHighest);
        _AmplitudeBuffer = Mathf.Clamp01 (_CurrentAmplitudeBuffer / _AmplitudeHighest);
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);

        }
    }

    void CreateAudioBands64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqBand64[i] > _freqBandHighest64[i])
            {
                _freqBandHighest64[i] = _freqBand64[i];
            }
            _audioBand64[i] = (_freqBand64[i] / _freqBandHighest64[i]);
            _audioBandBuffer64[i] = (_bandBuffer64[i] / _freqBandHighest64[i]);

        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand [g] > _bandBuffer [g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = BufferDecreaseDown;
            }

            if (_freqBand [g] < _bandBuffer[g])
            {
                _bufferDecrease[g] = (_bandBuffer[g] - _freqBand[g]) / 8;
                _bandBuffer[g] -= _bufferDecrease[g];
                //_bandBuffer[g] -= _bufferDecrease[g];
                //_bufferDecrease[g] *= BufferDecreaseUp;
            }
        }
    }

    void BandBuffer64()
    {
        for (int g = 0; g < 64; ++g)
        {
            if (_freqBand64[g] > _bandBuffer64[g])
            {
                _bandBuffer64[g] = _freqBand64[g];
                _bufferDecrease64[g] = BufferDecreaseDown64;
            }

            if (_freqBand64[g] < _bandBuffer64[g])
            {
                _bufferDecrease64[g] = (_bandBuffer64[g] - _freqBand64[g]) / 8;
                _bandBuffer64[g] -= _bufferDecrease64[g];
                //_bandBuffer[g] -= _bufferDecrease[g];
                //_bufferDecrease[g] *= BufferDecreaseUp;
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);
                }
                if (channel == _channel.Right)
                {
                    average += _samplesRight[count] * (count + 1);
                }
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }
    }

    void MakeFrequencyBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {
            float average = 0;
            //int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i ==16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if (power == 3)
                {
                    sampleCount -= 2;
                }
            }
            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);
                }
                if (channel == _channel.Right)
                {
                    average += _samplesRight[count] * (count + 1);
                }
                count++;
            }

            average /= count;

            _freqBand64[i] = average * 80;
        }
    }
}
