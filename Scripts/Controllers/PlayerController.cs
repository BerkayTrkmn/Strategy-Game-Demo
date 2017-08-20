using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //this one if I have more time
    //I think to do player colors and names etc 
    //But unfortunately I don't have time 

    Player player;
    public string playerName;
    public Color playerColor;
    public int playerTeam;
    private List<Building> techtree;

    public PlayerController instance;
    void Awake() {
       

    }

	void Start () {
       
	}
	
	
	void Update () {
		
	}

    public Player createPlayer() {

        return player = new Player(playerName, playerColor, playerTeam, techtree);
    }

}
