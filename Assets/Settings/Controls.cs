// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""4af1f215-05bb-4b68-a00f-e9b25af158d6"",
            ""actions"": [
                {
                    ""name"": ""DPad"",
                    ""type"": ""Value"",
                    ""id"": ""041381d5-f3b4-410b-afde-85061bf4b19b"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LAnalog"",
                    ""type"": ""Value"",
                    ""id"": ""36953092-2ca6-4c7c-8b5d-cf8d7388df00"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RAnalog"",
                    ""type"": ""Value"",
                    ""id"": ""a3a34c91-95d9-40a6-911a-7ba4748c945f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""West"",
                    ""type"": ""Button"",
                    ""id"": ""10b16df8-52f5-48b7-8b8d-3369f525fb5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""North"",
                    ""type"": ""Button"",
                    ""id"": ""3e23beaf-6c34-4643-8098-c6cb8605aff1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""South"",
                    ""type"": ""Button"",
                    ""id"": ""76818b20-c109-4764-b173-02960d097c72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""East"",
                    ""type"": ""Button"",
                    ""id"": ""54774646-d672-475a-9b35-c8049445bfa7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Console"",
                    ""type"": ""Button"",
                    ""id"": ""22989894-670a-44a3-96e1-d1a54bd2b205"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""b3668b1b-80a4-450f-93fc-c0b2b77bca18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""1bd8bcb5-3377-458b-8dff-aa9a16f242ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""R1"",
                    ""type"": ""Button"",
                    ""id"": ""e550389c-a3e5-49f6-9023-b64457cd2d40"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""R2"",
                    ""type"": ""Button"",
                    ""id"": ""ab7362c4-96f8-4bbc-8284-be2de846a70a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""L1"",
                    ""type"": ""Button"",
                    ""id"": ""4123b56d-2220-4d36-b5af-78672d7efbcc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""L2"",
                    ""type"": ""Button"",
                    ""id"": ""460ee372-9520-4fae-a1f4-dcb70fb3e693"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""7fc34d75-7caf-45d9-8e87-b1483beb30c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""52d08592-7a32-4256-b19e-4c197aef18fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""808ad2ef-8cd5-4a87-b326-f932e92db1db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""c2374503-f776-4900-9a4f-5f88fb6390ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""L3"",
                    ""type"": ""Button"",
                    ""id"": ""26ea7aed-0fdc-4b86-ad38-0ace4bc1e58b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""R3"",
                    ""type"": ""Button"",
                    ""id"": ""f2443414-c97b-4f81-85e5-6fc3c43f97a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""1"",
                    ""type"": ""Button"",
                    ""id"": ""d2580d76-3bc3-4828-8f62-5fca3a311909"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""2"",
                    ""type"": ""Button"",
                    ""id"": ""df3449ff-f8bd-488c-a4ab-bc1777702e6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""3"",
                    ""type"": ""Button"",
                    ""id"": ""3bf1ec33-8e07-404b-9e56-e670055f723f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""4"",
                    ""type"": ""Button"",
                    ""id"": ""599ed778-e6bf-4c55-a846-df53d8529480"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""Value"",
                    ""id"": ""c651d210-5987-40d2-bf2d-bc7bf959be7a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9fc393b0-34ba-4785-aee7-39e26c54ca69"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""LAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""e54249b6-be24-4669-a9e0-f20f339d077f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LAnalog"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""90cc627b-4ac5-4c77-8579-594f4f984578"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""LAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""00285973-6357-4155-bbad-540d864613b3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""LAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c42a150c-e09f-448b-975c-a85566efc464"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""LAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""622c2202-8fa7-46f2-927a-110627296bd3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""LAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2c2e6319-3a75-458e-ac0e-55c4d1b1cc36"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""South"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e45c6d46-ba4c-4fe5-bdaa-5fd91ebb4cb2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""South"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70748675-6b59-4310-ac69-cb26893c4dfb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""East"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""678134fe-7d0c-4421-ae54-77c8cf99be60"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""East"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b68eddd-39e3-49d2-aa36-1191a563cff1"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""RAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f68bb11f-f9a0-466e-83de-35522247b3a6"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.15,y=0.15)"",
                    ""groups"": ""Mouse"",
                    ""action"": ""RAnalog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25a02832-e4bb-408f-b13f-fc79fa31de29"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""West"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""986016f5-0b5c-4147-85c8-35830a6d7f28"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""West"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed677a22-5573-4c1a-aa31-02ec7aead5cd"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""North"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f077e372-b389-4e63-9021-a868715b1d6f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""North"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec47b2b1-6387-48f2-96dd-3d911d3332f1"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""DPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""dfa79ded-9fa3-4cec-ac31-6239949d3919"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DPad"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""36919d65-2e12-423f-a5f4-3dc371654564"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""DPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""54623999-1bdb-47e7-b066-cf6cd4bb878e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""DPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""01bfb18c-97ed-47b6-a74c-500e35571f5e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""DPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8acec1f8-49ed-4d47-8b66-e72c4b82f463"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""DPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b2ee02dd-de39-4191-afbf-f3d14fad53d8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""150ff40f-5830-4036-9214-5f6e79d3e94e"",
                    ""path"": ""<DualShockGamepad>/touchpadButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""601bc4f0-aa61-4381-b043-7d63815874e6"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d4d6d35-b82a-42de-a9f0-2d25fa8d1f94"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff15e37e-dfb5-4f02-9bc3-e281f608e9c9"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c769b6b4-4017-4ef4-964b-6c2cb89e59b7"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3e83851-8d99-4258-99c5-850be8f6f4bb"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""142af3b5-c3d4-4483-9329-02a164844405"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""R1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a9a6365-28f4-43a4-8ced-28fc0eed4a48"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccfbf668-4aa3-43a7-a46c-7c59d1d6c6d3"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6aad7819-21b6-4046-8053-1d20e9ce2510"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""L1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d86b233b-74a7-4b33-9c7e-6eb93a0417ba"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e428e64d-a93d-4b49-ba0a-f6073eacd2d6"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1131b6e5-1ee8-40f5-8f23-280d03c29dde"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b65c9f3-fd0e-4eae-a9e1-40b1cf82d8d4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84705c03-c270-4e4c-8fbf-e28dc5f0ae0d"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8514f6c0-059c-4b69-884d-84af3570b08a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4d7b12f-62ed-41ee-8221-17084761e002"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa76f8ea-951e-469d-98ea-0535f6a54ec6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae5f1e7d-9d8d-4645-8e92-126b92eb38f0"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7201da97-04cb-41c5-b84c-fbc2fc16d507"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a552506-50d4-4220-b2db-a56a07b3c1c0"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""L3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb58a399-3bd6-4933-9ef6-1a98f7ccf58d"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""R3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a72c130d-45a2-459d-a692-9bc16e1718be"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""R3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a364f15-5e94-4dcb-b0c8-c97bf6e60ff3"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38da77f3-74d5-4479-9cd9-49c2c2f1f36b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0227947-7170-4946-af3e-f72a3eafccda"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6b159e7-5ca4-45ef-af85-5a2e69ccf8b7"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cefff239-3929-4112-9956-be9c75192da7"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default"",
            ""bindingGroup"": ""Default"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_DPad = m_Default.FindAction("DPad", throwIfNotFound: true);
        m_Default_LAnalog = m_Default.FindAction("LAnalog", throwIfNotFound: true);
        m_Default_RAnalog = m_Default.FindAction("RAnalog", throwIfNotFound: true);
        m_Default_West = m_Default.FindAction("West", throwIfNotFound: true);
        m_Default_North = m_Default.FindAction("North", throwIfNotFound: true);
        m_Default_South = m_Default.FindAction("South", throwIfNotFound: true);
        m_Default_East = m_Default.FindAction("East", throwIfNotFound: true);
        m_Default_Console = m_Default.FindAction("Console", throwIfNotFound: true);
        m_Default_Start = m_Default.FindAction("Start", throwIfNotFound: true);
        m_Default_Select = m_Default.FindAction("Select", throwIfNotFound: true);
        m_Default_R1 = m_Default.FindAction("R1", throwIfNotFound: true);
        m_Default_R2 = m_Default.FindAction("R2", throwIfNotFound: true);
        m_Default_L1 = m_Default.FindAction("L1", throwIfNotFound: true);
        m_Default_L2 = m_Default.FindAction("L2", throwIfNotFound: true);
        m_Default_Up = m_Default.FindAction("Up", throwIfNotFound: true);
        m_Default_Left = m_Default.FindAction("Left", throwIfNotFound: true);
        m_Default_Right = m_Default.FindAction("Right", throwIfNotFound: true);
        m_Default_Down = m_Default.FindAction("Down", throwIfNotFound: true);
        m_Default_L3 = m_Default.FindAction("L3", throwIfNotFound: true);
        m_Default_R3 = m_Default.FindAction("R3", throwIfNotFound: true);
        m_Default__1 = m_Default.FindAction("1", throwIfNotFound: true);
        m_Default__2 = m_Default.FindAction("2", throwIfNotFound: true);
        m_Default__3 = m_Default.FindAction("3", throwIfNotFound: true);
        m_Default__4 = m_Default.FindAction("4", throwIfNotFound: true);
        m_Default_ScrollWheel = m_Default.FindAction("ScrollWheel", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_DPad;
    private readonly InputAction m_Default_LAnalog;
    private readonly InputAction m_Default_RAnalog;
    private readonly InputAction m_Default_West;
    private readonly InputAction m_Default_North;
    private readonly InputAction m_Default_South;
    private readonly InputAction m_Default_East;
    private readonly InputAction m_Default_Console;
    private readonly InputAction m_Default_Start;
    private readonly InputAction m_Default_Select;
    private readonly InputAction m_Default_R1;
    private readonly InputAction m_Default_R2;
    private readonly InputAction m_Default_L1;
    private readonly InputAction m_Default_L2;
    private readonly InputAction m_Default_Up;
    private readonly InputAction m_Default_Left;
    private readonly InputAction m_Default_Right;
    private readonly InputAction m_Default_Down;
    private readonly InputAction m_Default_L3;
    private readonly InputAction m_Default_R3;
    private readonly InputAction m_Default__1;
    private readonly InputAction m_Default__2;
    private readonly InputAction m_Default__3;
    private readonly InputAction m_Default__4;
    private readonly InputAction m_Default_ScrollWheel;
    public struct DefaultActions
    {
        private @Controls m_Wrapper;
        public DefaultActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @DPad => m_Wrapper.m_Default_DPad;
        public InputAction @LAnalog => m_Wrapper.m_Default_LAnalog;
        public InputAction @RAnalog => m_Wrapper.m_Default_RAnalog;
        public InputAction @West => m_Wrapper.m_Default_West;
        public InputAction @North => m_Wrapper.m_Default_North;
        public InputAction @South => m_Wrapper.m_Default_South;
        public InputAction @East => m_Wrapper.m_Default_East;
        public InputAction @Console => m_Wrapper.m_Default_Console;
        public InputAction @Start => m_Wrapper.m_Default_Start;
        public InputAction @Select => m_Wrapper.m_Default_Select;
        public InputAction @R1 => m_Wrapper.m_Default_R1;
        public InputAction @R2 => m_Wrapper.m_Default_R2;
        public InputAction @L1 => m_Wrapper.m_Default_L1;
        public InputAction @L2 => m_Wrapper.m_Default_L2;
        public InputAction @Up => m_Wrapper.m_Default_Up;
        public InputAction @Left => m_Wrapper.m_Default_Left;
        public InputAction @Right => m_Wrapper.m_Default_Right;
        public InputAction @Down => m_Wrapper.m_Default_Down;
        public InputAction @L3 => m_Wrapper.m_Default_L3;
        public InputAction @R3 => m_Wrapper.m_Default_R3;
        public InputAction @_1 => m_Wrapper.m_Default__1;
        public InputAction @_2 => m_Wrapper.m_Default__2;
        public InputAction @_3 => m_Wrapper.m_Default__3;
        public InputAction @_4 => m_Wrapper.m_Default__4;
        public InputAction @ScrollWheel => m_Wrapper.m_Default_ScrollWheel;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @DPad.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDPad;
                @DPad.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDPad;
                @DPad.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDPad;
                @LAnalog.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnLAnalog;
                @LAnalog.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnLAnalog;
                @LAnalog.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnLAnalog;
                @RAnalog.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRAnalog;
                @RAnalog.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRAnalog;
                @RAnalog.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRAnalog;
                @West.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnWest;
                @West.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnWest;
                @West.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnWest;
                @North.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnNorth;
                @North.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnNorth;
                @North.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnNorth;
                @South.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSouth;
                @South.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSouth;
                @South.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSouth;
                @East.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnEast;
                @East.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnEast;
                @East.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnEast;
                @Console.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnConsole;
                @Console.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnConsole;
                @Console.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnConsole;
                @Start.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnStart;
                @Select.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSelect;
                @R1.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR1;
                @R1.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR1;
                @R1.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR1;
                @R2.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR2;
                @R2.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR2;
                @R2.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR2;
                @L1.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL1;
                @L1.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL1;
                @L1.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL1;
                @L2.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL2;
                @L2.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL2;
                @L2.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL2;
                @Up.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnUp;
                @Left.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnRight;
                @Down.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnDown;
                @L3.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL3;
                @L3.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL3;
                @L3.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnL3;
                @R3.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR3;
                @R3.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR3;
                @R3.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnR3;
                @_1.started -= m_Wrapper.m_DefaultActionsCallbackInterface.On_1;
                @_1.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.On_1;
                @_1.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.On_1;
                @_2.started -= m_Wrapper.m_DefaultActionsCallbackInterface.On_2;
                @_2.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.On_2;
                @_2.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.On_2;
                @_3.started -= m_Wrapper.m_DefaultActionsCallbackInterface.On_3;
                @_3.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.On_3;
                @_3.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.On_3;
                @_4.started -= m_Wrapper.m_DefaultActionsCallbackInterface.On_4;
                @_4.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.On_4;
                @_4.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.On_4;
                @ScrollWheel.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnScrollWheel;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DPad.started += instance.OnDPad;
                @DPad.performed += instance.OnDPad;
                @DPad.canceled += instance.OnDPad;
                @LAnalog.started += instance.OnLAnalog;
                @LAnalog.performed += instance.OnLAnalog;
                @LAnalog.canceled += instance.OnLAnalog;
                @RAnalog.started += instance.OnRAnalog;
                @RAnalog.performed += instance.OnRAnalog;
                @RAnalog.canceled += instance.OnRAnalog;
                @West.started += instance.OnWest;
                @West.performed += instance.OnWest;
                @West.canceled += instance.OnWest;
                @North.started += instance.OnNorth;
                @North.performed += instance.OnNorth;
                @North.canceled += instance.OnNorth;
                @South.started += instance.OnSouth;
                @South.performed += instance.OnSouth;
                @South.canceled += instance.OnSouth;
                @East.started += instance.OnEast;
                @East.performed += instance.OnEast;
                @East.canceled += instance.OnEast;
                @Console.started += instance.OnConsole;
                @Console.performed += instance.OnConsole;
                @Console.canceled += instance.OnConsole;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @R1.started += instance.OnR1;
                @R1.performed += instance.OnR1;
                @R1.canceled += instance.OnR1;
                @R2.started += instance.OnR2;
                @R2.performed += instance.OnR2;
                @R2.canceled += instance.OnR2;
                @L1.started += instance.OnL1;
                @L1.performed += instance.OnL1;
                @L1.canceled += instance.OnL1;
                @L2.started += instance.OnL2;
                @L2.performed += instance.OnL2;
                @L2.canceled += instance.OnL2;
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @L3.started += instance.OnL3;
                @L3.performed += instance.OnL3;
                @L3.canceled += instance.OnL3;
                @R3.started += instance.OnR3;
                @R3.performed += instance.OnR3;
                @R3.canceled += instance.OnR3;
                @_1.started += instance.On_1;
                @_1.performed += instance.On_1;
                @_1.canceled += instance.On_1;
                @_2.started += instance.On_2;
                @_2.performed += instance.On_2;
                @_2.canceled += instance.On_2;
                @_3.started += instance.On_3;
                @_3.performed += instance.On_3;
                @_3.canceled += instance.On_3;
                @_4.started += instance.On_4;
                @_4.performed += instance.On_4;
                @_4.canceled += instance.On_4;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    private int m_DefaultSchemeIndex = -1;
    public InputControlScheme DefaultScheme
    {
        get
        {
            if (m_DefaultSchemeIndex == -1) m_DefaultSchemeIndex = asset.FindControlSchemeIndex("Default");
            return asset.controlSchemes[m_DefaultSchemeIndex];
        }
    }
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface IDefaultActions
    {
        void OnDPad(InputAction.CallbackContext context);
        void OnLAnalog(InputAction.CallbackContext context);
        void OnRAnalog(InputAction.CallbackContext context);
        void OnWest(InputAction.CallbackContext context);
        void OnNorth(InputAction.CallbackContext context);
        void OnSouth(InputAction.CallbackContext context);
        void OnEast(InputAction.CallbackContext context);
        void OnConsole(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnR1(InputAction.CallbackContext context);
        void OnR2(InputAction.CallbackContext context);
        void OnL1(InputAction.CallbackContext context);
        void OnL2(InputAction.CallbackContext context);
        void OnUp(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnL3(InputAction.CallbackContext context);
        void OnR3(InputAction.CallbackContext context);
        void On_1(InputAction.CallbackContext context);
        void On_2(InputAction.CallbackContext context);
        void On_3(InputAction.CallbackContext context);
        void On_4(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
    }
}
