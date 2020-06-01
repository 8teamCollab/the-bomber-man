using UnityEngine;
using TMPro;


public class UILevelTextBehaviour : MonoBehaviour
{
    private void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        int level = LevelSceneBehaviour.Instance.Level;

        text.SetText($"F.{level}");
    }
}
