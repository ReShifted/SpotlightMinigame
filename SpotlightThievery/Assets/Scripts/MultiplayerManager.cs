using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement[] players = new PlayerMovement[4];
    [SerializeField] private int keyboardSlot = 0;
    [SerializeField] private bool allowKeyboardSwitching = true;

    private InputDevice[] slotDevices;

    private void Start()
    {
        slotDevices = new InputDevice[players.Length];

        if (keyboardSlot >= 0 && keyboardSlot < players.Length && Keyboard.current != null)
        {
            slotDevices[keyboardSlot] = Keyboard.current;
            players[keyboardSlot].AssignDevice(Keyboard.current);
        }

        foreach (var pad in Gamepad.all)
            AssignToFreeSlot(pad);

        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDestroy()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void Update()
    {
        if (!allowKeyboardSwitching || Keyboard.current == null) return;

        var kb = Keyboard.current;

        if (kb.tabKey.wasPressedThisFrame)
        {
            int current = Array.IndexOf(slotDevices, kb);
            SwitchKeyboardTo((current + 1) % players.Length);
        }
        else if (kb.digit1Key.wasPressedThisFrame) SwitchKeyboardTo(0);
        else if (kb.digit2Key.wasPressedThisFrame) SwitchKeyboardTo(1);
        else if (kb.digit3Key.wasPressedThisFrame) SwitchKeyboardTo(2);
        else if (kb.digit4Key.wasPressedThisFrame) SwitchKeyboardTo(3);
    }

    private void SwitchKeyboardTo(int target)
    {
        if (target < 0 || target >= players.Length) return;

        var kb = Keyboard.current;
        int old = Array.IndexOf(slotDevices, kb);
        if (old == target) return;

        InputDevice displaced = slotDevices[target];

        if (old >= 0)
        {
            slotDevices[old] = null;
            players[old].AssignDevice(null);
        }

        slotDevices[target] = kb;
        players[target].AssignDevice(kb);

        if (displaced != null)
        {
            if (old >= 0)
            {
                slotDevices[old] = displaced;
                players[old].AssignDevice(displaced);
            }
            else
            {
                AssignToFreeSlot(displaced);
            }
        }

        Debug.Log($"Keyboard now controls {players[target].name}");
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is not Gamepad) return;

        switch (change)
        {
            case InputDeviceChange.Added:
            case InputDeviceChange.Reconnected:
                AssignToFreeSlot(device);
                break;

            case InputDeviceChange.Removed:
            case InputDeviceChange.Disconnected:
                FreeSlot(device);
                break;
        }
    }

    private void AssignToFreeSlot(InputDevice device)
    {
        if (Array.IndexOf(slotDevices, device) >= 0) return;

        for (int i = 0; i < players.Length; i++)
        {
            if (slotDevices[i] == null)
            {
                slotDevices[i] = device;
                players[i].AssignDevice(device);
                return;
            }
        }
    }

    private void FreeSlot(InputDevice device)
    {
        int i = Array.IndexOf(slotDevices, device);
        if (i < 0) return;

        slotDevices[i] = null;
        players[i].AssignDevice(null);
    }
}