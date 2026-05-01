using System;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]GameObject upgrade;
    bool canOpen;
    bool isActive;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            canOpen = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            canOpen = false;
            upgrade.SetActive(false);
        }
    }
    void Update()
    {
        if(canOpen && Input.GetKeyDown(KeyCode.E))
        {
            upgrade.SetActive(!isActive);
            isActive = !isActive;
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
