using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventPlayer : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _voidEventChannel = default;
    private AudioCue audioCue;

    public void Awake()
    {
        audioCue = GetComponent<AudioCue>();
    }

    private void OnEnable()
    {
        if (audioCue)
        {
            _voidEventChannel.onVoidEvent += audioCue.PlayAudioCue;
        }
    }

    private void OnDisable()
    {
        if (audioCue)
        {
            _voidEventChannel.onVoidEvent -= audioCue.PlayAudioCue;
        }  
    }
}
