using UnityEngine;

public class fpsSettings : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value);
    }
}
