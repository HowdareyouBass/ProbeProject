using UnityEngine;
using UnityEngine.AI;

public class EntityController : MonoBehaviour
{
    private Attack m_Attack;
    private Movement m_Movement;
    private SpellCaster m_SpellCaster;
    private NavMeshAgent m_Agent;

    private void Start()
    {
        m_Attack = GetComponent<Attack>();
        m_Movement = GetComponent<Movement>();
        m_SpellCaster = GetComponent<SpellCaster>();
        m_Agent = GetComponent<NavMeshAgent>();
    }

    public void StopActions()
    {
        m_Attack?.Stop();
        m_Movement?.Stop();
        m_SpellCaster?.Stop();
    }

    public virtual void OnMouseClick(Target target)
    {
        AvoidEntities();
        if (target.transform == transform)
            return;
        
        if (target.isEntity)
        {
            Attack(target);
        }
        else
        {
            Move(target);
        }
    }

    public void CastSpell(int spellSlot, Target target)
    {
        m_SpellCaster?.CastSpell(spellSlot, target);
    }

    private void Attack(Target target)
    {
        StopAvoidingEntities();
        m_Attack?.AttackTarget(target);
    }
    
    private void Move(Target target)
    {
        StopActions();
        m_Movement?.Move(target.GetPoint());
    }

    private void StopAvoidingEntities()
    {
        m_Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }
    private void AvoidEntities()
    {
        m_Agent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
    }
}