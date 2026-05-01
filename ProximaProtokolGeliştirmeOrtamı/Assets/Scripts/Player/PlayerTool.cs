using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTool : MonoBehaviour
{
    [SerializeField]Transform firePoint;
    [SerializeField]LayerMask bombLayer;
    [SerializeField]Animator animator;
    [SerializeField]float fireRate;
    [SerializeField]GameObject trail;
    [SerializeField]GameObject trail2;
    [SerializeField]GameObject cylinder;
    [SerializeField]GameObject effect;
    [SerializeField]LayerMask cristalLayer;
    [SerializeField]Image crosshair1;
    [SerializeField]Image crosshair2;
    [SerializeField]Image crosshair3;
    [SerializeField]PlayerRecoil playerRecoil;
    [SerializeField]GameObject hitParticle1;
    [SerializeField]GameObject hitParticle2;
    [SerializeField]Transform particlePoint;
    [SerializeField]LayerMask triggerLayer;
    [SerializeField]AudioSource shootSound;
    GameObject effectClone;
    float fireCooldown;
    bool isLaser;
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if(Input.GetMouseButton(0)&&fireCooldown <= 0)
        {
            shootSound.Play();
            Fire1();
        }
        else if(Input.GetMouseButton(1))
        {
            
            Fire2();
        }
        if(Input.GetMouseButtonUp(1))
        {
            trail2.SetActive(false);
            Destroy(effectClone);
            isLaser = false;
        }
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                crosshair1.color = Color.red;
            }
            else
            {
                crosshair1.color = Color.white;
            }
        }
    }
    void Fire1()
    {
        playerRecoil.Recoil();
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint;
        Instantiate(hitParticle1,particlePoint.position,Quaternion.identity);
        if(Physics.Raycast(ray,out hit,100))
        {
            if(hit.collider.gameObject.layer == bombLayer||hit.collider.gameObject.layer == triggerLayer)
            {
                return;
            }
            targetPoint = hit.point;
            if(hit.collider.CompareTag("Enemy"))
            {
                if(hit.collider.TryGetComponent(out EnemyController enemy))
                {
                    enemy.TakeDamage(10f,hit);
                }
                else if(hit.collider.TryGetComponent(out EnemyController2 enemy2))
                {
                    enemy2.TakeDamage(10f,hit);
                }
                else
                {
                    hit.collider.GetComponentInParent<EnemyController3>().TakeDamage(10,hit);
                }
                crosshair3.enabled = true;
                Invoke("CrosshairHit",0.1f);
            }
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }
        

        StartCoroutine(SpawnTrail(targetPoint));
        

        animator.SetTrigger("fire1");
        fireCooldown = 60/fireRate;
    }
    void Fire2()
    {
        
            trail2.SetActive(true);
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            RaycastHit hit;
            Vector3 targetPoint;
            if(Physics.Raycast(ray,out hit,20))
            {
                if(hit.collider.gameObject.layer == bombLayer)
                {
                    return;
                }
                targetPoint = hit.point;
                print(hit.collider.name);
                if(hit.collider.CompareTag("cristal"))
                {
                    hit.collider.GetComponentInParent<CristalHealth>().TakeDamage(8*Time.deltaTime);
                    print("kristal vuruldu");
                }
            }
            else
            {
                targetPoint = ray.GetPoint(20);
            }

            if(!isLaser)
            {
                effectClone = Instantiate(effect);
                isLaser = true;
            }

            trail2.GetComponent<LineRenderer>().SetPosition(0,firePoint.position);
            trail2.GetComponent<LineRenderer>().SetPosition(1,targetPoint);
            cylinder.transform.Rotate(0,cylinder.transform.rotation.y+3,0);
            effectClone.transform.position = targetPoint;
    }
    IEnumerator SpawnTrail(Vector3 targetPos)
    {
        GameObject trailClone = Instantiate(trail, firePoint.position, Quaternion.identity);
        yield return null;
        while (Vector3.Distance(trailClone.transform.position, targetPos) > 0.1f)
        {
            trailClone.transform.position = Vector3.MoveTowards(trailClone.transform.position, targetPos, 200f * Time.deltaTime);

            yield return null; 
        }
        Destroy(trailClone);
    }
    void CrosshairHit()
    {
        crosshair3.enabled = false;
        crosshair3.color = Color.white;
    }
}
