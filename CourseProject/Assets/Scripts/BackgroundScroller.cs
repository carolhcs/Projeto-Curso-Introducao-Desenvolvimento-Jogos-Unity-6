using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // Largura do sprite de fundo (use a largura real do sprite para calcular melhor)
    public float backgroundWidth = 20f;
    private Vector3 startPosition;

    void Start()
    {
        // Armazena a posição inicial do fundo
        startPosition = transform.position;
    }

    void Update()
    {
        // Se o jogo não estiver acabado
        if (!GameManager.Instance.isGameOver)
        {
            // Move o fundo para a esquerda usando a scrollSpeed do GameManager
            transform.Translate(Vector3.left * GameManager.Instance.scrollSpeed * Time.deltaTime);

            // Se o fundo sair para trás de um certo ponto, reposiciona para frente
            if (transform.position.x <= startPosition.x - backgroundWidth)
            {
                // Reposiciona para a posição inicial
                transform.position = new Vector3(
                    transform.position.x + backgroundWidth * 2f,
                    transform.position.y,
                    transform.position.z
                );
            }
        }
    }
}
