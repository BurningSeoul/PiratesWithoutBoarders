using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {
    private float x;
    private float y;
    private float speed = 10;
    public GameObject cannonBall;
    public GameObject gun;
    private List<GameObject> li_cannonBalls;
    
	// Use this for initialization
	void Start () {
        li_cannonBalls = new List<GameObject>();

        for(int i = 0; i < 30; i++)
        {
            GameObject temp;
            temp = Instantiate(cannonBall, gun.transform.position, gun.transform.rotation);
            temp.transform.parent = GameObject.Find("CannonBalls").transform;
            temp.SetActive(false);
            li_cannonBalls.Add(temp);
        }
	}

    GameObject GetCannonBall()
    {
        for(int i = 0; i < li_cannonBalls.Count; i++)
        {
            if (!li_cannonBalls[i].activeInHierarchy)
            {
                return li_cannonBalls[i];
            }
        }

        GameObject temp = Instantiate(cannonBall, gun.transform.position, gun.transform.rotation);
        temp.transform.parent = GameObject.Find("CannonBalls").transform;
        temp.SetActive(false);
        li_cannonBalls.Add(temp);

        return temp;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject temp = GetCannonBall();
            temp.transform.position = gun.transform.position;
            temp.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.left * 30);
            temp.SetActive(true);
        }

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(0, -1 * speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1 * speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(0, 0, -1 * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0, 0, 1 * speed * Time.deltaTime);
        }

    }
}
