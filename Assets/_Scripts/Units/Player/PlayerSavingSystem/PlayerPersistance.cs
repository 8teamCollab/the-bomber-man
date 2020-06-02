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


public static class PlayerPersistance
{
    public static PlayerData LoadData()
    {
        PlayerLogicBehaviour player = PlayerLogicBehaviour.Instance;

        int hitpoints       = PlayerPrefs.GetInt("hitpoints", player.hitpointsDefaultValue);
        int bombsCapacity   = PlayerPrefs.GetInt("bombsCapacity", player.bombsCapacityDefaultValue);
        int explosionRange  = PlayerPrefs.GetInt("explosionRange", player.explosionRangeDefaultValue);
        int explosionDamage = PlayerPrefs.GetInt("explosionDamage", player.explosionDamageDefaultValue);

        PlayerData data = new PlayerData()
        {
            Hitpoints       = hitpoints,
            BombsCapacity   = bombsCapacity,
            ExplosionRange  = explosionRange,
            ExplosionDamage = explosionDamage,
        };

        return data;
    }


    public static void DeleteAllPlayerParametersData()
    {
        PlayerPrefs.DeleteKey("hitpoints");
        PlayerPrefs.DeleteKey("bombsCapacity");
        PlayerPrefs.DeleteKey("explosionRange");
        PlayerPrefs.DeleteKey("explosionDamage");
    }
}
