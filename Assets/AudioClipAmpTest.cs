using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent ( typeof ( AudioSource ) )]
public class AudioClipAmpTest : MonoBehaviour
{

    private AudioSource aud;
    private float updateStep = 0.1f;
    private int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    public float ClipLoudness
    {
        get { return clipLoudness; }
    }
    private float[] clipSampleData;

    private void Start ( )
    {
        aud = GetComponent<AudioSource> ( );
        clipSampleData = new float [ sampleDataLength ];

    }

    private void Update ( )
    {

        currentUpdateTime += Time.deltaTime;
        if ( currentUpdateTime >= updateStep )
        {
            currentUpdateTime = 0f;
            aud.clip.GetData ( clipSampleData, aud.timeSamples ); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach ( var sample in clipSampleData )
            {
                clipLoudness += Mathf.Abs ( sample );
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        }

    }
}
