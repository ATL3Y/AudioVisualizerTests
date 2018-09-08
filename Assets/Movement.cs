using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public AudioClipAmpTest audClipAmp;

    // Use this for initialization
    void Start ( )
    {

    }

    // Update is called once per frame
    void Update ( )
    {
        float mult = audClipAmp.ClipLoudness;

        // Make mult from 0 to 1 ish
        if(mult < 0.01f )
        {
            mult = 0.0f;
        }

        mult *= 20.0f;
        Debug.Log ( mult );
        float height = Mathf.Lerp(transform.localPosition.y, mult, 5.0f * Time.deltaTime);
        transform.position = new Vector3 ( transform.localPosition.x, height, transform.localPosition.z );
    }
}
