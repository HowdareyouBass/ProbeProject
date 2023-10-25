using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityController : MonoBehaviour
{
    protected Attack m_Attack;
    protected Movement m_Movement;
    protected SpellCaster m_SpellCaster;
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

    public void Attack(Target target)
    {
        StopAvoidingEntities();
        m_Attack?.AttackTarget(target);
    }
    
    public void Move(Target target)
    {
        StopActions();
        m_Movement?.Move(target.GetPoint());
    }

    public void PatrolPoints(LinkedList<Vector3> points)
    {
        StopActions();
        m_Movement?.PatrolPoints(points);
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