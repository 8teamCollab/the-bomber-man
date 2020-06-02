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


public class MainMenuSceneBehaviour : MonoBehaviour
{
    #region Variables

    private GameManager gameManager;

    #endregion Variables


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }


    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        gameManager.OnStateChange += HandleOnStateChange;
        gameManager.SetGameState(GameState.MainMenu);
    }


    private void OnDisable() => gameManager.OnStateChange -= HandleOnStateChange;


    private void HandleOnStateChange() => AudioManager.Instance.PlaySound(Sound.MainMenuTheme);


    public void StartGame() => SceneManager.LoadScene("Level");


    public void ExitGame() => Application.Quit();
}
