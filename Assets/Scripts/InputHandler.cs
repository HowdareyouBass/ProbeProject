using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Camera playerCamera;

    private float timer = 0;

    private void Update()
    {
        MovementAndAttacking();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit = CastRayFromCamera();

            //If clicked on something
            if (hit.colliderInstanceID == 0)
                return;
            playerController.CastSpell(0, hit);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerController.StopActions();
        }
    }
    
    private void MovementAndAttacking()
    {
        if (timer >= 0.1f && Input.GetMouseButton(1))
        {
            RaycastHit hit = CastRayFromCamera();

            //If clicked on something
            if (hit.colliderInstanceID == 0)
                return;
            playerController.OnClick(hit);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private RaycastHit CastRayFromCamera()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        return hit;
    }
}
