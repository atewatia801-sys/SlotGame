using UnityEngine;

[CreateAssetMenu(fileName = "NewSlotSymbol", menuName = "Slot Game/Symbol")]
public class SlotSymbol : ScriptableObject
{
    [SerializeField] private string symbolName;
    [SerializeField] private Sprite symbolSprite;
    [SerializeField] private int payoutMultiplier = 2;

    public string SymbolName => symbolName;
    public Sprite SymbolSprite => symbolSprite;
    public int PayoutMultiplier => payoutMultiplier;
}