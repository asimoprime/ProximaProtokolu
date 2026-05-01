using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]float sensX;
    [SerializeField]float sensY;
    [SerializeField]Transform player;
    [SerializeField]GameObject cam;

    float mouseX;
    float mouseY;
    float xRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 120;
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-45,45);

        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);

        player.Rotate(Vector3.up*mouseX);
    }
}
