using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private float x;
    private float y = 0;
    private float z = 0;
    private Vector3 StartingPos;
    private Vector3 pos;
    

	// Use this for initialization
	void Start () {
        StartingPos = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        //x = Random.value * 0.1f * 2 - 0.1f;
        x = transform.localPosition.x - 0.475f;
        y = Random.value * 0.1f * 2 - 0.1f;
        //z = Random.value * 0.1f * 2 - 0.1f;
        z = StartingPos.z;
        //pos.x += x;
        pos.y += y;
        pos.z += z;
        // Debug.Log(y);
        if (pos.y > .5f)
        {
            pos.y = .5f;
        }

        if (pos.y < -.5f)
        {
            pos.y = -.5f;
        }

        if (pos.z > .5f)
        {
            pos.z = .5f;
        }

        if (pos.z < -.5f)
        {
            pos.z = -.5f;
        }

        if (pos.x > .5f)
        {
            pos.x = -0.475f;
        }

        if (pos.x < -.5f)
        {
            pos.x = -0.475f;
        }

        transform.localPosition = new Vector3(Mathf.Lerp(StartingPos.x, pos.x,5), Mathf.Lerp(StartingPos.y, pos.y, 5), Mathf.Lerp(StartingPos.z, pos.z, 5));
    }
}
