using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public string Id { get; set; }

    public string TargetMap { get; set; }

    public string TargetSpawn { get; set; }

    public Action<string,string> TriggeredAction { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player")
        {
            this.TriggeredAction(this.TargetMap, this.TargetSpawn);
        }
    }
}
