using UnityEngine;


public class ScreenResolutionDownscaler : SingletonBehaviour<ScreenResolutionDownscaler>
{
    #region Variables

    [Range(0.1f, 1f)] [SerializeField] private float screenResolutionDownscalingRatio = 0.8f;
    private bool screenResolutionDownscaled = false;

    #endregion Variables


    protected override void Awake()
    {
        base.Awake();

        if (!screenResolutionDownscaled)
        {
            DownscaleScreenResolution(screenResolutionDownscalingRatio);

            screenResolutionDownscaled = true;
        }
    }


    private void DownscaleScreenResolution(float downscalingRatio)
    {
        int width = (int)(downscalingRatio * Screen.width);
        int height = (int)(downscalingRatio * Screen.height);

        Screen.SetResolution(width, height, true);
    }
}
