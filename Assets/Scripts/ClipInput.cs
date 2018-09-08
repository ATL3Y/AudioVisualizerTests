using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ClipInput : MonoBehaviour
{
    public float ClipLoudness;
    public float ClipLoudnessinDecibels;
    private AudioSource aud;

    private int sampleWindow = 128;

    private void InitClip ( )
    {
        aud = GetComponent<AudioSource> ( );
        aud.playOnAwake = true;
        aud.loop = true;
    }

    private void OnEnable ( )
    {
        InitClip ( );
    }

    private float ClipLevelMax ( )
    {
        float levelMax = 0;
        float[] waveData = new float[sampleWindow];
        int clipPosition = aud.timeSamples;  // Does this update per clip? 
        aud.clip.GetData ( waveData, clipPosition );

        // Get a peak on the last 128 samples.
        for ( int i = 0; i < sampleWindow; i++ )
        {
            float wavePeak = waveData[i] * waveData[i];
            if ( levelMax < wavePeak )
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    // Get data from microphone into audioclip.
    private float ClipLevelMaxDecibels ( )
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(ClipLoudness));

        return db;
    }

    private void Update ( )
    {
        ClipLoudness = ClipLevelMax ( );
        ClipLoudnessinDecibels = ClipLevelMaxDecibels ( );
    }
}
