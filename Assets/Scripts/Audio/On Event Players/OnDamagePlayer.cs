using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamagePlayer : MonoBehaviour
{
    [SerializeField] private DamageEnumEventChannelSO _onDamageEventChannel = default;
    [SerializeField] private DamageType damageType;
    private AudioCue audioCue;

    public void Awake()
    {
        audioCue = GetComponent<AudioCue>();
    }

    private void OnEnable()
    {
        _onDamageEventChannel.onDamageEvent += PlayDamageSound;
    }

    private void OnDisable()
    {

        _onDamageEventChannel.onDamageEvent -= PlayDamageSound;
    }

    private void PlayDamageSound(DamageType receivedDamageType)
    {
        if (receivedDamageType == damageType)
        {
            audioCue.PlayAudioCue();
        }
    }
}
