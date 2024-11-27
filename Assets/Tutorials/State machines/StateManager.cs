using UnityEngine;

namespace CameronBonde.State_machines
{
	public class StateManager : MonoBehaviour
	{
		public GameObject currentState;

		public void ChangeState(GameObject newState)
		{
			currentState.SetActive(false);
			newState.SetActive(true);
			currentState = newState;
		}
	}
}