using System.Collections;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.External.Sense_Engine.Scripts.Senses
{
	[SenseEnginePath("GameObjects/Scale Object")]
	public class SenseScale : Sense
	{
		public enum EPossibleStates { INSTANT, LERP };

		[Header("Scale Settings")]

		public EPossibleStates States;

		public Vector3 StartScaleValue = Vector3.one;

		public Vector3 EndScaleValue = Vector3.zero;

		public float ScalingDuration = 1.0f;		

		public bool RevertAfterFinish = false;

		public GameObject TargetObject;

		private void Awake()
		{
			Label = "Scale Object";
		}

		public override void Play()
		{
			Transform objectToScale = TargetObject != null 
				? TargetObject.transform 
				: transform.root;


			switch (States)
			{
				case EPossibleStates.INSTANT:
					objectToScale.localScale = EndScaleValue;
					break;
				case EPossibleStates.LERP:
					StartCoroutine(StartScaling(ScalingDuration, objectToScale));
					break;
			}
		}

		public IEnumerator StartScaling(float duration, Transform objectToScale)
		{
			float elapsedTime = 0;

			while(elapsedTime <= duration)
			{
				elapsedTime += Time.deltaTime;
				objectToScale.localScale = Vector3.Lerp(StartScaleValue, EndScaleValue, (elapsedTime / duration));

				yield return null;
			}

			if (RevertAfterFinish)
				objectToScale.localScale = StartScaleValue;
		}
	}
}
