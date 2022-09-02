using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimplexNoiseLayer : NoiseInterface
{
	public SimplexNoiseLayer(NoiseProperties properties) : base(properties) { }

	public override float getTerrainHeight(Vector3 point)
	{
		float freqMultiplier = properties.baseFrequency;
		float amplitudeMultiplier = 1f;
		float fractal = 0f;

		for (int i = 1; i <= properties.detailLayers; i++)
		{
			Vector3 currentPoint = freqMultiplier * point + properties.center;

			fractal += (noise.Evaluate(currentPoint) + 1f) * 0.5f * amplitudeMultiplier;
			freqMultiplier *= properties.frequency;
			amplitudeMultiplier *= properties.amplitude;
		}

		fractal = Mathf.Max(0, fractal - properties.minHeight);

		fractal = Mathf.Pow(fractal, properties.redistribution);

		return fractal * properties.strength;
	}
}
