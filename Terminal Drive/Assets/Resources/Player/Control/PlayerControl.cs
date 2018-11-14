using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Variables públicas
    public float moveSpeed = 5;
    public Vector2 jumpHeight = new Vector2(0, 10);
    public float rangeStartMovement = 0.3f;
    // FIN variables públicas

    // Variables de control del jugador
    private float hAxis;
    private bool jumpKeyPressed;
    private bool stealth;
    GameObject handgun;
    GameObject so_shotgun;
    // FIN variables de control del jugador

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start() {
        handgun = GameObject.FindGameObjectWithTag("Handgun");
        so_shotgun = GameObject.FindGameObjectWithTag("SawedOffShotgun");
    }

    void Update() {
        updateKeyInput();

        updateIsJumping();
        if(jumpKeyPressed) {
            if(! animator.GetBool("isJumping")) {
                rb2d.AddForce(jumpHeight, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
            }
        }

        if (stealth) {
            if (!animator.GetBool("isStealth")) {
                animator.SetBool("isStealth", true);
                moveSpeed = 3;
                GameObject.FindGameObjectWithTag("Arm").GetComponent<SpriteRenderer>().enabled = false;
                if (handgun) {
                    handgun.SetActive(false);
                } 
                if (so_shotgun) {  
                    so_shotgun.SetActive(false);
                }
             } else {
                animator.SetBool("isStealth", false);
                moveSpeed = 5;
                GameObject.FindGameObjectWithTag("Arm").GetComponent<SpriteRenderer>().enabled = true;
                if (handgun) {    
                    handgun.SetActive(true);
                }
                if (so_shotgun) { 
                    so_shotgun.SetActive(true);
                }
            }
        }
        stealthFlip();
    }

    void FixedUpdate() {
        move();   
    }

    private void updateKeyInput() {
        // Actualiza las variables de control del jugador
        hAxis = Input.GetAxis("Horizontal");
        jumpKeyPressed = Input.GetButtonDown("Jump");
        if (animator.GetFloat("Speed") < 0.01f) {
            stealth = Input.GetKeyDown(KeyCode.S);
        }
    }

    private void move() {
        float absHAxis = Mathf.Abs(hAxis);
        if(absHAxis > rangeStartMovement) {
            animator.SetFloat("Speed", absHAxis * moveSpeed);
            rb2d.velocity = new Vector2(moveSpeed * hAxis, rb2d.velocity.y);
        } else {
            // Si no hay suficiente movimiento en el hAxis, para al jugador
            animator.SetFloat("Speed", 0);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

    }

    private void stealthFlip() {
        if (animator.GetBool("isRight")==true && animator.GetBool("isStealth")==true) {
            spriteRenderer.flipX=true; 
        } else {        
            spriteRenderer.flipX=false;
        }   
    }

    private void updateIsJumping() {
        // Comprueba la velocidad del eje y del personaje y si es 0 significa que no esta saltando
        if(rb2d.velocity.y == 0) {
            animator.SetBool("isJumping", false);
        }
    }
}
