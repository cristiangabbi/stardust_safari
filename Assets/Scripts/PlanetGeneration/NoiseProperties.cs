using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlanetNoise Properties")]
public class NoiseProperties : ScriptableObject
{
    public NoiseType type;
    public float baseFrequency = 0f;
    public float frequency = 0f;
    [Range(0f, 1f)]
    public float amplitude = 0f;
    public float strength = 0f;
    public float redistribution = 0f;
    public float minHeight;
    public Vector3 center;
    [Range(1, 10)]
    public int detailLayers = 1;
}

public enum NoiseType
{
    SimplexType,
    RidgidType,
    WorleyType
}
