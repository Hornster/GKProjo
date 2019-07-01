using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Contracts;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour, IHittable
{
    private EnemyHPController hpController;
    private bool isActive { get; set; } //is set to True when parent's component EnemyActivation was triggered by Player
    
    public void SetActive(bool value)
    {
        isActive = value;
    }

    public bool ChkHit(IProjectile projectile)
    {
        if (projectile!=null && projectile.Alignment == Teams.Player)
        {
            this.hpController.ApplyDamage(projectile.Damage);
            return true;
        }

        return false;
    }

    void Awake()
    {
        hpController = GetComponent<EnemyHPController>(); //Pobranie kontrolera hp
    }

    void Start()
    {
        isActive = false;
    }
    
    /// <summary>
    /// Nakłada efekty po ptrzymaniu trafienia 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        IProjectile projectile = other.gameObject.GetComponent<IProjectile>(); //Jeżeli collider to pocisk to pobierz referncje

        //If Enemy has been activated beforehand
        if (hpController != null && isActive)
        {
            if (projectile != null && projectile.Alignment == Teams.Player && ChkHit(projectile))
            {
                //Apply damage from Player's projectile to the enemy
                //hpController.ApplyDamage(projectile.Damage);
                Destroy(other.gameObject);

                //If enemy is not alive anymore then clean unnecessary components
                if (!hpController.IsAlive())
                {
                    CleanAfterDeath();
                }
            }
        }
        //else if Enemy was not activated just Destroy incoming projectiles
        else if (projectile!=null && projectile.Alignment == Teams.Player) Destroy(other.gameObject);
    }

    /// <summary>
    /// Dispose of unnecessary components
    /// </summary>
    private void CleanAfterDeath()
    {
        //Play enemy death animation
        Animator animator = gameObject.GetComponentInParent<Animator>();
        if (animator != null)
            animator.SetBool("isDead", true);

        //Set new size of BoxCollider2D (obstacle)
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        if (bc != null)
        {
            bc.size = new Vector2(bc.size.x, 0.3f);
            bc.offset = new Vector2(bc.offset.x, -0.5f);
        }
        //Dispose of parent's BoxCollider2d (trigger)
        BoxCollider2D parentBC = transform.parent.gameObject.GetComponent<BoxCollider2D>();
        if (parentBC != null)
            Destroy(parentBC);

        //Dispose of parent's EnemyActivation script 
        EnemyActivation ea = transform.parent.gameObject.GetComponent<EnemyActivation>();
        if (ea != null)
            Destroy(ea);

        //Dispose of DamageDealer sibling object
        GameObject damageDealer = transform.parent.gameObject.transform.GetChild(0).gameObject;
        if (damageDealer != null)
            Destroy(damageDealer);

        //Dispose of EnemyHPController script component
        EnemyHPController hpController = gameObject.GetComponent<EnemyHPController>();
        if (hpController != null)
            Destroy(hpController);

        //Dispose of EnemyDamageReceiver script component
        Destroy(this);
    }
}
