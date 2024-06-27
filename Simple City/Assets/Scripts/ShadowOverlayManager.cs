using UnityEngine;
using UnityEngine.UI;

public class ShadowOverlayManager : MonoBehaviour
{
    public UnityEngine.UI.Image shadowOverlay; // Use UnityEngine.UI.Image explicitly

    void Start()
    {
        SetShadowOverlayColor();
        shadowOverlay.gameObject.SetActive(false); // Hide the shadow overlay initially
    }

    void SetShadowOverlayColor()
    {
        Color semiTransparentBlack = new Color(0, 0, 0, 0.5f); // 50% transparent black
        shadowOverlay.color = semiTransparentBlack;
    }

    public void ShowShadowOverlay()
    {
        shadowOverlay.gameObject.SetActive(true);
    }

    public void HideShadowOverlay()
    {
        shadowOverlay.gameObject.SetActive(false);
    }
}
