using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointController : MonoBehaviour {
    //if unit selected then if click somewhere 
    //point object show where unit goes
    //this code controls point object

   private Tile currentTile;
   private UnitController unitController;
   private MouseController mouseController;

	void Start () {

        unitController = new UnitController();
        mouseController = new MouseController();
        
        	
	}
	
	void Update () {
        //get destination tile
        currentTile=mouseController.getTileAtWorldCoordinate(unitController.Destination);


        //if not in canvas object and if not clicked somewhere else 
        //and tile is buildable(buildable mean empty and moveable)
        if(EventSystem.current.IsPointerOverGameObject() == false && Input.GetMouseButtonDown(0) && currentTile.IsBuildable == true )
        {
            this.gameObject.SetActive(false);
        }

	}
}
