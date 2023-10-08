using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingInBuildingsManager : MonoBehaviour
{
    public ItemToSell itemToSell;
    public int buttonNumber;

    public void sellItem()
    {
        itemToSell.buyItem();
    }
}
