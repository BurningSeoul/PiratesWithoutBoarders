using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CardboardDude : MonoBehaviour {
   
    public GameObject mainCam;
    public GameObject[] waypoints;
    public GameManager gmanager;
    public GameObject fire;
    public AudioSource pain;
    public float distanceBeforeNewTarget = 0.2f;
    private float speedModifier = 20f;
    private bool canBeHit = true;
    private int currentTarget = 0;
    private NavMeshAgent agent;
    private float distanceToTarget = 0f;
    private int health = 3;
	// Use this for initialization
	void Start () {
        agent = gameObject.GetComponent<NavMeshAgent>();
        UpdateTarget();
        fire.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        switch (gmanager.GetGameState())
        {
            case GameManager.GameState.GAMEPLAY:
                CalculateDistancetoTarget();
                transform.LookAt(mainCam.transform);

                if (health <= 0)
                {
                    gameObject.SetActive(false);
                }
                break;
        }
        
        
        
    }
    private void CalculateDistancetoTarget()
    {
        if(agent.remainingDistance < 0.5f)
        {
            UpdateTarget();
        }
    }
    private void UpdateTarget()
    {
        currentTarget = Random.Range(0, waypoints.Length);
        agent.destination = waypoints[currentTarget].transform.position;
    }
    private void UpdateTarget(int newTarget)
    {
        currentTarget = newTarget;
        agent.destination = waypoints[currentTarget].transform.position;
    }
    private IEnumerator OnFire()
    {
        
        UpdateTarget();
       yield return new  WaitForSeconds(2);

        canBeHit = true;
        fire.SetActive(false);
        UpdateTarget();

    }
    void OnCollisionEnter(Collision other)
    {

        Debug.Log(health);
        if (other.gameObject.tag == "Ball" && canBeHit)
        {
            fire.SetActive(true);
            health -= 1;
            canBeHit = false;
            pain.Play();
            gmanager.AdjustScore(+10);
            StartCoroutine(OnFire());
        }
    }
}
