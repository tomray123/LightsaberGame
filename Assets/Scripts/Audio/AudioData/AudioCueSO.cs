using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioCue", menuName = "ScriptableObjects/Audio/AudioCue")]
public class AudioCueSO : ScriptableObject
{
	[SerializeField] private AudioClipsGroup _audioClipGroup = default;

	[SerializeField] private bool isLoop = false;
    public bool IsLoop { get { return isLoop; } }

	public AudioClip[] GetClips()
	{
		_audioClipGroup.InitializePlaylist();
		int numberOfClips = _audioClipGroup.GetLength();
		AudioClip[] resultingClips = new AudioClip[numberOfClips];

		for (int i = 0; i < numberOfClips; i++)
		{
			resultingClips[i] = _audioClipGroup.GetNextClip();
		}

		return resultingClips;
	}
}

/// <summary>
/// Represents a group of AudioClips that can be treated as one, and provides automatic randomisation or sequencing based on the <c>SequenceMode</c> value.
/// </summary>
[Serializable]
public class AudioClipsGroup
{
	public SequenceMode sequenceMode = SequenceMode.OneAudioRandomly;
	public AudioClip[] audioClips;

	private int _nextClipToPlay = -1;
	private int _lastClipPlayed = -1;

	/// <summary>
	/// Chooses the next clip in the sequence, either following the order or randomly.
	/// </summary>
	/// <returns>A reference to an AudioClip</returns>
	public AudioClip GetNextClip()
	{
		// Fast out if there is only one clip to play
		if (audioClips.Length == 1)
			return audioClips[0];

		if (_nextClipToPlay == -1)
		{
			// Index needs to be initialised: 0 if Sequential, random if otherwise
			_nextClipToPlay = (sequenceMode == SequenceMode.Sequential) ? 0 : UnityEngine.Random.Range(0, audioClips.Length);
		}
		else
		{
			// Select next clip index based on the appropriate SequenceMode
			switch (sequenceMode)
			{
				case SequenceMode.Random:
					_nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
					break;

				case SequenceMode.RandomNoImmediateRepeat:
					do
					{
						_nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
					} while (_nextClipToPlay == _lastClipPlayed);
					break;

				case SequenceMode.Sequential:
					_nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, audioClips.Length);
					break;
			}
		}

		_lastClipPlayed = _nextClipToPlay;

		return audioClips[_nextClipToPlay];
	}

	// Used in GetClips() method. Initializes variables, because ScriptableObjects don't change data during runtime.
	public void InitializePlaylist()
    {
		_nextClipToPlay = -1;
		_lastClipPlayed = -1;
	}

	// Used in GetClips() method. Defines how many clips need to be played.
	public int GetLength()
    {
		if (sequenceMode == SequenceMode.OneAudioRandomly)
        {
			return 1;
        }
		return audioClips.Length;
    }

	public enum SequenceMode
	{
		Random,
		RandomNoImmediateRepeat,
		Sequential,
		OneAudioRandomly
	}
}
