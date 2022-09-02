using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgeNoiseLayer : NoiseInterface
{
	public RidgeNoiseLayer(NoiseProperties properties) : base(properties) { }

	public override float getTerrainHeight(Vector3 point)
	{
		float freqMultiplier = properties.baseFrequency;
		float amplitudeMultiplier = 1f;
		float fractal = 0f;

		for (int i = 1; i <= properties.detailLayers; i++)
		{
			Vector3 currentPoint = freqMultiplier * point + properties.center;

			fractal += (1f - Mathf.Abs(noise.Evaluate(currentPoint)) + 1f) * 0.5f * amplitudeMultiplier;
			freqMultiplier *= properties.frequency;
			amplitudeMultiplier *= properties.amplitude;
		}

		fractal = Mathf.Max(0, fractal - properties.minHeight);

		return fractal * properties.strength;
	}
}
