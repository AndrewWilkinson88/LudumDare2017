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

    private GameObject previousFade;
    const float FADE_VALUE = 0.2f;

    void Start()
    {
        previousFade = null;
    }

    // Update is called once per frame
    void Update ()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerCharacter.transform.position - transform.position, out hit) )
        {
            if( previousFade != null)
            {
                Renderer prevWallRender = previousFade.GetComponentInChildren<Renderer>(true);
                Color noFade = prevWallRender.material.color;
                noFade.a = 1.0f;
                prevWallRender.material.color = noFade;
                changeRenderMode(hit.collider.gameObject, BlendMode.Opaque);
            }

            previousFade = hit.collider.gameObject;
            Renderer wallRender = hit.collider.gameObject.GetComponentInChildren<Renderer>(true);
            Color fade = wallRender.material.color;
            changeRenderMode(hit.collider.gameObject, BlendMode.Fade);

            fade.a = FADE_VALUE;
            wallRender.material.color = fade;
        }
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
