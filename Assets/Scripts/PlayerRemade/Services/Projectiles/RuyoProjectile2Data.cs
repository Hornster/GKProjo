using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    public class RuyoProjectile2Data : ProjectileData {
        /// <summary>
        /// The Start method has to be called before any meanings of reading the data from this class.
        /// </summary>
        //
    
        override public void Start()
        {
            Transform AOESprite = GetComponent<Transform>();
        
            clustersAmount = 8;
            clusterOffset = GetComponent<SpriteRenderer>().sprite.bounds.size.x / clustersAmount ;
            skillDuration = 0.5f;
            hasItsOwnLifeTimer = true;
            clusterLastingTime = skillDuration / (clustersAmount / 2);
            isLoaded = true;
            //effectType = Data.effects.RUYODMGBOOST;
        }
    }
}
//TODO