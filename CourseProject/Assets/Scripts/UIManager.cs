using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton para acesso fácil
    public static UIManager Instance;

    [Header("Referências de Text")]
    public TMP_Text scoreText;       // Texto que mostra a pontuação atual
    public TMP_Text lastScoreText;   // Texto que mostra a ultima pontuação
    public TMP_Text bestScoreText;   // Texto que mostra a melhor pontuação

    [Header("Painel de Game Over")]
    public GameObject gameOverPanel; // Painel que aparece quando o jogo acaba

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Time.timeScale = 1f;
    }

    void Start()
    {
        // Ao iniciar, deixamos o painel de Game Over inativo
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    // Atualiza o texto de pontuação
    public void UpdateScore(float score)
    {
        if (scoreText != null)
            scoreText.text = " " + ((int)score).ToString(); //"Score: " + ((int)score).ToString();
    }

    // Mostra o painel de Game Over e atualiza o texto de bestScore
    public void ShowGameOverUI()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if(lastScoreText != null)
            lastScoreText.text = ((int)GameManager.Instance.lastScore).ToString();

        if (bestScoreText != null)
            bestScoreText.text = ((int)GameManager.Instance.bestScore).ToString();
    }

    // Botão no Painel de GameOver para voltar ao Menu
    public void OnClickReturnToMenu()
    {
        // Chama a função no GameManager
        GameManager.Instance.GoToMenu();
    }

    // >>> NOVA FUNÇÃO: Reinicia a cena atual <<<
    public void OnClickRestartGame()
    {
        // Caso esteja pausado, por segurança, retorne o jogo ao tempo normal
        Time.timeScale = 1f;

        // Chama a função de reiniciar do GameManager
        GameManager.Instance.RestartGame();
    }
}
