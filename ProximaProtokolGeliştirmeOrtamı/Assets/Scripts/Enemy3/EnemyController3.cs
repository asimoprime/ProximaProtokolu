using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyController3 : MonoBehaviour
{
    [SerializeField]NavMeshAgent nav;
    [SerializeField]float range;
    [SerializeField]float hp = 100;
    [SerializeField]GameObject particle;
    [SerializeField]Material mat;
    [SerializeField]GameObject dieEffect;
    [SerializeField]AudioSource hitSound;
    AudioSource dieSound;
    Transform player;
    Image crosshair;
    void Start()
    {
        dieSound = GameObject.Find("die").GetComponent<AudioSource>();
        player = GameObject.Find("PlayerController").transform;
        mat = this.gameObject.GetComponentInChildren<Renderer>().material;
        crosshair = GameObject.Find("cross3").GetComponent<Image>();
        hitSound = GameObject.Find("hit").GetComponent<AudioSource>();
    }
    void Update()
    {
        if(Vector3.Distance(transform.position,player.position)< range)
        {
            nav.SetDestination(player.position);
        }
        
    }
    public void TakeDamage(float damage,RaycastHit hit)
    {
        hitSound.Play();
        Instantiate(particle,hit.point,Quaternion.LookRotation(hit.point,transform.position));
        hp-= damage;
        if(hp<= 0)
        {
            dieSound.Play();
            Instantiate(dieEffect,transform.position,Quaternion.identity);
            crosshair.color = Color.red;
            GetComponentInChildren<BoxCollider>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(die());
            
        }
    }
    IEnumerator die()
    {
        float t=0;
        while (t<2)
        {
            t+= Time.deltaTime;
            mat.SetFloat("_Float", t/2); 
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
