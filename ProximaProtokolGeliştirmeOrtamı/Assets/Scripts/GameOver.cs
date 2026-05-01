using UnityEngine;
using UnityEngine.Video;
public class GameOver : MonoBehaviour
{
    [SerializeField]VideoPlayer vPlayer;
    void Start()
    {
        vPlayer.loopPointReached += videofinish;
    }
    void videofinish(VideoPlayer vp)
    {
        Application.Quit();
    }
}
