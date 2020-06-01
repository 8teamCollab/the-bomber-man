using UnityEngine;


public class ExitBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelSceneBehaviour.Instance.ReloadLevelScene();
        }
    }
}
