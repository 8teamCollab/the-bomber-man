using System.Collections;
using UnityEngine;


[ExecuteInEditMode]
public class PPCrt : MonoBehaviour
{
    #region Variables

    [SerializeField] private Material crtMaterial;

    [Header("Image distortion settings")]
    [SerializeField] private float aberrationStrength      = 1f;
    [SerializeField] private bool imageDistortion          = false;
    [SerializeField] private float imageDistortionDuration = 1f;
    [SerializeField] private float distortionStartValue    = 1f;
    [SerializeField] private float distrotionEndValue      = 10f;

    #endregion Variables


    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        crtMaterial.SetFloat("_AberrationStrength", aberrationStrength);
        Graphics.Blit(source, destination, crtMaterial);
    }


    private IEnumerator TweenAberrationStrength(float startValue, float endValue, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            aberrationStrength = Mathf.Lerp(startValue, endValue, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        aberrationStrength = endValue;
    }


    public void DistortImage()
    {
        if (imageDistortion)
        {
            StartCoroutine(TweenAberrationStrength(distortionStartValue, distrotionEndValue, imageDistortionDuration / 2f));
            StartCoroutine(TweenAberrationStrength(distrotionEndValue, distortionStartValue, imageDistortionDuration / 2f));
        }
    }
}
