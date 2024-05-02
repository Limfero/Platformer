using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
