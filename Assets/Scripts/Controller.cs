using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{    
    public PlayerBehaviour player;
    public PlayerController playerController;
    public Camera playerCamera;

    private float timer = 0;

    void Update()
    {
        MovementAndAttacking();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.CastSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerController.StopAction();
            player.isCastingSpell = false;
        }
        
        if (Input.GetMouseButtonDown(0) && player.isCastingSpell)
        {
            RaycastHit hit = CastRayFromCamera();

            if (hit.colliderInstanceID == 0)
                return;

            if (hit.transform.CompareTag("Enemy"))
            {
                playerController.StopAction();
                playerController.LookAtTarget(hit);
                player.CastSpellAtTarget(hit);
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

            player.isCastingSpell = false;
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
