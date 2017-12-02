using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public static MovementController instance;

    public GameObject backpack;
    public Rigidbody playerPhysicsPost;

    //public List<DraggableObject> contacts = new List<DraggableObject>();
    public Dictionary<DraggableObject, int> contacts = new Dictionary<DraggableObject, int>();

    // Use this for initialization
    void Start () {
        //playerPhysicsPost = GetComponent<Rigidbody>();
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A))
        {
            playerPhysicsPost.AddForce(new Vector3(-1000f, 0, 0));
            //transform.position = transform.position + new Vector3(-.01f,0,0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            playerPhysicsPost.AddForce(new Vector3(1000f, 0, 0));
            //transform.position = transform.position + new Vector3(.01f, 0, 0);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, .1f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -.1f);
        }
        calculateOnMe();
    }

    /// <summary>
    /// This method updates all objects contacting it and sets them to being on it.
    /// </summary>
    void calculateOnMe()
    {
        //keep a hash of the objects we've already considered so we can skip them later
        Dictionary<DraggableObject, bool> hasBeenAdded = new Dictionary<DraggableObject, bool>();

        Queue<DraggableObject> q = new Queue<DraggableObject>();
        
        foreach(DraggableObject d in contacts.Keys)
        {
            if (!hasBeenAdded.ContainsKey(d) && contacts[d] > 0)
            {
                hasBeenAdded.Add(d, true);
                q.Enqueue(d);
            }
        }

        //Debug.Log("num in contact: " + q.Count);
        while (q.Count > 0)
        {
            DraggableObject d = q.Dequeue();
            Debug.Log(d);
            d.isOnBackpack = true;
            foreach (DraggableObject ndo in d.contacts.Keys)
            {
                if(!hasBeenAdded.ContainsKey(ndo) && d.contacts[ndo] > 0)
                {
                    hasBeenAdded.Add(ndo, true);
                    q.Enqueue(ndo);
                }
            }
        }
    }
}
