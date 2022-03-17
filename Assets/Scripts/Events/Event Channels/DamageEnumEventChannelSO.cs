using UnityEngine.Events;
using UnityEngine;

public enum DamageType
{
	Hit,
	Death
}

[CreateAssetMenu(fileName = "New DamageEnumEventChannel", menuName = "ScriptableObjects/Events/DamageEnumEventChannel")]
public class DamageEnumEventChannelSO : ScriptableObject
{
	public UnityAction<DamageType> onDamageEvent;

	public void RaiseEvent(DamageType damageType)
	{
		if (onDamageEvent != null)
		{
			onDamageEvent.Invoke(damageType);
		}
		else
		{
			Debug.LogWarning("A DamageEvent was requested, but nobody picked it up.");
		}
	}
}
