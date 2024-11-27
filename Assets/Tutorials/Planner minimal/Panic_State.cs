using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using DG.Tweening;
using Tanks;
using UnityEngine;
using UnityEngine.Serialization;

namespace CameronBonde
{
	public class Panic_State : AntAIState
	{
		public Panic_Model panicModel;

		public float scale = 4f;
		public float duration = 1f;
		
		public override void Create(GameObject _mainGameObject)
		{
			base.Create(_mainGameObject);

			panicModel = _mainGameObject.GetComponent<Panic_Model>();
		}

		public override void Enter()
		{
			base.Enter();
			
			Debug.Log("PANIC! Enter");

			panicModel.mainMarbleMesh.DOScale(Vector3.one * scale, duration).SetEase(Ease.OutElastic).OnComplete(Finished);
		}

		private void Finished()
		{
			Finish();
		}
	}
}