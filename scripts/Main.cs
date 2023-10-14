using Godot;
using System;

public partial class Main : Node3D
{
	public override void _PhysicsProcess(double delta)
	{
		var camera3D = GetNode<Marker3D>("Marker3D").GetNode<Camera3D>("Camera3D");
		var mousePosition = GetViewport().GetMousePosition();

		const float rayLength = 2000.0f;
		var rayOrigin = camera3D.ProjectRayOrigin(mousePosition);
		var rayTarget = rayOrigin + camera3D.ProjectRayNormal(mousePosition) * rayLength;

		var spaceState = GetWorld3D().DirectSpaceState;
		var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayTarget);
		var intersection = spaceState.IntersectRay(query);

		if (intersection.Count > 0)
		{
			var position = intersection["position"].AsVector3();
			GD.Print(position);
			GetNode<Node3D>("Player").GetNode<Node3D>("Pivot").LookAt(position, Vector3.Up);
		}
	}
}
