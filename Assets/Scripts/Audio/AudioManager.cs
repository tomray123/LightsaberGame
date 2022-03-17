using UnityEngine.Audio;
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

    [Header("Audio control")]
    [SerializeField] private AudioMixer audioMixer = default;
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1f;

    private void Start()
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

    /// <summary>
	/// This is only used in the Editor, to debug volumes.
	/// It is called when any of the variables is changed, and will directly change the value of the volumes on the AudioMixer.
	/// </summary>
	void OnValidate()
    {
        if (Application.isPlaying)
        {
            SetGroupVolume("MasterVolume", _masterVolume);
            SetGroupVolume("MusicVolume", _musicVolume);
            SetGroupVolume("SFXVolume", _sfxVolume);
        }
    }

    public void SetGroupVolume(string parameterName, float normalizedVolume)
    {
        bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
        if (!volumeSet)
            Debug.LogError("The AudioMixer parameter was not found");
    }

    public float GetGroupVolume(string parameterName)
    {
        if (audioMixer.GetFloat(parameterName, out float rawVolume))
        {
            return MixerValueToNormalized(rawVolume);
        }
        else
        {
            Debug.LogError("The AudioMixer parameter was not found");
            return 0f;
        }
    }

    // Both MixerValueNormalized and NormalizedToMixerValue functions are used for easier transformations
    /// when using UI sliders normalized format
    private float MixerValueToNormalized(float mixerValue)
    {
        // We're assuming the range [-80dB to 0dB] becomes [0 to 1]
        return 1f + (mixerValue / 80f);
    }
    private float NormalizedToMixerValue(float normalizedValue)
    {
        // We're assuming the range [0 to 1] becomes [-80dB to 0dB]
        // This doesn't allow values over 0dB
        return (normalizedValue - 1f) * 80f;
    }

    public void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO settings)
	{
        AudioClip[] clipsToPlay = audioCue.GetClips();
        int nOfClips = clipsToPlay.Length;

        for (int i = 0; i < nOfClips; i++)
        {
            SoundEmitter soundEmitter = pool.SpawnFromPool(soundEmitterPoolTag, transform.position, Quaternion.identity).GetComponent<SoundEmitter>();
            if (soundEmitter != null)
            {
                soundEmitter.PlayAudioClip(clipsToPlay[i], settings, audioCue.IsLoop);
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
