using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    private void OnEnable() {
        InputManager.AddInputAction("Fire",InputType.Started,Hello);
        InputManager.AddInputAction("Move",Move);
    }
    private void OnDisable() {
        InputManager.RemoveInputAction("Fire",InputType.Started,Hello);
        InputManager.RemoveInputAction("Move",Move);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            InputManager.ActionEnabled("Fire",true);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            InputManager.ActionEnabled("Fire",false);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level2");
        }
    }

    public void Hello()
    {
        Debug.Log("Shoot!");
    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        Debug.Log(direction);
    }
    
}
