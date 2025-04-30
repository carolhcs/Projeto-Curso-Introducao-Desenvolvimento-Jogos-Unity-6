using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton para acessar o GameManager de qualquer script
    public static GameManager Instance;

    // Variáveis de pontuação
    public float score;        // Pontuação atual

    public float lastScore;    // Ultimo score feito antes de perder
    public float bestScore;    // Melhor pontuação salva

    // Velocidade base que o cenário e os obstáculos vão se mover
    public float scrollSpeed = 5f;
    // Velocidade adicional com base no tempo de jogo, aumentando a cada obstáculo ou com o tempo
    public float speedIncreaseRate = 0.1f;

    // Indica se o jogo está em andamento
    public bool isGameOver;

    // Ajustes de velocidade podem estar aqui ou no ObstacleMover, 
    // mas agora centralizamos no ObstacleMover. 
    // Você pode remover scrollSpeed, speedIncreaseRate etc. 
    // se não for mais usar para mover cenário.

    private void Awake()
    {
        // Implementação de Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Permite que o GameManager persista entre cenas
        }
        else
        {
            Destroy(gameObject);
        }

        isGameOver = false;
    }

    private void Start()
    {
        // Carrega o bestScore salvo no PlayerPrefs
        bestScore = PlayerPrefs.GetFloat("BestScore", 0f);
        // Inicialmente, o jogo não está acabado
        isGameOver = false;
        // Pontuação começa em 0
        score = 0f;
        lastScore = score;
    }

    private void Update()
    {
        // Se o jogo não acabou, atualiza a pontuação com base no tempo decorrido
        if (!isGameOver)
        {
            // A pontuação pode ser, por exemplo, a distância = tempo * scrollSpeed
            score += Time.deltaTime;// * scrollSpeed;

            // Aumenta levemente a velocidade ao longo do tempo (efeito de ficar mais difícil)
            scrollSpeed += speedIncreaseRate * Time.deltaTime;

            // TODO: Colocar depois que já houver UIManager
            // Atualiza a UI
            UIManager.Instance.UpdateScore(score);
        }
    }

    // Chamada quando o jogador colide com obstáculo ou sai da tela
    public void GameOver()
    {
        // Marca que o jogo acabou
        isGameOver = true;

        // Ultimo score feito antes de perder 
        lastScore = score;

        // Verifica se a pontuação atual é melhor que a melhor pontuação salva
        if (score > bestScore)
        {
            bestScore = score;
            // Salva no PlayerPrefs
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }

        // TODO: Colocar depois que já houver UIManager
        // Exibe a tela de Game Over
        UIManager.Instance.ShowGameOverUI();
    }

    // Função para reiniciar o jogo
    public void RestartGame()
    {
        isGameOver = false;
        score = 0f;
        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Função para ir ao Menu (caso tenha uma cena de Menu separada)
    public void GoToMenu()
    {
        isGameOver = false;
        score = 0f;
        SceneManager.LoadScene("MainMenu");
    }
}
