using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text betText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private WinPopupController winPopupController;

    public void UpdateBalance(int balance)
    {
        balanceText.text = $"BALANCE: {balance}";
    }

    public void UpdateBet(int bet)
    {
        betText.text = $"BET: {bet}";
    }

   public void ShowWin(int payout)
{
    resultText.text = $"WIN! +{payout}";

    if (winPopupController != null)
    {
        winPopupController.ShowWin(payout);
    }
}

    public void ShowLoss()
{
    resultText.text = "TRY AGAIN!";

    if (winPopupController != null)
    {
        winPopupController.Hide();
    }
}

     public void ClearResult()
    {
        resultText.text = "";

        if (winPopupController != null)
        {
            winPopupController.Hide();
        }
    }
}