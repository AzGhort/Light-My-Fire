﻿using UnityEngine;

namespace LightMyFire
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		private void Awake() {
			DontDestroyOnLoad(gameObject);
		}
	}
}