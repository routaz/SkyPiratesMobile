using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class colorToPrefab
{
    public GameObject prefab;
    public Color color;
}
public class CoinGenerator : MonoBehaviour
{
    public Texture2D coinmap;
    public colorToPrefab[] colortoPrefab;
    public GameObject parentObj;


    private void Start()
    {
        GenerateMap();
        
    }

    void GenerateMap()
    {
        for (int x = 0; x < coinmap.width; x++)
        {
            for (int y = 0; y < coinmap.height; y++)
            {
                GenerateCoins(x, y);
            }
        }

    }

    void GenerateCoins(int x, int y)
    {
        Color mapColor = coinmap.GetPixel(x, y);
        foreach(colorToPrefab obj in colortoPrefab)
        {
            if (obj.color.Equals(mapColor))
            {
                Vector2 pos = new Vector2(x, y);
                Instantiate(obj.prefab, pos, Quaternion.identity,parentObj.transform);
            }
        }
    }
}
