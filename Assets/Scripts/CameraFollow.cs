using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject FollowGO;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.position = new Vector3(FollowGO.transform.position.x + 6, gameObject.transform.position.y, gameObject.transform.position.z);
	
	}
}
