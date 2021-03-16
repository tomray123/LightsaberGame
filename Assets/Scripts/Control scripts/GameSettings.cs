using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public static Device device = Device.Smartphone;

    public enum Device
    {
        PC,
        Smartphone
    }
}
