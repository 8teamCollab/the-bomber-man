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
