using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool CanControl { get; set; }

	[SerializeField] private float dragMultiplier = 1;
	[Space]
	[SerializeField] private float leftLimit;
	[SerializeField] private float rightLimit;

	private Vector3 playerPos;
	private float deltaX, previousPosX;

	private void Update()
	{
		Inputs();
	}

	private void Inputs()
	{
		if (!CanControl) return;

		if (Input.GetMouseButtonDown(0))
		{
			previousPosX = Input.mousePosition.x;
		}

		if (Input.GetMouseButton(0))
		{
			playerPos = transform.position;
			deltaX = Input.mousePosition.x - previousPosX;
			playerPos.x = Mathf.Clamp(playerPos.x + dragMultiplier * Time.deltaTime * deltaX, leftLimit, rightLimit);
			transform.position = playerPos;

			Player.Instance.Animations.SetFloat(AnimationType.Rotate, Mathf.Clamp(deltaX / 10f, -1, 1));

			previousPosX = Input.mousePosition.x;
		}

		if (Input.GetMouseButtonUp(0))
		{
			Player.Instance.Animations.SetFloat(AnimationType.Rotate, 0);
		}
	}
}