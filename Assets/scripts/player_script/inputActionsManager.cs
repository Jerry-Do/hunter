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
    public InputAction speedUp;
    public InputAction dash;

    public Button[] buttons;
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    [HideInInspector]
    public bool listening = false;
    [HideInInspector]
    public string control_name = "";
    public static InputActionsManager Instance { get; private set; }
    public DefaultInputActions GetDefaultInputActions()
    {
        return defaultInputActions;
    }

    /*public void OnButtonPress()
    {
        SceneManager.LoadScene("SampleScene");
    }*/
    
    private void Awake()
    {
        /*if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }*/
        defaultInputActions = new DefaultInputActions();
        // initialize 
        move = defaultInputActions.Player.Move;
        shoot = defaultInputActions.Player.Fire;
        speedUp = new InputAction("Speed", binding: "<Keyboard>/space");
        dash = new InputAction("Dash", binding: "<Keyboard>/leftShift");

        controls.Add("Up", KeyCode.W);
        controls.Add("Down", KeyCode.S);
        controls.Add("Left", KeyCode.A);
        controls.Add("Right", KeyCode.D);
        controls.Add("shoot", KeyCode.Mouse0);
        
        controls.Add("Dash", KeyCode.LeftShift);
        controls.Add("Speed", KeyCode.Space);

        // using default key bindings if there are no key bindings modified        
        if (PlayerPrefs.GetInt("BindingsModified", 0) == 0)
        {
            ApplyDefaultBindings();
        }
        else
        {
            // load modified key bindings
            LoadBindings();
            //clickFlag = false;
        }
        
        // update button labels
        UpdateButtonLabels();
    }
    // update button labels according to saved bindings
    private void UpdateButtonLabels()
    {
        // For each control, update its corresponding button label with the saved or default key
        foreach (var control in controls)
        {
            foreach (Button button in buttons)
            {
                if (button.name.StartsWith(control.Key, StringComparison.OrdinalIgnoreCase))
                {
                    
                        button.GetComponentInChildren<Text>().text = PlayerPrefs.GetString(control.Key + "Binding");
                        break;
                    
                     // Found the matching button, no need to continue this inner loop
                }
            }
        }
    }
    // initialize default bindings
    private void ApplyDefaultBindings()
    {
        // Set default bindings 
        BindAction("Up", KeyCode.W);
        BindAction("Down", KeyCode.S);
        BindAction("Left", KeyCode.A);
        BindAction("Right", KeyCode.D);
        BindAction("shoot", KeyCode.Mouse0);
        BindAction("Dash", KeyCode.LeftShift);
        BindAction("Speed", KeyCode.Space);
        //BindFireAction("<Mouse>/leftButton"); // default shoot
        //BindOtherActions("Dash", "<Keyboard>/leftShift"); // default dash
        //BindOtherActions("Speed", "<Keyboard>/space"); // default speed up
    }
    // receive key change and update
    public void ChangeControls(string control)
    {
        listening = true;
        control_name = control;
        // listen to key change, update button labels and bind new key binding
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
    // update button label with user input
    private void UpdateButtonText(string controlName, KeyCode key)
    {
        foreach (Button button in buttons)
        {
            if (button.name == controlName + "Button")
            {
                
                    button.GetComponentInChildren<Text>().text = ConvertKeyCodeToPath(key);
                
            }
            
            
        }
    }
    // check if shoot is binding to left click
    public bool IsShootBoundToLeftClick()
    {
        if (PlayerPrefs.GetString("shootBinding") == "<Mouse>/leftButton")
        {
            return true;
        }
        return false;
    }

    // bind actions with key bindings
    public void BindAction(string actionName, KeyCode key)
    {
        InputAction actionToBind;
        if (actionName == "shoot")
        {
            actionToBind = shoot;
        }
        else if (actionName == "Dash")
        {
            actionToBind = dash;
        }
        else if (actionName == "Speed")
        {
            actionToBind = speedUp;
        }
        else
        {
            actionToBind = move;
        }
        // convert key code to key path
        string keyPath = ConvertKeyCodeToPath(key);
        // bind actions
        if (actionToBind == shoot)
        {
            BindFireAction(keyPath);
            
        }
        else if (actionToBind == move)
        {
            BindMoveAction(actionName, keyPath);
        }
        else if (actionToBind == dash)
        {
            BindOtherActions(actionName, keyPath);
        }
        else if (actionToBind == speedUp)
        {
            BindOtherActions(actionName, keyPath);
        }
        // save key bindings
        PlayerPrefs.SetInt("BindingsModified", 1); // Set the flag
        PlayerPrefs.SetString($"{actionName}Binding", keyPath);
        PlayerPrefs.Save();
    }
    
    // retrieve saved bindings if there is modified key binding
    private void LoadBindings()
    {
        if (PlayerPrefs.HasKey("DashBinding"))
        {
            var dashBindingPath = PlayerPrefs.GetString("DashBinding");
            BindOtherActions("Dash", dashBindingPath);
        }
        if (PlayerPrefs.HasKey("SpeedBinding"))
        {
            var speedBindingPath = PlayerPrefs.GetString("Speedinding");
            BindOtherActions("Speed", speedBindingPath);
        }
        if (PlayerPrefs.HasKey("shootBinding"))
        {
            var fireBindingPath = PlayerPrefs.GetString("shootBinding");
            BindFireAction(fireBindingPath);
        }

        if (PlayerPrefs.HasKey("UpBinding"))
        {
            
            var DownBindingPath = PlayerPrefs.GetString("UpBinding");
            BindMoveAction("up", DownBindingPath);
        }

        if (PlayerPrefs.HasKey("DownBinding"))
        {
            
            var DownBindingPath = PlayerPrefs.GetString("DownBinding");
            BindMoveAction("down", DownBindingPath);
        }

        if (PlayerPrefs.HasKey("LeftBinding"))
        {
         
            var LeftBindingPath = PlayerPrefs.GetString("LeftBinding");
            BindMoveAction("left", LeftBindingPath);
        }

        if (PlayerPrefs.HasKey("RightBinding"))
        {
            
            var RightBindingPath = PlayerPrefs.GetString("RightBinding");
            BindMoveAction("right", RightBindingPath);
        }

    }

    // Helper method to convert KeyCode to the path format used by the new Input System
    private string ConvertKeyCodeToPath(KeyCode key)
    {
        if (key == KeyCode.Mouse0) return "<Mouse>/leftButton";
        return $"<Keyboard>/{key.ToString().ToLower()}";
    }
    // change control if there is any key updating
    private void Update()
    {
        if (listening)
        {
            ChangeControls(control_name);
        }
    }
    // Bind other actions not using default input actions
    public void BindOtherActions(string actionName, string key)
    {

        if (actionName == "Dash")
        {
            dash.Disable();
            //dash.ChangeBinding(key);
            //dash.ApplyBindingOverride(key);

            // assgin new key binding to dash
            dash = new InputAction("Dash", binding: key);
            dash.Enable();
        }
        else if (actionName == "Speed")
        {
            speedUp.Disable();
            //speedUp.ChangeBinding(key);
            //speedUp.ApplyBindingOverride(key);

            // assgin new key binding to speedUp
            speedUp = new InputAction("Speed", binding: key);
            speedUp.Enable();
        }

    }
    // save new key bingdings for move
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
    // enable input actions
    public void EnableInputActions()
    {
        //move = defaultInputActions.Player.Move;
        move.Enable();
        //shoot = defaultInputActions.Player.Fire;
        shoot.Enable();
        dash.Enable();
        speedUp.Enable();
    }
    // disable input actions
    public void DisableInputActions()
    {
        move.Disable();
        shoot.Disable();
        dash.Disable();
        speedUp.Disable();
    }
    // save new key bindings for shoot action
    public void BindFireAction(string newPath)
    {
        // Disable the action before modifying bindings
        shoot.Disable();

        // Find and modify bindings as necessary
        for (int i = 0; i < shoot.bindings.Count; i++)
        {
            if (shoot.bindings[i].groups.Contains("Keyboard&Mouse"))
            {
                //shoot.ChangeBinding(i).Erase();
                //shoot.AddBinding(newPath).WithGroup("Keyboard&Mouse");
                shoot.ChangeBinding(i).WithPath(newPath);
                shoot.ApplyBindingOverride(i, newPath);
                break;
            }
        }

        // Enable the action after modifying bindings
        shoot.Enable();
    }
}