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

        [SerializeField] private AudioResource _musicBattle;
        [SerializeField] private AudioResource _musicMenu;
        [SerializeField] private AudioResource _swap;
        [SerializeField] private AudioResource _defenderEat;
        [SerializeField] private AudioResource _wrongShot;
        [SerializeField] private AudioResource _shot;
        [SerializeField] private AudioResource _hit;

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

        public void PlayMusic(MusicTracks music, ulong delay)
        {
            switch (music)
            {
                case MusicTracks.menu:
                    _music.resource = _musicMenu;
                    break;
                case MusicTracks.battle:
                    _music.resource = _musicBattle;
                    break;
            }

            _music.Play(delay);
        }

        public void PlayShort(ShortClip clip, ulong delay, float pitch = 1)
        {
            switch (clip)
            {
                case ShortClip.swap:
                    _sounds.resource = _swap;
                    break;
                case ShortClip.hit:
                    _sounds.resource = _hit;
                    break;
                case ShortClip.defenderEat:
                    _sounds.resource = _defenderEat;
                    break;
                case ShortClip.shot:
                    _sounds.resource = _shot;
                    break;
                case ShortClip.wrongShot:
                    _sounds.resource = _wrongShot;
                    break;
            }

            _sounds.pitch = pitch;
            _sounds.Play(delay);
        }
    }

    public enum MusicTracks
    {
        menu,
        battle
    }

    public enum ShortClip
    {
        swap,
        defenderEat,
        wrongShot,
        shot,
        hit
    }
}