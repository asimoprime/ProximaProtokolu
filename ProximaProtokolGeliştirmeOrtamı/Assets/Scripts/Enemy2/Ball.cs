using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]GameObject explosionPrefab;
    AudioSource explosionSound;
    void Start()
    {
        explosionSound = GameObject.Find("explosion").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        explosionSound.Play();
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
