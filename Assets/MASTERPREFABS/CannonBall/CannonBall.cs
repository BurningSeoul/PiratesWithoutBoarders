using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public Rigidbody rb;
    public AudioSource sploosh;
    private GameManager gManager;
    private float time = 4f;
    private bool hitPirate = false;

    // Use this for initialization
    void Start() {
        //rb.velocity = transform.TransformDirection(Vector3.forward * 20);
    }
    void Awake()
    {
        gManager = GameObject.Find("LevelObjects").GetComponent<GameManager>();
        hitPirate = false;
        time = 3f;
    }
    // Update is called once per frame
    void Update () {
        time -= Time.deltaTime;

        if(time <= 0)
        {   
            gameObject.SetActive(false);
        }
	}

    void OnDisable()
    {
        time = 3f;
        if (!hitPirate)
        {
            gManager.AdjustScore(-10f);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            sploosh.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fish"))
            gManager.ResetScoreMultiplier();

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Start"))
            hitPirate = true;
    }
}
