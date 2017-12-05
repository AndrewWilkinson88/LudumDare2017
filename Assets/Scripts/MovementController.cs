using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public LayerMask movementLayerMask;

    public static MovementController instance;

    public GameObject backpack;
    public Rigidbody playerPhysicsPost;

    //public List<DraggableObject> contacts = new List<DraggableObject>();
    public Dictionary<DraggableObject, int> contacts = new Dictionary<DraggableObject, int>();
    public Dictionary<DraggableObject, bool> hasBeenAdded;

    // Use this for initialization
    void Start () {
        //playerPhysicsPost = GetComponent<Rigidbody>();
        instance = this;
    }

	// Update is called once per frame
	void Update () {

        if (RoundManager.instance!= null && RoundManager.instance.gameOver)
            return;

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerPhysicsPost.AddForce(new Vector3(-100f, 0, 0));
            //transform.position = transform.position + new Vector3(-.01f,0,0);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerPhysicsPost.AddForce(new Vector3(100f, 0, 0));
            //transform.position = transform.position + new Vector3(.01f, 0, 0);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Ray ray = new Ray(backpack.transform.position, new Vector3(0, 0, .05f));            
            if (!Physics.Raycast(ray , .25f , movementLayerMask))
            {
                transform.position += new Vector3(0, 0, .05f);
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Ray ray = new Ray(backpack.transform.position, new Vector3(0, 0, -.05f));
            if (!Physics.Raycast(ray, .25f, movementLayerMask))
            {
                transform.position += new Vector3(0, 0, -.05f);
            }
            
        }

        if(Input.GetKey(KeyCode.E))
        {
            backpack.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -40f));
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            backpack.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 40f));
        }

        calculateOnMe();
    }

    
    /// <summary>
    /// This method updates all objects contacting it and sets them to being on it.
    /// </summary>
    void calculateOnMe()
    {
        //keep a hash of the objects we've already considered so we can skip them later
        //Doubles as a list of all objects connected to the character
        hasBeenAdded = new Dictionary<DraggableObject, bool>();

        Queue<DraggableObject> q = new Queue<DraggableObject>();
        
        foreach(DraggableObject d in contacts.Keys)
        {
            if (!hasBeenAdded.ContainsKey(d) && contacts[d] > 0)
            {
                hasBeenAdded.Add(d, true);
                q.Enqueue(d);
            }
        }

        int curLootScore = 0;
        //int curMultiplier = 0;
        //Debug.Log("num in contact: " + q.Count);
        while (q.Count > 0)
        {
            DraggableObject d = q.Dequeue();
            if (d != null && d.gameObject != null && d.gameObject.activeSelf)
            {
                d.isOnBackpack = true;
                curLootScore += d.value;
                //curMultiplier++;
                foreach (DraggableObject ndo in d.contacts.Keys)
                {
                    if (!hasBeenAdded.ContainsKey(ndo) && d.contacts[ndo] > 0)
                    {
                        hasBeenAdded.Add(ndo, true);
                        q.Enqueue(ndo);
                    }
                }
            }
        }

        RoundManager.instance.SetCurLootScore(curLootScore, hasBeenAdded.Keys.Count);
    }

}
