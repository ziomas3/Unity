using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGeneration : MonoBehaviour
{
    [SerializeField]
    private NoiseGeneration noiseGeneration;
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private float levelScale;
    [SerializeField]
    private int[] neighborRadius;
    [SerializeField]
    private GameObject[] treePrefab;
    public void GenerateTrees(int levelWidth, int levelDepth, float distanceBetweenVertices,LevelData levelData)
    {
        float[,] treeMap = noiseGeneration.GeneratePerlinNoiseMap(levelWidth, levelDepth, levelScale, 0, 0, waves);

        for (int xIndex = 0; xIndex < levelWidth; xIndex++)
        {
            for (int zIndex = 0; zIndex < levelDepth; zIndex++)
            {
                TileCoordinate tileCoordinate = levelData.ConvertToTileCoordinate(xIndex, zIndex);
                TileData tileData = levelData.tilesData[tileCoordinate.tileXIndex, tileCoordinate.tileZIndex];
                TerrainType terrainType = tileData.chosenHeightTerrainTypes[tileCoordinate.tileXIndex, tileCoordinate.tileZIndex];

                Biome biome = tileData.chosenBiomes[tileCoordinate.coordinateXIndex, tileCoordinate.coordinateZIndex];

                if (terrainType.name != "water")
                {
                    float treeValue = treeMap[xIndex, zIndex];
                    int neighborZBegin = (int)Mathf.Max(0, zIndex - neighborRadius[biome.index]);
                    int neighborXBegin = (int)Mathf.Max(0, xIndex - neighborRadius[biome.index]);
                    int neighborZEnd = (int)Mathf.Min(levelDepth-1, zIndex + neighborRadius[biome.index]);
                    int neighborXEnd = (int)Mathf.Min(levelWidth-1, xIndex + neighborRadius[biome.index]);
                    float maxValue = 0f;
                    for (int neighborX=neighborXBegin; neighborX <= neighborXEnd; neighborX++)
                    {
                        for (int neighborZ = neighborZBegin; neighborZ <= neighborZEnd; neighborZ++)
                        {
                            float neighbourValue = treeMap[neighborX, neighborZ];
                            if (neighbourValue >= maxValue)
                            {
                                maxValue = neighbourValue;
                            }
                        }
                    }
                    if (treeValue == maxValue)
                    {
                        Vector3[] meshVertices = tileData.mesh.vertices;
                        int tileWidth = tileData.heightMap.GetLength(0);
                        int vertexIndex = tileCoordinate.coordinateZIndex * tileWidth + tileCoordinate.coordinateXIndex;
                        Vector3 treePosition = new Vector3(xIndex * distanceBetweenVertices, meshVertices[vertexIndex].y, zIndex * distanceBetweenVertices);
                        GameObject tree = Instantiate(treePrefab[biome.index], treePosition, Quaternion.identity) as GameObject;
                        tree.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    }
                }
            }
        }
    }
}