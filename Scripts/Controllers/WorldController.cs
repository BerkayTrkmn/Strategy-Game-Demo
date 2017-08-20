using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {
    //instantiate world into game space
    //get sprite to the world 
   
  public static WorldController Instance { get;  set; }


    Path_TileGraphs path_TileGraphs;
    //world and tile data
   public World World { get; protected set; }

    //tiletypes
    public Sprite[] tileSprites;

    public bool[,] tilesmap;
   

    void Start () {
        

        if (Instance != null) {
            Debug.Log("Multiple world created!");
        }

        Instance = this;
         
        // Instantiate empty world
        World = new World();
        World.randomizeTiles();
        //Create a Gameobject each tiles 

        int width = World.Width;
        int height = World.Height;

        //create new tilesmap
        tilesmap = new bool[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                Tile tile_data = World.getTileAt(x, y);
                //Add a sprite renderer.
                SpriteRenderer tile_sr= tile_go.AddComponent<SpriteRenderer>();
                //set all tiles transform
                tile_go.transform.position = new Vector3(tile_data.X , tile_data.Y,0);
                tile_go.transform.SetParent(this.transform, true);
                //set tile type
                if (tile_data.Type == Tile.TileType.Grass) {
                    tile_sr.sprite = tileSprites[0];
                    tile_data.IsBuildable = true;
                    tilesmap[x, y] = tile_data.IsBuildable;
                }else
                if (tile_data.Type == Tile.TileType.Hill)
                {
                    tile_sr.sprite = tileSprites[1];
                    tile_data.IsBuildable = false;
                    tilesmap[x, y] = tile_data.IsBuildable;
                }

            }
        }
        

      //  path_TileGraphs = new Path_TileGraphs(Instance.World);

    }
   
	
	void Update () {

       
	}
    // kullanılmayan fonksiyon
    // tile tipini değiştirmeye yarıyor.
    void onTileTypeChanged(Tile tile_data, GameObject tile_go) {
        if (tile_data.Type == Tile.TileType.Grass) {
            tile_go.GetComponent<SpriteRenderer>().sprite = tileSprites[0];
            
        }
        if (tile_data.Type == Tile.TileType.Hill)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = tileSprites[1];
            
        }
        else
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
            Debug.Log("Sprite bulunamadı.Null döndü.");    
        }


    }
    


}
