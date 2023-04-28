using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Camera playerCamera;

    private float timer = 0;

    void Update()
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.Damage(10);
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
