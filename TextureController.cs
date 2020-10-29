using System.Collections;
using UnityEngine;
using System.Linq;

public class TextureController : MonoBehaviour
{
    private Terrain terrain;
    private TerrainData terrainData;
    private Vector3 terrainPos;

    // Start is called before the first frame update
    void Start()
    {
        terrain = Terrain.activeTerrain;
        terrainData = terrain.terrainData;
        terrainPos = terrain.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flourish(int level)
    {
        // Splatmap data is stored internally as a 3d array of floats, so declare a new empty array ready for your custom splatmap data:
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
        float[,,] maps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);

        if (level == 1)
        {
            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    // Normalise x/y coordinates to range 0-1 
                    float y_01 = (float)y / (float)terrainData.alphamapHeight;
                    float x_01 = (float)x / (float)terrainData.alphamapWidth;

                    // Sample the height at this location (note GetHeight expects int coordinates corresponding to locations in the heightmap array)
                    float height = terrainData.GetHeight(Mathf.RoundToInt(y_01 * terrainData.heightmapResolution), Mathf.RoundToInt(x_01 * terrainData.heightmapResolution));

                    // Calculate the normal of the terrain (note this is in normalised coordinates relative to the overall terrain dimensions)
                    Vector3 normal = terrainData.GetInterpolatedNormal(y_01, x_01);

                    // Calculate the steepness of the terrain
                    float steepness = terrainData.GetSteepness(y_01, x_01);

                    // Setup an array to record the mix of texture weights at this point
                    float[] splatWeights = new float[terrainData.alphamapLayers];

                    // CHANGE THE RULES BELOW TO SET THE WEIGHTS OF EACH TEXTURE ON WHATEVER RULES YOU WANT

                    // Texture[0] has constant influence
                    splatWeights[0] = 0.3f;

                    // Texture[1] is stronger at lower altitudes
                    splatWeights[1] = Mathf.Clamp01((terrainData.heightmapResolution - height));

                    // Texture[2] stronger on flatter terrain
                    // Note "steepness" is unbounded, so we "normalise" it by dividing by the extent of heightmap height and scale factor
                    // Subtract result from 1.0 to give greater weighting to flat surfaces
                    splatWeights[2] = 1.0f - Mathf.Clamp01(steepness * steepness / (terrainData.heightmapResolution / 5.0f));

                    // Texture[3] increases with height but only on surfaces facing positive Z axis 
                    splatWeights[3] = height * Mathf.Clamp01(normal.z);

                    // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                    float z = splatWeights.Sum();

                    // Loop through each terrain texture
                    for (int i = 0; i < terrainData.alphamapLayers; i++)
                    {

                        // Normalize so that sum of all texture weights = 1
                        splatWeights[i] /= z;

                        // Assign this point to the splatmap array
                        splatmapData[x, y, i] = splatWeights[i];
                    }
                }
                for (int x = 100; x < terrainData.alphamapWidth; x++)
                {
                    for (int i = 0; i < terrainData.alphamapLayers; i++)
                    {
                        splatmapData[x, y, i] = maps[x, y, i];
                    }
                }
            }
        }
        else if(level == 2)
        {
            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                {
                    // Setup an array to record the mix of texture weights at this point
                    float[] splatWeights = new float[terrainData.alphamapLayers];

                    // Texture[0]
                    splatWeights[0] = maps[x,y,0];

                    // Texture[1]
                    splatWeights[1] = maps[x, y, 1]*2;

                    // Texture[2]
                    splatWeights[2] = maps[x, y, 2]*2;

                    // Texture[3]
                    splatWeights[3] = maps[x, y, 3];

                    // Sum of all textures weights must add to 1, so calculate normalization factor from sum of weights
                    float z = splatWeights.Sum();

                    // Loop through each terrain texture
                    for (int i = 0; i < terrainData.alphamapLayers; i++)
                    {
                        // Normalize so that sum of all texture weights = 1
                        splatWeights[i] /= z;

                        // Assign this point to the splatmap array
                        splatmapData[x, y, i] = splatWeights[i];
                    }
                }
            }
        }
        else if (level == 3)
        {
            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                {
                    
                }
            }
        }

        // Finally assign the new splatmap to the terrainData:
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
    private float[] GetTextureMix(Vector3 WorldPos)
    {
        // returns an array containing the relative mix of textures
        // on the main terrain at this world position.

        // The number of values in the array will equal the number
        // of textures added to the terrain.

        // calculate which splat map cell the worldPos falls within (ignoring y)
        int mapX = (int)(((WorldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((WorldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

        // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        // extract the 3D array data to a 1D array:
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

        for (int n = 0; n < cellMix.Length; n++)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }
        return cellMix;
    }
    private int GetMainTexture(Vector3 WorldPos)
    {
        // returns the zero-based index of the most dominant texture
        // on the main terrain at this world position.
        float[] mix = GetTextureMix(WorldPos);

        float maxMix = 0;
        int maxIndex = 0;

        // loop through each mix value and find the maximum
        for (int n = 0; n < mix.Length; n++)
        {
            if (mix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = mix[n];
            }
        }
        return maxIndex;
    }
}
