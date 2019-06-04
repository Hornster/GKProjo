using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject SolidTilePrefab;

    public GameObject AirTilePrefab;

    public GameObject TransitionTilePrefab;

    public GameObject PlayerSpawnerPrefab;
    
    public TextAsset DefaultMap;

    private const float TileUnit = 0.64f;

    // Start is called before the first frame update
    void Start()
    {
        LoadMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMap(string mapName = null, string spawnName = null)
    {
        TextAsset mapDescription;
        if (mapName != null && spawnName != null)
        {
            mapDescription = Resources.Load<TextAsset>($"Maps/JsonMaps/{mapName}");
        }
        else
        {
            mapDescription = this.DefaultMap;
            spawnName = string.Empty;
        }

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        var map = JsonConvert.DeserializeObject<MapSeed>(mapDescription.text);
        for (var y = 0; y < map.Tiles.Length; ++y)
        {
            for (var x = 0; x < map.Tiles[y].Length; ++x)
            {
                var prefab = map.Tiles[y][x] == 1 ? this.SolidTilePrefab : this.AirTilePrefab;
                Instantiate(prefab, new Vector3(x * TileUnit, (map.Tiles.Length - y) * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
            }
        }
        //
        foreach (var mapEntity in map.Entities)
        {
            if (mapEntity.Type == "PlayerSpawner")
            {
                var playerSpawnerGameObject = Instantiate(
                    this.PlayerSpawnerPrefab,
                    new Vector3((mapEntity.X + 1) * TileUnit, (mapEntity.Y + 1.5f) * TileUnit, 0),
                    Quaternion.identity);
                playerSpawnerGameObject.transform.parent = this.gameObject.transform;
                var playerSpawner = playerSpawnerGameObject.GetComponent<PlayerSpawner>();
                playerSpawner.Id = mapEntity.Id;
                if (playerSpawner.Id == spawnName)
                {
                    playerSpawner.SpawnPlayer();
                }
            }
            else if (mapEntity.Type == "Transition")
            {
                var playerSpawnerGameObject = Instantiate(
                    this.TransitionTilePrefab,
                    new Vector3(mapEntity.X * TileUnit, mapEntity.Y * TileUnit, 0),
                    Quaternion.identity);
                playerSpawnerGameObject.transform.parent = this.gameObject.transform;
                var playerSpawner = playerSpawnerGameObject.GetComponent<Transition>();
                playerSpawner.Id = mapEntity.Id;
                playerSpawner.TargetMap = mapEntity.Parameters["TargetMap"];
                playerSpawner.TargetSpawn = mapEntity.Parameters["TargetSpawn"];
                playerSpawner.TriggeredAction = this.LoadMap;
            }
        }
    }

    public class MapSeed
    {
        public int[][] Tiles { get; set; }

        public Entity[] Entities { get; set; }

        public class Entity
        {
            public string Id { get; set; }

            public string Type { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public Dictionary<string, string> Parameters { get; set; }
        }
    }
}
