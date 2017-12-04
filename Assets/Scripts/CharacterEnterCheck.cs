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
            foreach (DraggableObject d in MovementController.instance.hasBeenAdded.Keys)
            {
                if (d == null || d.gameObject == null)
                {
                    MovementController.instance.hasBeenAdded.Remove(d);
                }
            }
            int valueSum = 0;
            int multiplier = MovementController.instance.hasBeenAdded.Keys.Count;
            foreach (DraggableObject d in MovementController.instance.hasBeenAdded.Keys)
            {
                valueSum += d.value;
                //TODO:  Play an animation?  log that we collected this type of item?
                MovementController.instance.contacts[d] = 0;
                RoundManager.instance.collectedItems.Add(d);
                d.gameObject.SetActive(false);
                //GameObject.Destroy(d.gameObject);
            }

            RoundManager.instance.AddScore(valueSum * multiplier);
            ScoreManager.instance.SetLargestStack(MovementController.instance.hasBeenAdded.Keys.Count);
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
