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

    private IEnumerator ChangeSceneToPlayLevel()
    {
        _transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(_endTransitionWaitTime);
        SceneManager.LoadScene(_startSceneName);
    }
    /// <summary>
    /// Called as callback when the user presses the exit button
    /// </summary>
    public void MenuLeaveButtonPressed()
    {
        StartCoroutine(LeaveGame());  //return value does not have to be used
    }
    /// <summary>
    /// Called as callback when the user presses the Start button in Main Menu
    /// </summary>
    public void MenuStartButtonPressed()
    {
        StartCoroutine(ChangeSceneToPlayLevel());  //return value does not have to be used
        
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
//https://www.youtube.com/watch?v=Qd2em_ts5vs