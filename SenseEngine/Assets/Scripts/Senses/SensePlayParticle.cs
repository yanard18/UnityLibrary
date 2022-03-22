using System;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;

namespace DenizYanar.External.Sense_Engine.Scripts.Senses
{
	[SenseEnginePath("Particle/Play Particle")]
	public class SensePlayParticle : Sense
	{
		private enum EPlayMode
		{
			PLAY,
			STOP
		}
		
		[SerializeField]
		private ParticleSystem _particle;

		[SerializeField] private EPlayMode _playMode;

		private void Awake()
		{
			Label = "Play Particle";
		}

		public override void Play()
		{
			switch (_playMode)
			{
				case EPlayMode.PLAY:
					_particle.Play();
					break;
				case EPlayMode.STOP:
					_particle.Stop();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
