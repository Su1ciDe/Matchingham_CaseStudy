using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private bool canMove = true;
	public bool CanMove
	{
		get => canMove;
		set
		{
			canMove = value;
			Player.Instance.Animations.SetBool(AnimationType.Running, value);
		}
	}

	[SerializeField] private float moveSpeed = 10;
	[SerializeField] private float moveSpeedMultiplier = 1;

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		if (!CanMove) return;

		transform.Translate(moveSpeed * moveSpeedMultiplier * Time.deltaTime * Vector3.forward);
	}
}