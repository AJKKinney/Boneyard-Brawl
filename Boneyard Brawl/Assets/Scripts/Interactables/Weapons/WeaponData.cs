using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public string weaponName;
    public float weaponDamage;
    public float weaponStartupTime;
    public float weaponActiveTime;
    public float weaponRecoveryTime;
     
    //establish base attack function
    virtual internal void attack()
    {

    }
}

/*public class Sword : WeaponData
{
    internal override void attack()
    {
        base.attack();


    }
}*/