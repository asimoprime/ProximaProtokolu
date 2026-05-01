using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class DoFManager : MonoBehaviour
{
    [SerializeField] Volume globalVolume;
    DepthOfField dof;

    void Start()
    {
        // Eğer bu satırda hata alıyorsan, kütüphaneler (using) eksik demektir
        if (globalVolume.profile.TryGet<DepthOfField>(out dof))
        {
            StartCoroutine(NetlesmeEfekti());
        }
    }

    IEnumerator NetlesmeEfekti()
    {
        // 150 Kare Bekle
        // .value eklemeyi sakın unutma!
        dof.gaussianEnd.value = 1f; 
        yield return new WaitForSeconds(150f / 60f);

        // 100 Kare Sabit
        dof.gaussianEnd.value = 5f;
        yield return new WaitForSeconds(100f / 60f);

        // Yavaşça Netleş (250. kareye kadar)
        float sure = 100f / 60f;
        float gecenSure = 0;

        while (gecenSure < sure)
        {
            gecenSure += Time.deltaTime;
            // .value üzerinden eşitleme yapıyoruz
            dof.gaussianEnd.value = Mathf.Lerp(5f, 100f, gecenSure / sure);
            yield return null;
        }
        
        dof.gaussianEnd.value = 100f;
    }
}