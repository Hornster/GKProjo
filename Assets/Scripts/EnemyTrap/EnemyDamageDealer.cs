using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Characters;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services;
using Assets.Scripts.PlayerRemade.Services.Projectiles;
using UnityEngine;


/// <summary>
/// Klasa wykrywajaca kolizje gracza z przeciwnikiem, powinna zabierac hp graczowi 
/// </summary>
public class EnemyDamageDealer : MonoBehaviour
{
    private EnemyWallBasicAttackData enemyattack;
    private IHittable PlayerInstance;

    public float interval = 1.0f;
    private float timePassed;
    private bool isPlayerInRange;
    private bool isActive { get; set; }

    public void SetActive(bool value)
    { isActive = value; }

    void Awake()
    {
        enemyattack = GetComponent<EnemyWallBasicAttackData>();
    }

    void Start()
    {
        isPlayerInRange = false;
        timePassed = 0.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CharCollisionBody")
        {
            var hit = other.gameObject;
            PlayerInstance = hit.GetComponentInParent<Player>();
            if(PlayerInstance != null)
                PlayerInstance.ChkHit(enemyattack);
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "CharCollisionBody")
        {
            timePassed = 0.0f;
            isPlayerInRange = false;
        }
    }
    //zadawanie dmg over time musi być w Update - w OnTriggerStay nie działa, jeśli gracz stoi nieruchomo
    void Update()
    {
        if (isPlayerInRange && PlayerInstance != null)
        {
            timePassed += Time.deltaTime;
            if (timePassed > interval)
            {
                PlayerInstance.ChkHit(enemyattack);
                timePassed = 0.0f;
            }
        }
    }
}
