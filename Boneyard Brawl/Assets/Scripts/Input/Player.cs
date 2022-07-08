using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlayerManager.instance.RegisterPlayer(this.gameObject);
    }

    // Update is called once per frame
    void Quit()
    {
        PlayerManager.instance.UnregisterPlayer(this.gameObject);
        Destroy(this.gameObject);
    }
}
