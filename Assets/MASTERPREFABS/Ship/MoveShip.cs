using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour {

    public float f_speed = 1;
    public Transform ship;
    private float f_startingPos;
    private float f_direction = -1;

	// Use this for initialization
	void Start () {
        f_startingPos = ship.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, f_direction, 0) * f_speed * Time.deltaTime);

		if(this.transform.position.y >= f_startingPos + .5f)
        {
            f_direction *= -1;
        }

        if (this.transform.position.y <= f_startingPos - .5f)
        {
            f_direction *= -1;
        }
    }
}
