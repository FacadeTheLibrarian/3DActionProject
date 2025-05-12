//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.InputSystem.Utilities;

//internal sealed class MultiControllerManager : MonoBehaviour {

//    private int _currentConnectedControllers = 0;
//    private List<>;
//    private void Awake() {

//    }

//    private void OnDeviceChange(InputDevice device, InputDeviceChange change) {

//    }
//}

//internal sealed class DummyInput : IController {
//    public DummyInput() { }
//    public Vector2 GetLeftStickInput() {
//        return Vector2.zero;
//    }
//    public bool IsNorthPressed() {
//        return false;
//    }
//    public bool IsSouthPressed() {
//        return false;
//    }
//    public bool IsEastPressed() {
//        return false;
//    }
//    public bool IsWestPressed() {
//        return false;
//    }
//}

//internal sealed class GamepadInput : IController {
//    public GamepadInput(in Gamepad handler) {

//    }
//    public Vector2 GetLeftStickInput();
//    public bool IsNorthPressed();
//    public bool IsSouthPressed();
//    public bool IsEastPressed();
//    public bool IsWestPressed();
//}

//internal interface IController {
//    public Vector2 GetLeftStickInput();
//    public bool IsNorthPressed();
//    public bool IsSouthPressed();
//    public bool IsEastPressed();
//    public bool IsWestPressed();
//}