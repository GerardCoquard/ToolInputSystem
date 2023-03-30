using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Users;

public class TestScript : MonoBehaviour
{
    public int sceneNumber;
    private void OnEnable() {
        InputManager.GetAction("Fire").action += Shoot;
        InputManager.OnDeviceChanged += TestDevices;
        InputManager.GetAction("Jump").action += ChangeScene;
    }
    private void OnDisable() {
        InputManager.GetAction("Fire").action -= Shoot;
        InputManager.OnDeviceChanged -= TestDevices;
        InputManager.GetAction("Jump").action -= ChangeScene;
    }
    public void TestDevices(Devices newDevice)
    {
        Debug.Log("New Device Detected: " + newDevice.ToString());
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot! : " + context.phase);
    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        Debug.Log("Moving to: " + direction);
    }
    public void ChangeScene(InputAction.CallbackContext context)
    {
        if(context.started) SceneManager.LoadScene("Level"+sceneNumber);
    }
}
