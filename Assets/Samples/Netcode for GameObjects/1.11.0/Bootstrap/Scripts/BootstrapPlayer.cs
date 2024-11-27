using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unity.Netcode.Samples
{
	/// <summary>
	/// Player marble
	/// TODO: Network velocity AND angular velocity
	/// This allows ALL clients to 'fill in the gaps' between network ticks with real physics
	/// </summary>
	public class BootstrapPlayer : NetworkBehaviour
	{
		[Rpc(SendTo.Server)]
		public void RandomTeleportServerRpc(Vector3 randomPositionOnXYPlane)
		{
			Debug.Log("Server: RandomTeleportServerRpc");

			transform.position = randomPositionOnXYPlane;
			UpdateClientPositionRpc(randomPositionOnXYPlane);
		}

		
		
		
		
		/// <summary>
		/// TODO
		/// </summary>
		/// <param name="position">TODO</param>
		[Rpc(SendTo.ClientsAndHost)]
		public void UpdateClientPositionRpc(Vector3 position)
		{
			// TODO: Check thresholds for bothering to resync
			// TODO: Advanced: only update if being seen by the player
			Debug.Log("Client: RandomTeleportServerRpc");
			transform.position = position;
		}
		
		

		private static Vector3 GetRandomPositionOnXYPlane()
		{
			return new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
		}

		
		/// <summary>
		/// Replace this is a separate MarbleController script using mouse axis
		/// </summary>
		private void OnGUI()
		{
			// "Random Teleport" button will only be shown to clients
			if (IsLocalPlayer)
			{
				if (GUILayout.Button("Random Teleport"))
				{
					Debug.Log("ClientRequest: RandomTeleportServerRpc");

					Vector3 randomPositionOnXYPlane = GetRandomPositionOnXYPlane();
					transform.position = randomPositionOnXYPlane;
					
					RandomTeleportServerRpc(randomPositionOnXYPlane);
				}
			}
		}
	}
}