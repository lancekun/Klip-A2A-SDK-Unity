using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnClickSceneButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void OnClickOpenUrl(string url)
    {
        Application.OpenURL(url);
    }
}
