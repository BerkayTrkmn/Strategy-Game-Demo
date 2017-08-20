using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour {
    // this code does get building place building
    //select unselect building
    //have more than one building then change other one
    //if mouse on the isbuild =false dont build(only 1x1 tile :S )
    //

    public  string buildingName ;
    public Sprite buildingImage;
    public Sprite buttonSprite;

    private bool isSelected=false;

    public string panelSource;
    public string textSource;
    public string imageSource;
    public string buildingTag;
    public string proroductionName;
    public string buttonSource;

    

    UIController uiController;


    ObjectPooler op;
    GameObject createdUnit;
    public static GameObject selectedObject;
    bool createButton=false;

    void Start () {
        uiController = new UIController();
        //object pooler
        op = new ObjectPooler();

        if (op == null)
        {
            //get prefab from Resources folder 
            op.createPool((GameObject)Resources.Load("Prefab/InGameObjects/Soldier"));
        }
        
    }


    void Update()
    {
        

        //left click clicked
        if (Input.GetMouseButtonDown(0))
        {
            //if not over the canvas objects
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;

            }
            //if  selected thing is null do this
            if (uiController.clickSelect() == null)
            {
                transform.GetChild(1).gameObject.SetActive(false);
                isSelected = false;
                uiController.findObjectInCanvas(panelSource).SetActive(false);
            }
             else if (uiController.clickSelect()==this.gameObject)
            {//if we have selected object then do below
                
                selectedObject = uiController.clickSelect();
               Debug.Log(selectedObject);
                //if clicked object tag Barracks then do this
                if ( uiController.clickSelect().transform.tag =="Barracks")
                {//is button active in hierarcy?

                    if (!uiController.findButtonInCanvas(buttonSource).gameObject.activeInHierarchy)
                    {
                        uiController.findButtonInCanvas(buttonSource).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(true);

                    }
                uiController.findButtonInCanvas(buttonSource).image.sprite = buttonSprite;
                }
                //if clicked object tag PowerPlant then do this
                if (uiController.clickSelect().transform.tag == "PowerPlant")
                {
                    uiController.findButtonInCanvas(buttonSource).gameObject.SetActive(false);

                }
                
                //if we clicked an object then isSelected true
                isSelected = true;
                //change text in canvas to building name
                uiController.findTextInCanvas(textSource).text = buildingName;
                //change sprite in canvas to building sprite(image)
                uiController.findImageInCanvas(imageSource).sprite = buildingImage;
                //activeted panel if clicked an object
                uiController.findObjectInCanvas(panelSource).SetActive(true);


            }
            else
            {//if we have selected object then clicked other objects 
             //then do below
                selectedObject = uiController.clickSelect();

                if(selectedObject != this.gameObject)
                {  
                    try
                    {    transform.GetChild(1).gameObject.SetActive(false);
                         selectedObject.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    catch (System.Exception)
                    {

                        //if click barracks then soldier this gives
                        //unity exception because soldier dont have
                        //index = 1 child but program works
                       // we control this code
                    }
                        
                    
                   
                  

                }
               
               
                //if clicked object tag Barracks then do this
                if (selectedObject.transform.tag == "Barracks")
                {//is button active in hierarcy?

                    if (!uiController.findButtonInCanvas(buttonSource).gameObject.activeInHierarchy)
                    {
                        uiController.findButtonInCanvas(buttonSource).gameObject.SetActive(true);
                        transform.GetChild(1).gameObject.SetActive(false); ;
                        selectedObject.transform.GetChild(1).gameObject.SetActive(true);
                        uiController.findButtonInCanvas(buttonSource).image.sprite = buttonSprite;

                    }
                   
                }
                //if clicked object tag PowerPlant then do this
                if (uiController.clickSelect().transform.tag == "PowerPlant")
                {
                    //button deactive, deselected,and info panel button deactive
                    uiController.findButtonInCanvas(buttonSource).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false); ;
                    selectedObject.transform.GetChild(1).gameObject.SetActive(true);
                  
                }
            }
        }
    }

   
        
       

    








}
