using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class NoiseInterface
{
	[SerializeField]
	public NoiseProperties properties;
	protected Noise noise = new Noise();

    public NoiseInterface(NoiseProperties properties)
    {
        this.properties = properties;
    }

    public abstract float getTerrainHeight(Vector3 point);
}
