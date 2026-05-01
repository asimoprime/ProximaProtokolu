using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]LineRenderer lr;
    [SerializeField]GameObject shield;
    Transform lrPos;
    
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag =="shield")
        {
            shield.SetActive(true);
            lrPos = other.gameObject.transform;
        }
        else
        {
            shield.SetActive(false);
            lrPos = null;
        }
        if(lrPos == null) return;
        lr.SetPosition(0,transform.position);
        lr.SetPosition(1,lrPos.position);
    }
}
