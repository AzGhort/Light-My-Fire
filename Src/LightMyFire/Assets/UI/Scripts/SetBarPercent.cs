using UnityEngine;
using UnityEngine.UI;

namespace LightMyFire
{
	public class SetBarPercent : MonoBehaviour
	{
		[SerializeField] private Image fillBar;

		public void BarPercent(float percent) {
			fillBar.fillAmount = Mathf.Clamp(percent, 0, 1);
		}
	}
}
