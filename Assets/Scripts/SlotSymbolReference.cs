using UnityEngine;

public class SlotSymbolReference : MonoBehaviour
{
    [SerializeField] private SlotSymbol symbol;

    public SlotSymbol Symbol => symbol;
}