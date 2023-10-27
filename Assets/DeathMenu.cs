using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private PlayerScript m_PlayerScript;
    [SerializeField] private GameObject m_Panel;

    private void Start()
    {
        m_PlayerScript.GetEntity().Events.GetEvent(EntityEventName.OnDeath).Subscribe(TurnOnDeathMenu);
    }
    private void TurnOnDeathMenu()
    {
        m_Panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
