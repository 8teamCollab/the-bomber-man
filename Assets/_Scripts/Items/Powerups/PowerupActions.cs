/*
 * Copyright(C) 2020 Artyom Bezmenov (8nhuman8)

 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * at your option) any later version.

 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see<https://www.gnu.org/licenses/>.
 */


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
