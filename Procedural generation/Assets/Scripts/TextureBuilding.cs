using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureBuilding : MonoBehaviour
{
    public Texture2D BuildTexture(float[,] noiseMap, TerrainType[] terrainTypes, TerrainType[,] choosenTerrainTypes)
    {
        int tileWidth = noiseMap.GetLength(0);
        int tileDepth = noiseMap.GetLength(1);

        Color[] colorMap = new Color[tileWidth * tileDepth];
        for (int xIndex = 0; xIndex < tileWidth; xIndex++)

            for (int zIndex = 0; zIndex < tileDepth; zIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;
                float noise = noiseMap[xIndex, zIndex];
                TerrainType terrainType = ChooseTerrainType(noise, terrainTypes);
                colorMap[colorIndex] = terrainType.color;
                choosenTerrainTypes[xIndex, zIndex] = terrainType;
            }

        //tworzenie nowej tekstury i wypelnianie pixelami
        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.filterMode = FilterMode.Point;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }
    private TerrainType ChooseTerrainType(float noise, TerrainType[] terrainTypes)
    {
        int i = 0;
        while (terrainTypes[i].threshold < noise)
            if (i < terrainTypes.Length - 1)
                i++;
        return terrainTypes[i];

    }
}
[System.Serializable]
public class TerrainType
{
    public int index;
    public string name;
    public float threshold;
    public Color color;

}