using System.Collections.Generic;
using AddressableReferences;
using Messages.Server.SoundMessages;
using UnityEngine;


namespace Systems.Explosions
{
	/// <summary>
	/// Utilities for explosions
	/// </summary>
	public static class ExplosionUtils
	{
		private static readonly AddressableAudioSource[] EXPLOSION_SOUNDS = { CommonSounds.Instance.Explosion1, CommonSounds.Instance.Explosion2 };
		private static readonly AddressableAudioSource[] DISTANT_EXPLOSION_SOUNDS = { CommonSounds.Instance.ExplosionDistant1, CommonSounds.Instance.ExplosionDistant2 };
		private static readonly AddressableAudioSource[] STATION_GROAN_SOUNDS = { CommonSounds.Instance.ExplosionCreak1, CommonSounds.Instance.ExplosionCreak2, CommonSounds.Instance.ExplosionCreak3 };

		/// <summary>
		/// Play explosion sound and shake ground
		/// </summary>
		/// <param name="worldPosition">position explosion is centered at</param>
		/// <param name="shakeIntensity">intensity of shaking</param>
		/// <param name="shakeDistance">how far away the shaking can be felt</param>
		public static void PlaySoundAndShake(Vector3Int worldPosition, byte shakeIntensity, int shakeDistance)
		{

			AddressableAudioSource explosionSound = EXPLOSION_SOUNDS.PickRandom();
			AddressableAudioSource groanSound     = STATION_GROAN_SOUNDS.PickRandom();
			AddressableAudioSource distantSound   = DISTANT_EXPLOSION_SOUNDS.PickRandom();
			AudioSourceParameters audioSourceParameters = new AudioSourceParameters(0f, 100f);
			ShakeParameters shakeParameters = new ShakeParameters(true, shakeIntensity, shakeDistance);

			_ = SoundManager.PlayNetworkedAtPosAsync(explosionSound, worldPosition, audioSourceParameters, true, false, shakeParameters);

			if (groanSound?.AudioSource.OrNull()?.maxDistance == null) return;

			if (shakeDistance > distantSound.AudioSource.maxDistance)
			{
				_ = SoundManager.PlayNetworkedAtPosAsync(distantSound, worldPosition, global: true);
			}
			if (shakeIntensity > groanSound.AudioSource.maxDistance)
			{
				_ = SoundManager.PlayNetworkedAtPosAsync(groanSound, worldPosition, global: true);
			}
		}
	}
}
