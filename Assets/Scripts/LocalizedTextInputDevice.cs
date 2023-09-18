using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class LocalizedTextInputDevice : MonoBehaviour
{
    public TMP_Text text;
    public LocalizedString GamepadString;
    public LocalizedString KeyboardString;

    private string DeviceType = "Keyboard",_DeviceType;
    private InputDevice currentDevice;


    private void OnEnable()
    {
        //Sets locale text when enable in scene for first time.
        setLocale(DeviceType);

        InputSystem.onEvent += OnInputEvent;
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged; 
    }
    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
    }



    //Subscription: LocalizationSettings.SelectedLocaleChanged
    private void OnLocaleChanged(Locale _newLocale)
    {
        setLocale(DeviceType);
    }

    //Subscription: InputSystem.onEvent
    private void OnInputEvent(InputEventPtr eventPtr,InputDevice device)
    {
        if(currentDevice == null || eventPtr.time > currentDevice.lastUpdateTime)
        {
            currentDevice = device;
            _DeviceType = SetDeviceType(currentDevice);

            if(currentDevice != null && DeviceType != _DeviceType)
            {
                DeviceType = _DeviceType;
                setLocale(DeviceType);
            }
        }
    }


    private string SetDeviceType(InputDevice _device)
    {
        if(_device is Gamepad)
        {
            return "Gamepad";
        }
        else if(_device is Keyboard || _device is Mouse) //Keyboard and Mouse using at the same time
        {
            return "Keyboard";
        }
        else //For Unexpected errors
        {
            return "Unknown";
        }
       
    }

    //Takes DeviceType as a parameter and then changes the text depending on Device Type.
    private void setLocale(string _deviceType)
    {
        if(_deviceType == "Keyboard")
        {
            text.text = KeyboardString.GetLocalizedString();
        }
        else if(_deviceType == "Gamepad")
        {
            text.text = GamepadString.GetLocalizedString();
        }
       
    }
}
