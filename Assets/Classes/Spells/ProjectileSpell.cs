using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : SpellBase
{
    GameObject m_Projectile;

    public ProjectileSpell(GameObject projectile)
    {
        m_Projectile = projectile;
    }
    public override GameObject CastEffect()
    {
        return m_Projectile;
    }
}
