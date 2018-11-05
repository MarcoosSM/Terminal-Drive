using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator animator;

    // Variables públicas
    public float moveSpeed = 5;
    public Vector2 jumpHeight = new Vector2(0, 10);
    public float rangeStartMovement = 0.3f;
    // FIN variables públicas

    // Variables de control del jugador
    private float hAxis;
    private bool jumpKeyPressed;
    // FIN variables de control del jugador

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start() {
        
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
        
    }

    void FixedUpdate() {
        move();
    }

    private void updateKeyInput() {
        // Actualiza las variables de control del jugador
        hAxis = Input.GetAxis("Horizontal");
        jumpKeyPressed = Input.GetButtonDown("Jump");
       
    }

    private void move() {
        // Mueve al jugador, dependiendo de los valores de las variables de control
        updateOrientation();

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
    
    private void updateOrientation() {
        // Cambia la dirección a la que mira el jugador dependiendo de la entrada por teclado
        if(hAxis != 0) {
            if(hAxis > 0) {
                animator.SetBool("isRight", true);
            } else {
                animator.SetBool("isRight", false);
            }
        }
    }

    private void updateIsJumping() {
        // Comprueba la velocidad del eje y del personaje y si es 0 significa que no esta saltando
        if(rb2d.velocity.y == 0) {
            animator.SetBool("isJumping", false);
        }
    }
}
