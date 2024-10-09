using System;
using System.Collections.Generic;
using UnityEngine;

namespace CameronBonde
{
	public class NeighboursManager : MonoBehaviour
	{
		public List<Transform> neighbours;

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<CamBlasterGuy_Model>())
				neighbours.Add(other.transform);
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.GetComponent<CamBlasterGuy_Model>())
				neighbours.Remove(other.transform);
		}
	}
}