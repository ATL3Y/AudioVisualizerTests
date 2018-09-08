using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material AlphaEdgeMat;

    void Update ( )
    {
        AlphaEdgeMat.SetFloat ( "_EdgeSize", MicInput.MicLoudness );

        Color col = new Color (  MicInput.MicLoudness, 1.0f -  MicInput.MicLoudness, ( Mathf.Sin ( Time.timeSinceLevelLoad ) + 1.0f) / 2.0f , 1.0f);
        AlphaEdgeMat.SetColor ( "_EdgeColor", col);
    }
}
