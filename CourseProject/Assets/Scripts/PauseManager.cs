using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Painel de pause na UI

    private bool isPaused = false;

    void Update()
    {
        // Se o jogador apertar a tecla de pause (Escape, por exemplo)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Ativa ou desativa o painel de pause
        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        // Se pausou, tempo de jogo é 0; se despausou, tempo de jogo volta ao normal (1)
        Time.timeScale = isPaused ? 0 : 1;
    }

    // Botão no painel de Pause para voltar ao Menu
    public void OnClickReturnToMenu()
    {
        Time.timeScale = 1; // Importante restaurar o tempo antes de trocar de cena
        GameManager.Instance.GoToMenu();
    }
}
