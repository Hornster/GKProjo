using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject SolidTilePrefab;

    public GameObject AirTilePrefab;

    public GameObject PlayerSpawnerPrefab;

    private const float TileUnit = 0.64f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, -2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(-0.5f * TileUnit, -2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(0.5f * TileUnit, -2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, -2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, -1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.AirTilePrefab, new Vector3(-0.5f * TileUnit, -1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.AirTilePrefab, new Vector3(0.5f * TileUnit, -1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, -1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, 0.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.AirTilePrefab, new Vector3(-0.5f * TileUnit, 0.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.AirTilePrefab, new Vector3(0.5f * TileUnit, 0.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, 0.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, 1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.AirTilePrefab, new Vector3(-0.5f * TileUnit, 1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.AirTilePrefab, new Vector3(0.5f * TileUnit, 1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, 1.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, 2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(-0.5f * TileUnit, 2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(0.5f * TileUnit, 2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, 2.0f * TileUnit, 0), Quaternion.identity).transform.parent = this.gameObject.transform;
        //
        var playerSpawnerGameObject = Instantiate(this.PlayerSpawnerPrefab, new Vector3(0.0f * TileUnit, 0.0f * TileUnit, 0), Quaternion.identity);
        playerSpawnerGameObject.transform.parent = this.gameObject.transform;
        var playerSpawner = playerSpawnerGameObject.GetComponent<PlayerSpawner>();
        playerSpawner.Id = string.Empty;
        playerSpawner.SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
