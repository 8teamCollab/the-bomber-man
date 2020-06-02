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
