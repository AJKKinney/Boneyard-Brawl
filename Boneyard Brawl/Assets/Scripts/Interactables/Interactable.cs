using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //remove interactable from list
    public virtual void Interact(InteractManager interactManager)
    {
        interactManager.RemoveFromList(this.gameObject);
    }
}
