using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharacterController : MonoBehaviour {

    Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(new Vector2(-1000f, 0));
            //transform.position = transform.position + new Vector3(-.01f,0,0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(new Vector2(1000f, 0));
            //transform.position = transform.position + new Vector3(.01f, 0, 0);
        }

    }
}
