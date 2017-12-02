using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private bool mouseIsDown;
    private Rigidbody2D rigidBody;
    public bool isOnBackpack = false;
    //public List<DraggableObject> draggableContact = new List<DraggableObject>();
    public Dictionary<DraggableObject, int> contacts = new Dictionary<DraggableObject, int>();

    // Use this for initialization
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mouseIsDown)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            Camera c = Camera.main;

            rigidBody.velocity = Vector2.zero;

            Vector3 p = c.ScreenToWorldPoint(new Vector3(x, y, TempCharacterController.instance.backpack.transform.position.z - Camera.main.transform.position.z));

            gameObject.transform.position = p;            
        }        
        else if(isOnBackpack)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, TempCharacterController.instance.backpack.transform.position.z);
        }

        isOnBackpack = false;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject.name + "  collided");
        DraggableObject d = col.gameObject.GetComponent<DraggableObject>();
        if (col.gameObject == TempCharacterController.instance.backpack)
        {
            if (TempCharacterController.instance.contacts.ContainsKey(this))
            {
                TempCharacterController.instance.contacts[this] = 1;
            }
            else
            {
                TempCharacterController.instance.contacts.Add(this, 1);
            }
            Debug.Log("adding contact, new count: " + TempCharacterController.instance.contacts[this]);
            //TempCharacterController.instance.draggableContacts.Add(this);
        }
        else if(d != null)
        {
            //draggableContact.Add(d);
            if(contacts.ContainsKey(d))
            {
                contacts[d] = 1;
            }
            else
            {
                contacts.Add(d, 1);
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        DraggableObject d = col.gameObject.GetComponent<DraggableObject>();
        if (col.gameObject == TempCharacterController.instance.backpack)
        {
            //TempCharacterController.instance.contacts.Remove(d);            
            TempCharacterController.instance.contacts[this] = 0;
            Debug.Log("removing contact, new count: " + TempCharacterController.instance.contacts[this]);
        }
        else if(d != null)
        {
            contacts[d] = 0;
        }
    }

    void OnMouseDown()
    {
        Debug.Log(gameObject.name + "  clicked");
        mouseIsDown = true;
    }

    void OnMouseUp()
    {
        mouseIsDown = false;
    }
    
}
