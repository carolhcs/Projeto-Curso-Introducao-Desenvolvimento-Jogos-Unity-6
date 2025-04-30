using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Força de Salto")]
    public float jumpForce = 5f; // Força do pulo no eixo Y

    [Header("Referência de Animação")]
    public Animator animator;    // Referência para o Animator (pode ser nulo)

    private Rigidbody2D rb;
    private bool isGrounded;
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Removeu-se o controle horizontal. Jogador não se move para os lados.

        // Verifica se o jogador quer pular (Space/Tecla W) e se está no chão
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // Chama a animação de pulo, caso exista
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }

        // Caso o jogador caia muito abaixo da tela
        if (transform.position.y < -10f)
        {
            GameManager.Instance.GameOver();
        }
    }

    void FixedUpdate()
    {
        // Raio que sai do centro do jogador para baixo, para detectar se está no chão
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        isGrounded = (hit.collider != null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidir com um obstáculo, dispara GameOver
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
