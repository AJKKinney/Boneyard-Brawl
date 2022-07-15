using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
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

/*public class Sword : WeaponDatabase
{
    internal override void attack()
    {
        base.attack();


    }
}*/