using UnityEngine;
using UnityEngine.SceneManagement;


public class UIPauseMenuBehaviour : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeLevelScene();
        }
    }


    private void FreezeGameTime() => Time.timeScale = 0;
    private void PauseAudio() => AudioListener.pause = true;


    private void UnfreezeGameTime() => Time.timeScale = 1;
    private void ResumeAudio() => AudioListener.pause = false;


    public void PauseLevelScene()
    {
        gameObject.SetActive(true);
        FreezeGameTime();
        PauseAudio();
    }


    public void ResumeLevelScene()
    {
        gameObject.SetActive(false);
        UnfreezeGameTime();
        ResumeAudio();
    }


    public void RestartLevelScene()
    {
        UnfreezeGameTime();
        ResumeAudio();

        LevelSceneBehaviour.Instance.DeleteLevelSceneData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void BackToMainMenu()
    {
        UnfreezeGameTime();
        ResumeAudio();

        LevelSceneBehaviour.Instance.DeleteLevelSceneData();
        SceneManager.LoadScene("MainMenu");
    }
}
