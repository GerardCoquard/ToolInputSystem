using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
public static class InputManager
{
    static GameObject sceneInput;
    static List<ActionContainer> events = new List<ActionContainer>(); //List of all action events
    public static PlayerInput playerInput; //Current PlayerInput
    public static EventSystem eventSystem; //Current EventSystem
    static string path; //Path of the InputManager prefab
    static InputManager()
    {
        //Constructor called only one time when a static method of this class is called
        path = "InputManager"; //Modify path how you want, but remember the root file is Resources
        //Checks if prefab is there
        if(Resources.Load(path) == null)
        {
            Debug.LogWarning("Prefab not found. Your input prefab have to be at " + path);
            return;
        }
        //Instantiates InputManager prefab, sets the current EventSystem and PlayerInput, and creates all events for each action it has
        if(sceneInput!=null) return;
        sceneInput = CreateInputOnScene();
        MonoBehaviour.DontDestroyOnLoad(sceneInput);
        playerInput = sceneInput.GetComponent<PlayerInput>();
        eventSystem = sceneInput.GetComponent<EventSystem>();
        CreateEvents(playerInput);
        //Subscribe to scene changes and device changes
        SceneManager.sceneUnloaded += ClearListeners;
        InputUser.onChange += OnInputDeviceChange;
    }
    public static void ClearListeners(Scene a)
    {
        //Removes all subscribed listeners of all events
        foreach (ActionContainer _event in events)
        {
            _event.ClearListeners();
        }     
    }
    public static void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
       if(change == InputUserChange.ControlSchemeChanged) Debug.Log("New Device: " + user.controlScheme.Value.name);
       //Do stuff when device changed
    }
    public static void CreateEvents(PlayerInput playerInput)
    {
        //Creates one event for each action in PlayerInput
        events = new List<ActionContainer>();

        foreach (InputActionMap actionMap in playerInput.actions.actionMaps)
        {
            foreach (InputAction act in actionMap)
            {
                ActionContainer newActionEvent = new ActionContainer(act);
                events.Add(newActionEvent);
            }
        }
    }
    static GameObject CreateInputOnScene()
    {
        //Creates the InputManager GameObject on Scene, wich works alongside with InputManager(this)
        return MonoBehaviour.Instantiate(Resources.Load(path) as GameObject,Vector3.zero,Quaternion.identity);
    }
    public static ActionContainer GetAction(string _actionName)
    {
        //Subscribe to event of action named _actionName with _method
        return GetEvent(_actionName);
    }
    public static void ActionEnabled(string _actionName,bool _enabled)
    {
        //Sets event enabled of action named _ActionName to _enabled
        GetEvent(_actionName)?.SetEnabled(_enabled);
    }
    public static void ActionsEnabled(string[] _actionNames,bool _enabled)
    {
        //Sets events enabled of actions named _ActionName to _enabled
        foreach (string _actionName in _actionNames)
        {
            ActionEnabled(_actionName,_enabled);
        }
    }
    public static void ChangeActionMap(string _actionMapName)
    {
        //Change PlayerInput Action Map to one named _actionMapName
        playerInput.SwitchCurrentActionMap(_actionMapName);
    }
    static ActionContainer GetEvent(string _actionName)
    {
        //Returns event of action named _actionName
        foreach (ActionContainer _event in events)
        {
            if(_event.actionName == _actionName)
            {
                return _event;
            }
        }
        if(playerInput!=null) Debug.LogWarning("Action named " + _actionName + " doesn't exist");
        return null;
    }
}
