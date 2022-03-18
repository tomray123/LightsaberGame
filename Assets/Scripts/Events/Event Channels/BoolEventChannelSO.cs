using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "New BoolEventChannel", menuName = "ScriptableObjects/Events/BoolEventChannel")]
public class BoolEventChannelSO : ScriptableObject
{
	public UnityAction<bool> onBoolEvent;

	public void RaiseEvent(bool param)
	{
		if (onBoolEvent != null)
		{
			onBoolEvent.Invoke(param);
		}
		else
		{
			Debug.LogWarning("A BoolEvent was requested, but nobody picked it up.");
		}
	}
}
