using UnityEngine;

[System.Serializable]
public class DirectedAtEnemy : Spell, ICastableOnEntity, IDamaging, IHasRadius
{
    [SerializeField] private float m_Radius;
    [SerializeField] private float m_Damage;

    public float damage { get => m_Damage; }
    public float radius { get => m_Radius; }

    public void Cast(Transform caster, Transform target)
    {
        if (m_Radius < 0)
            throw new System.ArgumentOutOfRangeException();
        GameObject castEffect = GameObject.Instantiate(effect, target.position, Quaternion.identity);
        DirectedAtEnemyScript directedAtEnemyScript = castEffect.AddComponent<DirectedAtEnemyScript>();
        directedAtEnemyScript.target = target;
        directedAtEnemyScript.spell = this;
    }
}