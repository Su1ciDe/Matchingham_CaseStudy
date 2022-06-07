using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private static Camera mainCamera;
	public static Camera MainCamera
	{
		get
		{
			if (!mainCamera) mainCamera = Camera.main;
			return mainCamera;
		}
	}

	[Space]
	public ColorScheme ColorScheme;
}
