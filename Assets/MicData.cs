using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent ( typeof ( AudioSource ) )]
public class MicData : MonoBehaviour
{
    private string mic;
    private AudioSource aud;

    void Start ( )
    {
        foreach ( string device in Microphone.devices )
        {
            Debug.Log ( "Name: " + device );
        }

        if ( Microphone.devices.Length > 0 )
        {
            mic = Microphone.devices [ 0 ];
        }

        aud = GetComponent<AudioSource> ( );
        aud.loop = true;
        aud.playOnAwake = false;
        aud.clip = Microphone.Start ( mic, true, 1, 44100 );

        // aud.Play ( );
    }

    void Update ( )
    {
        if ( Input.GetKey ( KeyCode.Space ) )
        {
            Microphone.End ( mic );


        }

    }
}
