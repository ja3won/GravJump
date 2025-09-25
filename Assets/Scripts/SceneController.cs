using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{   
    public void OnStartClick()
    {
        SceneManager.LoadScene("GameScreen");
    }
}
