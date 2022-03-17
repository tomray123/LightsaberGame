using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShootEventPlayer : MonoBehaviour
{
    [SerializeField] private ShootEnumEventChannelSO _onShootEventChannel = default;
    [SerializeField] private ShootType shootType;
    private AudioCue audioCue;

    public void Awake()
    {
        audioCue = GetComponent<AudioCue>();
    }

    private void OnEnable()
    {
        _onShootEventChannel.onShootEvent += PlayShootSound;
    }

    private void OnDisable()
    {

        _onShootEventChannel.onShootEvent -= PlayShootSound;
    }

    private void PlayShootSound(ShootType receivedDamageType)
    {
        if (receivedDamageType == shootType)
        {
            audioCue.PlayAudioCue();
        }
    }
}
