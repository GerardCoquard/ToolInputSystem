using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    private void OnEnable() {
        InputScript.GetInputScript().AddInputAction("Fire",Hello);
    }
    private void OnDisable() {
        InputScript.GetInputScript().RemoveInputAction("Fire",Hello);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            InputScript.GetInputScript().ActionEnabled("Fire",true);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            InputScript.GetInputScript().ActionEnabled("Fire",false);
        }
    }

    public void Hello(InputAction.CallbackContext context)
    {
        if(context.started) Debug.Log("Shoot!");
    }
    
}
