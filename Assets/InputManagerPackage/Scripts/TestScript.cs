using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    private void OnEnable() {
        InputManager.AddInputAction("Fire",InputType.Started,Shoot);
        InputManager.AddInputAction("Move",Move);
    }
    private void OnDisable() {
        InputManager.RemoveInputAction("Fire",InputType.Started,Shoot);
        InputManager.RemoveInputAction("Move",Move);
    }

    public void Shoot()
    {
        Debug.Log("Shoot!");
    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        Debug.Log("Moving to: " + direction);
    }
    
}
