using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    public abstract class ProjectileData : MonoBehaviour {

        public bool isLoaded = false;
        public bool hasItsOwnLifeTimer;
        public int clustersAmount;
        public float skillDuration;
        public float clusterOffset;
        public float clusterLastingTime;

        abstract public void Start();
    }
}
//TODo