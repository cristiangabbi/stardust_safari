using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlanetProperties")]
public class PlanetProperties : ScriptableObject
{
    public float planetRadius = 1f;

    //planet resolution
    [Range(2, 256)]
    public int resolution = 10;

    [SerializeField]
    public NoiseProperties[] noiseProperties;

    [SerializeField]
    public MaterialProperties materialProperties;
}
