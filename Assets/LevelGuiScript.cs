using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelGuiScript : MonoBehaviour
{
    public Text levelText;
    public PlayerStatsScript playerStats;
    public InventoryManager inventoryManager;



    // Start is called before the first frame update
    void Start()
    {
        levelText.text = playerStats.level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.experience >= playerStats.nextLevelExperience)
        {
            levelUp();
            setLevel(playerStats.level);
        }
    }


    void setLevel(int level)
    {
        levelText.text = level.ToString();
    }

    void levelUp()
    {
        ++playerStats.level;
        playerStats.experience = 0;
        playerStats.nextLevelExperience *= 2;
        
        if (inventoryManager.maxAvalableSlots > inventoryManager.avalableSlots)
        {
            ++inventoryManager.avalableSlots;
        }

        inventoryManager.changeSlotAmount();
    }
}
