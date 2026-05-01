using UnityEngine;

public class OutlineCloser : MonoBehaviour
{
    [SerializeField]float range= 30;
    Transform player;
    Outline outline;
    void Awake()
    {
        player = GameObject.Find("PlayerController").transform;
        outline = GetComponent<Outline>();
    }
    void Update()
    {
        if(Vector3.Distance(transform.position,player.position)> range)
        {
            outline.enabled = false;
        }
        else
        {
            outline.enabled = true;
        }
    }
}
