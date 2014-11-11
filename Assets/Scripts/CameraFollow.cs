using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PanToNextChunk()
    {
        Chunk chunk = gameObject.GetComponent<ChunkSpawner>().SpawnChunk();

        Time.timeScale = 0;

        Hashtable ht = new Hashtable();
        ht.Add("position", chunk.CameraLocation.transform.position);
        ht.Add("time", 2);
        ht.Add("easeType", iTween.EaseType.linear);
        ht.Add("ignoretimescale", true);
        ht.Add("oncomplete", "resumeTime");
        iTween.MoveTo(gameObject, ht);

    }

    void resumeTime()
    {
        Time.timeScale = 1;
    }
}
