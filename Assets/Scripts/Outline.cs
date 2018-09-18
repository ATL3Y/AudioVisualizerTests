using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material AlphaEdgeMat;
    // public Material AlphaOutSource;
    private Material alphaOutInstance;
    public ClipInput clipInput;

    public enum Character { Player, Other_1, Other_2, Other_3, Other_5, Other_6, End };
    public Character character;

    public Color myColor;

    private void Start ( )
    {
        // Make new instance.
        //alphaOutInstance = new Material ( AlphaOutSource );
        //GetComponent<Renderer> ( ).material = alphaOutInstance;
        // Use enums to generate a float that is unique to each character from 0 to 1.
        // AlphaOut.SetFloat ( "_Alpha", ( float ) character / ( float ) Character.End );

        alphaOutInstance = GetComponent<Renderer> ( ).material;
    }

    private void Update ( )
    {
        float val = 0.0f;
        if ( clipInput == null )
        {
            val = MicInput.MicLoudness;
            // Debug.Log ( "Player: " + val );
        }
        else
        {
            val = 1000.0f * clipInput.ClipLoudness * clipInput.ClipLoudness;
            // Debug.Log ( "ClipLoudness: " + val );
        }


        // Filter noise.
        // Assumes only one speaker at a time. 
        if ( val > .001f )
        {
            alphaOutInstance.SetFloat ( "_Alpha", val ); 
            // AlphaEdgeMat.SetFloat ( "_EdgeSize", val );
            AlphaEdgeMat.SetColor ( "_EdgeColor", myColor );
        }
        else
        {
            alphaOutInstance.SetFloat ( "_Alpha", 1.0f ); 
        }
    }
}
