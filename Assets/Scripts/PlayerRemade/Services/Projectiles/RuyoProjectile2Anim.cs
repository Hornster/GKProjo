using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    public class RuyoProjectile2Anim : MonoBehaviour {
        /// <summary>
        /// The lightnings are spawned 2 at once, beggining from the sides and comming to the middle.
        /// </summary>
        ///
        #region Members
        //the data of the projectile
        ProjectileData projData;
        //a queue that stores the lightnings during the animation
        Queue<GameObject> lightnings;
        //the lightning game object used for the duration of the attack
        public GameObject myLightning;
        //the sprite of the area of effect
        Transform AOESprite;
        //amount of time the skill should last
        float lastingTime;
        //the amount of lightnings that will be creatred
        int lightningsAmount;
        //the duration of the skill
        float duration;
        //the distance between the lightnings
        float lightningsOffset;
        //the timer which will help with spawning the lightnings
        float timer;
        #endregion

        #region Functionalities
        // Use this for initialization
        void Start () {
            projData = GetComponent<RuyoProjectile2Data>();
            AOESprite = GetComponent<Transform>();
            lightnings = new Queue<GameObject>();

            if(!projData.isLoaded)
                projData.Start();

            lightningsAmount = projData.clustersAmount;
            duration = projData.skillDuration;
            lightningsOffset = projData.clusterOffset;
            lastingTime = projData.clusterLastingTime;
        }
        //called in order to set up the lightnings amount. Requires amount of lightnings, offset between two near lightnings and the duration of whole animation
        public void SetLightningsAmount(float offset, float duration)
        {
            //lightningsOffset = offset;
            // lastingTime = duration/(lightningsAmount/2);
        }
	
        // Update is called once per frame
        void Update () {
            if (lightningsAmount > 0)
            {
                if (lightnings.Count == 0)
                    SpawnLightnings();
                if (timer >= lastingTime)
                {
                    if (lightnings.Count > 0)
                    {
                        OnDestroy();
                    }

                    SpawnLightnings();
                }
            }
            else
            {
                if(timer >= lastingTime)
                    OnDestroy();
            }
            timer += Time.deltaTime;
        }
        private void OnDestroy()
        {
            if(lightnings != null)
                while(lightnings.Count > 0)
                {
                    Destroy(lightnings.Dequeue());
                }
        }
        void SpawnLightnings()
        {
            Vector3 position = new Vector3(transform.position.x + (lightningsOffset * (lightningsAmount / 2 - 1)), transform.position.y, transform.position.z);
            lightnings.Enqueue(Instantiate(myLightning, position, Quaternion.identity));
            position = new Vector3(transform.position.x - (lightningsOffset * (lightningsAmount / 2 - 1)), transform.position.y, transform.position.z);
            lightnings.Enqueue(Instantiate(myLightning, position, Quaternion.identity));
            timer = 0f;
            lightningsAmount -= 2;
        }
        #endregion
    }
}

