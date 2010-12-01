using UnityEngine;

public class SteerForTarget : Steering {
	
	/// <summary>
	/// Target the behavior will aim for
	/// </summary>
	public Transform Target;
	
	
	void Awake()
	{
		if (Target == null)
		{
			Debug.Log("No target at awake.");
		}
	}

	/// <summary>
	/// Calculates the force to apply to a vehicle to reach a target transform
	/// </summary>
	/// <returns>
	/// Force to apply <see cref="Vector3"/>
	/// </returns>
	protected override Vector3 CalculateForce()
	{
		if (Target == null)
		{
			return Vector3.zero;
		}
		return Vehicle.GetSeekVector(Target.position);
	}
}