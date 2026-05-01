using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Subtitle : MonoBehaviour
{
    [SerializeField]float speed = 0.05f;
    [SerializeField]string textString;
    [SerializeField]TMP_Text texttext;
    bool isActived;
    AudioSource yazi;
    AudioSource konusma;
    void Awake()
    {
        yazi = GameObject.Find("yazi").GetComponent<AudioSource>();
        konusma = gameObject.GetComponent<AudioSource>();
    }
    public void TextStart()
    {
        StartCoroutine(WriteText(textString));
    }
    IEnumerator WriteText(string text)
    {
        if(!isActived)
        {
            isActived = true;
            konusma.Play();
            foreach(char a in text.ToCharArray())
            {
                yazi.Play();
                texttext.text += a;
                
                yield return new WaitForSeconds(speed);
            }
            yield return new WaitForSeconds(5f);
            for (int i = 0; i < text.Length; i++)
            {
                texttext.text = texttext.text.Remove(texttext.text.Length - 1);
                yield return new WaitForSeconds(speed);

            }
            
        }
    }
}
