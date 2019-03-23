using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBuilder : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject BuildFloor(GameObject original, Vector3 position, Vector3 rotation, Vector3 scale, Transform parent)
    {
        GameObject newFloorPart = Instantiate(original, position, Quaternion.Euler(rotation), parent) as GameObject;
        newFloorPart.transform.localScale = scale;
        
        //newFloorPart.AddComponent<FloorController>();

        return newFloorPart;
        //trzeba dodac nowy skrypt do floorParts
    }
}
