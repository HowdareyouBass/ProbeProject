public class EnemyController : EntityController
{
    public bool AllSpellsOnCooldown()
    {
        return GetComponent<SpellInventory>().AllSpellsOnCooldown();
    }
    public void CastSpellNotOnCooldown(Target target)
    {
        m_SpellCaster?.CastSpell(GetComponent<SpellInventory>().GetIndexOfFirstSpellNotOnCooldown(), target);
    }
}