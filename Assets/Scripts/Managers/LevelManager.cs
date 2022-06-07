using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
	public static int CurrentLevel
	{
		get => PlayerPrefs.GetInt("CurrentLevel", 1);
		set
		{
			PlayerPrefs.SetInt("CurrentLevel", value);
			UIManager.Instance.LevelUI.ChangeLevelText(value);
		}
	}

	public static event UnityAction OnLevelLoad; 
	public static event UnityAction OnLevelStart;
	public static event UnityAction OnLevelSuccess;
	public static event UnityAction OnLevelFail;

	public void LoadLevel()
	{
		// 
		OnLevelLoad?.Invoke();
	}
	
	public void StartLevel()
	{
		OnLevelStart?.Invoke();
	}
	
	public void GameSuccess()
	{
		CurrentLevel++;
		OnLevelSuccess?.Invoke();
	}

	public void GameFail()
	{
		OnLevelFail?.Invoke();
	}
}
