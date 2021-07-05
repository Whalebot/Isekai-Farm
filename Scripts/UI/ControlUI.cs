﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    public ControlScheme scheme;
    public GameObject PS4;
    public GameObject XBOX;
    public GameObject keyboard;

    // Start is called before the first frame update
    void Start()
    {

        ChangeInput();
    }

    private void OnEnable()
    {
        InputManager.Instance.controlSchemeChange += ChangeInput;
        ChangeInput();
    }

    private void OnDisable()
    {
        InputManager.Instance.controlSchemeChange -= ChangeInput;
    }


    private void OnValidate()
    {
        UpdateUI();
    }

    void ChangeInput()
    {
        scheme = InputManager.Instance.controlScheme;
        UpdateUI();
    }

    // Update is called once per frame
    void UpdateUI()
    {
        if (PS4 == null)
            print(gameObject);
        PS4.SetActive(false);
        XBOX.SetActive(false);
        keyboard.SetActive(false);
        switch (scheme)
        {
            case ControlScheme.PS4: PS4.SetActive(true); break;
            case ControlScheme.XBOX: XBOX.SetActive(true); break;
            case ControlScheme.MouseAndKeyboard: keyboard.SetActive(true); break;
            default: PS4.SetActive(true); break;
        }
    }
}
