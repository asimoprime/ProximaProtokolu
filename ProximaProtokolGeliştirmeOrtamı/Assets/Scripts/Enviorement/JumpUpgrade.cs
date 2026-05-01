using UnityEngine;
using UnityEngine.UI;
public class JumpUpgrade : MonoBehaviour
{
    [SerializeField]PlayerMovement pm;
    [SerializeField]PlayerTablet pt;
    [SerializeField]Text buttonText;
    [SerializeField]AudioSource upgradeSound;
    public void JumpActivate()
    {
        if(pt.cristalValue >= 70)
        {
            upgradeSound.Play();
            PlayerPrefs.SetInt("canJump",1);
            pm.canDoubleJump = true;
            buttonText.text ="Aktifleştirildi";
            pt.cristalValue -= 70;
            this.gameObject.SetActive(false);
        }
        
    }
}
