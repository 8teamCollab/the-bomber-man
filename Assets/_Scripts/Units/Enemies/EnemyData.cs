/*
 * Copyright(C) 2020 Artyom Bezmenov (8nhuman8)

 * This file is part of The Bomber Man.

 * The Bomber Man is free software: you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.

 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with this The Bomber Man. If not,
 * see <https://www.gnu.org/licenses/>.
 */


using UnityEngine;


[System.Serializable]
public class EnemyData
{
    public string name;
    public RuntimeAnimatorController animator;
    public bool colorChanging = false;

    [Header("Parameters values")]
    [Range(1f, 5f)] public int hitpoints           = 1;
    [Range(1f, 5f)] public int hitDamage           = 1;
    [Range(1.1f, 10f)] public float speed          = 4f;
    [Range(1f, 10f)] public int fieldOfViewRadiusX = 1;
    [Range(1f, 10f)] public int fieldOfViewRadiusY = 1;
}
