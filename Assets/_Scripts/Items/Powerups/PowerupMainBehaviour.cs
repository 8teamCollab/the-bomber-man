using UnityEngine;


public class PowerupMainBehaviour : MonoBehaviour
{
    #region Variables

    [HideInInspector] public Powerup powerup;
    [HideInInspector] public PowerupsController powerupsController;
    [HideInInspector] public PowerupType type;

    #endregion Variables


    public void ActivatePowerup() => powerupsController.ActivatePowerup(powerup);


    public void SetPowerup(Powerup powerup)
    {
        this.powerup = powerup;
        GetComponent<Animator>().runtimeAnimatorController = powerup.animator;
        name = powerup.name;
        type = powerup.type;
    }
}
