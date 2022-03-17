using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New AudioCueEventChanel", menuName = "ScriptableObjects/Events/AudioCueEventChannel")]
public class AudioCueEventChannelSO : ScriptableObject
{
	public UnityAction<AudioCueSO, AudioConfigurationSO> OnAudioCueRequested;

	public void RaiseEvent(AudioCueSO audioCue, AudioConfigurationSO settings)
	{
		if (OnAudioCueRequested != null)
		{
			OnAudioCueRequested.Invoke(audioCue, settings);
		}
		else
		{
			Debug.LogWarning("An AudioCue was requested, but nobody picked it up. " +
				"Check why there is no AudioManager already loaded, " +
				"and make sure it's listening on this AudioCue Event channel.");
		}
	}
}
