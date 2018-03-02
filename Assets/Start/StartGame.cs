using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameManager gManager;
    public GameObject firstDestination;
    public GameObject finalDestination;
    private bool reachedDestination = false;
    public List<GameObject> clouds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gManager.GetCurrentState() == GameManager.GameState.INTRO)
        {
            

            if (reachedDestination)
            {
                if (!clouds[0].GetComponent<MoveClouds>().GetIsMoving())
                {
                    for (int i = 0; i < clouds.Count; i++)
                    {
                        clouds[i].GetComponent<MoveClouds>().SetIsMoving(true);
                    }
                }
                Vector3 pos = transform.position;
                Vector3 dest = finalDestination.transform.position;
                if(Vector3.Distance(dest,pos) <= 0.2f)
                {
                   
                    gManager.SetGameReady(true);
                    gameObject.SetActive(false);
                }
                pos = Vector3.MoveTowards(pos, finalDestination.transform.position, Time.deltaTime * 10);
                transform.position = pos;
            } else
            {
                
                Vector3 pos = transform.position;
                Vector3 dest = firstDestination.transform.position;
                if (Vector3.Distance(pos,dest) <= 0.1f)
                {
                    reachedDestination = true;
                   
                }
                pos = Vector3.MoveTowards(pos, dest, Time.deltaTime * 2);
                transform.position = pos;
            }
            
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ball")
        {
            gManager.HitStartButton();
        }
    }
}
