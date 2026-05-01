using UnityEngine;

public class can : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerTablet>().takeHealth(10f);
            Destroy(gameObject);
        }
    }
}
