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
