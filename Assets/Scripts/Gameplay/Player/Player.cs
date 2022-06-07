using UnityEngine;

public class Player : Singleton<Player>
{
	public PlayerController PlayerController { get; private set; }
	public PlayerMovement PlayerMovement { get; private set; }
	public GunController GunController { get; private set; }
	public AnimationController Animations { get; private set; }

	private void Awake()
	{
		PlayerMovement = GetComponent<PlayerMovement>();
		PlayerController = GetComponent<PlayerController>();
		GunController = GetComponent<GunController>();
		Animations = GetComponentInChildren<AnimationController>();

	}

	private void Start()
	{
		OnLevelStarted();
	}

	private void OnLevelStarted()
	{
		PlayerController.CanControl = true;
		PlayerMovement.CanMove = true;
		GunController.Gun.StartFiring();
	}

	public void Die()
	{
		
	}
}