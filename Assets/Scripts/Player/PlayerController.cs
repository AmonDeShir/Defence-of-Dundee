using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterController2D
{
    public Shoot weapon;
    protected bool shoot = false;

    protected new void Update() {
        base.Update();
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (shoot) {
            weapon.SpawnBullet();
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.started) {
            jump = true;

            if (direction.y <= -1f) {
                ultraJump = true;
            } 
        }
    }

    public void OnShoot(InputAction.CallbackContext context) {
        if (context.started) {
            shoot = true;
        }
        
        else if (context.canceled) {
            shoot = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context) {
        if (context.started && IsOnGround()) {
            run = true;
        }
        
        else if (context.canceled) {
            run = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context) {
        if (context.started) {
            bool grounded = IsOnGround();

            instantFall = grounded ? InstantFallState.DISABLED : InstantFallState.START;
            crouch = grounded;
        }

        if (context.canceled) {
            crouch = false;
        }
    }
}
