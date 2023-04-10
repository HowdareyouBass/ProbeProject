using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeOptionsOnTypeChange : MonoBehaviour
{
    public TMP_Dropdown spellType;
    private List<TMP_Dropdown.OptionData> m_Projectiles = new List<TMP_Dropdown.OptionData>();
    private List<TMP_Dropdown.OptionData> m_Effects = new List<TMP_Dropdown.OptionData>();

    private void Start()
    {
        m_Projectiles.Add(new TMP_Dropdown.OptionData("Fireball"));

        m_Effects.Add(new TMP_Dropdown.OptionData("Effect"));
    }

    public void ChangeOptions()
    {
        Debug.Log(spellType.captionText.text);
        if (spellType.captionText.text == "Projectile")
        {
            transform.GetComponent<TMP_Dropdown>().options = m_Projectiles;
        }
        if (spellType.captionText.text == "Effects")
        {
            transform.GetComponent<TMP_Dropdown>().options = m_Effects;
        }
    }
}
