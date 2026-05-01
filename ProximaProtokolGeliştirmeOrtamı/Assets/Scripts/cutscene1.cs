using UnityEngine;
using UnityEngine.Video;

public class cutscene1 : MonoBehaviour
{
    VideoPlayer vdPlayer;
    void Start()
    {
        vdPlayer = GetComponent<VideoPlayer>();
    }
    void Update()
    {
        if(vdPlayer.isPaused)

        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }    
    }
}
