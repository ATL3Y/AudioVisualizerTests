using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void Update ( )
    {
        transform.localPosition = new Vector3 ( transform.localPosition.x, 20.0f * MicInput.MicLoudness, transform.localPosition.z );
    }
}
