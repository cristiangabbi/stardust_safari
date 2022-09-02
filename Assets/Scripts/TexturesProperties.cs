using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Texture Properties")]
public class TexturesProperties : ScriptableObject
{
    [SerializeField]
    public Color albedo;

    [SerializeField]
    public Texture2D normal;

    [SerializeField]
    public Texture2D roughness;

    [SerializeField]
    public Texture2D ambientOcclusion;

    [SerializeField]
    public Texture2D heightmap;

    [SerializeField]
    public Texture2D emissive;

    [Range(0f, 1f)]
    public float metallic = 0;

    [Range(0f, 1f)]
    public float smoothness = 0;

    [Range(0f, 1f)]
    public float startSlope;
    [Range(0f, 1f)]
    public float endSlope;

    [Range(0f, 1f)]
    public float heightMax;
    [Range(0f, 1f)]
    public float heightMin;

}
