using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private Attack m_Attack;
    [SerializeField] private Movement m_Movement;
    [SerializeField] private SpellCaster m_SpellCaster;
    [SerializeField] private GameObject m_MovementEffect;

    public void StopActions()
    {
        m_Attack?.Stop();
        m_Movement?.Stop();
        m_SpellCaster?.Stop();
    }

    public void OnMouseClick(Target target)
    {
        SpawnEffect(target.GetPoint(), target.normal);
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
        m_Attack?.AttackTarget(target);
    }
    
    private void Move(Target target)
    {
        StopActions();
        m_Movement?.Move(target.GetPoint());
    }

    //TODO: Вынести в отдельный класс для спавна эффектов наверое?
    private void SpawnEffect(Vector3 position, Vector3 rotation)
    {
        if (m_MovementEffect != null)
            Instantiate(m_MovementEffect, position, Quaternion.LookRotation(rotation));
    }
}