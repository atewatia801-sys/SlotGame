using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LeverController : MonoBehaviour
{
    [Header("Lever Sprites")]
    [SerializeField] private Sprite leverUpSprite;
    [SerializeField] private Sprite leverDownSprite;

    [Header("Animation")]
    [SerializeField] private float downDuration = 0.15f;
    [SerializeField] private float upDelay = 0.25f;

    private Image leverImage;
    private Coroutine leverCoroutine;

    private void Awake()
    {
        leverImage = GetComponent<Image>();

        if (leverImage != null)
        {
            // Start in the UP position.
            leverImage.sprite = leverUpSprite;
        }
    }

    public void PullLever()
    {
        if (leverImage == null)
            return;

        if (leverCoroutine != null)
        {
            StopCoroutine(leverCoroutine);
        }

        leverCoroutine = StartCoroutine(PullAnimation());
    }

    private IEnumerator PullAnimation()
    {
        // Move lever down.
        leverImage.sprite = leverDownSprite;

        yield return new WaitForSeconds(downDuration);

        // Keep the lever down briefly.
        yield return new WaitForSeconds(upDelay);

        // Return lever to the UP position.
        leverImage.sprite = leverUpSprite;

        leverCoroutine = null;
    }
}