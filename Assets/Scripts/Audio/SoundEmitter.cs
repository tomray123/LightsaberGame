using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SoundEmitter : MonoBehaviour
{
	private AudioSource _audioSource;
	public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;

	private void Awake()
	{
		_audioSource = this.GetComponent<AudioSource>();
		_audioSource.playOnAwake = false;
	}

	public void PlayAudioClip(AudioClip clip, bool hasToLoop)
	{
		_audioSource.clip = clip;
		_audioSource.loop = hasToLoop;
		_audioSource.Play();

		if (!hasToLoop)
		{
			StartCoroutine(FinishedPlaying(clip.length));
		}
	}

	public void Resume()
	{
		_audioSource.Play();
	}

	public void Pause()
	{
		_audioSource.Pause();
	}

	// Used when the SFX finished playing. Called by the <c>AudioManager</c>.
	public void Stop()
	{
		_audioSource.Stop();
	}

	public bool IsInUse()
	{
		return _audioSource.isPlaying;
	}

	public bool IsLooping()
	{
		return _audioSource.loop;
	}

	IEnumerator FinishedPlaying(float clipLength)
	{
		yield return new WaitForSeconds(clipLength);

		OnSoundFinishedPlaying.Invoke(this); // The AudioManager will pick this up
	}
}
