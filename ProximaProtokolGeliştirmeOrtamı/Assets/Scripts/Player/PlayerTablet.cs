using UnityEngine;
using UnityEngine.UI;
public class PlayerTablet : MonoBehaviour
{
    [SerializeField]Slider hp;
    [SerializeField]Slider oksijen;
    [SerializeField]Slider cristal;
    [SerializeField]AudioSource hit;
    [SerializeField]Transform player;
    [SerializeField]Transform[] check;
    [SerializeField]int level;
    [SerializeField]Animator bitis;
    public float hpValue;
    public float cristalValue;
    void Start()
    {
        hpValue = 100;
        cristalValue =0;
        oksijen.value = 600;
        hp.value = hpValue;
    }
    void Update()
    {   
        level = PlayerPrefs.GetInt("CurrentLevel")-1;
        cristal.value = cristalValue;
        oksijen.value-= Time.deltaTime;
        hp.value = hpValue;
    }
    public void TakeDamage(float damage)
    {
        hit.Play();
        hpValue -= damage;
        hp.value = hpValue;
        if (hpValue <= 0)
        {
            hpValue = 100;
            player.GetComponent<CharacterController>().enabled = false;
            bitis.SetTrigger("bitti");
            
            player.position = check[PlayerPrefs.GetInt("CurrentLevel")-1].position;
            player.GetComponent<CharacterController>().enabled =  true;
        }
    }
    public void takeHealth(float health)
    {
        hpValue += health;
        hp.value = hpValue;
        if (hpValue >= 100)
        {
            hpValue = 100;
            hp.value = hpValue;
        }
    }
}
