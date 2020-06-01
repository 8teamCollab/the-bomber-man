using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuSceneBehaviour : MonoBehaviour
{
    #region Variables

    private GameManager gameManager;

    #endregion Variables


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }


    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        gameManager.OnStateChange += HandleOnStateChange;
        gameManager.SetGameState(GameState.MainMenu);
    }


    private void OnDisable() => gameManager.OnStateChange -= HandleOnStateChange;


    private void HandleOnStateChange() => AudioManager.Instance.PlaySound(Sound.MainMenuTheme);


    public void StartGame() => SceneManager.LoadScene("Level");


    public void ExitGame() => Application.Quit();
}
