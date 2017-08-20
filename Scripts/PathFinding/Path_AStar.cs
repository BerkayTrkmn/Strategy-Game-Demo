using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;
using System.Linq;
//this pathfinding I wroted
//but didn't work
//have some bugs and logic errors 

public class Path_AStar {

    Queue<Tile> path;

    public Path_AStar(World world , Tile tileStart, Tile tileEnd)
    {
        // Check to see if we have a valid tile graph
        if(world.TileGraph == null)
        {
            world.TileGraph = new Path_TileGraphs(world);

        }

        // a dictionary of all valid, buildable tiles
        Dictionary<Tile, Path_Nodes<Tile>> nodes = world.TileGraph.Nodes;

        Path_Nodes<Tile> start = nodes[tileStart];
        Path_Nodes<Tile> goal = nodes[tileEnd];

        // Control of start and ending tiles in the dictionary
        if (nodes.ContainsKey(tileStart) == false)
        {

            Debug.LogError("Path_AStar: The Starting tile isn't the lis of nodes");
        }

        if (nodes.ContainsKey(tileEnd) == false)
        {

            Debug.LogError("Path_AStar: The Ending tile isn't the lis of nodes");
        }

        List<Path_Nodes<Tile>> closedSet = new List<Path_Nodes<Tile>>();
        //List<Path_Nodes<Tile>> openSet = new List<Path_Nodes<Tile>>();
        //openSet.Add(start);

        
        SimplePriorityQueue<Path_Nodes<Tile>> openSet = new SimplePriorityQueue<Path_Nodes<Tile>>();
        openSet.Enqueue(start,0);


        Dictionary<Path_Nodes<Tile>, Path_Nodes<Tile>> came_From = new Dictionary<Path_Nodes<Tile>, Path_Nodes<Tile>>();

        Dictionary<Path_Nodes<Tile>, float> g_Score = new Dictionary<Path_Nodes<Tile>, float>();

        foreach(Path_Nodes<Tile> n in nodes.Values)
        {
            g_Score[n] = Mathf.Infinity;
        }
        g_Score[start] = 0;

        Dictionary<Path_Nodes<Tile>, float> f_Score = new Dictionary<Path_Nodes<Tile>, float>();

        foreach (Path_Nodes<Tile> n in nodes.Values)
        {
            f_Score[n] = Mathf.Infinity;
        }
        f_Score[start]= heuristic_Cost_Estimate(start,goal);

        while(openSet.Count > 0)
        {
            Path_Nodes<Tile> current = openSet.Dequeue();

            if(current == goal)
            {
                return;
            }
            closedSet.Add(current);

            foreach (Path_Edges<Tile> edge_neighbor in current.edges )
            {
                Path_Nodes<Tile> neighbor = edge_neighbor.node;


                if(closedSet.Contains(neighbor) == true)
                {
                    continue;
                 }
                float tentative_g_score = g_Score[current] + dist_between(current, neighbor);

                if(openSet.Contains(neighbor) && tentative_g_score >= g_Score[neighbor])
                   continue;

                came_From[neighbor] = current;
                g_Score[neighbor] = tentative_g_score;
                f_Score[neighbor] = g_Score[neighbor] + heuristic_Cost_Estimate(neighbor, goal);

                if(openSet.Contains(neighbor) == false)
                {
                    openSet.Enqueue(neighbor, f_Score[neighbor]);
                }

            }


        }//while end

        //if we reach here no path from start to goal

        //
    }



    float heuristic_Cost_Estimate(Path_Nodes<Tile> a, Path_Nodes<Tile> b)
    {

        return Mathf.Sqrt(
            Mathf.Pow(a.data.X -b.data.X , 2)+
            Mathf.Pow(a.data.Y - b.data.Y, 2));

    }

    float dist_between(Path_Nodes<Tile> a,Path_Nodes<Tile> b)
    {
        //horizontal/vertical neighbours have a distance of 1
        if (Mathf.Abs(a.data.X - b.data.X) + Mathf.Abs(a.data.Y - b.data.Y) == 1)
        {
            return 1f;

        }

        

       
       if( Mathf.Abs(a.data.X - b.data.X)== 1 && Mathf.Abs(a.data.Y - b.data.Y) == 1)
            {
                return 1.41421356237f;
            }

        return Mathf.Sqrt(
             Mathf.Pow(a.data.X - b.data.X, 2) +
             Mathf.Pow(a.data.Y - b.data.Y, 2));


    }

    void reconstruct_Path(
        Dictionary<Path_Nodes<Tile>, Path_Nodes<Tile>> came_From,
        Path_Nodes<Tile> current)
    {
        //this point, current is goal
        //we want the do is walk backwards through thecame_From
        //map, until reach the "end" of  that map...which will
        //be starting node.
        Queue<Tile> total_path = new Queue<Tile>();
        total_path.Enqueue(current.data);

        while (came_From.ContainsKey(current))
        {
            // came_From is a map,where the
            //key => value  relation is real saying
            //some_node=> we_got_there _from_this_node

            current = came_From[current];


        }

        path =new Queue<Tile>(total_path.Reverse());
    }


    public Tile dequeue()
    {
        return path.Dequeue();
    }

    public int Length()
    {
        if(path == null)
            return 0;

        return path.Count;

    }



}
