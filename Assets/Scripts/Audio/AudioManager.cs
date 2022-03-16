using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private ObjectPooler pool;

    [SerializeField] private string soundEmitterPoolTag = "Sound";
    [Header("Listening on channels")]
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play SFXs")]
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play Music")]
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;

    private void Awake()
    {
        pool = ObjectPooler.Instance;
    }

    private void OnEnable()
    {
        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested += PlayAudioCue;
    }

    private void OnDisable()
    {
        _SFXEventChannel.OnAudioCueRequested -= PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested -= PlayAudioCue;
    }

    public void PlayAudioCue(AudioCueSO audioCue)
	{
        AudioClip[] clipsToPlay = audioCue.GetClips();
        int nOfClips = clipsToPlay.Length;

        for (int i = 0; i < nOfClips; i++)
        {
            SoundEmitter soundEmitter = pool.SpawnFromPool(soundEmitterPoolTag, transform.position, Quaternion.identity).GetComponent<SoundEmitter>();
            if (soundEmitter != null)
            {
                soundEmitter.PlayAudioClip(clipsToPlay[i], audioCue.IsLoop);
                if (!audioCue.IsLoop)
                    soundEmitter.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
            }
        }
    }

    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
    {
        soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
        soundEmitter.Stop();
        pool.ReturnToPool(soundEmitter.gameObject);
    }
}
