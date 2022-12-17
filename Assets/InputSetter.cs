using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.EventSystems;

public class InputSetter : MonoBehaviour
{
    //Singleton to warn InputManager of not static things, like scene changes, device changes, or create action events at the beggining
    public static InputSetter instance;
    private void Awake() {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable() {
        SceneManager.activeSceneChanged += InputManager.ClearListeners;
        InputUser.onChange += InputManager.OnInputDeviceChange;
        InputManager.inputSetter = this;
        InputManager.CreateEvents(GetComponent<PlayerInput>());
    }
    private void OnDisable() {
        SceneManager.activeSceneChanged -= InputManager.ClearListeners;
        InputUser.onChange -= InputManager.OnInputDeviceChange;
    }
}
