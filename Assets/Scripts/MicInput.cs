using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    #region SingleTon

    public static MicInput Instance { set; get; }

    #endregion

    public static float MicLoudness;
    public static float MicLoudnessinDecibels;

    private string device;

    // Mic initialization.
    public void InitMic ( )
    {
        clipRecord = AudioClip.Create ( "clipRecord", 128, 1, 44100, true );
        recordedClip = AudioClip.Create ( "recordedClip", 128, 1, 44100, true );
        if ( device == null )
        {
            device = Microphone.devices [ 0 ];
        }
        clipRecord = Microphone.Start ( device, true, 999, 44100 );
        isInitialized = true;
    }

    public void StopMicrophone ( )
    {
        Microphone.End ( device );
        isInitialized = false;
    }

    AudioClip clipRecord; 
    AudioClip recordedClip; 
    int sampleWindow = 128;

    // Get data from microphone into audioclip.
    float MicrophoneLevelMax ( )
    {
        float levelMax = 0;
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // Null means the first microphone.S
        if ( micPosition < 0 ) return 0;
        clipRecord.GetData ( waveData, micPosition );

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
    float MicrophoneLevelMaxDecibels ( )
    {
        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));

        return db;
    }

    public float FloatLinearOfClip ( AudioClip clip )
    {
        StopMicrophone ( );

        recordedClip = clip;

        float levelMax = 0;
        float[] waveData = new float[recordedClip.samples];

        recordedClip.GetData ( waveData, 0 );

        // Get a peak on the last 128 samples.
        for ( int i = 0; i < recordedClip.samples; i++ )
        {
            float wavePeak = waveData[i] * waveData[i];
            if ( levelMax < wavePeak )
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    public float DecibelsOfClip ( AudioClip clip )
    {
        StopMicrophone ( );

        recordedClip = clip;

        float levelMax = 0;
        float[] waveData = new float[recordedClip.samples];

        recordedClip.GetData ( waveData, 0 );

        // Get a peak on the last 128 samples.
        for ( int i = 0; i < recordedClip.samples; i++ )
        {
            float wavePeak = waveData[i] * waveData[i];
            if ( levelMax < wavePeak )
            {
                levelMax = wavePeak;
            }
        }

        float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));

        return db;
    }

    void Update ( )
    {
        // LevelMax equals to the highest normalized value power 2, a small number because < 1.
        // Pass the value to a static var so we can access it from anywhere.
        MicLoudness = MicrophoneLevelMax ( );
        MicLoudnessinDecibels = MicrophoneLevelMaxDecibels ( );
    }

    bool isInitialized;
    // Start mic when scene starts.
    void OnEnable ( )
    {
        InitMic ( );
        isInitialized = true;
        Instance = this;
    }

    // Stop mic when loading a new level or quit application.
    void OnDisable ( )
    {
        StopMicrophone ( );
    }

    void OnDestroy ( )
    {
        StopMicrophone ( );
    }

    // Make sure the mic gets started & stopped when application gets focused.
    void OnApplicationFocus ( bool focus )
    {
        if ( focus )
        {
            //Debug.Log("Focus");

            if ( !isInitialized )
            {
                //Debug.Log("Init Mic");
                InitMic ( );
            }
        }
        if ( !focus )
        {
            //Debug.Log("Pause");
            StopMicrophone ( );
            //Debug.Log("Stop Mic");
        }
    }
}
