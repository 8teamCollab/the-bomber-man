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
