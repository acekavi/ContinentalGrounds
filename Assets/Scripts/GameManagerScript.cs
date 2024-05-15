using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private Animator player;
    [SerializeField] private ConversationsManager conversationManager;

    public void BuyBurger()
    {
        CustomEvent.Trigger(this.gameObject, "TriggerBartenderAnimation", "Burger");
        CustomEvent.Trigger(this.gameObject, "DecreaseMoney", 6.00);
    }

    public void BuyBeer()
    {
        CustomEvent.Trigger(this.gameObject, "TriggerBartenderAnimation", "Bartend");
        CustomEvent.Trigger(this.gameObject, "DecreaseMoney", 4.00);
    }

    public void AddItemToInventory(string itemName)
    {
        if (itemName.ToLower().Contains("burger"))
        {
            CustomEvent.Trigger(this.gameObject, "AddInventory", 0);
        }
        else if (itemName.ToLower().Contains("beer"))
        {
            CustomEvent.Trigger(this.gameObject, "AddInventory", 1);
        }
        else if (itemName.ToLower().Contains("axe"))
        {
            CustomEvent.Trigger(this.gameObject, "AddInventory", 2);
        }
        else
        {
            Debug.LogError("Item not found");
        }
    }
}
