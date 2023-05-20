using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    //Костыль ебейший просто
    public void AddEffect(StatusEffect effect)
    {
        StartCoroutine(effect.StartEffectRoutine(GetComponent<EntityScript>().GetEntity()));
    }
}
