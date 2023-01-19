using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float speedX = -1f;
    [SerializeField]private float jump = 500f;
    [SerializeField]private Animator animator;
    [SerializeField]private Transform playerModelTransform;
    
    
    private FixedJoystick _fixedJoystick;

    private float horizontal = 0f; 
    private bool isFacingRight = true;

    private bool isGround = false;
    private bool isJump = false;
    private bool isFinish = false;
    private bool isLeverArm;
    
    private Rigidbody2D rb;
    private Finish finish;
    private LeverArm leverArm;
    [SerializeField] private AudioSource jumpSound;

    const float SpeedMultiplier = 50f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _fixedJoystick = GameObject.FindGameObjectWithTag("Fixed Joystick").GetComponent<FixedJoystick>();
        leverArm = FindObjectOfType<LeverArm>();
    }

    void Update(){

       // horizontal = Input.GetAxis("Horizontal");
        horizontal = _fixedJoystick.Horizontal;
        animator.SetFloat("speedX", Mathf.Abs(horizontal));

        if(Input.GetKeyDown(KeyCode.W)) {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.F)) {
           Interact();
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speedX * SpeedMultiplier * Time.fixedDeltaTime, rb.velocity.y);

     if(isJump) {
        rb.AddForce(new Vector2(0f, jump));
        isGround = false;
        isJump = false;
        }

        if(horizontal > 0f && !isFacingRight) {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight) {
            Flip();
        }
    }

    void Flip() {
        isFacingRight = !isFacingRight;
            Vector3 playerScale = playerModelTransform.localScale; 
            playerScale.x *= -1;
            playerModelTransform.localScale = playerScale;
    }

    public void Jump() {
        if(isGround){
        isJump = true;
        jumpSound.Play();
        }
    }

    public void Interact() {
         if(isFinish) {
                finish.FinishLevel();
            }
            if(isLeverArm) {
                leverArm.ActiveLeverArm();
            }
    }

    void OnCollisionEnter2D (Collision2D other) {
        if(other.gameObject.CompareTag("Ground")) {
            isGround = true;
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if(other.CompareTag("Finish")) {
            Debug.Log("Worked");
            isFinish = true;
        }

        if(leverArmTemp != null) {
            isLeverArm = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if(other.CompareTag("Finish")) {
            Debug.Log("Not Worked");
            isFinish = false;
        }

        if(leverArmTemp != null) {
            isLeverArm = false;
        }
    }
}
