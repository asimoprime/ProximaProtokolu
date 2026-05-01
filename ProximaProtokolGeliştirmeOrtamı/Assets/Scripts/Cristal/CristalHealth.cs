using UnityEngine;

public class CristalHealth : MonoBehaviour
{
    [SerializeField]PlayerTablet tablet;
    public float hp;
    void Start()
    {
        hp = 24f;
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        tablet.cristalValue+= damage * (10f / 24f);
        if(hp<=0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
