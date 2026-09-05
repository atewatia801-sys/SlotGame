using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] symbolImages;

    [Header("Spin Settings")]
    [SerializeField] private float symbolHeight = 100f;
    [SerializeField] private float maxSpinSpeed = 1200f;
    [SerializeField] private float spinDuration = 1.8f;
    [SerializeField] private float stopDuration = 0.45f;

    private bool isSpinning;
    private int resultIndex;
    private SlotSymbol resultSymbol;

    public bool IsSpinning => isSpinning;
    public SlotSymbol ResultSymbol => resultSymbol;

    private void Start()
    {
        ResetSymbolPositions();
    }

    public IEnumerator Spin()
    {
        if (isSpinning)
            yield break;

        isSpinning = true;

        // Select the final result before the animation begins.
        resultIndex = Random.Range(0, symbolImages.Length);

        SlotSymbolReference reference =
            symbolImages[resultIndex]
                .GetComponent<SlotSymbolReference>();

        resultSymbol = reference.Symbol;

        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            float normalizedTime = elapsed / spinDuration;

            // Smoothly accelerate and decelerate the reel.
            float speedMultiplier =
                Mathf.Sin(normalizedTime * Mathf.PI);

            float currentSpeed =
                Mathf.Lerp(
                    maxSpinSpeed * 0.35f,
                    maxSpinSpeed,
                    speedMultiplier
                );

            MoveSymbols(currentSpeed);

            elapsed += Time.deltaTime;

            yield return null;
        }

        yield return StartCoroutine(AlignResult());

        isSpinning = false;
    }

    private void MoveSymbols(float speed)
    {
        foreach (Image symbol in symbolImages)
        {
            RectTransform rect = symbol.rectTransform;

            rect.anchoredPosition +=
                Vector2.down * speed * Time.deltaTime;

            // Recycle symbols to create a continuous reel.
            if (rect.anchoredPosition.y < -200f)
            {
                rect.anchoredPosition +=
                    Vector2.up * (symbolHeight * symbolImages.Length);
            }
        }
    }

    private IEnumerator AlignResult()
    {
        RectTransform resultRect =
            symbolImages[resultIndex].rectTransform;

        float startY = resultRect.anchoredPosition.y;
        float elapsed = 0f;

        while (elapsed < stopDuration)
        {
            elapsed += Time.deltaTime;

            float t =
                Mathf.Clamp01(elapsed / stopDuration);

            t = Mathf.SmoothStep(0f, 1f, t);

            float targetY =
                Mathf.Lerp(startY, 0f, t);

            float movement =
                targetY - resultRect.anchoredPosition.y;

            MoveAllSymbols(movement);

            yield return null;
        }

        // Guarantee exact final alignment.
        float finalMovement =
            -resultRect.anchoredPosition.y;

        MoveAllSymbols(finalMovement);

        resultRect.anchoredPosition =
            new Vector2(0f, 0f);
    }

    private void MoveAllSymbols(float amount)
    {
        foreach (Image symbol in symbolImages)
        {
            RectTransform rect = symbol.rectTransform;

            rect.anchoredPosition +=
                Vector2.up * amount;
        }
    }

    private void ResetSymbolPositions()
    {
        for (int i = 0; i < symbolImages.Length; i++)
        {
            float y =
                150f - (i * symbolHeight);

            symbolImages[i].rectTransform.anchoredPosition =
                new Vector2(0f, y);
        }
    }
}