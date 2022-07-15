using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject[] WeaponTypes;
    public List <GameObject> spawnerList;
    
    [SerializeField] private GameObject coffin;

    //private bool spawnTimerSet = true;
    private float spawnTimer;

    private readonly float spawnTimerReset = 20f;

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
            SpawnWeapon();

            //spawnTimerSet = !spawnTimerSet;
            spawnTimer = spawnTimerReset;
        }
    }

    private void SpawnWeapon()
    {
        GameObject currentSpawn;
        GameObject newCoffin;
        int index;

        index = Random.Range(0, spawnerList.Count);
        currentSpawn = spawnerList[index];
        newCoffin = Instantiate(coffin, currentSpawn.transform.position, currentSpawn.transform.rotation);
        newCoffin.GetComponent<CoffinBreak>().weaponMaster = this;
        spawnerList.RemoveAt(index);
    }
}
