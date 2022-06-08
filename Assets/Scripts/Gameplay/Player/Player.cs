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

	private void OnEnable()
	{
		LevelManager.OnLevelLoad += OnLevelLoaded;
		LevelManager.OnLevelStart += OnLevelStarted;
		LevelManager.OnLevelSuccess += OnLevelSuccess;
		LevelManager.OnLevelFail += OnLevelFailed;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelLoad -= OnLevelLoaded;
		LevelManager.OnLevelStart -= OnLevelStarted;
		LevelManager.OnLevelSuccess -= OnLevelSuccess;
		LevelManager.OnLevelFail -= OnLevelFailed;
	}

	private void OnLevelLoaded()
	{
		transform.position = Vector3.zero;
		GunController.CurrentGunIndex = 0;
		GunController.ChangeGun(0);
	}

	private void OnLevelStarted()
	{
		PlayerController.CanControl = true;
		PlayerMovement.CanMove = true;
		GunController.Gun.StartFiring();
	}

	private void OnLevelSuccess()
	{
		PlayerController.CanControl = false;
		PlayerMovement.CanMove = false;
		GunController.Gun.StopFiring();
	}

	private void OnLevelFailed()
	{
		PlayerController.CanControl = false;
		PlayerMovement.CanMove = false;
		GunController.Gun.StopFiring();
	}

	public void Die()
	{
		Animations.SetTrigger(AnimationType.Die);
		LevelManager.Instance.GameFail();
	}
}