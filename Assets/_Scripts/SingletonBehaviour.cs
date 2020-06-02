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
