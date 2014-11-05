using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {
    PlaceGadget GadgetSpawner;
	// Use this for initialization
	void Start () {
        GadgetSpawner = GameObject.Find("tk2dUICamera").GetComponent<PlaceGadget>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Q))
        {
            GadgetSpawner.SpawnFire();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GadgetSpawner.SpawnOil();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GadgetSpawner.SpawnReverseGravity();
        }
	}
}
