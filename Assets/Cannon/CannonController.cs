using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {
    private float x;
    private float y;
    private float speed = 12;
    private int baseFOV = 60;
    private float currentFOV = 60;
    private float lowestFOV = 30;
    private bool zoom = false;
    private bool canShoot = true;
    public GameObject Turn;
    public GameObject UpDown;
    public GameObject cannonBall;
    public GameObject gun;
    public Camera cam;
    private List<GameObject> li_cannonBalls;
    public GameManager gmanager;
    
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
     void FixedUpdate()
    {
        
    }
    void Update () {
        if (canShoot)
        {
            if (zoom)
            {
                currentFOV = Mathf.Lerp(currentFOV, lowestFOV, Time.deltaTime * speed/3);
            } else
            {
                currentFOV = Mathf.Lerp(currentFOV, baseFOV, Time.deltaTime * speed/2);
            }
            if (Input.GetMouseButton(0))
            {
                zoom = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                zoom = false;
                gmanager.SubtractBallsRemaining();
                GameObject temp = GetCannonBall();
                temp.transform.position = gun.transform.position;
                temp.GetComponent<Rigidbody>().velocity = gun.transform.TransformDirection(Vector3.forward * (baseFOV/currentFOV) * 30);
                temp.SetActive(true);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Turn.transform.Rotate(0, -1 * speed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Turn.transform.Rotate(0, 1 * speed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                UpDown.transform.Rotate(-.5f, 0, 0 * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                UpDown.transform.Rotate(.5f, 0, 0 * speed * Time.deltaTime);
            }
            cam.fieldOfView = currentFOV;
        }
    }

    public void SetCanShoot(bool shoot){canShoot = shoot; }
}
