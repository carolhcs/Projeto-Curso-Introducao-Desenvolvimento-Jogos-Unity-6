using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Função para ser chamada no botão "Play"
    public void PlayGame()
    {
        // Carrega a cena do jogo (certifique-se de tê-la adicionada no Build Settings)
        SceneManager.LoadScene("SampleScene");
    }

    // Função para sair do jogo (funciona em builds, não no editor)
    public void QuitGame()
    {
        Application.Quit();
    }
}
