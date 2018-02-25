using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFish : MonoBehaviour {

    public float f_speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(-1, 0, 0) * f_speed * Time.deltaTime);
	}
}
