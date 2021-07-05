﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Controls;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public ControlScheme controlScheme = ControlScheme.PS4;
    public delegate void InputEvent();

    public Controls controls = null;

    public List<int> inputQueue;
    public float bufferWindow;

    public InputEvent controlSchemeChange;
    public InputEvent keyboardEvent;
    public InputEvent gamepadEvent;

    public InputEvent westInput;
    public InputEvent northInput;
    public InputEvent eastInput;
    public InputEvent southInput;

    public InputEvent westRelease;
    public InputEvent northRelease;
    public InputEvent southRelease;
    public InputEvent eastRelease;

    public InputEvent startInput;
    public InputEvent selectInput;
    public InputEvent R1input;
    public InputEvent R1release;
    public InputEvent R2input;
    public InputEvent R2release;
    public InputEvent L1input;
    public InputEvent L2input;
    public InputEvent L2release;
    public InputEvent R3input;
    public InputEvent R3Right;
    public InputEvent R3Left;
    public InputEvent touchPadInput;
    public InputEvent leftInput;
    public InputEvent rightInput;

    [HideInInspector] public bool R1Hold;
    [HideInInspector] public bool R2Hold;
    [HideInInspector] public bool L1Hold;
    [HideInInspector] public bool L2Hold;

    public Vector2 inputDirection;
    public Vector2 lookDirection;
    public Vector2 mousePosition;
    public Vector2 mouseScroll;

    public bool dodgeTap;
    public int tapFrames = 10;
    int tapCounter;

    public bool debug;
    float lastTapTime;
    bool canTap;

    private void OnEnable() => controls.Default.Enable();
    private void OnDisable() => controls.Default.Disable();

    void Awake()
    {
        Instance = this;
        DataManager.Instance.loadDataEvent += LoadScheme;
        DataManager.Instance.saveDataEvent += SaveScheme;
        controls = new Controls();
        // controls.MouseScheme.

        controls.Default.West.performed += context => OnWest(context);
        controls.Default.West.canceled += context => OnWest(context);
        controls.Default.North.performed += context => OnNorth(context);
        controls.Default.North.canceled += context => OnNorth(context);
        controls.Default.South.performed += context => OnSouth(context);
        controls.Default.South.canceled += _ => OnSouthRelease();
        controls.Default.East.performed += context => OnEast(context);

        controls.Default.Up.performed += _ => OnUp();
        controls.Default.Left.performed += _ => OnLeft();
        controls.Default.Right.performed += _ => OnRight();
        controls.Default.Down.performed += _ => OnDown();

        controls.Default.R1.performed += context => OnR1(context);
        controls.Default.R1.canceled += _ => OnR1Release();

        controls.Default.R2.performed += _ => OnR2Press();
        controls.Default.R2.canceled += _ => OnR2Release();

        controls.Default.L1.performed += context => OnL1(context);
        controls.Default.L1.canceled += _ => OnL1Release();

        controls.Default.L2.performed += _ => OnL2Press();
        controls.Default.L2.canceled += _ => OnL2Release();

        controls.Default.R3.performed += context => OnR3();

        controls.Default.Start.performed += _ => OnStart();
        controls.Default.Select.performed += _ => OnSelect();

        controls.Default.Console.performed += _ => OnTouchPad();

        controls.Default._1.performed += _ => On1();
        controls.Default._2.performed += _ => On2();
        controls.Default._3.performed += _ => On3();
        controls.Default._4.performed += _ => On4();
    }

    void LoadScheme()
    {
        controlScheme = DataManager.Instance.currentSaveData.settings.controls;
    }

    void SaveScheme()
    {
        DataManager.Instance.currentSaveData.settings.controls = controlScheme;
    }

    private void OnLeft()
    {
        if (debug) print("Left");
        //StartCoroutine("InputBuffer", 7);
        leftInput?.Invoke();
    }

    private void OnUp()
    {
        if (debug) print("Up");
        // StartCoroutine("InputBuffer", 8);
    }
    private void OnDown()
    {
        if (debug) print("Down");
        //StartCoroutine("InputBuffer", 9);
    }
    private void OnRight()
    {
        if (debug) print("Right");
        rightInput?.Invoke();
        // StartCoroutine("InputBuffer", 10);
    }

    private void Update()
    {
        if (GameManager.isPaused || GameManager.inventoryMenuOpen)
        {
            inputDirection = Vector2.zero;
            lookDirection = Vector2.zero;
            return;
        }

        inputDirection = controls.Default.LAnalog.ReadValue<Vector2>();
        lookDirection = controls.Default.RAnalog.ReadValue<Vector2>();
        mouseScroll = controls.Default.ScrollWheel.ReadValue<Vector2>();

        if (mouseScroll.y > 0) { OnLeft(); }
        if (mouseScroll.y < 0) { OnRight(); }

        if (canTap && lastTapTime + 0.1F < Time.time)
        {
            if (lookDirection.x > 0.5F) RAnalogTapRight();
            else if (lookDirection.x < -0.5F) RAnalogTapLeft();
        }
        else if (Mathf.Abs(lookDirection.x) < 0.1F) canTap = true;
    }

    void ChangeControlScheme(InputAction.CallbackContext context)
    {

        if (context.control.device == Gamepad.current)
        {
            if (controlScheme == ControlScheme.MouseAndKeyboard) gamepadEvent?.Invoke();
            if (Gamepad.current.name.Contains("Dual"))
            {
                //     if (controlScheme != ControlScheme.PS4)

                controlScheme = ControlScheme.PS4;
                controlSchemeChange?.Invoke();
            }

            else
            {

                if (controlScheme != ControlScheme.XBOX)
                    controlSchemeChange?.Invoke();
                controlScheme = ControlScheme.XBOX;

            }
            //print(Gamepad.current.name);
            //print(Gamepad.current.device);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            //if (controlScheme != ControlScheme.MouseAndKeyboard)

            controlScheme = ControlScheme.MouseAndKeyboard;
            controlSchemeChange?.Invoke();
            keyboardEvent?.Invoke();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        if (R1Hold)
        {
            tapCounter++;
        }
    }

    void OnTouchPad()
    {
        touchPadInput?.Invoke();
    }

    void RAnalogTapRight()
    {
        canTap = false;
        lastTapTime = Time.time;
        R3Right?.Invoke();
    }

    void RAnalogTapLeft()
    {
        canTap = false;
        lastTapTime = Time.time;
        R3Left?.Invoke();
    }

    public void OnLAnalog(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }
    public void OnRAnalog(InputValue value)
    {
        lookDirection = value.Get<Vector2>();
    }

    public void OnWest(InputAction.CallbackContext context)
    {
        if (debug)
            print("Square");

        ChangeControlScheme(context);
        if (context.canceled) return;

        if (!GameManager.isPaused && !GameManager.menuOpen)
        {
            if (R2Hold)
            {
                StartCoroutine("InputBuffer", 7);
            }
            else
                StartCoroutine("InputBuffer", 1);
        }
        westInput?.Invoke();
    }

    public void OnNorth(InputAction.CallbackContext context)
    {
        if (debug) print("Triangle");
        ChangeControlScheme(context);
        if (context.canceled) return;

        if (!GameManager.isPaused && !GameManager.menuOpen)
        {
            if (R2Hold)
            { StartCoroutine("InputBuffer", 8); }
            else
                StartCoroutine("InputBuffer", 2);
        }
        northInput?.Invoke();
    }
    public void OnSouth(InputAction.CallbackContext context)
    {
        ChangeControlScheme(context);
        if (!GameManager.isPaused && !GameManager.menuOpen)
        {
            if (R2Hold)
            { StartCoroutine("InputBuffer", 9); }
            else
                StartCoroutine("InputBuffer", 3);
        }

        if (debug) print("X");
        southInput?.Invoke();
    }

    public void OnEast(InputAction.CallbackContext context)
    {
        ChangeControlScheme(context);
        if (!GameManager.isPaused && !GameManager.menuOpen)
        {
            if (R2Hold)
            { StartCoroutine("InputBuffer", 10); }
            else
                StartCoroutine("InputBuffer", 4);
        }

        if (debug) print("O");
        eastInput?.Invoke();
    }

    public void OnStart()
    {
        startInput?.Invoke();
    }

    public void OnSelect()
    {
        selectInput?.Invoke();
    }

    public void OnR1(InputAction.CallbackContext context)
    {
        if (debug) print("R1");
        R1input?.Invoke();
        tapCounter = 0;
        R1Hold = true;
        if (!dodgeTap)
            StartCoroutine("InputBuffer", 5);
    }

    void OnR1Release()
    {
        R1release?.Invoke();
        if (dodgeTap)
            if (tapCounter < tapFrames)
            {
                StartCoroutine("InputBuffer", 5);
            }
        R1Hold = false;
    }

    void OnSouthRelease()
    {
        southRelease?.Invoke();
        R1Hold = false;
    }

    void OnWestRelease()
    {
        westRelease?.Invoke();
    }

    void OnNorthRelease()
    {
        northRelease?.Invoke();

    }

    void OnEastRelease()
    {
        eastRelease?.Invoke();

    }


    public void OnL1(InputAction.CallbackContext context)
    {
        //print(context.ReadValueAsButton());
        if (debug) print("L1");
        L1Hold = true;
        L1input?.Invoke();
        StartCoroutine("InputBuffer", 6);
    }

    void OnR2Press()
    {
        if (R2Hold) return;
        R2Hold = true;
        R2input?.Invoke();
    }

    void OnR2Release()
    {
        if (!R2Hold) return;
        R2Hold = false;
        R2release?.Invoke();
    }

    void OnL2Press()
    {
        if (L2Hold) return;
        L2Hold = true;
        L2input?.Invoke();
    }

    void OnL2Release()
    {
        if (!L2Hold) return;
        L2release?.Invoke();
        L2Hold = false;
    }

    void OnL1Release()
    {
        L1Hold = false;
    }

    void OnR3()
    {
        R3input?.Invoke();
    }

    void On1()
    {
        if (GameManager.isPaused) return;
        StartCoroutine("InputBuffer", 7);
    }
    void On2()
    {
        if (GameManager.isPaused) return;
        StartCoroutine("InputBuffer", 8);
    }
    void On3()
    {
        if (GameManager.isPaused) return;
        StartCoroutine("InputBuffer", 9);
    }
    void On4()
    {
        if (GameManager.isPaused) return;
        StartCoroutine("InputBuffer", 10);
    }

    IEnumerator InputBuffer(int inputID)
    {
        // if (GameManager.isPaused) yield break;
        inputQueue.Add(inputID);
        for (int i = 0; i < bufferWindow; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        if (inputQueue.Count > 0)
        {
            if (inputQueue[0] == inputID)
                inputQueue.RemoveAt(0);
        }
    }
}

public enum ControlScheme { PS4, XBOX, MouseAndKeyboard }
