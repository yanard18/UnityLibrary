using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.External.Sense_Engine.Scripts.Senses
{
	[SenseEnginePath("GameObjects/Set Active")]
	public class SenseSetActive : Sense
	{
		public enum EPossibleStates { CONSTANT, TOGGLE };

		[Header("Set Active Configurations")]

		private EPossibleStates? EStates;

		private bool Activation = false;

		private GameObject TargetObject;


		private void Awake()
		{
			Label = "Set Active";
		}



		public override void Play()
		{
			GameObject objectToActivate = TargetObject != null 
				? TargetObject 
				: transform.root.gameObject;

			switch (EStates)
			{
				case EPossibleStates.CONSTANT:
					objectToActivate.SetActive(Activation);
					break;
				case EPossibleStates.TOGGLE:
					objectToActivate.SetActive(!objectToActivate.activeInHierarchy);
					break;
			}
		}
	}
}
