using UnityEngine;

namespace WW.Service
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void PlayWrongWord()
        {
            _audioSource.Play();
        }

    }
}
