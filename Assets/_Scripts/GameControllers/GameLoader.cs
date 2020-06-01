using UnityEngine;


public class GameLoader : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject gameManagerGameObject;

    #endregion Variables


    private void Awake()
    {
        if (!GameManager.Instance)
        {
            Instantiate(gameManagerGameObject);
        }
    }
}
