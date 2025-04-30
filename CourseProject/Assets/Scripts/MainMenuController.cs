using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Fun��o para ser chamada no bot�o "Play"
    public void PlayGame()
    {
        // Carrega a cena do jogo (certifique-se de t�-la adicionada no Build Settings)
        SceneManager.LoadScene("SampleScene");
    }

    // Fun��o para sair do jogo (funciona em builds, n�o no editor)
    public void QuitGame()
    {
        Application.Quit();
    }
}
