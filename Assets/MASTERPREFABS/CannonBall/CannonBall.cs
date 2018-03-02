using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public Rigidbody rb;
    public AudioSource sploosh;
    private float time = 4f;

	// Use this for initialization
	void Start () {
        //rb.velocity = transform.TransformDirection(Vector3.forward * 20);
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            this.gameObject.SetActive(false);
        }
	}

    void OnDisable()
    {
        time = 3f;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            sploosh.Play();
        }
    }
}
