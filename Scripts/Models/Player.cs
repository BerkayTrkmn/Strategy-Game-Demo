using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//no time to use 
//player model
public class Player  {

    string name;
    Color color;
    int team;

    int money;

    List<Building> techtree;

    List<Building> playerBuilding;

    List<Unit> playerArmy;

    public string Name
    {
        get
        {
            return name;
        }

      protected  set
        {
            name = value;
        }
    }

    public Color Color
    {
        get
        {
            return color;
        }

      protected  set
        {
            color = value;
        }
    }

    public int Team
    {
        get
        {
            return team;
        }

        set
        {
            team = value;
        }
    }

    public int Money
    {
        get
        {
            return money;
        }

        set
        {
            money = value;
        }
    }

    public List<Building> Techtree
    {
        get
        {
            return techtree;
        }

      protected  set
        {
            techtree = value;
        }
    }

    public List<Building> PlayerBuilding
    {
        get
        {
            return playerBuilding;
        }

        set
        {
            playerBuilding = value;
        }
    }

    public List<Unit> PlayerArmy
    {
        get
        {
            return playerArmy;
        }

        set
        {
            playerArmy = value;
        }
    }

    public Player(string name , Color color , int team, List<Building> techtree) {
        Name = name;
        Color = color;
        Team = team;
        Techtree = techtree;
       
    }

}
