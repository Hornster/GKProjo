using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    public class DestroyByTime : MonoBehaviour
    {
        float lifetime;
        float lifeCount;
        //tells whether the lifetime of the bullet's been set
        bool isReady = false;
        // Use this for initialization
        public void Start () {
            lifeCount = 0;
        }
	
        public void SetLifeTime(float lifetime)
        {
            this.lifetime = lifetime;
            isReady = true;
        }
        // Update is called once per frame
        void Update () {
            if (isReady)
            {
                lifeCount += Time.deltaTime;
                if (lifeCount > lifetime)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
