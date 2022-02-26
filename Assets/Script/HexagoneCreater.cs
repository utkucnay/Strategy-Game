using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
using System;

public class HexagoneCreater : MonoBehaviour
{
    [SerializeField]
    private GameObject Forest;
    [SerializeField]
    private GameObject Terrarin;
    [SerializeField]
    private GameObject Desert;
    [SerializeField]
    private GameObject Tiless;
    [SerializeField]
    private List<GameObject> Tiles;

    // Start is called before the first frame update
    void Start()
    {
        MapCreate(0, 0);
        MapCreate(-1f, -4.5f);
        MapCreate(1f, 4.5f);
        MapCreate(4, -3);
        MapCreate(5f, 1.5f);
        MapCreate(-4, 3);
        MapCreate(-5f, -1.5f);
        MapCreate(-9f, 1.5f);
        MapCreate(9f, -1.5f);
        MapCreate(6f, 6f);
        MapCreate(-6f, -6f);
        MapCreate(-10f, -3f);
        MapCreate(10f, 3f);
        MapCreate(8f, -6f);
        MapCreate(-8f, 6f);
        NeihborTiles();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MapCreate(float x, float y)
    {
        RandomTileCreate(x, y);
        RandomTileCreate(x + 1, y + 1.5f);
        RandomTileCreate(x + 1, y + -1.5f);
        RandomTileCreate(x + 2, y);
        RandomTileCreate(x + -1, y + 1.5f);
        RandomTileCreate(x + -1, y + -1.5f);
        RandomTileCreate(x + -2, y);
    }

    private void TileCreate(GameObject gameObject, float x, float y, TerrTur terrTur)
    {
        GameObject Obje = Instantiate(gameObject, new Vector3(x, y, 0), Quaternion.identity,Tiless.transform);
        Obje.GetComponent<Tile>().TerrTur = terrTur;
        Tiles.Add(Obje);
    }
    private void RandomTileCreate(float x, float y)
    {
        int Rand = UnityEngine.Random.Range(0, 4);
        switch (Rand)
        {
            case 0:
                TileCreate(Forest, x, y, TerrTur.Forest);
                break;
            case 1:
                TileCreate(Desert, x, y, TerrTur.Desert);
                break;
            case 2:
                TileCreate(Terrarin, x, y, TerrTur.Terrarin);
                break;
            case 3:
                TileCreate(Terrarin, x, y, TerrTur.Terrarin);
                break;
        }
    }
    private void NeihborTiles()
    {
        foreach (var TileMain in Tiles)
        {
            foreach (var TileOther in Tiles)
            {
                if (TileMain!=TileOther && Vector3.Distance(TileMain.transform.position, TileOther.transform.position) <= 2)
                {
                    TileMain.GetComponent<Tile>().NeihTile.Add(TileOther.GetComponent<Tile>());
                }
            }
        }
    }
}
