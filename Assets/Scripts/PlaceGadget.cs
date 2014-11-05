using UnityEngine;
using System.Collections;

public class PlaceGadget : MonoBehaviour {

    public GameObject WobbleGadgetLocation;
    public GameObject FirePrefab;
    public GameObject OilPrefab;
    public GameObject ReverseGravityPrefab;
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnFire()
    {
        GameObject fire = Instantiate(FirePrefab, WobbleGadgetLocation.transform.position, Quaternion.identity) as GameObject;
        GameObject.Destroy(fire, 5);
    }
    public void SpawnOil()
    {
        GameObject oil = Instantiate(OilPrefab, WobbleGadgetLocation.transform.position, Quaternion.identity) as GameObject;
        GameObject.Destroy(oil, 5);
    }
    public void SpawnReverseGravity()
    {
        GameObject ReverseGravity = Instantiate(ReverseGravityPrefab, WobbleGadgetLocation.transform.position, Quaternion.identity) as GameObject;
        GameObject.Destroy(ReverseGravity, 5);
    }
}
