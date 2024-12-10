using System;
using Pathfinding;
using Pathfinding.Util;
using Unity.VisualScripting;
using UnityEngine;
public class MinerAnimation : VersionedMonoBehaviour {
	public Animator anim;

	public float naturalSpeed = 5f;

	bool isAtEndOfPath;

	IAstarAI ai;
	Transform tr;

	const string NormalizedSpeedKey = "NormalizedSpeed";
	static int NormalizedSpeedKeyHash = Animator.StringToHash(NormalizedSpeedKey);

	protected override void Awake () {
		base.Awake();
		ai = GetComponent<IAstarAI>();
		tr = GetComponent<Transform>();
	}

	void OnEnable () {
		BatchedEvents.Add(this, BatchedEvents.Event.Update, OnUpdate);
	}

	void OnDisable () {
		BatchedEvents.Remove(this);
	}

	static void OnUpdate (MinerAnimation[] components, int count) {
		for (int i = 0; i < count; i++) components[i].OnUpdate();
	}

	void OnUpdate () {
		if (ai == null) return;

		Vector3 relVelocity = tr.InverseTransformDirection(ai.velocity);
		relVelocity.y = 0;

		anim.SetFloat(NormalizedSpeedKeyHash, relVelocity.magnitude / (naturalSpeed * anim.transform.lossyScale.x));
	}

	public void MiningAnimationStart()
	{
			anim.Play("mining");
	}
	
	public void DefaultAnimationStart()
	{
		anim.Play("forward");
	}
}
