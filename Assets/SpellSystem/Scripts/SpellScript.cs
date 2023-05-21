using UnityEngine;

public class SpellScript : MonoBehaviour
{
    public int slot;

    public Transform caster { get; private set; }
    public Transform target { get; private set; }

    public SpellEvents events { get; private set; }
    private void Awake()
    {
        events = new SpellEvents();
    }
    public void Init(Transform _caster, Transform _target)
    {
        caster = _caster;
        target = _target;
        foreach (SpellComponent component in GetComponents<SpellComponent>())
        {
            component.caster = caster;
            component.target = target;
            component.spellScript = this;
        }
    }
    public void TryCast(Transform caster, Transform target)
    {
        GetComponent<ICastable>().TryCast(caster, target);
    }
}
