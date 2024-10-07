using UnityEngine;
using UnityEngine.Audio;

namespace Gameplay
{
    public class SoundManager : MonoBehaviour
    {
        #region SINGLETONE
        private static SoundManager _instance;
        public static SoundManager Instance => _instance;
        #endregion

        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _sounds;

        [SerializeField] private AudioResource _music1;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayMusic(MusicTracks music)
        {
            switch (music)
            {
                case MusicTracks.back1:
                    _music.resource = _music1;
                    break;
            }

            _music.Play();
        }

        public void PlayShort(ShortClip clip)
        {
            switch (clip)
            {
                case ShortClip.smth:
                    //_sounds.resource = _music1;
                    break;
            }

            _music.Play();
        }
    }

    public enum MusicTracks
    {
        back1
    }
    public enum ShortClip
    {
        smth
    }
}
