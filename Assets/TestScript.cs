using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public InputScript inputScript;
    private void OnEnable() {
        InputScript.GetInputScript().AddInputAction("Fire",InputType.Started,Hello);
        InputScript.GetInputScript().AddInputAction("Move",Move);
    }
    private void OnDisable() {
        //InputScript.GetInputScript().RemoveInputAction("Fire",InputType.Started,Hello);
        //InputScript.GetInputScript().RemoveInputAction("Move",Move);
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
