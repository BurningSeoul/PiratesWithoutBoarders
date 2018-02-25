using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour {

    private float f_startingPosY;
    public float f_speed;
    public float speed;
    private float f_direction = -1;
    private bool b_isMoving = false;

    // Use this for initialization
    void Start () {
        f_startingPosY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetIsMoving())
        {
            transform.Translate(new Vector3(f_direction, 0, speed) * f_speed * Time.deltaTime);

            if (transform.position.y >= f_startingPosY + 2)
            {
                f_direction *= -1;
            }

            if (transform.position.y <= f_startingPosY - 2)
            {
                f_direction *= -1;
            }
        }
        
    }

    public bool GetIsMoving() { return b_isMoving; }

    public void SetIsMoving(bool move) { b_isMoving = move; }
}
