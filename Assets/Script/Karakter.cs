using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    [SerializeField]
    private Tile currTile;

    public Tile CurrTile
    {
        get { return currTile; }
        set { currTile = value; }
    }
    public bool IsSelected { get; set; }
    public Rigidbody2D MyRigibody2D { get; set; }
    public bool Moving { get; set; }
    public Tile moveTile { get; set; }
    public bool IsLock { get; set; }
    
    public bool MovingTile;
    public bool Attack { get; set; }
    public Enemy SelectedEnemy { get; set; }
    public int MovementTile;
    [SerializeField]
    public Tile FindedTile;
    public Karakter()
    {
        MovementTile = 6;
    }
    public Karakter(int MovementTile)
    {
        this.MovementTile = MovementTile;
    }
    void Start()
    {
        IsSelected = false;
        MyRigibody2D = gameObject.GetComponent<Rigidbody2D>();
        Moving = false;
        IsLock = false;
        MovingTile = false;
    }

    void Update()
    {
        MovetoCont();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            MovementTile = 6;
        }
    }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrTile = collision.gameObject.GetComponent<Tile>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Dokundu");
            //Attack = false;
            //moveto(CurrTile);
        }
    }
    private void OnMouseDown()
    {
        if (!IsSelected)
        {
            IsSelected = true;
        }
        else
        {
            IsSelected = false;
        }
    }
    public void moveto(Tile MoveTile)
    {
        if (!IsLock || MovementTile < 1)
        {
            moveTile = MoveTile;
            Moving = true;
            IsLock = true;
        }
        
    }
    public void MovetoCont()
    {
        
        if (Moving && transform.position != new Vector3(moveTile.transform.position.x, moveTile.transform.position.y + 0.5f, -0.5f) && MovementTile > 0)
        {
            MovingTileControl();
        }
        else if(Moving && transform.position == new Vector3(moveTile.transform.position.x, moveTile.transform.position.y + 0.5f, -0.5f) && IsLock && IsSelected)
        {
            Moving = false;
            IsLock = false;
            IsSelected = false;
        }
    }
    public Tile FindTile(Tile tile, Tile isTile)
    {
        float min;
        if (tile != isTile && isTile.GetComponent<Tile>().Enemy == null)
        {
            min = FindMinDistTile(tile, moveTile);
            foreach (Tile MovetotoTile in tile.GetComponent<Tile>().NeihTile)
            {
                if (min == Vector2.Distance(MovetotoTile.transform.position, new Vector2(isTile.transform.position.x, isTile.transform.position.y + 0.5f)) && MovetotoTile.GetComponent<Tile>().Enemy == null)
                {
                    return MovetotoTile;
                }
            }
        }
        return null;
    }
    public float FindMinDistTile(Tile tile, Tile isTile)
    {
        float min = float.MaxValue;
        float dist;
        foreach (Tile MovetotoTile in tile.GetComponent<Tile>().NeihTile)
        {
            dist = Vector2.Distance(MovetotoTile.transform.position, new Vector2(isTile.transform.position.x, isTile.transform.position.y + 0.5f));
            if (dist < min && MovetotoTile.GetComponent<Tile>().Enemy == null)
            {
                min = dist;
            }
        }
        return min;
    }
    public void GoTile(Tile tile)
    {
        float step = 2 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(tile.transform.position.x, tile.transform.position.y + 0.5f, -0.5f), step);
    }
    public void GoTileHalf(Tile tile)
    {
        float step = 2 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(tile.transform.position.x / 2, (tile.transform.position.y + 0.5f) / 2, -0.5f), step);
    }
    public void MovingTileControl()
    {
        if (!MovingTile)
        {
            FindedTile = FindTile(currTile,moveTile);
            if (FindedTile != null)
            {
                MovingTile = true;
            }
            else
            {
                Moving = false;
                IsLock = false;
            }
        }
        else if (MovingTile && transform.position != new Vector3(FindedTile.transform.position.x, FindedTile.transform.position.y + 0.5f, -0.5f))
        {
            GoTile(FindedTile);
        }
        if (MovingTile && transform.position == new Vector3(FindedTile.transform.position.x, FindedTile.transform.position.y + 0.5f, -0.5f))
        {
            MovingTile = false;
            MovementTileDecaeaser(currTile.GetComponent<Tile>().Penality);
        }
    }
    public void MovementTileDecaeaser(int Penality)
    {
        MovementTile -= Penality;
    }

    public void Attackto(Enemy Enemy)
    {
        if (!IsLock || MovementTile < 1)
        {
            SelectedEnemy = Enemy;
            Attack = true;
            IsLock = true;
        }
    }

    public void AttackCont()
    {
        if (Attack && isNegihbor())
        {
            //todo attack
            GoTileHalf(SelectedEnemy.CurrTile);
        }
        else if (Attack && !isNegihbor())
        {
            //todo move to enemy person
            var FindedTile2 = FindTile(SelectedEnemy.CurrTile, CurrTile);
            moveto(FindedTile2);
        }
    }

    public bool isNegihbor()
    {
        foreach (var NeighTile in CurrTile.NeihTile)
        {
            if (NeighTile.Enemy == SelectedEnemy)
            {
                return true;
            }
        }
        return false;
    }
}
