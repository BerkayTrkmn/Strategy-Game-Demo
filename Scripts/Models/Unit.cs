using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//unit model
//below the unit soldier model
public abstract class Unit  {
    public enum  UnitType{ Soldier }

    public float X {
        get {
            return Mathf.Lerp(CurrentTile.X, DestinationTile.X,movementPercentage);


        }
    }
    public float Y
    {
        get
        {
            return Mathf.Lerp(CurrentTile.Y, DestinationTile.Y, movementPercentage);


        }
    }

    UnitType type;

    float movementPercentage;
    Player army;

   public Tile currentTile { get; protected set; }
    Tile destinationTile; // if we are not move currentTile = destinationTile

    string name;
    
    float health;
    float moveSpeed;

    SpriteRenderer sprite;

    public string Name
    {
        get
        {
            return name;
        }

      protected  set
        {
            name = value;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }

        protected set
        {
            health = value;
        }
    }

    public Player Army
    {
        get
        {
            return army;
        }

       protected set
        {
            army = value;
        }
    }

   

    public SpriteRenderer Sprite
    {
        get
        {
            return sprite;
        }

        protected set
        {
            sprite = value;
        }
    }

    public UnitType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public Tile CurrentTile
    {
        get
        {
            return currentTile;
        }

       protected set
        {
            currentTile = value;
        }
    }

    public Tile DestinationTile
    {
        get
        {
            return destinationTile;
        }

       protected set
        {
            destinationTile = value;
        }
    }

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

       protected set
        {
            moveSpeed = value;
        }
    }
}




public class Soldier : Unit {
    //soldier damage
   public int Damage { get; private set; }

    public UnitType SoldierType
    {
        get
        {
            return soldierType;
        }
    }

    private UnitType soldierType = UnitType.Soldier;


  

    // soldier create function
    public static Soldier createSoldier(int damage = 10, int health = 100, float moveSpeed = 10f)
    {
        Soldier obj = new Soldier();
        obj.MoveSpeed = moveSpeed;
       // obj.Sprite = sprite;
       // obj.Tile = tile;
        obj.Health =health ;
        //obj.Army = army;
        obj.Damage = damage;

        return obj;
    }
    public static Soldier placeSoldier(Soldier prototype , Tile tile) {
        Soldier obj = new Soldier();

        obj.Health = prototype.Health;
        obj.Damage = prototype.Damage;

        obj.CurrentTile = obj.DestinationTile = tile;

        //this assumes 1x1 object
        if (tile.PlaceUnit(obj) == false)
        {
            //if can't place unit
            return null;

        }

        return obj;
    }
    


}

/*
 * Soldier
 * 
 * Archers?
 * 
 * Worker?
 * 
 * 
 * 
 * */

