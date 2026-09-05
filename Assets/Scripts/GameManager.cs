using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int startingBalance = 1000;
    [SerializeField] private int startingBet = 50;

    [Header("Bet Limits")]
    [SerializeField] private int minimumBet = 10;
    [SerializeField] private int maximumBet = 200;
    [SerializeField] private int betStep = 10;

    private int balance;
    private int currentBet;

    public int Balance => balance;
    public int CurrentBet => currentBet;

    public event Action<int> OnBalanceChanged;
    public event Action<int> OnBetChanged;

    private void Awake()
    {
        balance = startingBalance;
        currentBet = startingBet;
    }

    public bool CanSpin()
    {
        return balance >= currentBet;
    }

    // Deducts the current bet when a spin begins.
    public bool PlaceBet()
    {
        if (!CanSpin())
            return false;

        balance -= currentBet;
        OnBalanceChanged?.Invoke(balance);

        return true;
    }

    // Adds the payout after a winning spin.
    public void AddWinnings(int amount)
    {
        balance += amount;
        OnBalanceChanged?.Invoke(balance);
    }

    public void IncreaseBet()
    {
        if (currentBet >= maximumBet)
            return;

        currentBet = Mathf.Min(
            currentBet + betStep,
            maximumBet
        );

        OnBetChanged?.Invoke(currentBet);
    }

    public void DecreaseBet()
    {
        if (currentBet <= minimumBet)
            return;

        currentBet = Mathf.Max(
            currentBet - betStep,
            minimumBet
        );

        OnBetChanged?.Invoke(currentBet);
    }
}