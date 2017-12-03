using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnterCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("ON TRIGGER ENTER!  " + col.gameObject.name);
        if (col.gameObject == RoundManager.instance.depositeArea)
        {
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
        }
    }
}
