using UnityEngine;


public class PowerupActions : MonoBehaviour
{
    public void PlayerSpeedIncStartAction() => PlayerLogicBehaviour.Instance.Speed /= 2;
    public void PlayerSpeedIncEndAction() => PlayerLogicBehaviour.Instance.Speed = PlayerLogicBehaviour.Instance.speedDefaultValue;


    public void PlayerSpeedDecStartAction() => PlayerLogicBehaviour.Instance.Speed *= 2;
    public void PlayerSpeedDecEndAction() => PlayerLogicBehaviour.Instance.Speed = PlayerLogicBehaviour.Instance.speedDefaultValue;


    public void PlayerHitpointsIncStartAction() => PlayerLogicBehaviour.Instance.Hitpoints++;
    public void PlayerHitpointsDecStartAction() => PlayerLogicBehaviour.Instance.Hitpoints--;


    public void PlayerBombsCapacityIncStartAction() => PlayerLogicBehaviour.Instance.BombsCapacity++;
    public void PlayerBombsCapacityDecStartAction() => PlayerLogicBehaviour.Instance.BombsCapacity--;


    public void PlayerExplosionRangeIncStartAction() => PlayerLogicBehaviour.Instance.ExplosionRange++;
    public void PlayerExplosionRangeDecStartAction() => PlayerLogicBehaviour.Instance.ExplosionRange--;


    public void PlayerExplosionDamageIncStartAction() => PlayerLogicBehaviour.Instance.ExplosionDamage++;
    public void PlayerExplosionDamageDecStartAction() => PlayerLogicBehaviour.Instance.ExplosionDamage--;
}
