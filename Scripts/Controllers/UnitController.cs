using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : MonoBehaviour {
    //this code does unit movement and selection

    Soldier soldier;
    public GameObject point;
    private bool move;

    private Vector3 destination;
    ObjectPooler op;
    GameObject pooledPoint;


    MouseController mouseController;
    UIController uiController;

    bool isSelected = false;

    Tile currentTile;
    Tile destinationTile;
    Tile nextTile;

    float movementPercantage;

    Path_AStar pathAStar;

    public Vector3 Destination
    {
        get
        {
            return destination;
        }

        set
        {
            destination = value;
        }
    }
    List<PathFind.Point> path;

    bool isClicked = false;
    public float moveTimer = 8f;
    private float moveTimerCounter;
    void Start() {

        op = new ObjectPooler();

        uiController = new UIController();

        moveTimerCounter = moveTimer;

        mouseController = new MouseController();
        soldier = Soldier.createSoldier(10, 100, 5);
        op.createPool(point);

    }


    void Update() {
        
        currentTile = mouseController.getTileAtWorldCoordinate(this.transform.position);

        destinationTile = mouseController.getTileAtWorldCoordinate(destination);

        //first time click unit selected
        if (Input.GetMouseButtonDown(0))
        {
            if (uiController.clickSelect() == this.gameObject)
            {//unit selected
                isSelected = true;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        //if unit selected then move unit
        if (isSelected == true)
        {//click somewhere move to unit 
            moveToPoint();

            ///IMPORTANT!:This codes A* pathfinding initialize(execute) codes(failed)

            //if (Input.GetMouseButtonDown(0))
            //{

            //    PathFind.Point _from = new PathFind.Point(currentTile.X, currentTile.Y);
            //    PathFind.Point _to = new PathFind.Point(destinationTile.X, destinationTile.Y);

            //    PathFind.Grid grid = new PathFind.Grid(WorldController.Instance.World.Width, WorldController.Instance.World.Height, WorldController.Instance.tilesmap);

            //    path = PathFind.Pathfinding.FindPath(grid, _from, _to);
            //    Debug.Log(path);

            //    move = true;
            //    pooledPoint = op.getPooledObjects();
            //    pooledPoint.transform.position = Destination;
            //    pooledPoint.SetActive(true);

            //}
            //if (this.transform.position == Destination)
            //{
            //    move = false;
            //    pooledPoint.SetActive(false);

            //}

            //if (move == true)
            //{
            //    for (int i = 0; i < path.Count-1; i++)
            //    {
            //        Vector3 movePath = new Vector3(path[i].x, path[i].y, 0f);

            //        transform.position = Vector3.MoveTowards(new Vector3(path[i].x, path[i].y, 0f), new Vector3(path[i + 1].x, path[i + 1].y, 0f), soldier.MoveSpeed * Time.deltaTime);
            //    }
            //}
        }
        
       
     // if click right button then selected false
            if (Input.GetMouseButtonDown(1))
            {
               
                isSelected = false;
                transform.GetChild(0).gameObject.SetActive(false);
                try
                {
                    // if no point object active
                    // gives null point exception
                    pooledPoint.SetActive(false);
                }
                catch (System.NullReferenceException)
                {
                    //  giving null exception
                    // but my game is working
                    // then I use try catch and
                    //control nullexception

                }



            }
       
    }

    public void moveToPoint()
    {   //destination point tile
        destinationTile = mouseController.getTileAtWorldCoordinate(destination);
     
        //if destination point is on the eventsystem  do nothing
        if ( destinationTile == null)
            return;
        //if click mouse button 0
        if (Input.GetMouseButtonDown(0))
        { //refresh timer if job refreshed
            moveTimer = moveTimerCounter;
            //destination
            Destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            move = true;
            pooledPoint = op.getPooledObjects();
            pooledPoint.transform.position = Destination;
            pooledPoint.SetActive(true);

        }
        if (this.transform.position == Destination)
        {
            move = false;
            pooledPoint.SetActive(false);

        }
        // if move true then move object or objects
        if (move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, soldier.MoveSpeed * Time.deltaTime);
        }
       
        moveTimer -= Time.deltaTime;
        Debug.Log(moveTimer);
        if (moveTimer <= 0f)
        {
            
            move = false;
            transform.GetChild(0).gameObject.SetActive(false);
            pooledPoint.SetActive(false);
            moveTimer = moveTimerCounter;
            isSelected = false;
        }

    }
   //A* pathfinding initialize function(failed)
    public void update_DoMovement()
    {
        if(currentTile == destinationTile)
        {
            return;
        }
        if(nextTile ==null || nextTile == currentTile)
        {// get the next tile from the pathfinder
            if(pathAStar == null)
            {//this will calculate path current to destination
                pathAStar = new Path_AStar(currentTile.World,currentTile,destinationTile);
                if(pathAStar.Length() == 0)
                {
                    Debug.LogError("Returned no path to destination");
                    pathAStar = null;
                    return;
                }

            }
            //grab the next waypoint from the pathing system;
            nextTile = pathAStar.dequeue();
            if(nextTile == currentTile)
            {
                Debug.LogError("update_DoMovement - nextTile is currentTile!");
            }
        }
        //if code came this point we should have avalid nextTile


        float distanceToTravel = Mathf.Sqrt(
            Mathf.Pow(currentTile.X - nextTile.X, 2) +
            Mathf.Pow(currentTile.X - nextTile.X, 2)

            );
        //how much distance can be travel this update
        float distanceThisFrame = soldier.MoveSpeed * Time.deltaTime;

        float percantageThisFrame = distanceThisFrame/distanceToTravel;

        movementPercantage += percantageThisFrame;

        if(movementPercantage >= 1)
        {
            currentTile = nextTile;
            movementPercantage = 0;
        }

        
    }


}
