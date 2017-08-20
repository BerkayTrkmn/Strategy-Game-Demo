using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_TileGraphs  {

    // this class constructs a simple path-finding compatible graph
    //of our world. Each tile is a node. Each WALKABLE neighbour
    // from a tile is linked via edge connection

    Dictionary<Tile, Path_Nodes<Tile>> nodes;

    public Dictionary<Tile, Path_Nodes<Tile>> Nodes
    {
        get
        {
            return nodes;
        }

        protected set
        {
            nodes = value;
        }
    }


    public Path_TileGraphs(World world) {
        //Loop trough all tiles of the world
        //for each tile, create a node
        //dont create nodes for isbuildable = false tiles
        Nodes = new Dictionary<Tile, Path_Nodes<Tile>>();

        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                Tile tile = world.getTileAt(x,y);

                //eğer olmazsa tile isbuildable= false olmadan isbuildable ı çağırıyor
                if(tile.IsBuildable == false)// is tile buildable?
                {
                    Path_Nodes<Tile> n = new Path_Nodes<Tile>();
                    n.data = tile;
                    Nodes.Add(tile,n);

                }
            }
        }



        // now loop through all tiles again
        //create edges for neighbours

        foreach(Tile t in Nodes.Keys)
        {
            Path_Nodes<Tile> n = Nodes[t];

            List <Path_Edges<Tile>> edges = new List<Path_Edges<Tile>>();
             
            // get a list of neighbours for the tile
             Tile[] neighbours = t.getNeighbours(true);


            //if neighbour is walkable, create an edge to the relevant node
            for (int i = 0; i < neighbours.Length; i++)
            {
                if (neighbours[i] != null && neighbours[i].IsBuildable == true)
                {

                    //this neighbour exist and buildable(nothing have on tile), create an edge to the relevant node
                    Path_Edges<Tile> edge = new Path_Edges<Tile>();
                    edge.isBuildable = neighbours[i].IsBuildable;
                    edge.node = Nodes[neighbours[i]];

                    //Add to edge to temporary list
                    edges.Add(edge);
                }

            }

            n.edges = edges.ToArray();
        }


    }

    
}
