using System.Collections.Generic;
using UnityEngine;

namespace DenizYanar.External.Sense_Engine.Scripts.Core
{
    public class SenseEnginePlayer : MonoBehaviour
    {
		[SerializeField]
        public List<Sense> SenseList = new List<Sense>();

		public void Play()
		{
			foreach (Sense s in SenseList)
				s.Play();
		}
	}
}
