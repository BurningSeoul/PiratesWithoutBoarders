using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardDude : MonoBehaviour {
   
    public GameObject mainCam;
    private float speedModifier = 20f;
	// Use this for initialization
	void Start () {
        //_rigidbody.velocity = new Vector3(0,0,.000002f);
	}
	
	// Update is called once per frame
	void Update () {
      //  Vector3.MoveTowards(transform.position,waypoints.progressPoint.position, Time.deltaTime * speedModifier);
        transform.LookAt(mainCam.transform);
        //_rigidbody.velocity = Vector3.forward;

    }
}
