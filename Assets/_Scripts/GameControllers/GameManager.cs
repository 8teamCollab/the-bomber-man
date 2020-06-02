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


public class GameManager : SingletonBehaviour<GameManager>
{
    #region Variables

    public delegate void OnStateChangeHandler();

    public event OnStateChangeHandler OnStateChange;

    public GameState GameState { get; set; }

    #endregion Variables


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }


    public void SetGameState(GameState gameState)
    {
        GameState = gameState;
        OnStateChange?.Invoke();
    }
}
