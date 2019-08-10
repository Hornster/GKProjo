

using UnityEngine;
/// <summary>
/// Made by: Kozuch Karol
/// </summary>
namespace Assets.Scripts.PlayerRemade.Contracts.Characters.Healthbars
{
    public interface IHealthbar : IObserver<Vector3>
    {
        void ChangeHealthbarWidth(float maxHealthValue, float currentHealthValue);

        void ResetHealthbarWidth();
    }
}
