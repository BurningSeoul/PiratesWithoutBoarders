using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSun : MonoBehaviour {
    private float f_startingPosY;
    private float f_speed = 5;
    private float f_direction = -1;

	// Use this for initialization
	void Start () {
        f_startingPosY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, f_direction, 0) * f_speed * Time.deltaTime);

        if(transform.position.y >= f_startingPosY + 2)
        {
            f_direction *= -1;
        }

        if (transform.position.y <= f_startingPosY - 2)
        {
            f_direction *= -1;
        }
    }
}
