using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public class ProjectileData : MonoBehaviour {

        public bool isLoaded = false;
        public bool hasItsOwnLifeTimer;
        public int clustersAmount;
        public float skillDuration;
        public float clusterOffset;
        public float clusterLastingTime;

        public virtual void Start()
        {

        }
    }
}
//TODo