using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;


public class InputScript : MonoBehaviour
{
    List<InputAction> actionList;
    PlayerInput playerInput;
    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        actionList = new List<InputAction>();
        foreach (InputAction act in playerInput.actions.FindActionMap("Player"))
        {
            actionList.Add(act);
        }
        foreach (InputAction item in actionList)
        {
            Debug.Log(item.name);
        }
    }
}
