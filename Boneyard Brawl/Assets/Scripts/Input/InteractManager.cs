using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public List<GameObject> interactableList = new List<GameObject>();

    [SerializeField] private GameObject rightGrip;
    [SerializeField] private GameObject leftGrip;
    private GameObject myWeapon;

    //initiate interact
    //takes in the buttons float value 0 = release, 1 = press
    public void Interact(float buttonValue)
    {
        if (interactableList.Count > 0 && buttonValue == 1f)
        {
            GameObject priorityInteract;
            priorityInteract = interactableList[interactableList.Count - 1];

            priorityInteract.GetComponent<Interactable>().Interact(this);
        }
    }

    //add upon entering collision with interactive object
    private void OnTriggerEnter(Collider other)
    {
        AddToList(other.gameObject);
    }

    //cycle through all objects in list of interactive elements and remove the object that is no longer in range
    private void OnTriggerExit(Collider other)
    {
        RemoveFromList(other.gameObject);
    }

    //add interactive objects to list so that they can be interacted with
    public void AddToList(GameObject interactableToAdd)
    {
        if (interactableToAdd.tag.Equals("Interactable"))
        {
            interactableList.Add(interactableToAdd);
        }
    }

    //remove interactive objects from list so that they cannot be interacted with
    public void RemoveFromList(GameObject interactiveToRemove)
    {
        for (int i = 0; i < interactableList.Count; i++)
        {
            if (interactableList[i] == interactiveToRemove)
                interactableList.RemoveAt(i);
        }
    }
}