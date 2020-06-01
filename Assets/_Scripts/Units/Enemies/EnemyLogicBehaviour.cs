using UnityEngine;


public class EnemyLogicBehaviour : MonoBehaviour
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

                gameObject.SetActive(false);
            }
        }
    }
    public int HitDamage
    {
        get => hitDamage;
        set
        {
            if (value > hitDamageMinValue)
            {
                hitDamage = value;
            }
            else
            {
                hitDamage = hitDamageMinValue;
            }
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
        }
    }
    public int FieldOfViewRadiusX
    {
        get => fieldOfViewRadiusX;
        set
        {
            if (value > fieldOfViewRadiusXMinValue)
            {
                fieldOfViewRadiusX = value;
            }
            else
            {
                fieldOfViewRadiusX = fieldOfViewRadiusXMinValue;
            }
        }
    }
    public int FieldOfViewRadiusY
    {
        get => fieldOfViewRadiusY;
        set
        {
            if (value > fieldOfViewRadiusYMinValue)
            {
                fieldOfViewRadiusY = value;
            }
            else
            {
                fieldOfViewRadiusY = fieldOfViewRadiusYMinValue;
            }
        }
    }

    [Header("Parameters minimum values")]
    [SerializeField] private int hitpointsMinValue          = 0;
    [SerializeField] private int hitDamageMinValue          = 1;
    [SerializeField] private float speedMinValue            = 1.1f;
    [SerializeField] private int fieldOfViewRadiusXMinValue = 1;
    [SerializeField] private int fieldOfViewRadiusYMinValue = 1;

    private int hitpoints;
    private int hitDamage;
    private float speed;
    private int fieldOfViewRadiusX;
    private int fieldOfViewRadiusY;

    #endregion Variables


    public void SetEnemyData(EnemyData enemyData)
    {
        name = enemyData.name;
        GetComponent<Animator>().runtimeAnimatorController = enemyData.animator;
        GetComponent<EnemyFieldOfViewRenderer>().colorChanging = enemyData.colorChanging;

        Hitpoints = enemyData.hitpoints;
        HitDamage = enemyData.hitDamage;
        Speed = enemyData.speed;
        FieldOfViewRadiusX = enemyData.fieldOfViewRadiusX;
        FieldOfViewRadiusY = enemyData.fieldOfViewRadiusY;
    }


    public void LoseLife(int lifeLoss = 1) => Hitpoints -= lifeLoss;


    public void HitPlayer(PlayerLogicBehaviour player) => player.LoseLife(HitDamage);
}
