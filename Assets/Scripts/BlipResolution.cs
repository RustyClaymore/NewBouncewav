using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BlipResolution : MonoBehaviour {

    public Material effectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        RenderTexture rt = RenderTexture.GetTemporary(160, 144, 0);
        rt.filterMode = FilterMode.Bilinear;
        rt.anisoLevel = 0;
        Graphics.Blit(src, rt, effectMaterial);
        Graphics.Blit(rt, dst);
        RenderTexture.ReleaseTemporary(rt);
    }
}
