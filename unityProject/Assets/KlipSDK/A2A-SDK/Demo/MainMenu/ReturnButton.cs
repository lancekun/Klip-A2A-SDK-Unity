using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    public void ReturnToMainMenuButton_OnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
