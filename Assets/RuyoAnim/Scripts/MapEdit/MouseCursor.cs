using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {
    const int maxAlphaValue = 255, minAlphaValue = 0;
    const float rayDistance = 0.0001f;
    const float rotationMultiplier = 2.5f, scaleMultiplier = 0.4f;
    bool buildModeActive;
    //the map editing management class reference
    public MapEdit mapEdit;
    public WindowControlScr windowControl;
    //the ghostly appearance showing the azimute and position of the object to spawn.   Set in the editor
    public GameObject cursorPrefab;
    GameObject cursor;
    //the alpha channel value for the ghastly cursor ghost.                             Set in the editor
    [Range(minAlphaValue, maxAlphaValue)]
    public int alphaValue;
    //mask used for raycasting clicks.                                                  Set in the editor
    public LayerMask collisionLayer;

    // Use this for initialization
    void Start ()
    {
        buildModeActive = false;
        cursor = Instantiate(cursorPrefab);
        cursor.SetActive(buildModeActive);
        
        cursor.GetComponent<BoxCollider2D>().enabled = false;
        SpriteRenderer ghast = cursor.GetComponent<SpriteRenderer>();
        ghast.color = new Color(ghast.color.r, ghast.color.g, ghast.color.b, (float)alphaValue/ maxAlphaValue);
    }
	
	// Update is called once per frame
	void Update () {
        ChkInput();
	}

    void ChkInput()
    {
        float mouseWheelDelta = Input.mouseScrollDelta.y;
        if(buildModeActive)
        {
            Vector3 tempPos = windowControl.GetWorldPosition(Input.mousePosition);
            mapEdit.Position = new Vector3(tempPos.x, tempPos.y, 0);
            cursor.transform.position = mapEdit.Position;

            if (Input.GetMouseButtonDown(0))
            {
                mapEdit.AddFloor();
            }
           
            if(Input.GetMouseButtonDown(1))
            {
                ChkClickedObjects();
            }

            ChkTransforms(mouseWheelDelta);
        }


        if(Input.GetKeyDown(KeyCode.H))
        {
            buildModeActive = !buildModeActive;
            cursor.SetActive(buildModeActive);
        }
    }
    
    void ChkTransforms(float mouseWheelDelta)
    {
        if (Input.GetKey(KeyCode.R))
        {
            cursor.transform.Rotate(new Vector3(0, 0, mouseWheelDelta * rotationMultiplier));
            mapEdit.Rotation = new Vector3(0, 0, mapEdit.Rotation.z + mouseWheelDelta * rotationMultiplier);
        }

        if(Input.GetKey(KeyCode.T))
        {
            cursor.transform.localScale += (new Vector3(mouseWheelDelta * scaleMultiplier, mouseWheelDelta * scaleMultiplier, 0));
            mapEdit.Scale = cursor.transform.localScale;
        }
    }

    void ChkClickedObjects()
    {
        Ray2D ray = new Ray2D(Input.mousePosition, new Vector3(0, 0.001f, -1));
        RaycastHit2D hit = Physics2D.Raycast(windowControl.GetWorldPosition(Input.mousePosition), new Vector2(0,1), rayDistance,collisionLayer);

        if(hit)
        {
            mapEdit.DeleteFloor(hit.transform);
        }
    }
}
