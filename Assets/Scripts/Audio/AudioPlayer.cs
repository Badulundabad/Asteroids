using UnityEngine;

namespace Asteroids.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private Sounds sounds;
        [SerializeField] private AudioSource soundPlayer;
        [SerializeField] private AudioSource musicPlayer;

        public void Stop()
        {
            soundPlayer.Stop();
        }

        public void OnButtonClick()
        {
            soundPlayer.PlayOneShot(sounds.ButtonClick);
        }

        public void OnGunShot()
        {
            soundPlayer.PlayOneShot(sounds.GunShot);
        }

        public void OnLaserShot()
        {
            soundPlayer.PlayOneShot(sounds.Laser);
        }

        public void OnExplosion()
        {
            soundPlayer.PlayOneShot(sounds.Explosion);
        }
    }
}
