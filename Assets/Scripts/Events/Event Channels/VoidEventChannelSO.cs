using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "New VoidEventChannel", menuName = "ScriptableObjects/Events/VoidEventChannel")]
public class VoidEventChannelSO : ScriptableObject
{
	public UnityEvent onVoidEvent;

	public void RaiseEvent()
	{
		if (onVoidEvent != null)
		{
			onVoidEvent.Invoke();
		}
		else
		{
			Debug.LogWarning("A VoidEvent was requested, but nobody picked it up.");
		}
	}
}
