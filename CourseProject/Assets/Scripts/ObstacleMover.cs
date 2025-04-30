using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Configura��es de Movimento")]
    public float baseSpeed = 5f;           // Velocidade inicial
    public float increaseInterval = 5f;    // A cada quantos segundos a velocidade aumenta
    public float speedIncrement = 1f;      // Quanto a velocidade aumenta a cada intervalo

    [Header("Limite para Destruir")]
    public float destroyXPosition = -15f;  // Posi��o X em que o obst�culo � destru�do

    private float currentSpeed;            // Velocidade atual
    private float timeSinceLastIncrease;   // Tempo desde que a velocidade foi incrementada

    void Start()
    {
        currentSpeed = baseSpeed + GameManager.Instance.scrollSpeed;
        timeSinceLastIncrease = 0f;
    }

    void Update()
    {
        // Se o jogo n�o estiver em Game Over, move o obst�culo
        if (!GameManager.Instance.isGameOver)
        {
            // Move o obst�culo para a esquerda
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);

            // Aumenta a velocidade a cada "increaseInterval" segundos
            timeSinceLastIncrease += Time.deltaTime;
            if (timeSinceLastIncrease >= increaseInterval)
            {
                currentSpeed += (speedIncrement + GameManager.Instance.scrollSpeed);
                timeSinceLastIncrease = 0f;
            }

            // Destr�i o obst�culo se sair muito � esquerda
            if (transform.position.x < destroyXPosition)
            {
                Destroy(gameObject);
            }
        }
    }
}
