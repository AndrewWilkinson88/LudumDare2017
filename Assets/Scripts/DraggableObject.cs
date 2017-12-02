using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private bool mouseIsDown;
    private Rigidbody2D rigidBody;

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

            Vector3 p = c.ScreenToWorldPoint(new Vector3(x, y, 10));

            gameObject.transform.position = p;
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
