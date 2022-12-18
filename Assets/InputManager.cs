using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
public static class InputManager
{
    //List of all action events
    static List<InputUnityEvent> events = new List<InputUnityEvent>();
    //InputSetter instance
    public static InputScene inputScene;
    public static void ClearListeners(Scene a,Scene b)
    {
        //Removes all subscribed listeners of all events
        if(a.buildIndex == -1) return;
        foreach (InputUnityEvent _event in events)
        {
            _event.ClearListeners();
        }
    }
    public static void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        //Do stuff when device changed
    }
    public static void CreateEvents(PlayerInput playerInput)
    {
        //Creates one event for each action in PlayerInput
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
    static void CreateInputOnScene()
    {
        //Creates the InputManager GameObject on Scene, wich works alongside with InputManager(this)
        GameObject pref = MonoBehaviour.Instantiate(Resources.Load("Prefabs/InputManager") as GameObject,Vector3.zero,Quaternion.identity);
    }
    public static void AddInputAction(string _actionName, UnityEngine.Events.UnityAction<InputAction.CallbackContext> _method)
    {
        //Subscribe to event of action named _actionName with _method
        if(inputScene == null) CreateInputOnScene();
        GetAction(_actionName)?.AddListener(_method);
    }
    public static void AddInputAction(string _actionName,InputType _type, UnityEngine.Events.UnityAction _method)
    {
        //Subscribe to specific interaction event of action named _actionName with _method
        if(inputScene == null) CreateInputOnScene();
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
    public static void RemoveInputAction(string _actionName, UnityEngine.Events.UnityAction<InputAction.CallbackContext> _method)
    {
        //Unsubscribe to event of action named _actionName with _method
        if(inputScene == null) CreateInputOnScene();
        GetAction(_actionName)?.RemoveListener(_method);
    }
    public static void RemoveInputAction(string _actionName,InputType _type, UnityEngine.Events.UnityAction _method)
    {
        //Unsubscribe to specific interaction event of action named _actionName with _method
        if(inputScene == null) CreateInputOnScene();
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

    public static void ActionEnabled(string _actionName,bool _enabled)
    {
        //Sets event enabled of action named _ActionName to _enabled
        if(inputScene == null) CreateInputOnScene();
        GetAction(_actionName)?.SetEnabled(_enabled);
    }
    public static void ActionEnabled(string[] _actionNames,bool _enabled)
    {
        //Sets events enabled of actions named _ActionName to _enabled
        if(inputScene == null) CreateInputOnScene();
        foreach (string _actionName in _actionNames)
        {
            ActionEnabled(_actionName,_enabled);
        }
    }
    public static void ChangeActionMap(string _actionMapName)
    {
        if(inputScene == null) CreateInputOnScene();
        //cambiar action map
    }
    static InputUnityEvent GetAction(string _actionName)
    {
        //Returns event of action named _actionName
        foreach (InputUnityEvent _event in events)
        {
            if(_event.actionName == _actionName)
            {
                return _event;
            }
        }
        Debug.LogWarning("Action named " + _actionName + " doesn't exist");
        return null;
    }
}
public enum InputType
{
    Started,
    Performed,
    Canceled
}
