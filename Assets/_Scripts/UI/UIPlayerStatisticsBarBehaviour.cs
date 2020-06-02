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
using TMPro;


public class UIPlayerStatisticsBarBehaviour : MonoBehaviour
{
    #region Variables

    [Header("Text components of indicators references")]
    [SerializeField] private TextMeshProUGUI hitpointsIndicatorText;
    [SerializeField] private TextMeshProUGUI bombsCapacityIndicatorText;
    [SerializeField] private TextMeshProUGUI explosionRangeIndicatorText;
    [SerializeField] private TextMeshProUGUI explosionDamageIndicatorText;
    [SerializeField] private TextMeshProUGUI speedIndicatorText;

    [Space] [SerializeField] private Color criticalHitpointsValueTextColor;

    private PlayerLogicBehaviour player;

    #endregion Variables


    private void Update()
    {
        player = PlayerLogicBehaviour.Instance;

        if (player)
        {
            hitpointsIndicatorText.SetText(player.Hitpoints.ToString());

            if (player.Hitpoints == 1)
            {
                hitpointsIndicatorText.color = criticalHitpointsValueTextColor;
            }
            else
            {
                hitpointsIndicatorText.color = Color.white;
            }

            bombsCapacityIndicatorText.SetText(player.BombsCapacity.ToString());
            explosionRangeIndicatorText.SetText(player.ExplosionRange.ToString());
            explosionDamageIndicatorText.SetText(player.ExplosionDamage.ToString());
            speedIndicatorText.SetText(player.Speed.ToString());
        }
    }
}
