
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    [SerializeField]Text text;
    [SerializeField]string[]enemyInfo;
    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        if(Input.GetKeyDown(KeyCode.E)&&Physics.Raycast(ray,out hit,100f))
        {
            if(hit.collider.gameObject.tag== "Enemy")
            {
                print(hit.collider.gameObject.tag);
                int enemyCode = int.Parse(hit.collider.gameObject.name[5].ToString());
                enemyCode--;
                print(enemyCode);
                text.text = enemyInfo[enemyCode];
            }
            else if(hit.collider.transform.parent !=null&&hit.collider.gameObject.transform.parent.gameObject.tag =="Enemy")
            {
                print(hit.collider.gameObject.tag);
                int enemyCode = int.Parse(hit.collider.gameObject.name[5].ToString());
                enemyCode--;
                print(enemyCode);
                text.text = enemyInfo[enemyCode];
            }
            
        }
    }
}
