using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBroken : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("PoofFinished", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PoofFinished()
    {
        GameObject.Destroy(gameObject);
    }
}
