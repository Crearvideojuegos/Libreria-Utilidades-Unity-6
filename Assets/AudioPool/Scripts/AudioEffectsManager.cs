using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace SpaceAudioPool
{
    public class AudioEffectsManager : MonoBehaviour
    {
        public static AudioEffectsManager Instance;
        [SerializeField] private GameObject _audioSourcePrefab;
        private int _initialPoolSize = 10;
        private List<AudioSource> _audioPool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeAudioPool();
            }
        }

        private void InitializeAudioPool()
        {
            _audioPool = new List<AudioSource>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                CreateNewAudioSource();
            }
        }

        private AudioSource CreateNewAudioSource()
        {
            GameObject newAudioObj = Instantiate(_audioSourcePrefab);
            newAudioObj.transform.parent = transform;
            AudioSource newSource = newAudioObj.GetComponent<AudioSource>();
            newSource.spatialBlend = 1.0f;
            newSource.playOnAwake = false;
            newSource.loop = false;
            _audioPool.Add(newSource);
            return newSource;
        }

        private AudioSource GetAvailableAudioSource()
        {
            foreach (var source in _audioPool)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }
            
            return CreateNewAudioSource();
        }

        public void PlaySound(AudioClip clip, Vector3 position, float volume = 1.0f, bool loop = false, float spatialBlend = 1.0f)
        {
            AudioSource source = GetAvailableAudioSource();
            source.transform.position = position;
            source.clip = clip;
            source.volume = volume;
            source.loop = loop;
            source.Play();
            source.spatialBlend = spatialBlend;
            
            if (!loop)
            {
                StartCoroutine(DisableAfterPlay(source, clip.length));
            }
        }

        private IEnumerator DisableAfterPlay(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);
            source.Stop();
            source.clip = null;
        }

        public void StopAllSounds()
        {
            foreach (var source in _audioPool)
            {
                source.Stop();
                source.clip = null;
            }
        }
    }

}
