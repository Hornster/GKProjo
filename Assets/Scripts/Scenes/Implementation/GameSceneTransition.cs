using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Scenes.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.Implementation
{
    public class GameSceneTransition : MonoBehaviour, ISceneTransition
    {
        [SerializeField]
        private float _endTransitionWaitTime;
        [SerializeField]
        private Animator _transitionAnimator;
        [SerializeField]
        private string _startSceneName;

        private IEnumerator ChangeSceneToPlayLevel()
        {
            _transitionAnimator.SetTrigger("end");
            yield return new WaitForSeconds(_endTransitionWaitTime);
            SceneManager.LoadScene(_startSceneName);
        }

        public void OnTrigger()
        {
            StartCoroutine(ChangeSceneToPlayLevel());  //return value does not have to be used
        }
    }
}
