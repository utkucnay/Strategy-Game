using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class Tile : MonoBehaviour
{
    public TerrTur TerrTur;
    public int Food { get; set; }
    public int Production { get; set; }
    [SerializeField]
    private List<Tile> neihTile;
    //komþu tilelar
    public List<Tile> NeihTile
    {
        get { return neihTile; }
        set { neihTile = value; }
    }
    [SerializeField]
    private Sprite tickTile;

    public Sprite TickTile
    {
        get { return tickTile; }
        set { tickTile = value; }
    }
    public Sprite UntickTile { get; set; }
    public bool IsTick { get; set; }
    public Karakter character { get; set; }
    public Enemy Enemy { get; set; }
    public int Penality { get; set; }
    // sadece ilk karade çalýþýr
    void Start()
    {
        UntickTile = gameObject.GetComponent<SpriteRenderer>().sprite;
        IsTick = false;
        character = GameObject.Find("character").GetComponent<Karakter>();
        
    }

    // Her karede çalýþýr.
    void Update()
    {
        FoodProducSet();
    }
    
    void TickTrigger()
    {
        if (!IsTick)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = tickTile;
            IsTick = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = UntickTile;
            IsTick = false;
        }
    }

    private void OnMouseDown()
    {
        if (character.IsSelected)
        {
            character.moveto(gameObject.GetComponent<Tile>());
        }
    }
    private void FoodProducSet()
    {
        switch (TerrTur)
        {
            case TerrTur.Desert:
                Food = 1;
                Production = 1;
                Penality = 2;
                break;
            case TerrTur.Forest:
                Food = 2;
                Production = 2;
                Penality = 3;
                break;
            case TerrTur.Terrarin:
                Food = 2;
                Production = 1;
                Penality = 2;
                break;
        }
    }
}
