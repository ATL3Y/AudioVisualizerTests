using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public ClipInput clipInput;

    void Update ( )
    {
        float val = 0.0f;
        if ( clipInput == null )
        {
            val = MicInput.MicLoudness;
        }
        else
        {
            val = clipInput.ClipLoudness;
        }

        transform.localPosition = new Vector3 ( transform.localPosition.x, 40.0f * val, transform.localPosition.z );
    }
}
