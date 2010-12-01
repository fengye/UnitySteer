using UnityEngine;
using UnitySteer;
using System;
using UnitySteer.Helpers;

public class SteerForArrival : Steering {
	
	/// <summary>
	/// Target the behavior will aim for
	/// </summary>
	public Transform Target
	{
		get
		{
			return _target;
		}
		
		set
		{
			_reportedArrival = false;
			_target = value;
		}
	}
	public float BrakingDistance = 100;
	public float ArrivalOffset = 0;
	
	void Awake()
	{
		if (Target == null)
		{
			//Destroy(this);
			//throw new System.Exception("SteerForTarget need a target transform. Dying.");
			print("No target at awake.");
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
		
		Vector3 targetOffset = Target.position + Target.forward * ArrivalOffset - Vehicle.Position;
		float distance = targetOffset.magnitude;
		
		float rampedSpeed = Vehicle.MaxSpeed * (distance / BrakingDistance );
		float clippedSpeed = Math.Min(rampedSpeed, Vehicle.MaxSpeed);
		
		Vector3 force =  (clippedSpeed / distance) * targetOffset - Vehicle.Velocity;

		// Raise the arrival event
		if (!_reportedArrival ) 
		{
			if ( (_target.position - this.transform.position).magnitude < 1.0f)
			{
				_reportedArrival = true;
				if (_onArrival != null)
				{
					_onArrival(new SteeringEvent<Transform>(this, "arrived", Target));
				}
			}
		}
		
		return force;
	}
	
	public SteeringEventHandler<Transform> OnArrival {
		get {
			return this._onArrival;
		}
		set {
			_onArrival = value;
		}
	}
	
	bool _reportedArrival = false;
	SteeringEventHandler<Transform> _onArrival;
	Transform _target;
}
