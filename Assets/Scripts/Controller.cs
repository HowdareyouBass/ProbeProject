using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public PlayerController playerController;
    public Camera playerCamera;

    private float timer = 0;

    void Update()
    {
        MovementAndAttacking();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerController.CastSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerController.StopAction();
            playerController.player.isCastingSpell = false;
        }
        
        if (Input.GetMouseButtonDown(0) && playerController.player.isCastingSpell)
        {
            RaycastHit hit = CastRayFromCamera();

            if (hit.colliderInstanceID == 0)
                return;

            if (hit.transform.CompareTag("Enemy"))
            {
                playerController.CastSpellOnTarget(hit);
            }
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

            playerController.player.isCastingSpell = false;
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
