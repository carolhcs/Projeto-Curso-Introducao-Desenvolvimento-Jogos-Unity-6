using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton para acessar o GameManager de qualquer script
    public static GameManager Instance;

    // Vari�veis de pontua��o
    public float score;        // Pontua��o atual

    public float lastScore;    // Ultimo score feito antes de perder
    public float bestScore;    // Melhor pontua��o salva

    // Velocidade base que o cen�rio e os obst�culos v�o se mover
    public float scrollSpeed = 5f;
    // Velocidade adicional com base no tempo de jogo, aumentando a cada obst�culo ou com o tempo
    public float speedIncreaseRate = 0.1f;

    // Indica se o jogo est� em andamento
    public bool isGameOver;

    // Ajustes de velocidade podem estar aqui ou no ObstacleMover, 
    // mas agora centralizamos no ObstacleMover. 
    // Voc� pode remover scrollSpeed, speedIncreaseRate etc. 
    // se n�o for mais usar para mover cen�rio.

    private void Awake()
    {
        // Implementa��o de Singleton
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
        // Inicialmente, o jogo n�o est� acabado
        isGameOver = false;
        // Pontua��o come�a em 0
        score = 0f;
        lastScore = score;
    }

    private void Update()
    {
        // Se o jogo n�o acabou, atualiza a pontua��o com base no tempo decorrido
        if (!isGameOver)
        {
            // A pontua��o pode ser, por exemplo, a dist�ncia = tempo * scrollSpeed
            score += Time.deltaTime;// * scrollSpeed;

            // Aumenta levemente a velocidade ao longo do tempo (efeito de ficar mais dif�cil)
            scrollSpeed += speedIncreaseRate * Time.deltaTime;

            // TODO: Colocar depois que j� houver UIManager
            // Atualiza a UI
            UIManager.Instance.UpdateScore(score);
        }
    }

    // Chamada quando o jogador colide com obst�culo ou sai da tela
    public void GameOver()
    {
        // Marca que o jogo acabou
        isGameOver = true;

        // Ultimo score feito antes de perder 
        lastScore = score;

        // Verifica se a pontua��o atual � melhor que a melhor pontua��o salva
        if (score > bestScore)
        {
            bestScore = score;
            // Salva no PlayerPrefs
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }

        // TODO: Colocar depois que j� houver UIManager
        // Exibe a tela de Game Over
        UIManager.Instance.ShowGameOverUI();
    }

    // Fun��o para reiniciar o jogo
    public void RestartGame()
    {
        isGameOver = false;
        score = 0f;
        // Reinicia a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Fun��o para ir ao Menu (caso tenha uma cena de Menu separada)
    public void GoToMenu()
    {
        isGameOver = false;
        score = 0f;
        SceneManager.LoadScene("MainMenu");
    }
}
