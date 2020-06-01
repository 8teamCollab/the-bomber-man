using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Powerup
{
    #region Variables

    public string name;
    public RuntimeAnimatorController animator;
    public float duration;

    public PowerupType type;

    public UnityEvent startAction;
    public UnityEvent endAction;

    #endregion Variables


    public void StartAction() => startAction?.Invoke();
    public void EndAction() => endAction?.Invoke();
}
