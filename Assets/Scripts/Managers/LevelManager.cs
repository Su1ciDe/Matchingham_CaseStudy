using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

	public SceneAsset TutorialLevel, MainLevel;

	public static event UnityAction OnLevelLoad;
	public static event UnityAction OnLevelStart;
	public static event UnityAction OnLevelSuccess;
	public static event UnityAction OnLevelFail;

	private AsyncOperation loadLevel;

	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadLevel();
	}

	public void LoadLevel()
	{
		StartCoroutine(LoadLevelCoroutine());
	}

	private IEnumerator LoadLevelCoroutine()
	{
		if (CurrentLevel.Equals(1))
		{
			if (!SceneManager.GetActiveScene().name.Equals(TutorialLevel.name))
				loadLevel = SceneManager.LoadSceneAsync(TutorialLevel.name);
		}
		else
		{
			if (!SceneManager.GetActiveScene().name.Equals(MainLevel.name))
				loadLevel = SceneManager.LoadSceneAsync(MainLevel.name);

			yield return new WaitUntil(() => loadLevel.isDone);

			LevelGenerator.Instance.GenerateLevel();
		}

		yield return new WaitUntil(() => loadLevel.isDone);
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