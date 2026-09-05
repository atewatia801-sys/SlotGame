using System.Collections;
using TMPro;
using UnityEngine;

public class WinPopupController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject popup;
    [SerializeField] private TMP_Text winMessage;
    [SerializeField] private TMP_Text payoutMessage;

    [Header("Animation")]
    [SerializeField] private float animationDuration = 0.35f;

    private RectTransform popupTransform;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        if (popup != null)
        {
            popupTransform = popup.GetComponent<RectTransform>();
            popup.SetActive(false);
        }
    }

    public void ShowWin(int payout)
    {
        if (popup == null ||
            popupTransform == null ||
            winMessage == null ||
            payoutMessage == null)
        {
            Debug.LogWarning("Win popup references are not assigned.");
            return;
        }

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        popup.SetActive(true);

        winMessage.text = "BIG WIN!";
        payoutMessage.text = $"+{payout}";

        animationCoroutine = StartCoroutine(ShowAnimation());
    }

    private IEnumerator ShowAnimation()
    {
        popupTransform.localScale = Vector3.zero;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(
                elapsed / animationDuration
            );

            float scale = Mathf.Sin(
                t * Mathf.PI * 0.5f
            );

            popupTransform.localScale =
                Vector3.one * scale;

            yield return null;
        }

        popupTransform.localScale = Vector3.one;
        animationCoroutine = null;
    }

    public void Hide()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        if (popupTransform != null)
        {
            popupTransform.localScale = Vector3.one;
        }

        if (popup != null)
        {
            popup.SetActive(false);
        }
    }
}