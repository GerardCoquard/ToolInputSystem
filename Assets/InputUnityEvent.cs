using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputUnityEvent : UnityEvent<InputAction.CallbackContext>
{
    public string actionName;
    bool _enabled;
    public UnityEvent startedAction;
    public UnityEvent performedAction;
    public UnityEvent canceledAction;

    public InputUnityEvent(InputAction _action)
    {
        actionName = _action.name;
        _enabled = true;
        _action.started += InvokeEvent;
        _action.started += InvokeStartedEvent;
        _action.performed += InvokeEvent;
        _action.performed += InvokePerformedEvent;
        _action.canceled += InvokeEvent;
        _action.canceled += InvokeCanceledEvent;
    }
    void InvokeEvent(InputAction.CallbackContext context)
    {
        if(_enabled) this?.Invoke(context);
    }
    void InvokeStartedEvent(InputAction.CallbackContext context)
    {
        if(_enabled) startedAction?.Invoke();
    }
    void InvokePerformedEvent(InputAction.CallbackContext context)
    {
        if(_enabled) performedAction?.Invoke();
    }
    void InvokeCanceledEvent(InputAction.CallbackContext context)
    {
        if(_enabled) canceledAction?.Invoke();
    }
    public void SetEnabled(bool enable)
    {
        _enabled = enable;
    }
    public void ClearListeners()
    {
        this.RemoveAllListeners();
        startedAction.RemoveAllListeners();
        performedAction.RemoveAllListeners();
        canceledAction.RemoveAllListeners();
    }
}
