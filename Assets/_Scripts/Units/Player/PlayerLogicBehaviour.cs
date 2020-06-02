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


public class PlayerLogicBehaviour : SingletonBehaviour<PlayerLogicBehaviour>
{
    #region Variables

    public int Hitpoints
    {
        get => hitpoints;
        set
        {
            if (value > hitpointsMinValue)
            {
                hitpoints = value;
            }
            else
            {
                hitpoints = hitpointsMinValue;

                LevelSceneBehaviour.Instance.ShowDeathScreen();
                LevelSceneBehaviour.Instance.DeleteLevelSceneData();
            }

            PlayerPrefs.SetInt("hitpoints", hitpoints);
        }
    }
    public int BombsCapacity
    {
        get => bombsCapacity;
        set
        {
            if (value > bombsCapacityMinValue)
            {
                bombsCapacity = value;
            }
            else
            {
                bombsCapacity = bombsCapacityMinValue;
            }

            PlayerPrefs.SetInt("bombsCapacity", bombsCapacity);
        }
    }
    public int ExplosionRange
    {
        get => explosionRange;
        set
        {
            if (value > explosionRangeMinValue)
            {
                explosionRange = value;
            }
            else
            {
                explosionRange = explosionRangeMinValue;
            }

            PlayerPrefs.SetInt("explosionRange", explosionRange);
        }
    }
    public int ExplosionDamage
    {
        get => explosionDamage;
        set
        {
            if (value > explosionDamageMinValue)
            {
                explosionDamage = value;
            }
            else
            {
                explosionDamage = explosionDamageMinValue;
            }

            PlayerPrefs.SetInt("explosionDamage", explosionDamage);
        }
    }
    public float Speed
    {
        get => speed;
        set
        {
            if (value > speedMinValue)
            {
                speed = value;
            }
            else
            {
                speed = speedMinValue;
            }

            PlayerPrefs.SetFloat("speed", speed);
        }
    }

    [Header("Default parameters values")]
    [Range(1f, 10f)] public int hitpointsDefaultValue       = 5;
    [Range(1f, 10f)] public int bombsCapacityDefaultValue   = 1;
    [Range(1f, 10f)] public int explosionRangeDefaultValue  = 2;
    [Range(1f, 10f)] public int explosionDamageDefaultValue = 1;
    [Range(1.1f, 10f)] public float speedDefaultValue       = 2.9f;


    [Header("Parameters minimum values")]
    [SerializeField] private int hitpointsMinValue = 0;
    [SerializeField] private int bombsCapacityMinValue = 1;
    [SerializeField] private int explosionRangeMinValue = 1;
    [SerializeField] private int explosionDamageMinValue = 1;
    [SerializeField] private float speedMinValue = 1.1f;

    [Space] [SerializeField] private float invulnerabilityDuration;

    private int hitpoints;
    private int bombsCapacity;
    private int explosionRange;
    private int explosionDamage;
    private float speed;

    private bool invulnerable = false;

    #endregion Variables


    protected override void Awake()
    {
        base.Awake();

        LoadPlayerPersistanceData();
    }


    private void LoadPlayerPersistanceData()
    {
        PlayerData data = PlayerPersistance.LoadData();

        Hitpoints       = data.Hitpoints;
        BombsCapacity   = data.BombsCapacity;
        ExplosionRange  = data.ExplosionRange;
        ExplosionDamage = data.ExplosionDamage;
        Speed           = speedDefaultValue;
    }


    public void LoseLife(int lifeLoss = 1)
    {
        if (!invulnerable)
        {
            Hitpoints -= lifeLoss;

            invulnerable = true;
            Invoke("BecomeVulnerable", invulnerabilityDuration);
        }
    }


    private void BecomeVulnerable() => invulnerable = false;
}
