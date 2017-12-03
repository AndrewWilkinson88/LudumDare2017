using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOrbit : MonoBehaviour
{
    public GameObject target;
    public float radius = 10.0f;
    public float rotationSpeed = 5.0f;
    private float rot;

	// Use this for initialization
	void Start ()
    {
        transform.position = (transform.position - target.transform.position).normalized * radius + target.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(target.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        var desiredPosition = (transform.position - target.transform.position).normalized * radius + target.transform.position;
        transform.position = desiredPosition;
	}
}
