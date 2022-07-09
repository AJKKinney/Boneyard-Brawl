using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerCharacterPrefab;

    // Start is called before the first frame update
    void Awake()
    {
       for(int i = 0; i < PlayerManager.instance.players.Count; i++)
       {
            var playerCharacter = Instantiate(playerCharacterPrefab);
            playerCharacter.GetComponent<PlayerCharacterController>().playerInput = PlayerManager.instance.players[i].GetComponent<PlayerInputProvider>();
            PlayerManager.instance.players[i].GetComponent<PlayerInputProvider>().playerController = playerCharacter.GetComponent<PlayerCharacterController>();
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
