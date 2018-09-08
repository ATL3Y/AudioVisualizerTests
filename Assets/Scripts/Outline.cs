using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material AlphaEdgeMat;
    public ClipInput clipInput;

    private void Update ( )
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

        AlphaEdgeMat.SetFloat ( "_EdgeSize", val );

        Color col = new Color (  val, 1.0f - val, ( Mathf.Sin ( Time.timeSinceLevelLoad ) + 1.0f) / 2.0f , 1.0f);
        AlphaEdgeMat.SetColor ( "_EdgeColor", col);
    }
}
