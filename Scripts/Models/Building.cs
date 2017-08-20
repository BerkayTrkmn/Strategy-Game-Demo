using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//building model
public class Building
{
    public enum BuildingType { Barracks, PowerPlant }
// size tile
    Tile tile;
    private BuildingType buildType;
    
    bool isPassable;

   

    int width = 1;
    int height = 1;

    public BuildingType BuildType
    {
        get
        {
            return buildType;
        }

        set
        {
            buildType = value;
        }
    }
    

    public int Width
    {
        get
        {
            return width;
        }

        set
        {
            width = value;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }

        set
        {
            height = value;
        }
    }

   static public Building CreatePrototype(BuildingType buildType ,bool isPassable=false ,int width =1 , int  height =1  )
    {
        Building obj = new Building();

        obj.BuildType = buildType;
        obj.isPassable = isPassable;
        obj.Width = width;
        obj.Height = height;
       

        return obj;

    }

  static  public Building PlaceInstance(Building prototype, Tile tile) {
        Building obj = new Building();

        obj.BuildType = prototype.buildType;
        obj.isPassable = prototype.isPassable;
        obj.Width = prototype.width;
        obj.Height = prototype.height;
        

        obj.tile = tile;
        //this assumes 1x1 object
        if(tile.PlaceObject(obj) == false)
        {
            //if place object false can't place object 
            return null;

        }

        return obj;
    }

}

public class Defense : Building
{
   
  
}


public class Production : Building {

   
   

}

public class Research : Building
{
   
    
}

public class UnitProduction : Production {


}

public class Barracks : UnitProduction {


}