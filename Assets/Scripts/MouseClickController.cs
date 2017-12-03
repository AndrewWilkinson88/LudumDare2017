using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickController : MonoBehaviour {
    //Right now make this really long, we should probaly make it shorter eventually
    private float maxReach = 5f;
    public LayerMask itemLayerMask;    
    private DraggableObject curDraggable;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        if (RoundManager.instance.gameOver)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            Debug.Log("CASTING RAY : " + ray + " on layer "+ itemLayerMask); 
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, itemLayerMask))
            {
                Debug.Log("HIT OBJECT : " + hit.collider.gameObject.name);
                DraggableObject d = hit.collider.gameObject.GetComponentInParent<DraggableObject>();
                
                if (d != null && Vector3.Distance(d.transform.position, MovementController.instance.backpack.transform.position) <= maxReach)
                {
                    Rigidbody r = d.GetComponent<Rigidbody>();
                    r.useGravity = true;
                    
                    d.mouseIsDown = true;
                    curDraggable = d;                    
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && curDraggable != null)
        {
            curDraggable.mouseIsDown = false;
            curDraggable = null;            
        }
	}
}
