using UnityEngine;
using System;
//tile model
public class Tile  {
    // world tiles;
    public enum TileType { Empty, Grass, Hill  };
    //tiletype default empty
    TileType type = TileType.Empty;
    //tile x axis and y axis
    private int x;
    private int y;
    //Is this tile buildable?
    private bool isBuildable = true;

    Building building;
    Unit unit;
    
    //ore object
    Ore ore;

    public Unit Unit { get; private set;}
    public Building Building { get; private set; }
    //world object
    World world;
    
    #region Encapsulations x, y, Tiletype, isBuildable, world 
    public TileType Type
    {
        get
        {  return type;}
        set
        {  type = value;   }
    }
    public int X
    {  get
        { return x; }
        
    }
    public int Y
    {
        get
        { return y; }
        
    }

    public bool IsBuildable
    {
        get
        {
            return isBuildable;
        }

        set
        {
            isBuildable = value;
        }
    }

    public World World
    {
        get
        {
            return world;
        }

         set
        {
            world = value;
        }
    }
    #endregion
    public Tile(World _world, int _x,int _y)
        {
        World = _world;
        x = _x;
        y = _y;

        }

    public bool PlaceObject(Building objInstance)
    {
        if (objInstance == null) {

            building = null;
            return true;
        }

        if(building!= null)
        {

            Debug.Log("You can not build there");
            return false;
        }

        building = objInstance;
     return true;
    }


    public bool PlaceUnit(Unit objInstance)
    {
        if (objInstance == null)
        {

            unit = null;
            return true;
        }

        if (unit != null)
        {

            Debug.Log("You can not summon there");
            return false;
        }

        unit = objInstance;
        return true;
    }


    public bool isNeighbour(Tile tile , bool diagOkay= true)
    {
       
        //check horizontal adjacency 1.line
        return Mathf.Abs(this.X - tile.X) + Mathf.Abs(this.Y - tile.Y) == 1 ||
                (diagOkay && (Mathf.Abs(this.X - tile.X) == 1 && Mathf.Abs(this.Y - tile.Y) == 1));
        //2.line check diagonal adjacency

    }

    public Tile[] getNeighbours(bool diagOkay = true)
    {
        Tile[] neighbours;

        if(diagOkay == false)
        {
            neighbours = new Tile[4]; // Tile order :NORTH , EAST SOUTH,WEST
        }else
        {
            neighbours = new Tile[8];// Tile order : NORTH,EAST,SOUTH,WEST,NORTHEAST,SOUTHEAST,SOUTHWEST,NORTHWEST

        }

        Tile n;

        n = World.getTileAt(X, Y + 1);
        neighbours[0] = n;//Could be null it's okay
        n = World.getTileAt(X+1, Y);
        neighbours[1] = n;//Could be null it's okay
        n = World.getTileAt(X, Y - 1);
        neighbours[2] = n;//Could be null it's okay
        n = World.getTileAt(X-1, Y );
        neighbours[3] = n;//Could be null it's okay


        if (diagOkay == true) {

            n = World.getTileAt(X +1, Y + 1);
            neighbours[4] = n;//Could be null it's okay
            n = World.getTileAt(X + 1, Y -1);
            neighbours[5] = n;//Could be null it's okay
            n = World.getTileAt(X-1, Y - 1);
            neighbours[6] = n;//Could be null it's okay
            n = World.getTileAt(X - 1, Y+1);
            neighbours[7] = n;//Could be null it's okay





        }

        return neighbours;
    }





}
