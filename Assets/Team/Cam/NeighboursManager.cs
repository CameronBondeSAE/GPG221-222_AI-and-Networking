using System;
using System.Collections.Generic;
using UnityEngine;

namespace CameronBonde
{
	public class NeighboursManager : MonoBehaviour
	{
		public List<Transform> neighbours;

		public LayerMask layerMask;
		
		private void OnTriggerEnter(Collider other)
		{
			// if ((layerMask.value & (1 << layerMask)) != 0)
			// {
				neighbours.Add(other.transform);
			// }

			// if (other.gameObject.layer == layerMask.value)
		}

		private void OnTriggerExit(Collider other)
		{
			// if (other.GetComponent<CamBlasterGuy_Model>())
				neighbours.Remove(other.transform);
		}
	}
}