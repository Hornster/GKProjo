using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool createEnemy;

    private GameObject enemy;

    public GameObject EnemyPrefab { get; set; }

    public string Id { get; set; }

    public void SpawnEnemy()
    {
        this.createEnemy = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.enemy == null && this.createEnemy)
        {
            this.enemy = Instantiate(this.EnemyPrefab, this.gameObject.transform.position, Quaternion.identity);
            this.enemy.transform.parent = this.gameObject.transform;
        }

        this.createEnemy = false;
    }
}
