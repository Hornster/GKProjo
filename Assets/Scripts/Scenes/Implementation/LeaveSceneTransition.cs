using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Scenes.Contracts;
using UnityEngine;

namespace Assets.Scripts.Scenes.Implementation
{
    public class LeaveSceneTransition : MonoBehaviour, ISceneTransition
    {
        [SerializeField]
        private float _endTransitionWaitTime;
        [SerializeField]
        private Animator _transitionAnimator;
        [SerializeField]
        private string _startSceneName;

        public void OnTrigger()
        {
            StartCoroutine(LeaveGame());  //return value does not have to be used
        }

        /// <summary>
        /// Called as callback when the user presses the Leave button in Main Menu
        /// </summary>
        private IEnumerator LeaveGame()
        {
            _transitionAnimator.SetTrigger("end");
            yield return new WaitForSeconds(_endTransitionWaitTime);
            Application.Quit();
        }
    }
}
