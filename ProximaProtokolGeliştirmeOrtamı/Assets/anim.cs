using UnityEngine;
using System.Collections;

public class RocketLaunchCamera : MonoBehaviour
{
    [Header("Roket (Hedef) Ayarları")]
    public Transform rocketTransform;   // Takip edilecek roket
    public Transform pivotPoint;        // Roketin etrafında döneceği merkez nokta
    public float riseSpeed = 10f;       // Yükselme hızı
    public float rotateSpeed = 40f;     // Pivot etrafında dönme hızı
    public float launchDelay = 10f;     // Sahne başladıktan kaç sn sonra kalkış başlasın?

    [Header("Görsel Ayarlar")]
    public Camera mainCamera;           
    public float colorDelay = 5f;       
    public float colorDuration = 10f;   

    [Header("Kamera Ayrılma Ayarı")]
    public float detachDelay = 5f;      // Kalkıştan kaç sn sonra parent'tan ayrılsın?

    private bool isLaunched = false;
    private bool isDetached = false;
    private Vector3 positionOffset;     // Ayrıldıktan sonraki mesafeyi korumak için

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        // Başlangıçta kamerayı roketin çocuğu yap (eğer değilse)
        if (mainCamera != null && rocketTransform != null)
        {
            mainCamera.transform.SetParent(rocketTransform);
        }

        if (mainCamera != null) mainCamera.clearFlags = CameraClearFlags.SolidColor;
        RenderSettings.fog = true;

        StartCoroutine(TransitionVisuals());
        StartCoroutine(LaunchSequence());
    }

    void Update()
    {
        if (isLaunched && rocketTransform != null && pivotPoint != null)
        {
            // 1. ROKET YÜKSELME
            rocketTransform.Translate(Vector3.up * riseSpeed * Time.deltaTime, Space.World);

            // 2. ROKET PİVOT ETRAFINDA DÖNME
            rocketTransform.RotateAround(pivotPoint.position, Vector3.up, rotateSpeed * Time.deltaTime);
            
            // Pivotun da roketle beraber yükselmesi (Spiral hareket için)
            pivotPoint.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null || rocketTransform == null) return;

        // EĞER AYRILDIYSA (DETACH)
        if (isDetached)
        {
            // 1. Pozisyonu Y ekseninde roketle aynı hızda yükselt
            // (X ve Z pozisyonu ayrıldığı andaki yerinde sabit kalır)
            Vector3 currentPos = mainCamera.transform.position;
            currentPos.y += riseSpeed * Time.deltaTime;
            mainCamera.transform.position = currentPos;

            // 2. Rotasyonu rokete bakacak şekilde güncelle
            mainCamera.transform.LookAt(rocketTransform);
        }
        // Not: İlk 5 saniye child olduğu için pozisyon/rotasyonu Unity otomatik halleder.
    }

    IEnumerator TransitionVisuals()
    {
        yield return new WaitForSeconds(colorDelay);
        Color startFogColor = RenderSettings.fogColor;
        Color startCamColor = mainCamera.backgroundColor;
        Color targetColor = Color.black;
        float elapsed = 0f;

        while (elapsed < colorDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / colorDuration;
            RenderSettings.fogColor = Color.Lerp(startFogColor, targetColor, t);
            mainCamera.backgroundColor = Color.Lerp(startCamColor, targetColor, t);
            yield return null;
        }
        
        RenderSettings.fogColor = targetColor;
        mainCamera.backgroundColor = targetColor;
    }

    IEnumerator LaunchSequence()
    {
        // Kalkış bekleme
        yield return new WaitForSeconds(launchDelay);
        isLaunched = true;

        // Kalkıştan 5 saniye sonra AYIR
        yield return new WaitForSeconds(detachDelay);
        
        if (mainCamera != null)
        {
            // Bağlantıyı kopar
            mainCamera.transform.SetParent(null);
            isDetached = true;
            Debug.Log("Kamera ayrıldı: Sadece Y ekseninde yükseliyor ve rokete bakıyor.");
        }
    }
}