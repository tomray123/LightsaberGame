using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public static Device device = Device.PC;

    public enum Device
    {
        PC,
        Smartphone
    }
}
