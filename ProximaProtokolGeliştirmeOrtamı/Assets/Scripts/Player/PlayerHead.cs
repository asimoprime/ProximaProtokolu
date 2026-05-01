using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float timeMagnitude = 14f;
    [SerializeField] float sallanma = 0.05f;
    [SerializeField] GameObject player;
    float defaultPosY = 0;
    float timer = 0;
    void Update()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            timer += Time.deltaTime*timeMagnitude;
            float x = Mathf.Sin(timer/2)*sallanma;
            float y = Mathf.Sin(timer)*sallanma;
            player.transform.localPosition = new Vector3(x, defaultPosY + y, 0);
        }
        else
        {
            timer = 0;
            Vector3 targetPos = new Vector3(0, defaultPosY, player.transform.localPosition.z);

            player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, targetPos, Time.deltaTime * 10f);
        }
    }
}
