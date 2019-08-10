
using Assets.Scripts.PlayerRemade.Contracts.Characters.Healthbars;
using Assets.Scripts.PlayerRemade.Contracts;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Characters.Healthbars.Ruyo
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public class RuyoHealthbar : MonoBehaviour, IHealthbar
    {
        [SerializeField]
        private Transform _healthBarFront;
        private Vector3 _healthBarFrontMaxScale;

        public void Start()
        {
            _healthBarFrontMaxScale = _healthBarFront.localScale;
        }

        public void ChangeHealthbarWidth(float maxHealthValue, float currentHealthValue)
        {
            var newBarXScale = currentHealthValue / maxHealthValue;
            _healthBarFront.localScale = new Vector3(_healthBarFrontMaxScale.x* newBarXScale, _healthBarFrontMaxScale.y, _healthBarFrontMaxScale.z);
        }

        public void ResetHealthbarWidth()
        {
            _healthBarFront.localScale = _healthBarFrontMaxScale;
        }

        public void Notify(Vector3 observedObject)
        {
            RotateHealthbar(observedObject);
        }

        private void RotateHealthbar(Vector3 playerScale)
        {
            if (IsPlayerAndBarFacingSameDirection(playerScale.x))
            {
                return;
            }

            var currentBarScale = _healthBarFront.localScale;
            _healthBarFront.localScale = new Vector3(-currentBarScale.x, currentBarScale.y, currentBarScale.z);
        }

        private bool IsPlayerAndBarFacingSameDirection(float playerScaleX)
        {
            return (_healthBarFront.localScale.x < 0.0f && playerScaleX < 0.0f) ||
                   (_healthBarFront.localScale.x >= 0.0f && playerScaleX >= 0.0f);
        }
    }
}
