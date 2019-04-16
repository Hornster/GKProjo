using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject SolidTilePrefab;

    public GameObject AirTilePrefab;

    public GameObject PlayerPrefab;

    private const float TileUnit = 1.28f;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, -2f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(-0.5f * TileUnit, -2f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(0.5f * TileUnit, -2f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, -2f * TileUnit, 0), Quaternion.identity);
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, -1f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.AirTilePrefab, new Vector3(-0.5f * TileUnit, -1f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.AirTilePrefab, new Vector3(0.5f * TileUnit, -1f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, -1f * TileUnit, 0), Quaternion.identity);
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, 0 * TileUnit, 0), Quaternion.identity);
        Instantiate(this.AirTilePrefab, new Vector3(-0.5f * TileUnit, 0 * TileUnit, 0), Quaternion.identity);
        Instantiate(this.AirTilePrefab, new Vector3(0.5f * TileUnit, 0 * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, 0 * TileUnit, 0), Quaternion.identity);
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, 1f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.AirTilePrefab, new Vector3(-0.5f * TileUnit, 1f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.AirTilePrefab, new Vector3(0.5f * TileUnit, 1f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, 1f * TileUnit, 0), Quaternion.identity);
        //
        Instantiate(this.SolidTilePrefab, new Vector3(-1.5f * TileUnit, 2f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(-0.5f * TileUnit, 2f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(0.5f * TileUnit, 2f * TileUnit, 0), Quaternion.identity);
        Instantiate(this.SolidTilePrefab, new Vector3(1.5f * TileUnit, 2f * TileUnit, 0), Quaternion.identity);
        //
        Instantiate(this.PlayerPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
