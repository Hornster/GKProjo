using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdit : MonoBehaviour {
   
    //the parent object that stores all of the floors
    public Transform parent;
    //the prefab of the floor object
    public GameObject floorPrefab;
    List<FloorController> floorParts;
    //floor factory
    public FloorBuilder floorBuilder;

    Vector3 position;
    Vector3 scale;
    Vector3 rotation;

    public Vector3 Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }
    public Vector3 Scale
    {
        get
        {
            return scale;
        }

        set
        {
            scale = value;
        }
    }
    public Vector3 Rotation
    {
        get
        {
            return rotation;
        }

        set
        {
            rotation = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        FloorController[] availableFloorParts = FindObjectsOfType<FloorController>();
        floorParts = new List<FloorController>();
        for(int i = 0; i < availableFloorParts.Length; i++)
            floorParts.Add(availableFloorParts[i]);
        scale = new Vector3(3, 3, 1);
        rotation = new Vector3(0, 0, 0);
        position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AddFloor()
    {
        GameObject newFloor = floorBuilder.BuildFloor(floorPrefab, Position, Rotation, Scale, parent);

        floorParts.Add(newFloor.GetComponent<FloorController>());
    }
    
    public void DeleteFloor(Transform target)
    {
        if(target != null)
        {
            FloorController targetController = target.GetComponent<FloorController>();
            floorParts.Remove(targetController);
            targetController.GetRidOfMe();
        }
    }
}
