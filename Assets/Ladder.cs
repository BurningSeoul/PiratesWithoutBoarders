using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {
    private bool canBePickedUp = false;
    private int ladderHealth = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ladderHealth >= 0)
            canBePickedUp = true;
	}
    public bool GetPickedUp() { return canBePickedUp; }
    public int UpdateLadderHealth() { ladderHealth -= 1; return ladderHealth; }
}
