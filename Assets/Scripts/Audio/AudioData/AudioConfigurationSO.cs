using UnityEngine;
using UnityEngine.Audio;

//TODO: Check which settings we really need at this level
[CreateAssetMenu(fileName = "New AudioConfig", menuName = "ScriptableObjects/Audio/AudioConfig")]
public class AudioConfigurationSO : ScriptableObject
{
	public AudioMixerGroup OutputAudioMixerGroup = null;

	// Simplified management of priority levels (values are counterintuitive, see enum below)
	[SerializeField] private PriorityLevel _priorityLevel = PriorityLevel.Standard;
	[HideInInspector]
	public int Priority
	{
		get { return (int)_priorityLevel; }
		set { _priorityLevel = (PriorityLevel)value; }
	}

	[Header("Sound properties")]
	public bool Mute = false;
	[Range(0f, 1f)] public float Volume = 1f;

	[Header("Ignores")]
	public bool BypassEffects = false;
	public bool BypassListenerEffects = false;
	public bool BypassReverbZones = false;
	public bool IgnoreListenerVolume = false;
	public bool IgnoreListenerPause = false;

	private enum PriorityLevel
	{
		Highest = 0,
		High = 64,
		Standard = 128,
		Low = 194,
		VeryLow = 256,
	}

	public void ApplyTo(AudioSource audioSource)
	{
		audioSource.outputAudioMixerGroup = this.OutputAudioMixerGroup;
		audioSource.mute = this.Mute;
		audioSource.bypassEffects = this.BypassEffects;
		audioSource.bypassListenerEffects = this.BypassListenerEffects;
		audioSource.bypassReverbZones = this.BypassReverbZones;
		audioSource.priority = this.Priority;
		audioSource.volume = this.Volume;
		audioSource.ignoreListenerVolume = this.IgnoreListenerVolume;
		audioSource.ignoreListenerPause = this.IgnoreListenerPause;
	}
}
