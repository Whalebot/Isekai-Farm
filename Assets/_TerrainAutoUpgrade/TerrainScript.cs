using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.IO;
using Sirenix.OdinInspector;



public class TerrainScript : MonoBehaviour
{
    public static TerrainScript Instance { get; private set; }
    public Terrain t;
    public float noise;
    public Transform pos;
    public int defaultSize;
    public int startIndex;
    public int endIndex;
    public int[] unfarmableLayers;
    public float addHeight;
    public DayCycle cycle;
    public bool checkOffset;
    public Vector3 offset;
    private void Awake()
    {
        Instance = this;
        DataManager.Instance.saveDataEvent += SaveTerrain;
        DataManager.Instance.loadDataEvent += LoadTerrain;
        TimeManager.Instance.resetEvent += DryTerrain;
    }

    private void Start()
    {
        if (cycle.weather == Weather.Rainy) WaterTerrain();
    }

    private void OnDisable()
    {
        DataManager.Instance.saveDataEvent -= SaveTerrain;
        DataManager.Instance.loadDataEvent -= LoadTerrain;
        TimeManager.Instance.resetEvent -= DryTerrain;
    }
    private void Update()
    {

    }

    [Button]
    void ButtonThing()
    {
        SwapTexture(startIndex, endIndex);
    }
    [Button]
    public void WinterGround()
    {
        SwapTexture(0, 5);
        SwapTexture(1, 6);
    }
    [Button]
    public void NormalGround()
    {
        SwapTexture(5, 0);
        SwapTexture(6, 1);
    }
    [Button]
    public void PaintGround()
    {
        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);
        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {


                float[] weights = new float[t.terrainData.alphamapLayers];

                for (int i = 0; i < maps.GetLength(2); i++)
                {
                    weights[i] = maps[x, y, i];
                }

                float sum = weights.Sum();
                //for (int ww = 0; ww < weights.Length; ww++)
                //{
                //    weights[ww] /= sum;
                //    maps[x, y, ww] = weights[ww];
                //}


                if (sum < 0.5F)
                    maps[x, y, 0] = 1;

            }
        }
        t.terrainData.SetAlphamaps(0, 0, maps);
    }


    void SwapTexture(int start, int end)
    {

        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);

        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                float temp = maps[x, y, start];
                float temp2 = maps[x, y, end];
                float sum = temp + temp2;

                maps[x, y, start] = 0;
                maps[x, y, end] = sum;

            }

        }
        t.terrainData.SetAlphamaps(0, 0, maps);
    }

    // Blend the two terrain textures according to the steepness of
    // the slope at each point.
    [Button]
    void Paint()
    {
        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);
        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                // Get the normalized terrain coordinate that
                // corresponds to the the point.
                float normX = x * 1.0f / (t.terrainData.alphamapWidth - 1);
                float normY = y * 1.0f / (t.terrainData.alphamapHeight - 1);

                // Get the steepness value at the normalized coordinate.
                var angle = t.terrainData.GetSteepness(normX, normY);

                // Steepness is given as an angle, 0..90 degrees. Divide
                // by 90 to get an alpha blending value in the range 0..1.
                var frac = angle / 90.0;
                maps[x, y, 2] = (float)frac;
                //  map[x, y, 0] = (float)(1 - frac);

            }
        }


        t.terrainData.SetAlphamaps(0, 0, maps);
    }

    void AddAlphaNoise(Terrain t, float noiseScale)
    {
        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);

        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                float a0 = maps[x, y, 0];
                float a1 = maps[x, y, 1];

                a0 += Random.value * noiseScale;
                a1 += Random.value * noiseScale;

                float total = a0 + a1;

                maps[x, y, 0] = a0 / total;
                maps[x, y, 1] = a1 / total;
            }
        }

        t.terrainData.SetAlphamaps(0, 0, maps);
    }

    public void ChangeTexture(Vector3 worldPos)
    {
        Vector3 offsetPos = worldPos - offset;
        int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, defaultSize, defaultSize);

        for (var y = 0; y < defaultSize; y++)
        {
            for (var x = 0; x < defaultSize; x++)
            {
                foreach (int layer in unfarmableLayers)
                {
                    if (splatmapData[x, y, layer] > 0.5F) return;
                }
                if (cycle.weather == Weather.Rainy)
                {
                    splatmapData[x, y, 0] = 0;
                    splatmapData[x, y, 1] = 0;
                    splatmapData[x, y, 2] = 0;
                    splatmapData[x, y, 3] = 0;
                    splatmapData[x, y, 4] = 1;
                }
                else
                {
                    splatmapData[x, y, 0] = 0;
                    splatmapData[x, y, 1] = 0;
                    splatmapData[x, y, 2] = 0;
                    splatmapData[x, y, 3] = 1;
                    splatmapData[x, y, 4] = 0;
                }
            }
        }
        t.terrainData.SetAlphamaps(mapX, mapZ, splatmapData);
    }

    public void TillTerrain(Vector3 worldPos, int size)
    {
        Vector3 offsetPos = worldPos - new Vector3(size / 2, 0, size / 2);
        int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, size, size);
        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                foreach (int layer in unfarmableLayers)
                {
                    if (splatmapData[x, y, layer] > 0.5F) return;
                }
                if (cycle.weather == Weather.Rainy)
                {
                    splatmapData[x, y, 0] = 0;
                    splatmapData[x, y, 1] = 0;
                    splatmapData[x, y, 2] = 0;
                    splatmapData[x, y, 3] = 0;
                    splatmapData[x, y, 4] = 1;
                }
                else
                {
                    splatmapData[x, y, 0] = 0;
                    splatmapData[x, y, 1] = 0;
                    splatmapData[x, y, 2] = 0;
                    splatmapData[x, y, 3] = 1;
                    splatmapData[x, y, 4] = 0;
                }
            }
        }
        //for (var y = 0; y < size; y++)
        //{
        //    for (var x = 0; x < size; x++)
        //    {
        //        int j;


        //        float[] weights = new float[t.terrainData.alphamapLayers];

        //        for (int i = 0; i < splatmapData.GetLength(2); i++)
        //        {
        //            weights[i] = splatmapData[x, y, i];
        //        }

        //        foreach (int layer in unfarmableLayers)
        //        {
        //            if (weights[layer] > 0.5F) return;
        //        }

        //        if (size % 2 == 0) j = size / 2;
        //        else j = (size - 1) / 2;

        //        int power = Mathf.Abs(x - j) + Mathf.Abs(y - j);
        //        float percentage = (float)(size - power) / size;


        //        weights[3] += percentage * 2;

        //        float sum = weights.Sum();
        //        for (int ww = 0; ww < weights.Length; ww++)
        //        {
        //            weights[ww] /= sum;
        //            splatmapData[x, y, ww] = weights[ww];
        //        }
        //    }
        //}
        t.terrainData.SetAlphamaps(mapX, mapZ, splatmapData);
        // t.Flush();
    }
    public void TillTerrain(Vector3 worldPos, int sizeX, int sizeY)
    {
        Vector3 offsetPos = worldPos - new Vector3(sizeX / 2, 0, sizeY / 2);
        int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, sizeX, sizeY);

        for (var y = 0; y < sizeY; y++)
        {
            for (var x = 0; x < sizeX; x++)
            {

                print("deg");

                float[] weights = new float[t.terrainData.alphamapLayers];

                foreach (int layer in unfarmableLayers)
                {
                    if (weights[layer] > 0.5F) return;
                }
                print("Pog");

                splatmapData[x, y, 0] = 0;
                splatmapData[x, y, 1] = 0;
                splatmapData[x, y, 2] = 0;
                splatmapData[x, y, 3] = 1;
                splatmapData[x, y, 4] = 0;


            }
        }
        t.terrainData.SetAlphamaps(mapX, mapZ, splatmapData);
        // t.Flush();
    }


    public void WaterTerrain(Vector3 worldPos)
    {

        Vector3 offsetPos = worldPos - offset;
        int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, defaultSize, defaultSize);


        for (var y = 0; y < defaultSize; y++)
        {
            for (var x = 0; x < defaultSize; x++)
            {
                //  float val = Mathf.Clamp(0.5, 0 , splatmapData[x, y, 3])
                float f = Mathf.Clamp(0.5F, 0, splatmapData[x, y, 3]);
                splatmapData[x, y, 4] += f;
                splatmapData[x, y, 3] -= f;
            }
        }
        t.terrainData.SetAlphamaps(mapX, mapZ, splatmapData);
    }


    public void NormalizeTerrain(Vector3 worldPos)
    {
        Vector3 offsetPos = worldPos - offset;
        int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, defaultSize, defaultSize);


        for (var y = 0; y < defaultSize; y++)
        {
            for (var x = 0; x < defaultSize; x++)
            {
                float f = splatmapData[x, y, 3] + splatmapData[x, y, 4];

                splatmapData[x, y, 0] += f;
                splatmapData[x, y, 3] = 0;
                splatmapData[x, y, 4] = 0;
            }
        }
        t.terrainData.SetAlphamaps(mapX, mapZ, splatmapData);
    }

    public void WaterTerrain(Vector3 worldPos, int size)
    {

        Vector3 offsetPos = worldPos - new Vector3(size / 2, 0, size / 2);
        int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, size, size);

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                int j;


                float[] weights = new float[t.terrainData.alphamapLayers];

                for (int i = 0; i < splatmapData.GetLength(2); i++)
                {
                    weights[i] = splatmapData[x, y, i];
                }

                if (size % 2 == 0) j = size / 2;
                else j = (size - 1) / 2;

                int power = Mathf.Abs(x - j) + Mathf.Abs(y - j);
                float percentage = (float)(size - power) / size;


                weights[4] += percentage * weights[3];
                weights[3] -= percentage * weights[3];

                float sum = weights.Sum();
                for (int ww = 0; ww < weights.Length; ww++)
                {
                    weights[ww] /= sum;
                    splatmapData[x, y, ww] = weights[ww];
                }
            }
        }
        t.terrainData.SetAlphamaps(mapX, mapZ, splatmapData);

        //Vector3 offsetPos = worldPos - new Vector3(size / 2, 0, size / 2);
        //int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        //int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        //float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, size, size);


        //for (var y = 0; y < size; y++)
        //{
        //    for (var x = 0; x < size; x++)
        //    {
        //        float f = splatmapData[x, y, 3];

        //        splatmapData[x, y, 4] += f;
        //        splatmapData[x, y, 3] = 0;
        //    }
        //}
        //t.terrainData.SetAlphamaps(mapX, mapZ, splatmapData);
    }

    public bool CheckTexture(Vector3 worldPos)
    {


        if (checkOffset)
        {
            Vector3 offsetPos = worldPos - offset;
            int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
            int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);


            float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, 2, 2);
            float val = 0;
            for (var y = 0; y < 2; y++)
            {
                for (var x = 0; x < 2; x++)
                {
                    val += splatmapData[x, y, 3];
                    val += splatmapData[x, y, 4];
                }
            }

            print(val);
            if (val > 3)
            {

                return true;
            }
            else return false;
        }
        else
        {
            int mapX = (int)(((worldPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
            int mapZ = (int)(((worldPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

            float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

            if (splatmapData[0, 0, 3] > 0.5F || splatmapData[0, 0, 4] > 0.5F)
            {

                return true;
            }
            else return false;
        }

    }

    public float CheckWater(Vector3 worldPos)
    {

        Vector3 offsetPos = worldPos - offset;
        int mapX = (int)(((offsetPos.x - t.GetPosition().x) / t.terrainData.size.x) * t.terrainData.alphamapWidth);
        int mapZ = (int)(((offsetPos.z - t.GetPosition().z) / t.terrainData.size.z) * t.terrainData.alphamapHeight);

        float[,,] splatmapData = t.terrainData.GetAlphamaps(mapX, mapZ, 2, 2);
        float val = 0;
        for (var y = 0; y < 2; y++)
        {
            for (var x = 0; x < 2; x++)
            {
                val += splatmapData[x, y, 4];
            }
        }

        return val;

    }

    public enum TerrainLayer
    {
        Default = 0,

        Green,
        Red
    }

    private void SaveTerrain()
    {
        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);
        List<float> degen = new List<float>();
        List<float> degen2 = new List<float>();
        List<float> degen3 = new List<float>();
        List<float> degen4 = new List<float>();
        List<float> degen5 = new List<float>();
        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                degen.Add(maps[x, y, 0] + maps[x, y, 5]);
                degen2.Add(maps[x, y, 1] + maps[x, y, 6]);
                degen3.Add(maps[x, y, 2]);
                degen4.Add(maps[x, y, 3]);
                degen5.Add(maps[x, y, 4]);
            }
        }
        DataManager.Instance.currentSaveData.terrainMap1 = degen;
        DataManager.Instance.currentSaveData.terrainMap2 = degen2;
        DataManager.Instance.currentSaveData.terrainMap3 = degen3;
        DataManager.Instance.currentSaveData.terrainMap4 = degen4;
        DataManager.Instance.currentSaveData.terrainMap5 = degen5;
    }

    [Button]
    public void DryTerrain()
    {
        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);
        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                float temp = maps[x, y, 3];
                float temp2 = maps[x, y, 4];
                float sum = temp + temp2;

                maps[x, y, 3] = sum;
                maps[x, y, 4] = 0;
            }
        }
        t.terrainData.SetAlphamaps(0, 0, maps);
    }

    [Button]
    public void WaterTerrain()
    {
        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);
        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                if (maps[x, y, 3] > 0.5) { }
                float temp = maps[x, y, 3];
                float temp2 = maps[x, y, 4];
                float sum = temp + temp2;

                maps[x, y, 3] = 0;
                maps[x, y, 4] = sum;
            }
        }
        t.terrainData.SetAlphamaps(0, 0, maps);
    }

    void LoadTerrain()
    {
        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);
        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                maps[x, y, 0] = DataManager.Instance.currentSaveData.terrainMap1[x + y * t.terrainData.alphamapWidth];
                maps[x, y, 1] = DataManager.Instance.currentSaveData.terrainMap2[x + y * t.terrainData.alphamapWidth];
                maps[x, y, 2] = DataManager.Instance.currentSaveData.terrainMap3[x + y * t.terrainData.alphamapWidth];
                maps[x, y, 3] = DataManager.Instance.currentSaveData.terrainMap4[x + y * t.terrainData.alphamapWidth];
                maps[x, y, 4] = DataManager.Instance.currentSaveData.terrainMap5[x + y * t.terrainData.alphamapWidth];
            }
        }
        t.terrainData.SetAlphamaps(0, 0, maps);
    }

}
