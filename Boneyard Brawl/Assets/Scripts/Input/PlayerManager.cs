using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public List<GameObject> players = new List<GameObject>();

    // Initialize
    void Awake()
    {
        //Singleton Setup
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Registers a player to the player manager
    public void RegisterPlayer(GameObject player)
    {
        players.Add(player);
    }

    //Unregisters a player from the player manager
    public void UnregisterPlayer(GameObject player)
    {
        for(int i = 0; i <players.Count; i++)
        {
            if(players[i] == player)
            {
                players.RemoveAt(i);
            }
        }
    }
}
