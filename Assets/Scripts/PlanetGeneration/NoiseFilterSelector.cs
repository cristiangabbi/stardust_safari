using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterSelector
{
    public static NoiseInterface GetLayerNoise(NoiseProperties properties)
    {
        switch(properties.type)
        {
            case NoiseType.SimplexType:
                return new SimplexNoiseLayer(properties);
            case NoiseType.RidgidType:
                return new RidgeNoiseLayer(properties);
            case NoiseType.WorleyType:
                return new WorleyNoiseLayer(properties);
        }
        return null;
    }
}
