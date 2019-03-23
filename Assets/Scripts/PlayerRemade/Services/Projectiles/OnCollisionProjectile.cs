using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    public class OnCollisionProjectile : MonoBehaviour {
        IProjectile projectile;
        //stores the data about the projectile
        public ProjectileData projData;
        
        //how many damage (or healing) points carries the projectile
        float dmg;
        //tag of the team that's friendly to the projectile:
        //string ownerTeamTag;
        Teams ownerTeam;
        
        
        //sets the tags required for recognizing collision type
    
        public void SetParams(Teams ownerTeam)
        {
            this.ownerTeam = ownerTeam;
        }
        private void OnTriggerEnter(Collider other)
        {
            var hit = other.gameObject;
            IHittable hitEntity = hit.GetComponent<Player>();
            if (hitEntity != null)
            {
                hitEntity.ChkHit(projectile);
            }
            if(!projectile.CanPenetrate)
                Destroy(gameObject);
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