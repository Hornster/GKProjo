using System.Collections;
using System.Collections.Generic;

using Assets.Scripts.PlayerRemade.Services;

using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private bool createPlayer;

    private GameObject player;

    public GameObject PlayerPrefab;

    public string Id { get; set; }

    public void SpawnPlayer()
    {
        this.createPlayer = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.player == null && this.createPlayer)
        {
            this.player = Instantiate(this.PlayerPrefab, this.gameObject.transform.position, Quaternion.identity);
            this.player.transform.parent = this.gameObject.transform;
            this.player.GetComponent<Player>().GetCharacter().CharacterDiedCallback = this.PlayerDiedCallback;
        }

        this.createPlayer = false;
    }

    private void PlayerDiedCallback()
    {
        Destroy(this.player);
        this.createPlayer = true;
    }
}
