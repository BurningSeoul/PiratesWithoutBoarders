using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Transform hands;
    public GameObject sheathedSword;
    public GameObject activeSword;
    public Animator anim;
    private bool isLockedInPlace = false;
    private bool isTouchingSomethingInteractive = false;
    private GameObject contextualObject;
    private GameObject contextualObjectPlantedPosition;
    private Rigidbody rigid;
    private bool oppositePush = false;


    // Use this for initialization
    void Start () {
        activeSword.SetActive(false);
        rigid = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLockedInPlace)
        {
            if (Input.GetButtonDown("Use"))
            {

                if (isTouchingSomethingInteractive)
                {
                    InteractWithObject(contextualObject);
                } else
                {
                    EquipSword();
                }
            } else if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("ATTACK");
            }
        } else
        {
            if(oppositePush && Input.GetButtonDown("Fire1")){
                oppositePush = !oppositePush;
            } else if( !oppositePush && Input.GetButtonDown("Fire2"))
            {
                oppositePush = !oppositePush;
            }

            if (contextualObject.GetComponent<Ladder>().UpdateLadderHealth() <= 0)
            {
                transform.parent = null;
      //          controller.SetCanMove(true);
                PickUp(contextualObject);
            }
            rigid.velocity = Vector3.zero;
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Interactive")
        {
            isTouchingSomethingInteractive = true;
            contextualObject = other.gameObject;
            Debug.Log("Context me Daddy");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isTouchingSomethingInteractive = false;
        contextualObject = null;
    }

    private void InteractWithObject(GameObject gameO)
    {

    }

    private void PickUp(GameObject gameO)
    {
        gameO.GetComponent<Collider>().enabled = false;
        gameO.GetComponent<Rigidbody>().useGravity = false;
        gameO.transform.parent = hands.transform;
    }
    private void EquipSword()
    {
        if (sheathedSword.activeInHierarchy)
        {
            anim.SetTrigger("Use");
        } else if (activeSword.activeInHierarchy)
        {
            anim.SetTrigger("noUse");
        }
        

    }
    public void SwordSwap()
    {
        if (sheathedSword.activeInHierarchy)
        {
            sheathedSword.SetActive(false);
            activeSword.SetActive(true);
        }
        else
        {
            sheathedSword.SetActive(true);
            activeSword.SetActive(false);
        }
    }
}
