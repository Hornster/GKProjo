using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    public class RuyoProjectile2Anim : MonoBehaviour {
        /// <summary>
        /// The lightnings are spawned 2 at once, begining from the sides and comming to the middle.
        /// Remember to call Initialize method before using.
        /// </summary>
        ///
        #region Members
        [SerializeField]
        //the data of the projectile
        private RuyoProjectile2Data projData;
        //a queue that stores the lightnings during the animation
        Queue<GameObject> lightnings;
        [SerializeField]
        //the lightning game object used for the duration of the attack
        private GameObject myLightning;
        //the sprite of the area of effect
        Transform AOESprite;
        //Total duration time of the skill
        private float duration;
        //amount of time one lightning lasts.
        float lastingTime;
        //the amount of lightnings that will be creatred
        int lightningsAmount;
        //the distance between the lightnings
        float lightningsOffset;
        //the timer which will help with spawning the lightnings
        float timer;
        #endregion

        #region Functionalities
        // Use this for initialization
        public void Initialize () {
            AOESprite = GetComponent<Transform>();
            lightnings = new Queue<GameObject>();

            lightningsAmount = projData.ClustersAmount;
            duration = projData.SkillDuration;
            lightningsOffset = projData.ClusterOffset;
            lastingTime = projData.ClusterLastingTime;
        }
	
        // Update is called once per frame
        public void Update () {
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

