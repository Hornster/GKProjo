using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject SolidTilePrefab;

    public GameObject AirTilePrefab;

    public GameObject TransitionTilePrefab;

    public GameObject VictoryTilePrefab;

    public GameObject PlayerSpawnerPrefab;

    public GameObject EnemySpawnerPrefab;

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

        var mapJson = new JSONObject(mapDescription.text);
        var map = new MapSeed(mapJson);
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
            else if (mapEntity.Type == "Victory")
            {
                var playerSpawnerGameObject = Instantiate(
                    this.VictoryTilePrefab,
                    new Vector3(mapEntity.X * TileUnit, mapEntity.Y * TileUnit, 0),
                    Quaternion.identity);
                playerSpawnerGameObject.transform.parent = this.gameObject.transform;
            }
            else if (mapEntity.Type == "EnemySpawner")
            {
                var enemySpawnerGameObject = Instantiate(
                    this.EnemySpawnerPrefab,
                    new Vector3((mapEntity.X + 1) * TileUnit, (mapEntity.Y + 1.5f) * TileUnit, 0),
                    Quaternion.identity);
                enemySpawnerGameObject.transform.parent = this.gameObject.transform;
                var enemySpawner = enemySpawnerGameObject.GetComponent<EnemySpawner>();
                enemySpawner.Id = mapEntity.Id;
                enemySpawner.EnemyPrefab = Resources.Load<GameObject>($"Prefabs/{mapEntity.Parameters["Enemy"]}");
                Debug.Log(mapEntity.Parameters["Enemy"]);
                enemySpawner.SpawnEnemy();
            }
        }
    }

    public class MapSeed
    {
        public MapSeed(JSONObject json)
        {
            var tempJson = json.list[json.keys.IndexOf("Tiles")];
            Tiles = tempJson.list.Select(x => x.list.Select(y => (int)y.n).ToArray()).ToArray();

            tempJson = json.list[json.keys.IndexOf("Entities")];
            Entities = tempJson.list.Select(x => new Entity(x)).ToArray();
        }

        public int[][] Tiles { get; set; }

        public Entity[] Entities { get; set; }

        public class Entity
        {
            public Entity(JSONObject json)
            {
                var tempJson = json.list[json.keys.IndexOf("Type")];
                Type = tempJson.str;

                tempJson = json.list[json.keys.IndexOf("X")];
                X = (int)tempJson.n;

                tempJson = json.list[json.keys.IndexOf("Y")];
                Y = (int)tempJson.n;

                var index = json.keys.IndexOf("Id");
                if (index > -1)
                {
                    tempJson = json.list[index];
                    Id = tempJson.str;
                }
                else
                {
                    Id = string.Empty;
                }

                Parameters = new Dictionary<string, string>();
                index = json.keys.IndexOf("Parameters");
                if (index > -1)
                {
                    tempJson = json.list[index];
                    for (var i = 0; i < tempJson.keys.Count; ++i)
                    {
                        Parameters.Add(tempJson.keys[i], tempJson.list[i].str);
                    }
                }
            }

            public string Id { get; set; }

            public string Type { get; set; }

            public int X { get; set; }

            public int Y { get; set; }

            public Dictionary<string, string> Parameters { get; set; }
        }
    }
}