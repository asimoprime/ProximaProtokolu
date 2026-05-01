using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class CheckPoint : MonoBehaviour
{
    [SerializeField]int level;
    [SerializeField]GameObject[] subtitles;
    [SerializeField]AudioClip[] sounds;
    [SerializeField]AudioSource background;
    float soundSpeed = 1f;
    Color32 mavi;
    Color32 orange;
    Color32 black;
    Camera mainCam;
    void Start()
    {
        mavi = new Color32(11,21,97,255);
        orange = new Color32(183,86,0,255);
        black = new Color32(43,6,0,0);
        mainCam = Camera.main;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(level == 12)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene2");
            }
            if(level > PlayerPrefs.GetInt("CurrentLevel"))
            {
                PlayerPrefs.SetInt("CurrentLevel",level); 
            }
            if(level < 5)
            {
                
                StartCoroutine(RengiYumusakcaDegistir(orange,2f));
                if(background.clip != sounds[0])
                {
                    StartCoroutine(SoundChange(sounds[0]));
                }

            }
            else if(level >= 5&&level < 9)
            {
                StartCoroutine(RengiYumusakcaDegistir(mavi,2f));
                if(background.clip != sounds[1])
                {
                    StartCoroutine(SoundChange(sounds[1]));
                }
            }
            else if(level >= 9)
            {
               StartCoroutine(RengiYumusakcaDegistir(black,2f));
               if(background.clip != sounds[2])
                {
                    StartCoroutine(SoundChange(sounds[2]));
                }
            }
            subtitles[level-1].SetActive(true);
            subtitles[level-1].GetComponent<Subtitle>().TextStart();
            if(level == 12)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene2");
            }
        }   
    }
    IEnumerator RengiYumusakcaDegistir(Color32 hedefRenk, float gecisSuresi)
    {
        float gecenSure = 0f;
        
        
        Color baslangicFog = RenderSettings.fogColor;
        Color baslangicCam = mainCam.backgroundColor;

        while (gecenSure < gecisSuresi)
        {
            gecenSure += Time.deltaTime;
            float t = gecenSure / gecisSuresi;

            RenderSettings.fogColor = Color.Lerp(baslangicFog, hedefRenk, t);
            mainCam.backgroundColor = Color.Lerp(baslangicCam, hedefRenk, t);

           
            yield return null; 
        }

        
        RenderSettings.fogColor = hedefRenk;
        mainCam.backgroundColor = hedefRenk;
    }
    IEnumerator SoundChange(AudioClip Soundclip)
    {
        while(background.volume > 0)
        {
            background.volume -= Time.deltaTime*soundSpeed;
            yield return null;
        }
        background.clip = Soundclip;
        background.Play();
        while(background.volume < 1)
        {
            background.volume += Time.deltaTime*soundSpeed;
            yield return null;
        }
    }
}
