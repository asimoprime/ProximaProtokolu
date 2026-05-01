
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerMovement : MonoBehaviour
{
    public bool canDash;
    public bool canDoubleJump;
    [SerializeField]float maxSpeed = 7f;
    [SerializeField]float acceleration = 25f;
    [SerializeField]float deAcceleration = 15f;
    [SerializeField]float gravity = -10f;
    [SerializeField]float jumpForce = 5f;
    CharacterController controller;
    [SerializeField]float dashForce;
    [SerializeField]float dashCooldown;
    [SerializeField]float dashCooldownMax=2;
    [SerializeField]AudioSource jumpSound;
    [SerializeField]AudioSource dashSound;
    Vector3 move;
    Vector3 velocity;
    float velocity_y;
    float speed;
    Vector3 dashVelocity;
    int jumpCount;
    bool wasGrounded;
    
    void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
        dashCooldown = dashCooldownMax ;
        if(PlayerPrefs.GetInt("canDash")== 1)
        {
            canDash = true;
        }
        if(PlayerPrefs.GetInt("canJump")== 1)
        {
            canDoubleJump = true;
        }
    }
    void Update()
    {
        if(controller.enabled == false)return;
        dashCooldown -= Time.deltaTime;
        move = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        if(move.magnitude < 0.1f)
        {
            speed = Mathf.MoveTowards(speed,0,deAcceleration*Time.deltaTime);
        }
        else
        {
            speed = Mathf.MoveTowards(speed,maxSpeed,acceleration*Time.deltaTime);
        }
        
        if(controller.isGrounded && velocity_y < 0)
        {
            velocity_y = -2f;
        }
        if(Input.GetKeyDown(KeyCode.Space)&&jumpCount > 0)
        {
            //jumpForce = controller.isGrounded ? 6 : 5;
            jumpSound.Play();
            velocity_y = jumpForce;
            jumpCount --;
        }
        if(canDash && Input.GetKeyDown(KeyCode.LeftShift)&&dashCooldown<= 0)
        {
            dashSound.Play();
            dashVelocity =  transform.TransformDirection(move)*dashForce;
            velocity_y = 5;

            dashCooldown = dashCooldownMax;
        }
        dashVelocity = Vector3.Lerp(dashVelocity, Vector3.zero, 5f * Time.deltaTime);


        move = transform.TransformDirection(move);
        
        velocity = move.normalized *speed;
        velocity += dashVelocity;
        velocity_y += gravity * Time.deltaTime;
        velocity.y = velocity_y;

        if(controller.isGrounded&&!wasGrounded)
        {
            jumpCount = canDoubleJump ? 2 : 1;
        }

        wasGrounded = controller.isGrounded;

        Move();
    }
    void Move()
    {
        controller.Move(velocity*Time.deltaTime);
    }
    
}
