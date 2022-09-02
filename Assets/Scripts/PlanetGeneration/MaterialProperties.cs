using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Properties")]
public class MaterialProperties : ScriptableObject
{
    [SerializeField]
    public Material planetMaterial;

    [SerializeField]
    public List<TexturesProperties> texProperties = new List<TexturesProperties>();

    public float blendingRate = 0.5f;
    public float normalStrength = 1f;
    public Vector2 normalTiling;
    public Vector2 normalOffset;
    public Vector2 emissiveTiling;
    public Vector2 emissiveOffset;
    public float emissionStrenght = 1f;

    public void mapTexturesToShader()
    {
        int i = 0;

        foreach(var element in texProperties)
        {
            if (element != null)
            {
                if (element.albedo != null)
                    planetMaterial.SetColor("_Color" + i, element.albedo);

                if (element.normal != null)
                    planetMaterial.SetTexture("_normal_Mat_" + i, element.normal);

                if (element.roughness != null)
                    planetMaterial.SetTexture("_roughness_Mat_" + i, element.normal);

                if (element.ambientOcclusion != null)
                    planetMaterial.SetTexture("_ambientOcclusion_Mat_" + i, element.ambientOcclusion);

                if (element.emissive != null)
                    planetMaterial.SetTexture("_emissive_Mat_" + i, element.emissive);

                if (element.heightmap != null)
                    planetMaterial.SetTexture("_height_Mat_" + i, element.heightmap);

                //set also metallic and smoothness
                /*planetMaterial.SetVector("_smoothMetallic_Mat_" + i.ToString(), new Vector4(
                                                                                    texProperties[i].smoothness, 
                                                                                    texProperties[i].metallic
                                                                                    ));*/

                //set also height range and slope range
                planetMaterial.SetVector("_heightRange_slopeRange_Mat_" + i, new Vector4(
                                                                                    element.startSlope,
                                                                                    element.endSlope,
                                                                                    element.heightMin,
                                                                                    element.heightMax
                                                                                    ));

                i++;

            }
        }
        planetMaterial.SetVector("_blendingRate", new Vector2(blendingRate, 0));
        planetMaterial.SetVector("_NormalStrength", new Vector2(normalStrength, 0f));
        planetMaterial.SetVector("_NormalTiling", normalTiling);
        planetMaterial.SetVector("_NormalOffset", normalOffset);
        planetMaterial.SetVector("_emissionTiling", emissiveTiling);
        planetMaterial.SetVector("_emissionOffset", emissiveOffset);
        planetMaterial.SetFloat("_emissionStrenght", emissionStrenght);

    }
}
