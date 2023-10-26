using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScript : MonoBehaviour
{
    [Header("PlayerStats")]
    public int level = 1;
    public int experience = 0;
    public int nextLevelExperience = 100;
    public int maxHealth = 100;
    public float currentHealth;
    public int maxStamina = 100;
    public float currentStamina;
    public int Strength;
    public int maxStrength = 10;
    public int Perception;
    public int maxPerception = 10;
    public int Endurance;
    public int maxEndurance = 10;
    public int Charisma;
    public int maxCharisma = 10;
    public int Intelligence;
    public int maxIntelligence = 10;
    public int Agility;
    public int maxAgility = 10;
    public int Luck;
    public int maxLuck = 10;

    [Header("StatsText")]
    public Text HealthText;
    public Text StaminaText;
    public Text StrenghtText;
    public Text PerceptionText;
    public Text EnduranceText;
    public Text CharismaText;
    public Text IntelligenceText;
    public Text AgilityText;
    public Text LuckText;

    private float regenDelay;
    private float elapsed;
    private float regenRate;

    public void UpdateStats()
    {
        HealthText.text = "Health: " + currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        StaminaText.text = "Stamina: " + currentStamina.ToString("F0") + "/" + maxStamina.ToString("F0");
        StrenghtText.text = "Strenght: " + Strength.ToString("F0") + "/" + maxStrength.ToString("F0");
        PerceptionText.text = "Perception: " + Perception.ToString("F0") + "/" + maxPerception.ToString("F0");
        EnduranceText.text = "Endurance: " + Endurance.ToString("F0") + "/" + maxEndurance.ToString("F0");
        CharismaText.text = "Charisma: " + Charisma.ToString("F0") + "/" + maxCharisma.ToString("F0");
        IntelligenceText.text = "Intelligence: " + Intelligence.ToString("F0") + "/" + maxIntelligence.ToString("F0");
        AgilityText.text = "Agility: " + Agility.ToString("F0") + "/" + maxAgility.ToString("F0");
        LuckText.text = "Luck: " + Luck.ToString("F0") + "/" + maxLuck.ToString("F0");
    }

    public void addExperience()
    {
        experience += 100;
        Debug.Log("Experience added");
    }

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    void Start()
    {
        currentHealth = maxHealth;
        //FIXME: healthBar.SetMaxHealth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        regenDelay = 10.0f;
        elapsed = 0.0f;
        regenRate = 1.0f;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            UseStamina(20);
        }

        if (elapsed > regenDelay)
        {
            currentStamina = Mathf.Min(currentStamina + regenRate * Time.deltaTime, maxStamina);
            currentHealth = Mathf.Min(currentHealth + regenRate * Time.deltaTime, maxHealth);
        }

        elapsed += Time.deltaTime;


        UpdateStats();
        //FIXME: healthBar.SetHealth(currentHealth);
        staminaBar.SetStamina(currentStamina);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //FIXME: healthBar.SetHealth(currentHealth);
    }

    void UseStamina(int usedStamina)
    {
        currentStamina -= usedStamina;

        staminaBar.SetStamina(currentStamina);
    }

    
}
