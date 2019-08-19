using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [SerializeField]
    private string TargetScene;

    [SerializeField]
    private string PlayerTag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == this.PlayerTag)
        {
            SceneManager.LoadScene(this.TargetScene);
        }
    }
}
