using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.UI;
public class EnemyController2 : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float health = 20;
    [SerializeField]float range;
    [SerializeField]float walkRange;
    [SerializeField]float attackRange;
    [SerializeField]float attackCooldown = 2.0f;
    [SerializeField]float attackForce = 10.0f;
    [SerializeField]float recoveryTime = 0.5f;
    [SerializeField]float maxWalkRange;
    [SerializeField]Transform player;
    [SerializeField]Animator animator;
    [SerializeField]float attackForceY = 5f;
    [SerializeField]GameObject bullet;
    [SerializeField]Transform bulletSpawn;
    [SerializeField]GameObject matObject;
    [SerializeField]Image crosshair;
    [SerializeField] Collider col;
    [SerializeField] GameObject hitParticle;
    [SerializeField]Animator shieldAnim;
    [SerializeField]GameObject dieEffect;
    AudioSource hitSound;
    AudioSource dieSound;
    Material material;
    NavMeshAgent nav;
    float distance; 
    bool isAttacking = false;
    bool isWalking = true;
    bool dead;
    void Start()
    {
        dieSound = GameObject.Find("die").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        material = matObject.GetComponent<Renderer>().material;
        attackCooldown = Random.Range(3f,5f);
        player = GameObject.Find("PlayerController").transform;
        crosshair = GameObject.Find("cross3").GetComponent<Image>();
        hitSound = GameObject.Find("hit").GetComponent<AudioSource>();
    }
    void Update()
    {
        if(dead)
        {
            return;
        }
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(player.position.x,transform.position.y,player.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5);
        attackCooldown -= Time.deltaTime;
        distance = Vector3.Distance(transform.position, player.position);
        //print(player.position);
        if(distance <= range&&distance > walkRange)
        {
            animator.speed = 1;
            shieldAnim.speed=1;
            nav.SetDestination(player.position);
        }
        else
        {
            animator.speed = 0;
            shieldAnim.speed = 0;
            animator.StopPlayback();
        }
        if(distance <= attackRange&&attackCooldown <= 0&&!isAttacking)
        {
            Attack();
        } 
    }
    void Attack()
    {
        
        attackCooldown = Random.Range(3f,5f);
        isAttacking = true;
        float posY = 0-bulletSpawn.position.y;
        GameObject bulletClone = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        Rigidbody bulletRb = bulletClone.GetComponent<Rigidbody>();
        Vector3 velocityY = Vector3.up*Mathf.Sqrt(-2*attackForceY*Physics.gravity.y);
        Vector3 XZ = new Vector3(player.position.x-bulletSpawn.position.x,0,player.position.z-bulletSpawn.position.z);
        
        float timeUp = Mathf.Sqrt(-2*attackForceY/Physics.gravity.y);
        float timeDown = Mathf.Sqrt(2*(posY-attackForceY)/Physics.gravity.y);
        Vector3 velocityXZ = XZ/(timeUp+timeDown);
        bulletRb.AddForce(new Vector3(velocityXZ.x,velocityY.y,velocityXZ.z), ForceMode.Impulse);

        
        Invoke("Recover", recoveryTime);
    }
    void Recover()
    {
        isAttacking = false;        
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")&&isAttacking)
        {
            collision.gameObject.GetComponentInParent<PlayerTablet>().TakeDamage(10f);
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
            shieldAnim.speed = 0;
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
