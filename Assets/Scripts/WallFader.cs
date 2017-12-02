using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlendMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent
}

public class WallFader : MonoBehaviour
{
    public GameObject playerCharacter;

    private List<GameObject> previousFade;
    private List<GameObject> fadeCheck;
    const float FADE_VALUE = 0.2f;

    void Start()
    {
        previousFade = new List<GameObject>();
        fadeCheck = new List<GameObject>();
    }

    // Update is called once per frame
    void Update ()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Wall");
        Vector3 camToPlayer = playerCharacter.transform.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, camToPlayer, camToPlayer.magnitude, layerMask);
        previousFade.Clear();
        if (hits.Length != 0)
        {
            foreach (RaycastHit hit in hits)
            {
                int fadeIndex = fadeCheck.FindIndex((x) => x == hit.collider.gameObject);
                if( fadeIndex != -1)
                {
                    fadeCheck.RemoveAt(fadeIndex);
                }

                previousFade.Add(hit.collider.gameObject);
                Renderer wallRender = hit.collider.gameObject.GetComponentInChildren<Renderer>(true);
                Color fade = wallRender.material.color;
                changeRenderMode(hit.collider.gameObject, BlendMode.Fade);

                fade.a = FADE_VALUE;
                wallRender.material.color = fade;
            }
        }

        foreach( GameObject fadeObject in fadeCheck )
        {
            Renderer prevWallRender = fadeObject.GetComponentInChildren<Renderer>(true);
            Color noFade = prevWallRender.material.color;
            noFade.a = 1.0f;
            prevWallRender.material.color = noFade;
            changeRenderMode(fadeObject, BlendMode.Opaque);
        }
        fadeCheck = new List<GameObject>(previousFade);
    }

    private void changeRenderMode(GameObject go, BlendMode blendMode)
     {
        Material standardShaderMaterial = go.GetComponentInChildren<Renderer>().material;
        switch (blendMode)
        {
            case BlendMode.Opaque:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                break;
            case BlendMode.Cutout:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 2450;
                break;
            case BlendMode.Fade:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
            case BlendMode.Transparent:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                standardShaderMaterial.SetInt("_ZWrite", 0);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = 3000;
                break;
        }
    }
}
