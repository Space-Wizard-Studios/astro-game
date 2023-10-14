using Godot;

public partial class PlayerController : CharacterBody3D
{
	[Export]
	public int Speed { get; set; } = 14;
	[Export]
	public int FallAcceleration { get; set; } = 75;
	private Vector3 _targetVelocity = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
		var moveDirection = Vector3.Zero;

		if (Input.IsActionPressed("move_right"))
		{
			moveDirection.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			moveDirection.X -= 1.0f;
		}
		if (Input.IsActionPressed("move_back"))
		{
			moveDirection.Z += 1.0f;
		}
		if (Input.IsActionPressed("move_forward"))
		{
			moveDirection.Z -= 1.0f;
		}

		// Ground velocity
		_targetVelocity.X = moveDirection.X * Speed;
		_targetVelocity.Z = moveDirection.Z * Speed;

		// Vertical velocity
		if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
		{
			_targetVelocity.Y -= FallAcceleration * (float)delta;
		}

		// Moving the character
		Velocity = _targetVelocity;
		MoveAndSlide();

	}
}