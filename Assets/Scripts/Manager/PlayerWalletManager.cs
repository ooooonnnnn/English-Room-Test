using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Holds the player's money and determines if they can buy an item
/// </summary>
public class PlayerWalletManager : PersistentSingleton<PlayerWalletManager>
{
    [SerializeField] private float money;
    [SerializeField] private InputActionReference debugAction;
    public float Money => money;
    public UnityEvent<float> onMoneyChanged;

    private void OnValidate()
    {
        debugAction.action.performed += ctx => AddMoney(100);
    }
    
    public void AddMoney(float amount)
    {
        money += amount;
        onMoneyChanged.Invoke(money);
    }
    
    public bool CanBuyItem(Gear itemToBuy) => money >= itemToBuy.ItemData.Price;
}
