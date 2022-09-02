using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralNoiseProject;

public class WorleyNoiseLayer : NoiseInterface
{
	WorleyNoise wn;

    public WorleyNoiseLayer(NoiseProperties properties) : base(properties) { }

    public override float getTerrainHeight(Vector3 point)
	{
		float freqMultiplier = properties.baseFrequency;
		float amplitudeMultiplier = 1f;
		float fractal = 0f;

		wn = new WorleyNoise(0, properties.baseFrequency, properties.redistribution, amplitudeMultiplier);


		//fractal = wn.Sample3D(point.x, point.y, point.z);

		for (int i = 1; i <= properties.detailLayers; i++)
		{
			wn.UpdateSeed(i + 100);
			fractal += i * properties.frequency * ((wn.Sample3D(point.x + properties.center.x, point.y + properties.center.y, point.z + properties.center.z) + 1) * 0.5f);
			wn.Amplitude *= properties.amplitude;
		}

		fractal = Mathf.Max(0, fractal - properties.minHeight);

		return fractal * properties.strength;
	}
}
