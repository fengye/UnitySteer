using UnityEngine;

/// <summary>
/// Steers a vehicle to keep separate from neighbors
/// </summary>
public class SteerForSeparation : SteerForNeighbors
{
	#region Methods
	protected override Vector3 CalculateNeighborContribution(Vehicle other)
	{
		// add in steering contribution
		// (opposite of the offset direction, divided once by distance
		// to normalize, divided another time to get 1/d falloff)
		Vector3 offset = other.Position - Vehicle.Position;
		float distanceSquared = Vector3.Dot (offset, offset);
		if ( distanceSquared > 0 )
		{
			Vector3 steering = (offset / -distanceSquared);	
			return steering;
		}
		else
		{
			// randomly offset this vehicle a direction to propel the vehicle
			Debug.Log("Zero offset detected");
			return Vector3.one;
		}
	}
	#endregion
}

