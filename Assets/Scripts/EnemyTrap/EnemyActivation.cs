using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Services;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemyActivation : MonoBehaviour
{
    private EnemyDamageReceiver receiver;
    private EnemyDamageDealer dealer;

    void Start()
    {
        receiver = gameObject.GetComponentInChildren<EnemyDamageReceiver>();
        dealer = gameObject.GetComponentInChildren<EnemyDamageDealer>();
    }

    /// <summary>
    /// If Player walked into the trigger area for the first time it activates
    /// both animation of the enemy and EnemyDamageDealer located in child-object
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CharCollisionBody") && receiver != null && dealer != null) 
        {
            //EnemyDamageReceiver activation (now Trap can take damage from Player)
            receiver.SetActive(true);
            dealer.SetActive(true);
            //Animate the "activation" of an enemy
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("ActivationTrigger");
            }
            //Destroy BoxCollider trigger (it's not necessary anymore)
            BoxCollider2D bc2d = GetComponent<BoxCollider2D>();
            Destroy(bc2d);
            //The same goes for rigidbody
            Rigidbody2D rg2d = GetComponent<Rigidbody2D>();
            Destroy(rg2d);
        }
    }
}
