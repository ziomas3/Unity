using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    [SerializeField]
    NoiseGeneration noiseGeneration;
    [SerializeField]
    TextureBuilding textureBuilding;
    [SerializeField]
    BiomeBuilding biomeBuilding;
    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private MeshCollider meshCollider;
    [SerializeField]
    private MeshRenderer tileRenderer;
    [SerializeField]
    private float levelScale;
    [SerializeField]
    private TerrainType[] heightTerrainTypes;
    [SerializeField]
    private TerrainType[] heatTerrainTypes;
    [SerializeField]
    private TerrainType[] moistureTerrainTypes;
    [SerializeField]
    private float heightMultiplier;

    [SerializeField]
    private AnimationCurve heightCurve;

    [SerializeField]
    private Wave[] heightWaves;
    [SerializeField]
    private Wave[] heatWaves;
    [SerializeField]
    private Wave[] moistureWaves;
    [SerializeField]
    private VisualisationMode visualisationMode;


    public TileData GenerateTile(float centerVertexZ, float maxDistanceZ)
    {
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;

        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;

        Vector3 tileDimensions = this.meshFilter.mesh.bounds.size;
        float distanceBetweenVertices = tileDimensions.z / (float)tileDepth;
        float vertexOffsetZ = this.gameObject.transform.position.z / distanceBetweenVertices;
        
        // Generowanie Map i Textur
        float[,] heightMap = noiseGeneration.GeneratePerlinNoiseMap(tileWidth, tileDepth, levelScale, offsetX, offsetZ, heightWaves);
        TerrainType[,] chosenHeightTerrainTypes = new TerrainType[tileWidth, tileDepth];
        Texture2D heightTexture = textureBuilding.BuildTexture(heightMap, heightTerrainTypes, chosenHeightTerrainTypes);

        // generate a uniformHeatMap using uniform noise
        float[,] uniformHeatMap = noiseGeneration.GenerateUniformNoiseMap(tileWidth, tileDepth, centerVertexZ, maxDistanceZ, vertexOffsetZ);
        // generate a randomHeatMap using Perlin Noise
        float[,] randomHeatMap = noiseGeneration.GeneratePerlinNoiseMap(tileWidth, tileDepth, this.levelScale, offsetX, offsetZ, this.heatWaves);
        float[,] heatMap = new float[tileWidth, tileDepth];
        for (int xIndex = 0; xIndex < tileWidth; xIndex++)
        {
            for (int zIndex = 0; zIndex < tileDepth; zIndex++)
            {
                // mix both heat maps together by multiplying their values
                heatMap[xIndex, zIndex] = uniformHeatMap[xIndex, zIndex] * randomHeatMap[xIndex, zIndex];
                TerrainType heightTerrainType = chosenHeightTerrainTypes[xIndex, zIndex];
                if (heightTerrainType.name == "mountain")
                {
                    // makes mountains even colder, by using a greater multiplier
                    heatMap[xIndex, zIndex] += 0.8f * heightMap[xIndex, zIndex];
                }
                else
                {
                    // makes higher regions colder, by adding the height value to the heat map
                    heatMap[xIndex, zIndex] += 0.5f * heightMap[xIndex, zIndex];
                }
                heatMap[xIndex, zIndex] = Mathf.Min(1f, heatMap[xIndex, zIndex]);
            }
        }
        
   
        TerrainType[,] chosenHeatTerrainTypes = new TerrainType[tileWidth, tileDepth];
        Texture2D heatTexture = textureBuilding.BuildTexture(heatMap, heatTerrainTypes, chosenHeatTerrainTypes);


        float[,] moistureMap = this.noiseGeneration.GeneratePerlinNoiseMap(tileDepth, tileWidth, this.levelScale, offsetX, offsetZ, this.moistureWaves);
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                TerrainType heightTerrainType = chosenHeightTerrainTypes[xIndex, zIndex];
                if (heightTerrainType.name == "water")
                {
                    moistureMap[xIndex, zIndex] += 0.5f * heightMap[xIndex, zIndex];
                }
                else
                {
                    // Wyzsze regiony staja sie suchsze
                    moistureMap[xIndex, zIndex] -= 0.1f * heightMap[xIndex, zIndex];
                }
                moistureMap[xIndex, zIndex] = Mathf.Min(1f, moistureMap[xIndex, zIndex]);
            }
        }
        TerrainType[,] chosenMoistureTerrainTypes = new TerrainType[tileWidth, tileDepth];
        Texture2D moistureTexture = textureBuilding.BuildTexture(moistureMap, moistureTerrainTypes, chosenMoistureTerrainTypes);

        Biome[,] chosenBiomes = new Biome[tileWidth, tileDepth];
        Texture2D biomeTexture = biomeBuilding.BuildBiomeTexture(chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes, chosenBiomes);


        switch (this.visualisationMode)
        {
            case VisualisationMode.Height:
                this.tileRenderer.material.mainTexture = heightTexture; 
                break;
            case VisualisationMode.Heat:
                this.tileRenderer.material.mainTexture = heatTexture; 
                break;
            case VisualisationMode.Moisture:
                this.tileRenderer.material.mainTexture = moistureTexture; 
                break;
            case VisualisationMode.Biome:
                this.tileRenderer.material.mainTexture = biomeTexture;
                break;
        }
        
        UpdateMeshVertices(heightMap);
        TileData tileData = new TileData(heightMap, heatMap, moistureMap, chosenHeightTerrainTypes, chosenHeatTerrainTypes, chosenMoistureTerrainTypes, chosenBiomes, meshFilter.mesh, (Texture2D)tileRenderer.material.mainTexture);
        return tileData;
    }
    
    private void UpdateMeshVertices(float[,] heightMap)
    {
        int tileWidth = heightMap.GetLength(0);
        int tileDepth = heightMap.GetLength(1);

        Vector3[] meshVertices = this.meshFilter.mesh.vertices;

        for (int xIndex = 0; xIndex < tileWidth; xIndex++)
        {
            for (int zIndex = 0; zIndex < tileDepth; zIndex++)
            {
                int vertexIndex = zIndex * tileWidth + xIndex;

                float height = heightMap[xIndex, zIndex];

                Vector3 vertex = meshVertices[vertexIndex];
                meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * heightMultiplier, vertex.z);
            }
        }
        this.meshFilter.mesh.vertices = meshVertices;
        this.meshFilter.mesh.RecalculateBounds();
        this.meshFilter.mesh.RecalculateNormals();
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
        
    }

}
public class TileData
{
    public float[,] heightMap;
    public float[,] heatMap;
    public float[,] moistureMap;

    public TerrainType[,] chosenHeightTerrainTypes;
    public TerrainType[,] chosenHeatTerrainTypes;
    public TerrainType[,] chosenMoistureTerrainTypes;

    public Biome[,] chosenBiomes;
    public Mesh mesh;
    public Texture2D texture;
    
    public TileData(float[,] heightMap, float[,] heatMap, float[,] moistureMap, TerrainType[,] chosenHeightTerrainTypes, TerrainType[,] chosenHeatTerrainTypes, TerrainType[,] chosenMoistureTerrainTypes, Biome[,] chosenBiomes, Mesh mesh, Texture2D texture)
    {
        this.heightMap = heightMap;
        this.heatMap = heatMap;
        this.moistureMap = moistureMap;

        this.chosenHeightTerrainTypes = chosenHeightTerrainTypes;
        this.chosenHeatTerrainTypes = chosenHeatTerrainTypes;
        this.chosenMoistureTerrainTypes = chosenMoistureTerrainTypes;

        this.chosenBiomes = chosenBiomes;
        this.mesh = mesh;
        this.texture = texture;
    }
}
enum VisualisationMode { Height, Heat, Moisture, Biome }
