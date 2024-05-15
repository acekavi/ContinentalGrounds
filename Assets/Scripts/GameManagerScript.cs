using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class GameManagerScript : MonoBehaviour
{
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
}
