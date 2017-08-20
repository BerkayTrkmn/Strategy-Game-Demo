using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    //some functions in this code
    //ex: clickSelect()
    public UIController instance;

    Canvas canvas;
    Text text;
    Image image;
    GameObject panel;
    Button button;
    BuildingController barracksController;
   
    void Awake()
    {
        
        instance = this;

    }
	
	void Start () {
      
        canvas = FindObjectOfType<Canvas>();
       
    }
	
	
	void Update () {
		
	}

    // in canvas have different object
    //if this object was deactived when start running game
    //we use find() function the find this deactiveted objects  
    #region get objects in canvas

    public Text findTextInCanvas(string source)
    {

        
        return GameObject.Find("Canvas").transform.Find(source).gameObject.GetComponent<Text>();
    }
    public Image findImageInCanvas(string source)
    {
       
        return GameObject.Find("Canvas").transform.Find(source).gameObject.GetComponent<Image>();
    }

    public GameObject findObjectInCanvas(string source)
    {

       
        return GameObject.Find("Canvas").transform.Find(source).gameObject;

    }
    public Button findButtonInCanvas(string source)
    {

       
        return GameObject.Find("Canvas").transform.Find(source).gameObject.GetComponent<Button>();

    }
    #endregion


    //create 0 length raycast(point) this raycast sarching game objects
    //and if hit one of them returns this gameobject 
    public GameObject clickSelect()
    {

      
        //current mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Converting Mouse Pos to 2D (vector2) World Pos
        Vector2 rayPos = new Vector2(mousePosition.x, mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        
        if (hit)
        {
           //return hit gameobject
            return hit.transform.gameObject;
        }
        else return null;


    }
    //If mouse position  is buildable turn true else false
    public bool isBuildable()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rayPos = new Vector2(mousePosition.x, mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        

        if (hit)
        {
            return false;
        }
           return true;
    }


}
