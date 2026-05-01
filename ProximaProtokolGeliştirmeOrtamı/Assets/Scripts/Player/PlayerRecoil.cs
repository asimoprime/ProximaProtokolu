using UnityEngine;

public class PlayerRecoil : MonoBehaviour
{
    [SerializeField] float rotationRecoil = 2f; 
    [SerializeField] float positionRecoil = 0.1f; 
    [SerializeField] float snappiness = 6f;
    [SerializeField] float returnSpeed = 2f;
    [SerializeField]GameObject cross;
    [SerializeField] float expansionAmount = 10f;

    Vector3 currentRotation;
    Vector3 targetRotation;

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
       
            
        float recoilVal = Mathf.Abs(currentRotation.x);    
        float newScale = 0.7f + (recoilVal / expansionAmount);

         cross.transform.localScale = new Vector3(newScale, newScale, newScale);
        
        
    }

    public void Recoil()
    {
        float randomX = Random.Range(-0.5f, 0.5f);
        
        targetRotation += new Vector3(-rotationRecoil,randomX,0);
    }
}
