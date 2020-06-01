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
