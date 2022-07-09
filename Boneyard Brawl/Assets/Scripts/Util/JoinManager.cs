using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinManager : MonoBehaviour
{

    public static JoinManager instance;

    [SerializeField] private Button startButton;

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

        if(index < 1)
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }


}
