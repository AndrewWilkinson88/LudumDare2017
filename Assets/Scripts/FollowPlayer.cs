using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerCharacter;
    Vector3 camDisatance = new Vector3(-1.5f, 2.0f, -4.5f);

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = playerCharacter.transform.position + camDisatance;
	}
}
