using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour {

    public float f_speed = 3;
    private float f_direction = -1;
    private float f_directionY = -1;
    public Transform waterTransform;
    private float f_startingPos;
    private float f_startingPosY;

	// Use this for initialization
	void Start () {
        f_startingPos = waterTransform.position.z;
        f_startingPosY = waterTransform.position.y;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, f_directionY, f_direction) * f_speed * Time.deltaTime);


        if(this.transform.position.z <= f_startingPos - 5)
        {
            f_direction *= -1;
        }

        if(this.transform.position.z >= f_startingPos + 5)
        {
            f_direction *= -1;
        }

        if (this.transform.position.y <= f_startingPosY - 2)
        {
            f_directionY *= -1;
        }

        if (this.transform.position.y >= f_startingPosY + 2)
        {
            f_directionY *= -1;
        }
    }
}
