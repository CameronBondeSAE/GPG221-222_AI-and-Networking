using System.Collections.Generic;
using UnityEngine;

namespace CameronBonde.Managers
{
	public class PerceptionManager : ManagerBase<PerceptionManager>
	{
		public List<Transform> perceptionObjects = new List<Transform>();

		public void RegisterPerceptionObject(Transform perceptionObject)
		{
			perceptionObjects.Add(perceptionObject);
		}
		public void DeregisterPerceptionObject(Transform perceptionObject)
		{
			perceptionObjects.Remove(perceptionObject);
		}
	}
}