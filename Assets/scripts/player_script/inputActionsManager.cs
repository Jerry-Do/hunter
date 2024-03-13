using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputActionsManager : MonoBehaviour
{
    public DefaultInputActions defaultInputActions;

    // Expose the actions for external access
    public InputAction move;
    public InputAction shoot;
    public Button[] buttons;

    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    [HideInInspector]
    public bool listening = false;
    [HideInInspector]
    public string control_name = "";

    public DefaultInputActions GetDefaultInputActions()
    {
        return defaultInputActions;
    }
    public void OnButtonPress()
    {
        //LoadBindings(); // Assumes LoadBindings() is your method to load and apply saved bindings
        //EnableInputActions(); // Make sure your input actions are enabled
        //Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
        /*for (int i = 0; i < move.bindings.Count; i++)
        {

            Debug.Log(move.bindings[i]);

        }*/
    }
    
    private void Awake()
    {
        defaultInputActions = new DefaultInputActions();
        move = defaultInputActions.Player.Move;
        shoot = defaultInputActions.Player.Fire;
        controls.Add("Up", KeyCode.W);
        controls.Add("Down", KeyCode.S);
        controls.Add("Left", KeyCode.A);
        controls.Add("Right", KeyCode.D);
        controls.Add("fire", KeyCode.Mouse0);
        if (PlayerPrefs.GetInt("BindingsModified", 0) == 0)
        {
            ApplyDefaultBindings();
        }
        else
        {
            LoadBindings();
        }
        PlayerPrefs.SetInt("BindingsModified", 0); // Reset the flag for the next session
        PlayerPrefs.Save();
        UpdateButtonLabels();
    }
    private void UpdateButtonLabels()
    {
        // For each control, update its corresponding button label with the saved or default key
        foreach (var control in controls)
        {
            foreach (Button button in buttons)
            {
                if (button.name.StartsWith(control.Key, StringComparison.OrdinalIgnoreCase))
                {
                    button.GetComponentInChildren<Text>().text = control.Value.ToString();
                    break; // Found the matching button, no need to continue this inner loop
                }
            }
        }
    }
    private void ApplyDefaultBindings()
    {
        // Set default bindings here, e.g., for WASD and arrow keys
        BindAction("Up", KeyCode.W);
        BindAction("Down", KeyCode.S);
        BindAction("Left", KeyCode.A);
        BindAction("Right", KeyCode.D);
        BindFireAction("<Mouse>/leftButton");
    }

    public void ChangeControls(string control)
    {
        listening = true;
        control_name = control;
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                controls[control] = key;
                listening = false;
                // Update the button text
                UpdateButtonText(control_name, key);
                // Rebind the action based on the new key
                BindAction(control_name, key);
                break;
            }
        }

    }
    private void UpdateButtonText(string controlName, KeyCode key)
    {
        foreach (Button button in buttons)
        {
            if (button.name == controlName + "Button")
            {
                if (key.ToString() == "Mouse0")
                {
                    button.GetComponentInChildren<Text>().text = "Left Click";
                }
                else
                {
                    button.GetComponentInChildren<Text>().text = key.ToString();
                }
            }
            else if (controlName == "fire")
            {
                if (key.ToString() == "Mouse0")
                {
                    button.GetComponentInChildren<Text>().text = "Left Click";
                }
                else
                {
                    button.GetComponentInChildren<Text>().text = key.ToString();
                }
            }
        }
    }
    // This method now takes KeyCode and converts it to the correct path
    public void BindAction(string actionName, KeyCode key)
    {
        InputAction actionToBind = actionName == "fire" ? shoot : move; // Simplified example, you'd likely need more logic here
        string keyPath = ConvertKeyCodeToPath(key);
        if (actionToBind == shoot)
        {
            BindFireAction(keyPath);
        }
        if (actionToBind == move)
        {
            BindMoveAction(actionName, keyPath);
        }
        PlayerPrefs.SetInt("BindingsModified", 1); // Set the flag
        PlayerPrefs.SetString($"{actionName}Binding", keyPath);
        PlayerPrefs.Save();
    }
    private void LoadBindings()
    {
        if (PlayerPrefs.HasKey("fireBinding"))
        {
            var fireBindingPath = PlayerPrefs.GetString("fireBinding");
            BindFireAction(fireBindingPath);
        }

        if (PlayerPrefs.HasKey("UpBinding"))
        {
            
            // Load the saved binding
            var DownBindingPath = PlayerPrefs.GetString("UpBinding");
            BindMoveAction("up", DownBindingPath);
        }

        if (PlayerPrefs.HasKey("DownBinding"))
        {
            
            // Load the saved binding
            var DownBindingPath = PlayerPrefs.GetString("DownBinding");
            BindMoveAction("down", DownBindingPath);
        }

        if (PlayerPrefs.HasKey("LeftBinding"))
        {
         
            // Load the saved binding
            var LeftBindingPath = PlayerPrefs.GetString("LeftBinding");
            BindMoveAction("left", LeftBindingPath);
        }

        if (PlayerPrefs.HasKey("RightBinding"))
        {
            
            // Load the saved binding
            var RightBindingPath = PlayerPrefs.GetString("RightBinding");
            BindMoveAction("right", RightBindingPath);
        }

    }

    // Helper method to convert KeyCode to the path format used by the new Input System
    private string ConvertKeyCodeToPath(KeyCode key)
    {
        if (key == KeyCode.Mouse0) return "<Mouse>/leftButton";
        // Add more conversions here as needed
        return $"<Keyboard>/{key.ToString().ToLower()}";
    }
    private void Update()
    {
        if (listening)
        {
            ChangeControls(control_name);
        }
    }
    public void BindMoveAction(string direction, string keyPath)
    {
        // Disable the move action to modify bindings
        move.Disable();
        
        for (int i = 0; i < move.bindings.Count; i++)
        {
            
                if (!move.bindings[i].path.Contains("Arrow") && move.bindings[i].name == direction.ToLower())
                {
                    move.ChangeBinding(i).WithPath(keyPath);
                    move.ApplyBindingOverride(i, keyPath);
                }
            
        }
        // Re-enable the move action
        move.Enable();
        // Check each binding in the move action

    }

    public void EnableInputActions()
    {
        //move = defaultInputActions.Player.Move;
        move.Enable();
        //shoot = defaultInputActions.Player.Fire;
        shoot.Enable();
    }

    public void DisableInputActions()
    {
        move.Disable();
        shoot.Disable();
    }

    public void BindFireAction(string newPath)
    {
        // Disable the action before modifying bindings
        shoot.Disable();

        // Find and modify bindings as necessary
        for (int i = 0; i < shoot.bindings.Count; i++)
        {
            if (shoot.bindings[i].groups.Contains("Keyboard&Mouse"))
            {
                shoot.ChangeBinding(i).Erase();
                shoot.AddBinding(newPath).WithGroup("Keyboard&Mouse");
                shoot.ApplyBindingOverride(i, newPath);
                break;
            }
        }

        // Enable the action after modifying bindings
        shoot.Enable();
    }
}