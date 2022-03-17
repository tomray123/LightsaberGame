using UnityEngine.Events;
using UnityEngine;

public enum ShootType
{
	Simple,
	Sniper,
	Rocket,
	Turret
}

[CreateAssetMenu(fileName = "New ShootEnumEventChannel", menuName = "ScriptableObjects/Events/ShootEnumEventChannel")]
public class ShootEnumEventChannelSO : ScriptableObject
{
	public UnityAction<ShootType> onShootEvent;

	public void RaiseEvent(ShootType shootType)
	{
		if (onShootEvent != null)
		{
			onShootEvent.Invoke(shootType);
		}
		else
		{
			Debug.LogWarning("A ShootEvent was requested, but nobody picked it up.");
		}
	}
}
