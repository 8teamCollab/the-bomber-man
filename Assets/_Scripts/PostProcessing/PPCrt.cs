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
