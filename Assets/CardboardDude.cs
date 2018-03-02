using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class CardboardDude : MonoBehaviour {
   
    public GameObject mainCam;
    public GameObject waypointObject;
    [SerializeField]
    private GameObject[] waypoints;
    public GameManager gmanager;
    public GameObject[] fireLives;
    public AudioSource pain;
    public float distanceBeforeNewTarget = 0.2f;
    private bool canBeHit = true;
    private int currentTarget = 0;
    private NavMeshAgent agent;
    private int health = 3;
	// Use this for initialization
	void Start () {
        int waypointSize = waypointObject.transform.childCount;
        waypoints = new GameObject[waypointSize];
        //Intitalize pirate arrray
        for (int i = 0; i < waypointSize; i++)
        {
            waypoints[i] = waypointObject.transform.GetChild(i).gameObject;
        }

        agent = gameObject.GetComponent<NavMeshAgent>();
        UpdateTarget();
        
        foreach (GameObject life in fireLives)
        {
            life.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {

        switch (gmanager.GetGameState())
        {
            case GameManager.GameState.GAMEPLAY:
                CalculateDistancetoTarget();
                transform.LookAt(mainCam.transform);

                if (health <= 0 && canBeHit)
                {
                    gameObject.SetActive(false);
                }
                break;
        }

    }
    private void CalculateDistancetoTarget()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            UpdateTarget();
        }
    }
    private void UpdateTarget()
    {
        currentTarget = Random.Range(0, waypoints.Length);
        agent.ResetPath();
        agent.SetDestination(waypoints[currentTarget].transform.position);
    }
    private void UpdateTarget(int newTarget)
    {
        currentTarget = newTarget;
        agent.SetDestination(waypoints[currentTarget].transform.position);
    }
    private IEnumerator OnFire()
    {
        
        UpdateTarget();
       yield return new  WaitForSeconds(3);

        canBeHit = true;
        foreach(GameObject life in fireLives)
        {
            life.SetActive(false);
        }
        UpdateTarget();

    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball" && canBeHit)
        {
            switch (health)
            {
                case 1:
                    fireLives[0].SetActive(true);
                    fireLives[1].SetActive(true);
                    fireLives[2].SetActive(true);
                    gmanager.IncreaseScoreMultiplier();
                    break;
                case 2:
                    fireLives[0].SetActive(true);
                    fireLives[1].SetActive(true);
                    break;
                case 3:
                    fireLives[0].SetActive(true);
                    break;
            }
            
           
            canBeHit = false;
            health -= 1;
            pain.Play();
            gmanager.AdjustScore(10);
            StartCoroutine(OnFire());
        }
    }
}
