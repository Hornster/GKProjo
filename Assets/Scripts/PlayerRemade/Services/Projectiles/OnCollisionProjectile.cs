using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public class OnCollisionProjectile : MonoBehaviour {
        IProjectile projectile;

        [SerializeField] private LayerMask ignoredLayersMask;
        //how many damage (or healing) points carries the projectile
        float dmg;
        //tag of the team that's friendly to the projectile:
        //string ownerTeamTag;
        Teams ownerTeam;

        void Start()
        {
            projectile = gameObject.GetComponent<IProjectile>();
        }
        //sets the tags required for recognizing collision type
    
        public void SetParams(Teams ownerTeam)
        {
            this.ownerTeam = ownerTeam;
        }
        //TODO Add the projectile interface to this class for ALL Ruyo and the test projectiles.
        private void OnTriggerEnter2D(Collider2D other)
        {
            var hit = other.gameObject;

            if (hit != null)
            {
                if ((ignoredLayersMask.value & (1 << hit.layer)) != 0) //Is the detected collider assigned to any of the ignored layers?
                {
                    return; //If yes - return, ignoring the collision.
                }
            }

            IHittable hitEntity = hit.GetComponentInParent<Player>();
            if (hitEntity != null)
            {
                if (hitEntity.ChkHit(projectile) && !(projectile.CanPenetrate))
                {
                    Destroy(gameObject);//The projectile hit the player and cannot penetrate them - destroy the projectile.
                    return;
                }
                
                return;//The projectile has done no harm to player or can penetrate through them. Let it phase through
            }
            //if we got here - the projectile did not hit player at all. probably hit a wall.
            if (!projectile.CanPenetrate)
            {
                Destroy(gameObject);
            }
        }
    }
}
/*kolizja przebiega tak:
 -pocisk posiada nazwe przeciwnej druzyny, parametr bool czy ma
 pozytywny wplyw na przyjazna druzyne i wartosc ataku,
 -podczas kolizji pocisk wysyla cialu, z ktorym koliduje, swoje informacje
    -jezeli jest to gracz, na ktory pocisk dziala - pocisk zostaje
    zniszczony (funkcja u gracza zwraca boola czy pocisk zadzialal).
    W przeciwnym wypadku leci dalej
 -sam gracz na podstawie danch z pocisku decyduje, czy otrzymal obrazenia,
 czy nie
     */