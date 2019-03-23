using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowControlScr : MonoBehaviour {
    Camera camera;
    const float scaleMultiplier = 0.3f;
    bool instructionsOn;

    //both text below are set in the editor. Only either of these is enabled at given time.
    public Text instructionsFull;
    public Text instructionsReminder;
	// Use this for initialization
	void Start () {
        camera = Camera.main;
        instructionsOn = false;
        ToggleInstructions();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(Input.GetKeyDown(KeyCode.J))
        {
            instructionsOn = !instructionsOn;
            ToggleInstructions();
        }

        ChangeZoom();
	}

    void ChangeZoom()
    {
        Vector2 wheelDelta;
        if(Input.GetKey(KeyCode.Z))
        {
            wheelDelta = Input.mouseScrollDelta;

            camera.orthographicSize -= wheelDelta.y * scaleMultiplier;
            //camera.aspect = camera.aspect + wheelDelta.y * scaleMultiplier;
        }
    }

    void ToggleInstructions()
    {
        instructionsFull.gameObject.SetActive(instructionsOn);
        instructionsReminder.gameObject.SetActive(!instructionsOn);
    }

    public Vector3 GetWorldPosition(Vector3 pixelPosition)
    {
        return camera.ScreenToWorldPoint(pixelPosition);
    }
}
