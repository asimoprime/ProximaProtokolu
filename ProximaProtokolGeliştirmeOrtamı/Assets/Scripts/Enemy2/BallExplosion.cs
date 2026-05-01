using UnityEngine;

public class BallExplosion : MonoBehaviour
{
    bool damaged = false;
    void Start()
    {
        Destroy(this.gameObject, 1.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player"&&!damaged)
        {
            other.gameObject.GetComponentInParent<PlayerTablet>().TakeDamage(10);
            damaged = true;
        }
    }
}
