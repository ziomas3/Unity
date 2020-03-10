using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeBuilding : MonoBehaviour
{
    [SerializeField]
    private BiomeRow[] biomes;
    [SerializeField]
    private Color waterColor;


    public Texture2D BuildBiomeTexture(TerrainType[,] heightTerrainTypes, TerrainType[,] heatTerrainTypes, TerrainType[,] moistureTerrainTypes, Biome[,] chosenBiomes)
    {
        int tileWidth = heatTerrainTypes.GetLength(0);
        int tileDepth = heatTerrainTypes.GetLength(1);
        Color[] colorMap = new Color[tileWidth * tileDepth];
        for (int xIndex=0; xIndex < tileWidth; xIndex++)
        {
            for (int zIndex = 0; zIndex < tileDepth; zIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;
                TerrainType heightTerrainType = heightTerrainTypes[xIndex, zIndex];
                if (heightTerrainType.name != "water")
                {
                    TerrainType heatTerrainType = heatTerrainTypes[xIndex, zIndex];
                    TerrainType moistureTerrainType = moistureTerrainTypes[xIndex, zIndex];

                    Biome biome = this.biomes[moistureTerrainType.index].biomes[heatTerrainType.index];
                    chosenBiomes[xIndex, zIndex] = biome;
                    colorMap[colorIndex] = biome.color;
                }
                else
                {
                    colorMap[colorIndex] = waterColor;
                }
            }
        }
        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.filterMode = FilterMode.Point;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }

}

[System.Serializable]
public class BiomeRow
{
    public Biome[] biomes;
}

[System.Serializable]
public class Biome
{
    public string name;
    public Color color;
}
