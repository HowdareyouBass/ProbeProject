using UnityEngine;
using UnityEngine.AI;

//FIXME:
public sealed class S_BlinkComponent : S_ActiveSpellComponent
{
    [SerializeField] private int m_MaxDistance;

    protected override void Cast(Transform caster, Target target)
    {
        base.Cast(caster, target);
        Vector2 desiredPoint = new Vector2(target.GetPoint().x, target.GetPoint().z);
        Vector2 casterPoint = new Vector2(caster.position.x, caster.position.z);
        if (desiredPoint.x == casterPoint.x || desiredPoint.y == casterPoint.y)
        {
            //No way this will happen but just to make sure
            return;
        }
        Vector2 finalPoint = desiredPoint;

        if (Vector2.Distance(casterPoint, desiredPoint) > m_MaxDistance / 100)
        {
            //Taken from here: https://ru.stackoverflow.com/questions/1123552/Как-найти-ближайшую-точку-на-окружности
            //Vector between caster and desired position
            Vector2 deltaVector = desiredPoint - casterPoint;
            float sqrtOfSum = Mathf.Sqrt(Mathf.Pow(deltaVector.x, 2) + Mathf.Pow(deltaVector.y, 2));
            float x = casterPoint.x + (m_MaxDistance/100*(deltaVector.x)/sqrtOfSum);
            float y = casterPoint.y + (m_MaxDistance/100*(deltaVector.y)/sqrtOfSum);
            finalPoint = new Vector2(x, y);
        }

        Vector3 raycastPosition = new Vector3(finalPoint.x, caster.position.y, finalPoint.y);
        Vector3 finalPosition = new Vector3(finalPoint.x, 0, finalPoint.y);

        if (Physics.Raycast(raycastPosition, Vector3.up, out RaycastHit hit))
        {
            finalPosition.y = hit.point.y;
        }
        if (Physics.Raycast(raycastPosition, Vector3.down, out RaycastHit hit1))
        {
            finalPosition.y = hit1.point.y;
        }
        caster.position = finalPosition;
        caster.GetComponent<NavMeshAgent>().SetDestination(finalPosition);
    }
}