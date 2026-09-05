using System.Collections;
using UnityEngine;

public class SlotMachineController : MonoBehaviour
{
    [Header("Reels")]
    [SerializeField] private ReelController[] reels;

    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LeverController leverController;

    [Header("Spin Settings")]
    [SerializeField] private float reelStopDelay = 0.4f;

    private bool isSpinning;
    private int spinBet;

    public bool IsSpinning => isSpinning;

    private void Start()
    {
        gameManager.OnBalanceChanged += HandleBalanceChanged;
        gameManager.OnBetChanged += HandleBetChanged;

        UpdateUI();
    }

    private void OnDestroy()
    {
        if (gameManager == null)
            return;

        gameManager.OnBalanceChanged -= HandleBalanceChanged;
        gameManager.OnBetChanged -= HandleBetChanged;
    }

    public void Spin()
    {
        if (isSpinning)
            return;

        // Store the bet used for this specific spin.
        int betForSpin = gameManager.CurrentBet;

        if (!gameManager.PlaceBet())
        {
            uiManager.ShowLoss();
            return;
        }

        spinBet = betForSpin;

        // Pull the lever when the spin begins.
        if (leverController != null)
        {
            leverController.PullLever();
        }

        uiManager.ClearResult();

        StartCoroutine(SpinAllReels());
    }

    private IEnumerator SpinAllReels()
    {
        isSpinning = true;

        // Start all reels together.
        foreach (ReelController reel in reels)
        {
            StartCoroutine(reel.Spin());
        }

        // Wait for each reel to finish before checking the result.
        for (int i = 0; i < reels.Length; i++)
        {
            while (reels[i].IsSpinning)
            {
                yield return null;
            }

            if (i < reels.Length - 1)
            {
                yield return new WaitForSeconds(reelStopDelay);
            }
        }

        CheckResult();

        isSpinning = false;
    }

    private void CheckResult()
    {
        if (reels.Length < 3)
        {
            Debug.LogError("Slot machine requires three reels.");
            return;
        }

        SlotSymbol firstResult = reels[0].ResultSymbol;
        SlotSymbol secondResult = reels[1].ResultSymbol;
        SlotSymbol thirdResult = reels[2].ResultSymbol;

        if (firstResult == null ||
            secondResult == null ||
            thirdResult == null)
        {
            Debug.LogError("A reel has no result symbol.");
            return;
        }

        bool isWin =
            firstResult == secondResult &&
            secondResult == thirdResult;

        if (isWin)
        {
            int payout =
                spinBet * firstResult.PayoutMultiplier;

            gameManager.AddWinnings(payout);
            uiManager.ShowWin(payout);

            Debug.Log(
                $"WIN! {firstResult.SymbolName} x3 | " +
                $"Payout: {payout}");
        }
        else
        {
            uiManager.ShowLoss();

            Debug.Log(
                $"Result: {firstResult.SymbolName}, " +
                $"{secondResult.SymbolName}, " +
                $"{thirdResult.SymbolName}");
        }
    }

    private void HandleBalanceChanged(int newBalance)
    {
        uiManager.UpdateBalance(newBalance);
    }

    private void HandleBetChanged(int newBet)
    {
        uiManager.UpdateBet(newBet);
    }

    private void UpdateUI()
    {
        uiManager.UpdateBalance(gameManager.Balance);
        uiManager.UpdateBet(gameManager.CurrentBet);
    }
}