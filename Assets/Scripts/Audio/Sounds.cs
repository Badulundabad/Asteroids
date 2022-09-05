using UnityEngine;

namespace Asteroids.Audio
{
    [CreateAssetMenu(fileName = "Sounds", menuName = "Asteroids/Lists/Sounds")]
    public class Sounds : ScriptableObject
    {
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip gunShot;
        [SerializeField] private AudioClip laser;
        [SerializeField] private AudioClip explosion;
        [SerializeField] private AudioClip music;

        public AudioClip ButtonClick { get => buttonClick; }
        public AudioClip GunShot { get => gunShot; }
        public AudioClip Laser { get => laser; }
        public AudioClip Explosion { get => explosion; }
        public AudioClip Music { get => music; }
    }
}