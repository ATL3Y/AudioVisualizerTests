using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent ( typeof ( AudioSource ) )]
public class ClipInput : MonoBehaviour
{
    public float ClipLoudness;
    public float ClipLoudnessinDecibels;
    private AudioSource aud;

    private int sampleWindow = 128;

    private void InitClip ( )
    {
        aud = GetComponent<AudioSource> ( );
        aud.playOnAwake = false;
        aud.loop = false;
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
        // Debug.Log ( levelMax );
        return levelMax;
    }

    // Get data from microphone into audioclip.
    private float ClipLevelMaxDecibels ( )
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(ClipLoudness));

        return db;
    }

    private float velocity = 0.0f;
    private void Update ( )
    {
        if ( !aud.isPlaying )
        {
            ClipLoudness = 0.0f;
            ClipLoudnessinDecibels = -30.0f;

            if ( Input.GetKeyDown ( KeyCode.Space ) )
            {
                aud.Play ( );
            }
        }
        else
        {
            // ClipLoudness = Mathf.MoveTowards(ClipLoudness, ClipLevelMax ( ), 0.01f * Time.deltaTime);

            // ClipLoudness = Mathf.Lerp ( ClipLoudness, ClipLevelMax ( ), Time.deltaTime );

            ClipLoudness = Mathf.SmoothDamp ( ClipLoudness, ClipLevelMax ( ), ref velocity, 10.0f * Time.deltaTime );
            ClipLoudnessinDecibels = ClipLevelMaxDecibels ( );
        }
    }
}
