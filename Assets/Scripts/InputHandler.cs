using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController m_PlayerController;
    [SerializeField] private EnemyController m_EnemyController;
    [SerializeField] private Camera m_PlayerCamera;

    private float m_Timer = 0;

    private void Update()
    {
        MovementAndAttackingPlayer();
        MovementAndAttackingEnemy();
        int i = 0;
        foreach(KeyCode key in Controls.SpellKeys)
        {
            if (Input.GetKeyDown(key) && CastRayFromCamera(out RaycastHit hit))
            {
                m_PlayerController.CastSpell(i, new Target(hit));
                GlobalGameEvents.OnAnySpellCast.Trigger();
            }
            i++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_PlayerController.StopActions();
        }
    }

    private void MovementAndAttackingEnemy()
    {
        if (m_Timer >= 0.1f && Input.GetMouseButton(0))
        {
            if (CastRayFromCamera(out RaycastHit hit))
            {
                m_EnemyController.OnMouseClick(new Target(hit));
                m_Timer = 0f;
            }
        }
        else
        {
            m_Timer += Time.deltaTime;
        }
    }
    
    private void MovementAndAttackingPlayer()
    {
        if (m_Timer >= 0.1f && Input.GetMouseButton(1))
        {
            if (CastRayFromCamera(out RaycastHit hit))
            {
                m_PlayerController.OnMouseClick(new Target(hit));
                m_Timer = 0f;
            }
        }
        else
        {
            m_Timer += Time.deltaTime;
        }
    }

    private bool CastRayFromCamera(out RaycastHit hit)
    {
        Ray ray = m_PlayerCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
}