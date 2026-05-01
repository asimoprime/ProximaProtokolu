using UnityEngine;

public class WaterTexture : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    PlayerTablet pt;
    private Material waterMaterial;

    private void Awake()
    {
        waterMaterial = GetComponent<Renderer>().material;
        pt = GameObject.Find("Player").GetComponent<PlayerTablet>();
    }

    private void Update()
    {
        waterMaterial.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, scrollSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            pt.TakeDamage(100);
        }
    }
}
