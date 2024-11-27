using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using DG.Tweening;
using UnityEngine;

namespace CameronBonde
{
	public class Idle_State : AntAIState
	{
		public Panic_Model panicModel;
		public float duration = 4f;

		public override void Create(GameObject _mainGameObject)
		{
			base.Create(_mainGameObject);

			panicModel = _mainGameObject.GetComponent<Panic_Model>();
		}

		public override void Enter()
		{
			base.Enter();
			
			Debug.Log("Idle Enter");
			Debug.Break();
			
			panicModel.mainMarbleMesh.DOScale(Vector3.one, duration).SetEase(Ease.Linear).OnComplete(Finished);
		}

		private void Finished()
		{
			// Finish();
		}
	}
}