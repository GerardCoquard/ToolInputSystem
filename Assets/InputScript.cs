using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class InputScript : MonoBehaviour
{
    private static InputScript instance;
    List<InputUnityEvent> events;
    PlayerInput playerInput;
    private void OnEnable() {
        SceneManager.sceneLoaded += ClearListeners;
        InputUser.onChange += OnInputDeviceChange;
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= ClearListeners;
        InputUser.onChange -= OnInputDeviceChange;
    }
    void ClearListeners(Scene a,LoadSceneMode b)
    {
        foreach (InputUnityEvent _event in events)
        {
            _event.ClearListeners();
        }
    }
    void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        //Do stuff when device changed
    }
    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Init() 
    {
        playerInput = GetComponent<PlayerInput>();
        events = new List<InputUnityEvent>();

        foreach (InputActionMap actionMap in playerInput.actions.actionMaps)
        {
            foreach (InputAction act in actionMap)
            {
                InputUnityEvent newActionEvent = new InputUnityEvent(act);
                events.Add(newActionEvent);
            }
        }
    }
    public static InputScript GetInputScript()
    {
        if(instance == null)
        {
            instance = new GameObject("InputManager").AddComponent<InputScript>();
            instance.Init();
        }
        return instance;
    }
    public void AddInputAction(string _actionName, UnityEngine.Events.UnityAction<InputAction.CallbackContext> _method)
    {
        GetAction(_actionName)?.AddListener(_method);
    }
    public void AddInputAction(string _actionName,InputType _type, UnityEngine.Events.UnityAction _method)
    {
        switch (_type)
        {
            case InputType.Started:
                GetAction(_actionName)?.startedAction.AddListener(_method);
            break;

            case InputType.Performed:
                GetAction(_actionName)?.performedAction.AddListener(_method);
            break;

            case InputType.Canceled:
                GetAction(_actionName)?.canceledAction.AddListener(_method);
            break;
        }
        
    }
    public void RemoveInputAction(string _actionName, UnityEngine.Events.UnityAction<InputAction.CallbackContext> _method)
    {
        GetAction(_actionName)?.RemoveListener(_method);
    }
    public void RemoveInputAction(string _actionName,InputType _type, UnityEngine.Events.UnityAction _method)
    {
        switch (_type)
        {
            case InputType.Started:
                GetAction(_actionName)?.startedAction.RemoveListener(_method);
            break;

            case InputType.Performed:
                GetAction(_actionName)?.performedAction.RemoveListener(_method);
            break;

            case InputType.Canceled:
                GetAction(_actionName)?.canceledAction.RemoveListener(_method);
            break;
        }
        
    }

    public void ActionEnabled(string _actionName,bool _enabled)
    {
        GetAction(_actionName)?.SetEnabled(_enabled);
    }
    public void ActionEnabled(string[] _actionNames,bool _enabled)
    {
        foreach (string _actionName in _actionNames)
        {
            ActionEnabled(_actionName,_enabled);
        }
    }
    InputUnityEvent GetAction(string _actionName)
    {
        foreach (InputUnityEvent _event in events)
        {
            if(_event.actionName == _actionName)
            {
                return _event;
            }
        }
        Debug.Log("Action named " + _actionName + " doesn't exist");
        return null;
    }
}
public enum InputType
{
    Started,
    Performed,
    Canceled
}
