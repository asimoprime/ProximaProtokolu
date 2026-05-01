using UnityEngine;

public class Triger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetComponentInParent<EnemyController>().Hit(other);
        }
    }
}
