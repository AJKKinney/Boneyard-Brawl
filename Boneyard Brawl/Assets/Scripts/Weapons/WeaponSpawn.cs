using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject[] WeaponTypes;
    public List <GameObject> spawnerList;
    
    [SerializeField] private GameObject coffin;

    private bool spawnTimerSet = true;
    private float spawnTimer;

    private readonly float spawnTimerReset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //initialize timer
        spawnTimer = spawnTimerReset;
        GameObject[] spawnerArray = GameObject.FindGameObjectsWithTag("Weapon Spawn");
        
        for(int i = 0; i < spawnerArray.Length; i++)
        {
            spawnerList.Add(spawnerArray[i]);
        }
}

    // Update is called once per frame
    void Update()
    {
        //Update timer
        spawnTimer -= Time.deltaTime;
        
        //Reset timer
        if (spawnTimer <= 0f)
        {
            if (spawnTimerSet == true)
            {
                SpawnWeapon();
            }

            spawnTimerSet = !spawnTimerSet;
            spawnTimer = spawnTimerReset;
        }
    }

    private void SpawnWeapon()
    {
        if (spawnTimerSet == true)
        {
            GameObject currentSpawn;
            int index;

            index = Random.Range(0, spawnerList.Count);
            currentSpawn = spawnerList[index];
            Instantiate(coffin, currentSpawn.transform.position, currentSpawn.transform.rotation);
            spawnerList.RemoveAt(index);
        }
    }
}
