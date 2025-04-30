using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float baseSpeed = 5f;           // Velocidade inicial
    public float increaseInterval = 5f;    // A cada quantos segundos a velocidade aumenta
    public float speedIncrement = 1f;      // Quanto a velocidade aumenta a cada intervalo

    [Header("Limite para Destruir")]
    public float destroyXPosition = -15f;  // Posição X em que o obstáculo é destruído

    private float currentSpeed;            // Velocidade atual
    private float timeSinceLastIncrease;   // Tempo desde que a velocidade foi incrementada

    void Start()
    {
        currentSpeed = baseSpeed + GameManager.Instance.scrollSpeed;
        timeSinceLastIncrease = 0f;
    }

    void Update()
    {
        // Se o jogo não estiver em Game Over, move o obstáculo
        if (!GameManager.Instance.isGameOver)
        {
            // Move o obstáculo para a esquerda
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

            // Aumenta a velocidade a cada "increaseInterval" segundos
            timeSinceLastIncrease += Time.deltaTime;
            if (timeSinceLastIncrease >= increaseInterval)
            {
                currentSpeed += (speedIncrement + GameManager.Instance.scrollSpeed);
                timeSinceLastIncrease = 0f;
            }

            // Destrói o obstáculo se sair muito à esquerda
            if (transform.position.x < destroyXPosition)
            {
                Destroy(gameObject);
            }
        }
    }
}
