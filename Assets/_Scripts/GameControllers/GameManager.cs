public class GameManager : SingletonBehaviour<GameManager>
{
    #region Variables

    public delegate void OnStateChangeHandler();

    public event OnStateChangeHandler OnStateChange;

    public GameState GameState { get; set; }

    #endregion Variables


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }


    public void SetGameState(GameState gameState)
    {
        GameState = gameState;
        OnStateChange?.Invoke();
    }
}
