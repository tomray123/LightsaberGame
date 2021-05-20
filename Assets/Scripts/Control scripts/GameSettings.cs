using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    // Change the device setting on PC if you want to run the game from PC or change this setting on Smartphone.
    public static Device device = Device.PC;

    public enum Device
    {
        PC,
        Smartphone
    }
}
