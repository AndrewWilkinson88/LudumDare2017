using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public int value = 100;

    public int breakThreashold = 50;

    public bool mouseIsDown;
    private Rigidbody rigidBody;
    public bool isOnBackpack = false;
    //public List<DraggableObject> draggableContact = new List<DraggableObject>();
    public Dictionary<DraggableObject, int> contacts = new Dictionary<DraggableObject, int>();

    // Use this for initialization
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (RoundManager.instance.gameOver)
            return;

        if (mouseIsDown)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            Camera c = Camera.main;

            rigidBody.velocity = Vector2.zero;
            //rigidBody.useGravity = false;
            //Vector3 p = c.ScreenToWorldPoint(new Vector3(x, y, MovementController.instance.backpack.transform.position.z - Camera.main.transform.position.z));
            Vector3 p = c.ScreenToWorldPoint(new Vector3(x, y, Vector3.Distance(MovementController.instance.backpack.transform.position, Camera.main.transform.position)));
            //Vector3 goalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, MovementController.instance.backpack.transform.position.z);
            Vector3 goalPos = new Vector3(p.x, Mathf.Max(p.y, .5f), MovementController.instance.backpack.transform.position.z);
            transform.position = Vector3.Lerp(gameObject.transform.position, goalPos, .1f);
            //gameObject.transform.position = new Vector3(p.x, Mathf.Max(p.y, .5f), MovementController.instance.backpack.transform.position.z);
        }
        else if (isOnBackpack)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, MovementController.instance.backpack.transform.position.z);
        }

        isOnBackpack = false;
    }


    void OnCollisionEnter(Collision col)
    {
        Debug.Log("magnitude = " + col.impulse.magnitude);

        //Debug.Log(col.gameObject.name + "  collided");
        DraggableObject d = col.gameObject.GetComponent<DraggableObject>();
        if (col.gameObject == MovementController.instance.backpack)
        {
            if (MovementController.instance.contacts.ContainsKey(this))
            {
                MovementController.instance.contacts[this] ++;
            }
            else
            {
                MovementController.instance.contacts.Add(this, 1);
            }
            Debug.Log("adding contact, new count: " + MovementController.instance.contacts[this]);
            //TempCharacterController.instance.draggableContacts.Add(this);
        }
        else if(d != null)
        {
            //draggableContact.Add(d);
            if(contacts.ContainsKey(d))
            {
                contacts[d] ++;
            }
            else
            {
                contacts.Add(d, 1);
            }
        }
        else if(!mouseIsDown && col.impulse.magnitude > breakThreashold)
        {
            Debug.Log("breaking = " + gameObject.name);
            if(MovementController.instance.contacts.ContainsKey(this))
            {
                MovementController.instance.contacts[this] --;
            }

            GameObject.Destroy(gameObject);
            GameObject breakEffect = GameObject.Instantiate<GameObject>(RoundManager.instance.itemBreakPrefab);
            breakEffect.transform.position = new Vector3(transform.position.x, col.transform.position.y, transform.position.z);
        }
    }

    void OnCollisionExit(Collision col)
    {
        DraggableObject d = col.gameObject.GetComponent<DraggableObject>();
        if (col.gameObject == MovementController.instance.backpack)
        {
            //TempCharacterController.instance.contacts.Remove(d);            
            MovementController.instance.contacts[this] --;
            Debug.Log("removing contact, new count: " + MovementController.instance.contacts[this]);
        }
        else if(d != null)
        {
            contacts[d] --;
        }
    }

    /*void OnMouseDown()
    {
        Debug.Log(gameObject.name + "  clicked");
        mouseIsDown = true;
    }

    void OnMouseUp()
    {
        mouseIsDown = false;
    }*/
    
}
