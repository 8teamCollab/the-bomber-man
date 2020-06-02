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
