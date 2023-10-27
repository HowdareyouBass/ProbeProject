using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private PlayerLevelRequirementsSO m_LevelRequirementSO;

    public float ExperienceRequired()
    {
        return m_LevelRequirementSO.GetLevelRequirement(m_Player.GetComponent<PlayerScript>().GetEntity().Stats.CurrentLevel); ;
    }

    private void Start()
    {
        EntityScript[] es = FindObjectsOfType<EntityScript>();
        foreach(EntityScript e in es)
        {
            if (e.transform == m_Player)
            {
                continue;
            }
            e.GetEntity().Events.GetEvent<float>(EntityEventName.OnDeath, true).Subscribe(GainExperience);
        }
    }
    private void GainExperience(float value)
    {
        m_Player.GetComponent<PlayerScript>().GetEntity().GainExperience(value);
    }
    void Update()
    {
        
    }
}
