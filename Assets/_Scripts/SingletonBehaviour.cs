using UnityEngine;


public class SingletonBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    #region Variables

    public static T Instance;

    #endregion Variables


    protected virtual void Awake()
    {
        if (Instance)
        {
            Debug.LogError($"Duplicate subclass of type {typeof(T)}! eliminating {name} while preserving {Instance.name}");
            Destroy(gameObject);
        }
        else
        {
            Instance = this as T;
        }
    }


    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
