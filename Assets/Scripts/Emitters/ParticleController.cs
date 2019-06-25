using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Emitters
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public class ParticleController : MonoBehaviour
    {
        private Color _disabledColor = Color.clear;
        private Color _enabledColor = Color.white;
        [SerializeField]
        private SpriteRenderer _particleRenderer;
        [HideInInspector]
        public Transform _parentPosition;
        [HideInInspector]
        public Vector3 _velocity;
        [HideInInspector]
        public float _currentLifeTime;
        [HideInInspector]
        public float _maxLifeTime = 0;

        public bool isAlive { get; private set; }

        public void Start()
        {
            isAlive = false;
            _particleRenderer.color = _disabledColor;
            //gameObject.transform.position = _parentPosition.position;
        }
        public void Update()
        {
            if (!isAlive)
            {
                return;
            }

            float lastFrameTime = Time.deltaTime;
            
            _currentLifeTime += lastFrameTime;
            if (_currentLifeTime > _maxLifeTime)            //This particle served its purpose. Disable it.
            {
                isAlive = false;
                _particleRenderer.color = _disabledColor;
            }
            
            gameObject.transform.Translate(_velocity);
        }
        /// <summary>
        /// Activates the particle.
        /// </summary>
        public void Launch()
        {
            isAlive = true;
            _currentLifeTime = 0;
            _particleRenderer.color = _enabledColor;
            gameObject.transform.position = _parentPosition.position;
        }
    }
}