using TMPro;
using UnityEngine;

namespace CameronBonde
{
	public class ScoreUI : MonoBehaviour
	{
		public GameManager gameManager;
		
		public TextMeshProUGUI scoreUI;
		
		private void Update()
		{
			scoreUI.text = gameManager.score.ToString();
		}
	}
}