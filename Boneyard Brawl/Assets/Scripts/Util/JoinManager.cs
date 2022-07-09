using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinManager : MonoBehaviour
{

    public static JoinManager instance;

    [SerializeField] private GameObject[] playerBlackouts;

    // Initialize
    void Awake()
    {
        //Singleton Setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayerJoin(int index)
    {
        if (index <= playerBlackouts.Length - 1)
        {
            playerBlackouts[index].SetActive(false);
        }
    }


}
