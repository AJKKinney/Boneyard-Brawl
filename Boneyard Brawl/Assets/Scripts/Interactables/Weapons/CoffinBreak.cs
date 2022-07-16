using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinBreak : MonoBehaviour
{
    [HideInInspector] public WeaponSpawn weaponMaster;

    private void OnTriggerEnter(Collider other)
    {
        //destroy coffin and spawn random weapon upon colliding with damage
        if (other.gameObject.tag.Equals("Player"))
        {
            int chosenWeapon = Random.Range(0, weaponMaster.WeaponTypes.Length);
            Instantiate(weaponMaster.WeaponTypes[chosenWeapon], transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}