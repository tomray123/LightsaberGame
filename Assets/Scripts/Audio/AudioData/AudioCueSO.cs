using UnityEngine;

[CreateAssetMenu(fileName = "New AudioCue", menuName = "ScriptableObjects/Audio/AudioCue")]
public class AudioCueSO : ScriptableObject
{
    [SerializeField]
    private AudioClip audioClip;
    public AudioClip AudioClip { get { return audioClip; } }

    [SerializeField]
    private bool isLoop = false;
    public bool IsLoop { get { return isLoop; } }
}
