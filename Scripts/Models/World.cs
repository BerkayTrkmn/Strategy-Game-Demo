using UnityEngine;
using System.Collections.Generic;
//world model
//create world with tiles
public class World {
    private readonly int RANDOMISER_MAX_NUMBER =10;
    private readonly int RANDOMISER_MIN_NUMBER=0;
   
   private Tile[,] tiles;
    // The pathfinding graph use to navigate our world
    Path_TileGraphs tileGraph;

    //world width and height
   private int width;
   private int height;
  
    #region Encapsulations Height,Width, TileGraph
    public int Height
    {
        get
        {
            return height;
        }

        
    }

    public int Width
    {
        get
        {
            return width;
        }

    }

    public Path_TileGraphs TileGraph
    {
        get
        {
            return tileGraph;
        }

         set
        {
            tileGraph = value;
        }
    }
    #endregion

    public World(int _width = 50, int _height = 50)
    {// create world
        width = _width;
        height = _height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World created !"+ " Height : " + height +" Width :" + width );

       
    }
    
    public void randomizeTiles()
    {
        
        //if random number is 0 use tiletype.grass 1 then use tiletype.hill 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            { int randomTileNumber =Random.Range(RANDOMISER_MIN_NUMBER, RANDOMISER_MAX_NUMBER);
                if ( randomTileNumber == 0  || 
                     randomTileNumber == 1  || 
                     randomTileNumber == 2  ||
                     randomTileNumber == 3  || 
                     randomTileNumber == 4  || 
                     randomTileNumber == 5  ||
                     randomTileNumber == 6  ||
                     randomTileNumber == 7  ||
                     randomTileNumber == 8  
                    
                     ) {

                    tiles[x, y].Type = Tile.TileType.Grass;

                } else if (randomTileNumber == 9)
                {

                    tiles[x, y].Type = Tile.TileType.Hill;
                }


            }
        }
       
    }
    // get tile function
    public Tile getTileAt(int _x, int _y)
    {// tile  false action controller
        try
        {
            if (_x > width || _x < 0 || _y > width || _y < 0)
            {
                Debug.LogError("Tile x:" + _x + " y:" + _y + " out of range");

                return null;



            }

            return tiles[_x, _y];
        }
        catch (System.IndexOutOfRangeException)
        {

            return null;
        }
       
    
    
    }
    //A* not working then this function not used
    //This should be called whenever a change to the world
    //means that our world pathfinding info is invalid.
    public void invalidateTileGraph()
    {
        TileGraph = null;
    }

    
}
