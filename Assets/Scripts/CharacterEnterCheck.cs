using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnterCheck : MonoBehaviour
{
    public bool collecting = false;

    void Update()
    {
        if(collecting)
        {
            List<DraggableObject> toRemove = new List<DraggableObject>();
            foreach (DraggableObject d in MovementController.instance.hasBeenAdded.Keys)
            {
                if (d == null || d.gameObject == null || !d.gameObject.activeSelf)
                {
                    toRemove.Add(d);//MovementController.instance.hasBeenAdded.Remove(d);
                }
            }
            foreach (DraggableObject d in toRemove)
            {
                MovementController.instance.hasBeenAdded.Remove(d);                
                MovementController.instance.contacts.Remove(d);
            }

            toRemove = new List<DraggableObject>();
            int valueSum = 0;
            int multiplier = MovementController.instance.hasBeenAdded.Keys.Count;
            foreach (DraggableObject d in MovementController.instance.hasBeenAdded.Keys)
            {
                valueSum += d.value;
                //TODO:  Play an animation?  log that we collected this type of item?
                MovementController.instance.contacts[d] = 0;
                RoundManager.instance.collectedItems.Add(d);
                d.gameObject.SetActive(false);
                toRemove.Add(d);
                //GameObject.Destroy(d.gameObject);
            }
            
            foreach (DraggableObject d in toRemove)
            {
                MovementController.instance.hasBeenAdded.Remove(d);
                MovementController.instance.contacts.Remove(d);
            }

            RoundManager.instance.AddScore(valueSum * multiplier);
            ScoreManager.instance.SetLargestStack(multiplier);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("ON TRIGGER ENTER!  " + col.gameObject.name);
        if (col.gameObject == RoundManager.instance.depositeArea)
        {
            collecting = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == RoundManager.instance.depositeArea)
        {
            collecting = false;
        }
    }
}
