using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour {

    MouseController mouseController;
    UIController uiController;

    Vector3 mousePosition = Vector3.zero;

    public GameObject prefab;

    ObjectPooler op;
    public GameObject createdUnit;

    GameObject createPoint;
    BuildingController barracksController;

   
    GameObject fadeGameObject;
    public int buildingTypeIndex;
    void Start () {
        mouseController = new MouseController();
        uiController = new UIController();
        barracksController = new BuildingController();
        op = new ObjectPooler();

        if (op == null)
        {
            //get soldier in resources folder
            op.createPool((GameObject)Resources.Load("Prefab/InGameObjects/Soldier"));
        }
        //get fade object (fade object is showing create place to building on mouse position)
        fadeGameObject=GameObject.Find("FadeObjects").transform.GetChild(buildingTypeIndex).gameObject;
    }
 
    Tile buildingTile;
    
    void Update () {
        

        if (mouseController.isBuildModeOn==true) {

           
            // mouse position tracker
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mouse position on the tile tracker
            buildingTile = mouseController.getTileAtWorldCoordinate(mousePosition);
            //if mouse clicks UI or non-buildable tile dont do anyting
            fadeGameObject.SetActive(true);
            fadeGameObject.transform.position = new Vector3(buildingTile.X,buildingTile.Y);



            //if isbuildable false then fade object  active goes false and builded tile 
            //is buildable change to false 
            if (uiController.isBuildable() == false)
            {
                fadeGameObject.SetActive(false);
                buildingTile.IsBuildable = false;
                return;
            }
           
            //if mouse not over the Canvas objects
            //or is  buildable not false
            if (EventSystem.current.IsPointerOverGameObject() || buildingTile.IsBuildable == false )
            {
                return;

            }
            
            // if left mouse button clicked do below
            if (Input.GetMouseButtonDown(0))
            {
                //get object pooler object
                GameObject building = mouseController.objectpooler.getPooledObjects();
                //put buildingtile ttransform
                building.transform.position = new Vector3(buildingTile.X, buildingTile.Y, 0);
                //builded til isbuildable goes false
                ///FIXME: Only 1x1 tile 
                buildingTile.IsBuildable = false;
                //build mode off
                mouseController.isBuildModeOn = false;
              
                //build building
                building.SetActive(true);
                //fadeobject deactive
                fadeGameObject.SetActive(false);
            }
        }
       
        }
    

    public void buildingButton()
    {
      
        //build mode on
        mouseController.isBuildModeOn = true;
        mouseController.createBuildings(prefab);


    }
    
   
    //crate a unit
    public void crateUnit()
    {
       
        Debug.Log(BuildingController.selectedObject);
        if (BuildingController.selectedObject.transform.tag == "Barracks")
        {
            //get pooled objects
            createdUnit = op.getPooledObjects();
            //activate
            createdUnit.SetActive(true);
            //put the world
            createdUnit.transform.position = BuildingController.selectedObject.transform.GetChild(0).transform.position;
            createdUnit.transform.rotation = BuildingController.selectedObject.transform.GetChild(0).transform.rotation;

        }
        
        ///this one dont have object pooler
        //createdUnit = op.getPooledObjects();
        //createdUnit.SetActive(true);

        //Vector3 createUnitPosition = new Vector3(
        //    createdUnit.transform.position.x + 2.5f,
        //    createdUnit.transform.position.y
        //    );
        // createdUnit.transform.position = this.pointToCreate.gameObject.transform.position;
        // Instantiate(createdUnit,createUnitPosition,createdUnit.transform.rotation );


    }


}
