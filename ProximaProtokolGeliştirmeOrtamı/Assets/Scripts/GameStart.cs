using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void startgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene1");
        PlayerPrefs.SetInt("CurrentLevel",0);
        PlayerPrefs.SetInt("canJump",0);
        PlayerPrefs.SetInt("canDash",0);
    }
    public void gameclose()
    {
        Application.Quit();
    }
}
