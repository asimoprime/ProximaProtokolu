using System;
using UnityEngine;
using UnityEngine.UI;
public class DashUpdate : MonoBehaviour
{
    [SerializeField]PlayerMovement pm;
    [SerializeField]PlayerTablet pt;
    [SerializeField]Text buttonText;
    [SerializeField]AudioSource upgradeSound;
    public void DashActivate()
    {
        if(pt.cristalValue >= 50)
        {
            upgradeSound.Play();
            PlayerPrefs.SetInt("canDash",1);
            pm.canDash = true;
            buttonText.text ="Aktifleştirildi";
            pt.cristalValue -= 50;
            this.gameObject.SetActive(false);
        }
        
    }
}
