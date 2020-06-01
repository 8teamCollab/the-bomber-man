using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class LevelSceneBehaviour : SingletonBehaviour<LevelSceneBehaviour>
{
    #region Variables

    public int Level
    {
        get => level;
        set
        {
            level = value;
            PlayerPrefs.SetInt("level", level);
        }
    }


    [SerializeField] private GameObject levelSceneManagerGameObject;

    [Header("Death screen object references")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI deathScreenText;

    private GameManager gameManager;

    private int level;

    #endregion Variables


    protected override void Awake()
    {
        base.Awake();
        Level = PlayerPrefs.GetInt("level", 1);
    }


    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        gameManager.OnStateChange += HandleOnStateChange;
        gameManager.SetGameState(GameState.Level);
    }


    private void OnDisable() => gameManager.OnStateChange -= HandleOnStateChange;


    private void OnApplicationQuit() => DeleteLevelSceneData();
    private void OnApplicationPause(bool _) => DeleteLevelSceneData();


    public void DeleteLevelSceneData()
    {
        PlayerPersistance.DeleteAllPlayerParametersData();
        PlayerPrefs.DeleteKey("level");
    }


    public void HandleOnStateChange()
    {
        if (!MapManager.Instance)
        {
            Instantiate(levelSceneManagerGameObject);
        }

        MapManager.Instance.SetupScene(Level);
    }


    public void ReloadLevelScene()
    {
        Level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        deathScreenText.SetText($"You died on the {Level} floor. Good job!");
    }
}
