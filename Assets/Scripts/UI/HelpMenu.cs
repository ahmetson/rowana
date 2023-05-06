using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    private InputMaster controls;
    private TextMeshProUGUI help;
    private bool enable = false;

    private void Awake()
    {
        controls = new InputMaster();
        help = GetComponentInChildren<TextMeshProUGUI>();
        ShowBinds();
    }
    private void Update()
    {
        if (controls.Menu.Help.triggered)
        {
            ShowBinds();
        }  
    }
    public void ShowBinds()
    {    
        if (enable)
        {
            enable = false;
            help.text = string.Format("Move - {0}", controls.Player.Movement.GetBindingDisplayString());
            help.text += string.Format("\r\nSprint - {0}", controls.Player.Sprint.GetBindingDisplayString());
            help.text += string.Format("\r\nJump - {0}", controls.Player.Jump.GetBindingDisplayString());
            help.text += string.Format("\r\nCrouch - {0}", controls.Player.Crouch.GetBindingDisplayString());
            help.text += string.Format("\r\nChange item - {0},{1}", controls.Inventory._1.GetBindingDisplayString(), controls.Inventory._2.GetBindingDisplayString());
            help.text += string.Format("\r\nOther hand - {0}", controls.Player.Swap.GetBindingDisplayString());
            help.text += string.Format("\r\nSetting Crystal - {0}", controls.Inventory.SettingsCrystal.GetBindingDisplayString());
            help.text += string.Format("\r\nMenu - {0}", controls.Menu.Escape.GetBindingDisplayString());
            help.text += string.Format("\r\n\nHelp - {0}", controls.Menu.Help.GetBindingDisplayString());
        }
        else
        {
            enable = true;
            help.text = string.Format("Help - {0}", controls.Menu.Help.GetBindingDisplayString());
        }
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
