using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages the scene switching and transitions.
/// Karol Kozuch
/// </summary>
public class SceneTransitions : MonoBehaviour
{
    [SerializeField]
    private float _endTransitionWaitTime;
    [SerializeField]
    private Animator _transitionAnimator;
    [SerializeField]
    private string _startSceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayStartFade()
    {

    }

    private IEnumerator PlayEndFade()
    {
        _transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(_endTransitionWaitTime);
    }
    /// <summary>
    /// Called as callback when the user presses the Start button in Main Menu
    /// </summary>
    public void MenuStartButtonPressed()
    {
        PlayEndFade();
        SceneManager.LoadScene(_startSceneName);
        PlayStartFade();
    }
    /// <summary>
    /// Called as callback when the user presses the Leave button in Main Menu
    /// </summary>
    public void MenuLeaveButtonPressed()
    {
        PlayEndFade();
    }
}
//https://www.youtube.com/watch?v=Qd2em_ts5vs