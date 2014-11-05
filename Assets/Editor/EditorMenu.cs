using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class EditorMenu : EditorWindow {

    [MenuItem("Chunk Menu/Check Chunk Bounds")]
    private static void CheckChunkBounds()
    {
        GameObject[] chunk = GameObject.FindGameObjectsWithTag("Chunk");
        BoxCollider[] BoxColliders = chunk[0].GetComponentsInChildren<BoxCollider>();

        GameObject lowestX = null;
        GameObject highestX = null;

        for (int i = 0; i < BoxColliders.Length; i++)
        {
            if (lowestX == null)
            {
                lowestX = BoxColliders[i].gameObject;
                
            }
            else
            {
                if ( EditorMenu.getLeftSide(BoxColliders[i].gameObject) < EditorMenu.getLeftSide(lowestX))
                {
                    lowestX = BoxColliders[i].gameObject;
                }
            }
        }

        for (int i = 0; i < BoxColliders.Length; i++)
        {
            if (highestX == null)
            {
                highestX = BoxColliders[i].gameObject;
            }
            else
            {
                if (EditorMenu.getRightSide(BoxColliders[i].gameObject) > EditorMenu.getRightSide(highestX))
                {
                    highestX = BoxColliders[i].gameObject;
                }
            }
        }

        float leftSide = getLeftSide(lowestX);
        float rightSide = getRightSide(highestX);
        chunk[0].gameObject.GetComponent<Chunk>().chunkLength = rightSide - leftSide;

        Debug.Log("HighestX: " + highestX.name + rightSide.ToString());
        Debug.Log("LowestX: " + lowestX.name + leftSide.ToString());


        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(leftSide, lowestX.transform.position.y, 0);

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(rightSide, highestX.transform.position.y, 0);
    }

    public static float getLeftSide(GameObject go)
    {
        return (go.transform.position.x + go.GetComponent<BoxCollider>().center.x) - (go.GetComponent<BoxCollider>().bounds.size.x / 2);
    }
    public static float getRightSide(GameObject go)
    {
        return (go.transform.position.x + go.GetComponent<BoxCollider>().center.x) + (go.GetComponent<BoxCollider>().bounds.size.x / 2);
    }
}
