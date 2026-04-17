using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Holds the player's money and determines if they can buy an item
/// </summary>
public class PlayerWalletManager : PersistentSingleton<PlayerWalletManager>
{
    [SerializeField] private float money;
    public float Money => money;
    public UnityEvent<float> onMoneyChanged;
    [SerializeField] private float debugAddMoneyAmount;

    private void Start()
    {
        onMoneyChanged.Invoke(money);
    }

    public void DebugAddMoney(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed) return;
        AddMoney(debugAddMoneyAmount);
    }

    public void AddMoney(float amount)
    {
        money += amount;
        onMoneyChanged.Invoke(money);
    }
    
    public bool CanBuyItem(Gear itemToBuy) => money >= itemToBuy.ItemData.Price;
}
