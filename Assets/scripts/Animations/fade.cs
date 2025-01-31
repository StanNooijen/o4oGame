using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    public GameObject Fade;

    // Start is called before the first frame update
    public void FadeIn()
    {
        LeanTween.alpha(Fade.GetComponent<RectTransform>(), 1, 1f).setEase(LeanTweenType.easeOutQuad);
    }

    public void FadeOut()
    {
        LeanTween.alpha(Fade.GetComponent<RectTransform>(), 0, 1f).setEase(LeanTweenType.easeOutQuad);
    }

    public void ScaleIn()
    {
        Fade.transform.localScale = Vector3.zero;
        LeanTween.scale(Fade, Vector3.one, 1f).setEase(LeanTweenType.easeOutQuad);
    }
    
    public void ScaleOut()
    {
        LeanTween.scale(Fade, Vector3.zero, 1f).setEase(LeanTweenType.easeOutQuad);
    }
}
