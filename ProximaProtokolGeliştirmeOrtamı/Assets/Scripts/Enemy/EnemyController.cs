using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    
    [SerializeField] float speed = 5.0f;
    [SerializeField]GameObject dieEffect;
    [SerializeField] float health = 20;
    [SerializeField]float range;
    [SerializeField]float attackRange;
    [SerializeField]float attackCooldown = 2.0f;
    [SerializeField]float attackForce = 10.0f;
    [SerializeField]float recoveryTime = 0.5f;
    [SerializeField]Transform player;
    [SerializeField]Animator animator;
    [SerializeField]GameObject matObject;
    [SerializeField]Image crosshair;
    [SerializeField] Collider col;
    [SerializeField] GameObject hitParticle;
    [SerializeField]Animator animator2;
    AudioSource hitSound;
    AudioSource dieSound;
    Material material;
    NavMeshAgent nav;
    float distance;
    bool isAttacking = false;
    Rigidbody rb;
    Vector3 attackDirection;
    bool dead;
    void Start()
    {
        dieSound = GameObject.Find("die").GetComponent<AudioSource>();
        hitSound = GameObject.Find("hit").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        material = matObject.GetComponentInChildren<Renderer>().material;
        player = GameObject.Find("PlayerController").transform;
        crosshair = GameObject.Find("cross3").GetComponent<Image>();
    }
    void Update()
    {
        if(dead)
        {
            return;
        }
        attackCooldown -= Time.deltaTime;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(player.position.x,transform.position.y,player.position.z) - transform.position);
        transform.rotation = lookRotation;
        distance = Vector3.Distance(transform.position, player.position);
        
        if(distance <= range&&!isAttacking)
        {
            animator.speed = 2.5f;
            animator2.speed = 2.5f;
            nav.SetDestination(player.position);
        }
        else
        {
            animator.speed = 0;
            animator2.speed = 0;
        }
        if(distance <= attackRange&&attackCooldown <= 0&&!isAttacking)
        {
            rb.isKinematic = false;
            nav.enabled = false;
            animator.speed = 0;
            animator2.speed =0;
            attackCooldown = 2.0f;
            isAttacking = true;
            attackDirection = (player.position - transform.position).normalized;
            rb.AddForce(attackDirection * attackForce+new Vector3(0,2,0), ForceMode.Impulse);
            
            Invoke("Recover", recoveryTime);
        } 
    }
    void Recover()
    {
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        nav.enabled = true;
        isAttacking = false;
       
    }
    public void Hit(Collider other)
    {
        if(dead)
        {
            return;
        }
        if(other.CompareTag("Player")&&isAttacking)
        {
            other.gameObject.GetComponentInParent<PlayerTablet>().TakeDamage(10f);
        }
    }
    public void TakeDamage(float damage,RaycastHit hit)
    {
        hitSound.Play();
        Instantiate(hitParticle,hit.point,Quaternion.LookRotation(hit.point-transform.position));
        if(dead)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            dieSound.Play();
            Instantiate(dieEffect,transform.position,Quaternion.identity);
            health = 0;
            crosshair.color = Color.red;
            col.enabled = false;
            StartCoroutine(die());
            dead = true;
            nav.enabled = false;
            animator.speed = 0;
            animator2.speed = 0;
        }
    }
    IEnumerator die()
    {
        float t = 0;
        while(t < 2)
        {
            t += Time.deltaTime;
            material.SetFloat("_Float", t/2);
            yield return null;
        }
        Destroy(gameObject);
    }
}
