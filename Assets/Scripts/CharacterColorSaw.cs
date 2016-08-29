using UnityEngine;
using System.Collections;

public class CharacterColorSaw : MonoBehaviour
{
    public PaletteColor Color;
    private Color tunicColor;
    //TODO: skin color?

    private MeshRenderer meshRenderer;
    private Material tunicMat;
    private int tunicIndex;

    // Use this for initialization
    void Start()
    {
        Color = GetComponentInParent<CharacterColor>().Color;

        //get tunic 
        Transform torso = transform.FindChild("Body");
        if (torso != null)
        {
            meshRenderer = torso.GetComponent<MeshRenderer>();
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                if (meshRenderer.materials[i].name.Contains("Tunic"))
                {
                    tunicMat = meshRenderer.materials[i];
                    tunicIndex = i;
                }
            }

            if (tunicMat == null)
                Debug.LogError("Could not find tunic material!");

            tunicColor = Palette.GetColor(Color);
            if (tunicMat.color != tunicColor)
            {
                tunicMat.color = tunicColor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (meshRenderer != null)
        {
            //update color
            tunicColor = Palette.GetColor(Color);
            if (tunicMat.color != tunicColor)
            {
                tunicMat.color = tunicColor;
                //meshRenderer.materials[tunicIndex] = tunicMat;
            }
        }

    }
}
