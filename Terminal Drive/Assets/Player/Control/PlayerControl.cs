using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed;
    public Vector2 jumpHeight = new Vector2(0, 10);
    Rigidbody2D RB2d;
    private Animator animator;
    private SpriteRenderer sr;
    private bool isGrounded;

	// Use this for initialization
	void Start () {
		RB2d = GetComponent<Rigidbody2D>();
        speed = 5;
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

	void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        checkIsJumping();

        float h = Input.GetAxis("Horizontal");

        checkFlip(h);

        float absH = Mathf.Abs(h);

        if (absH > 0.3) // Solo se mueve si la fuerza h es mayor que cierto número
        {
            animator.SetFloat("Speed", absH * speed);
            RB2d.velocity = new Vector2(speed * h, RB2d.velocity.y);
        }
        else
        {
            // Stop al jugador y a la animación
            animator.SetFloat("Speed", 0);
            RB2d.velocity = new Vector2(0, RB2d.velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!animator.GetBool("isJumping"))
            {
                // Si no esta saltando, añade la fuerza del salto
                RB2d.AddForce(jumpHeight, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
            }
        }
    }

    private void checkFlip(float h)
    {
        // Comprueba si el personaje tiene que hacer un flip
        if(h != 0)
        {
            // Si h > 0 entonces flipX = true
            sr.flipX = h > 0;
        }
    }

    private void checkIsJumping()
    {
        // Comprueba la velocidad del eje y del personaje y si es 0 significa que no esta saltando
        if (RB2d.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
        }
        /**
         * Precaución: Puede darse que la Y sea 0 cuando el personaje esta 
         * en la parte superior del arco del salto, cuando ha dejado de subir pero todavia no esta bajando.
         * En este caso sería posible que el jugador hiciese un segundo salto.
         **/
    }
    
}
