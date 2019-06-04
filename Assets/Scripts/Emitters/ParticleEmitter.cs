using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Emitters
{
    public class ParticleEmitter : MonoBehaviour, IParticleEmitter
    {
        /// <summary>
        /// Contains the particles that will be used by this emitter.
        /// </summary>
        private List<ParticleController> particles = new List<ParticleController>();
        /// <summary>
        /// Starting position of emitted particles.
        /// </summary>
        private Transform _parentTransform;
        /// <summary>
        /// Velocity of single particle.
        /// </summary>
        private Vector2 _particleVelocity;
        /// <summary>
        /// Defines the time between two given particles being launched.
        /// </summary>
        private float _launchInterval;
        /// <summary>
        /// Time since last particle being launched.
        /// </summary>
        private float _lastLaunchTime;
        /// <summary>
        /// Index of last activated particle.
        /// </summary>
        private int _lastActivatedParticleIndex;

        [SerializeField]
        private GameObject _particlePrefab;
        [SerializeField]
        private float _maxLifeTime;
        [SerializeField]
        private int _particlesAmount;
        /// <summary>
        /// By this factor the scale of each particle will be multiplied.
        /// </summary>
        [SerializeField]
        private Vector2 _particlesScale;


        public void Start()
        {
            _launchInterval = _maxLifeTime/_particlesAmount;
            for (int i = 0; i < _particlesAmount; i++)
            {
                var newParticle = Instantiate(_particlePrefab);
                newParticle.transform.localScale = newParticle.transform.localScale*_particlesScale;
                var particleController = newParticle.GetComponentInChildren<ParticleController>();
                
                particleController._maxLifeTime = _maxLifeTime;
                particleController._velocity = _particleVelocity;

                particles.Add(particleController);
            }
            transform.DetachChildren();     //The particles shall not be bound by the parent.
        }

        public void Update()
        {
            if (_parentTransform == null)
            {
                return;                                 //Currently there's no source object connected with this emitter - do not emit anything.
            }
            float lastFrameTime = Time.deltaTime;

            _lastLaunchTime += lastFrameTime;
            if (_lastLaunchTime >= _launchInterval)     //If it is time to launch another particle...
            {
                _lastActivatedParticleIndex++;          //...then increment the index.
                if (_lastActivatedParticleIndex >= _particlesAmount)
                {
                    _lastActivatedParticleIndex = 0;    //If we have reached the end of the particles list - go back to the beginning.
                }

                particles[_lastActivatedParticleIndex].Launch();    //Launch another particle.
            }
        }

        public void AssignParent(Transform parent)
        {
            _parentTransform = parent;
            for (int i = 0; i < _particlesAmount; i++)
            {
                particles[i]._parentPosition = parent;
            }
        }
    }
}
