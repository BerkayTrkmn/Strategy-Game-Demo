using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseController : MonoBehaviour {

    public GameObject cursor;

    Vector3 currentFramePosition;
    Vector3 lastFramePosition;
    Tile tileUnderMouse;

    Vector3 move = new Vector3(0f, 0f, 0f);

    public bool isBuildModeOn=false;

    public ObjectPooler objectpooler = new ObjectPooler();

    


    void Start () {

        
        hideCursor();
        
	}
	
	
	
    void Update () {
       
        

        currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //cursor to mouse position
        cursor.transform.position = currentFramePosition;

       tileUnderMouse = getTileAtWorldCoordinate(currentFramePosition);

        //if (getTileAtWorldCoordinate(currentFramePosition) != null)
        //{
        //    Debug.Log(tileUnderMouse.X + ", " + tileUnderMouse.Y);

        //}

        //If over a UI element, then dont do camera mobement
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;

        }
           
         updateCameraMovement();
        
    }
    //does like function name :)
  public Tile getTileAtWorldCoordinate(Vector3 coordinate) {
        int x = Mathf.FloorToInt(coordinate.x);
        int y = Mathf.FloorToInt(coordinate.y);

        
        return WorldController.Instance.World.getTileAt(x , y);
    }
    //cursor show and hide
    #region Cursor Show and Hide Functions

    public void showCursor() {
        Cursor.visible = true;

    }
    public void hideCursor()
    {
        Cursor.visible = false;

    }
    #endregion

    World world;
    Tile firstTile;
    Tile lastTile;
    float halfHeight;
    float halfWidth;
    public void updateCameraMovement() {

        //transform of main camera
        Transform mainCameraTransform = Camera.main.transform;
        //if world null get world
    if (world == null)
    {
        world = WorldController.Instance.World;
    }
    //first tile in the world
    firstTile = world.getTileAt(0, 0);
    //last tile in the world
    lastTile = world.getTileAt(world.Height - 1, world.Width - 1);
        
        if (world.getTileAt(0, 0) != null && world.getTileAt(world.Height - 1, world.Width - 1) != null)
        {



            halfHeight = Camera.main.orthographicSize;;
            halfWidth = halfHeight * Screen.width / Screen.height;
            ///With Clamp, camera restriction
            ///FIXME:have optimization problems 
            float mainCameraX = Mathf.Clamp(mainCameraTransform.transform.position.x, firstTile.X + halfWidth, lastTile.X - halfWidth);
            float mainCameraY = Mathf.Clamp(mainCameraTransform.transform.position.y, firstTile.Y + halfHeight, lastTile.Y -halfHeight);
            mainCameraTransform.position = new Vector3(mainCameraX, mainCameraY);



            #region Screen dragging
            //screen dragging with mouse 1 and 2 button 
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                
                move = lastFramePosition - currentFramePosition;
                Camera.main.transform.Translate(move);
            }
            //find camera position
            lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            #endregion

            // camera zoom options
            Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 3;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2f, 10f);

        }  

    }
    //object pooler is created?
       bool isCreatedOP = false;
    //object pool ile building(object) yaratmaya yarıyor
    //button ile çalışabilmesi için kontrolü var
    public void createBuildings(GameObject prefab)
    {
        
        if (isCreatedOP == false)
        {
            objectpooler.createPool(prefab, 5);
            isCreatedOP = true;
        }
       
    }


    //kullanılmayan fonksiyon (unused function)
    //object poolsuz building çağırmaya yarıyor.
    //without object pooler calls building
    public void buildMode_Buildings(Building building ,Sprite[] sprite) {
        //mouse position vector
        Vector3 mousePosition= Vector3.zero;
        //tile on the building
        Tile buildingTile;
        // mouse position tracker
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouse position on the tile tracker
        buildingTile = getTileAtWorldCoordinate(mousePosition);
        if (isBuildModeOn)
        {  //mouse position in the screen
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //
            GameObject building_cursor_go = new GameObject();
            building_cursor_go.name = "Building_cursor";
            SpriteRenderer building_cursor_sr = building_cursor_go.AddComponent<SpriteRenderer>();
            building_cursor_sr.sortingLayerName = "FadeGameObjects";
            building_cursor_go.transform.position = mousePosition;
            
        }

        if (EventSystem.current.IsPointerOverGameObject() || buildingTile.IsBuildable == false)
        {
            return;

        }
       

        GameObject building_go = new GameObject();
        building_go.name = "Building";

        SpriteRenderer building_sr = building_go.AddComponent<SpriteRenderer>();
        building_sr.sortingLayerName = "GameObjects";
 
        building_go.transform.position =new Vector3(buildingTile.X,buildingTile.Y,0);
        buildingTile.IsBuildable = false;
        
        if (building.BuildType == Building.BuildingType.Barracks) {

           
                building_sr.sprite = sprite[0];
          
        }
        else if(building.BuildType == Building.BuildingType.PowerPlant) {

           building_sr.sprite = sprite[1];
           
        }




    }




}
