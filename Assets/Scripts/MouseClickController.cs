using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickController : MonoBehaviour {
    public LayerMask itemLayerMask;

	// Use this for initialization
	void Start () {
		
	}

    private DraggableObject curDraggable;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            Debug.Log("CASTING RAY : " + ray + " on layer "+ itemLayerMask); 
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, itemLayerMask))
            {
                Debug.Log("HIT OBJECT : " + hit.collider.gameObject.name);
                DraggableObject d = hit.collider.gameObject.GetComponent<DraggableObject>();
                if (d != null)
                {
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
