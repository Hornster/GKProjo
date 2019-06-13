using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMapTransitions : MonoBehaviour
{
    [SerializeField]
    private Animator _transitionAnimator;
    [SerializeField]
    private string _returnKeyName;
    [SerializeField]
    private string _menuSceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(_returnKeyName))
        {
            SceneManager.LoadScene(_menuSceneName);
        }
    }
}
