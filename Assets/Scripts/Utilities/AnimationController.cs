using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
	[SerializeField] private MultiAimConstraint bodyAim;
	[SerializeField] private MultiAimConstraint handAim;

	private Animator anim;
	//private Animator animator => anim || anim.runtimeAnimatorController ? anim : anim = GetComponent<Animator>();
	[SerializeField]private Animator animator;
	public LevelUI LevelUI => levelUI ? levelUI : levelUI = GetComponentInChildren<LevelUI>(true);
	private LevelUI levelUI;
	private readonly Dictionary<AnimationType, int> hashDictionary = new Dictionary<AnimationType, int>();

	private void Awake()
	{
		// animator = GetComponent<Animator>();

		if (animator)
			SetupAnimationHashes();
	}

	private void SetupAnimationHashes()
	{
		var names = Enum.GetNames(typeof(AnimationType));
		var values = Enum.GetValues(typeof(AnimationType));

		for (int i = 0; i < Enum.GetNames(typeof(AnimationType)).Length; i++)
			hashDictionary.Add((AnimationType)values.GetValue(i), Animator.StringToHash(names[i]));
	}

	public void SetTrigger(AnimationType type)
	{
		animator.SetTrigger(hashDictionary[type]);
	}

	public void SetBool(AnimationType type, bool value)
	{
		animator.SetBool(hashDictionary[type], value);
	}

	public void SetInt(AnimationType type, int value)
	{
		animator.SetInteger(hashDictionary[type], value);
	}

	public void SetFloat(AnimationType type, float value)
	{
		animator.SetFloat(hashDictionary[type], value);
	}

	public bool GetBool(AnimationType type) => animator.GetBool(hashDictionary[type]);
	public float GetFloat(AnimationType type) => animator.GetFloat(hashDictionary[type]);
	public int GetInt(AnimationType type) => animator.GetInteger(hashDictionary[type]);

	public void ChangeBodyAimWeight(float weight, float duration = 0)
	{
		bodyAim.DOComplete();
		float bodyAimWeight = bodyAim.weight;
		DOTween.To(() => bodyAimWeight, x =>
		{
			bodyAimWeight = x;
			bodyAim.weight = bodyAimWeight;
		}, weight, duration).SetEase(Ease.OutCubic).SetTarget(bodyAim);
	}

	public void ChangeHandAimWeight(float weight, float duration = 0)
	{
		handAim.DOComplete();
		float bodyAimWeight = handAim.weight;
		DOTween.To(() => bodyAimWeight, x =>
		{
			bodyAimWeight = x;
			handAim.weight = bodyAimWeight;
		}, weight, duration).SetEase(Ease.OutCubic).SetTarget(handAim);
	}
}