using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGeneration : MonoBehaviour
{
    public float[,] GeneratePerlinNoiseMap(int width, int depth, float scale, float offsetX, float offsetZ, Wave[] waves)
    {
        float[,] noiseMap = new float[width, depth];
        for (int xIndex = 0; xIndex < width; xIndex++)
        {
            for (int zIndex = 0; zIndex < depth; zIndex++)
            {
                float sampleX = (xIndex + offsetX) / scale;
                float sampleZ = (zIndex + offsetZ) / scale;

                float noise = 0f;
                float normalization = 0f;
                foreach (Wave wave in waves)
                {
                    noise += wave.amplitude * Mathf.PerlinNoise(sampleX * wave.frequency + wave.seed, sampleZ * wave.frequency + wave.seed);
                    normalization += wave.amplitude;
                }

                noise /= normalization;
                noiseMap[xIndex, zIndex] = noise;
            }
        }
        return noiseMap;
    }

    public float[,] GenerateUniformNoiseMap(int width, int depth, float centerVertexZ, float maxVertexDistanceZ, float vertexOffsetZ)
    {
        float[,] noiseMap = new float[width, depth];

        for(int zIndex = 0; zIndex < depth; zIndex++)
        {
            float sampleZ = zIndex + vertexOffsetZ;

            float noise = Mathf.Abs(sampleZ - centerVertexZ) / maxVertexDistanceZ;

            for (int xIndex = 0; xIndex < depth; xIndex++)
                noiseMap[xIndex, depth - zIndex - 1] = noise;
        }
        return noiseMap;
    }
}
[System.Serializable]
public class Wave
{
    public float seed;
    public float frequency;
    public float amplitude;
}
