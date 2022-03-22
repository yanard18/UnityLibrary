using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.External.Sense_Engine.Scripts
{
    public class CubeController : MonoBehaviour
    {
		[SerializeField]
		private SenseEnginePlayer _engine;

		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				_engine.Play();
			}
		}
	}
}
