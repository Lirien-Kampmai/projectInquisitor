using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FirePaw
{
    namespace SettingSystem
    {
        public class VolumeInit : MonoBehaviour
        {
            /// <summary>
            /// инициализируем звук, если настройки звука при запуске не могут установится из сеттинг слайдер скрипта (например обьект выключен при старте)
            /// </summary>
            [SerializeField] private string volumeParam;
            [SerializeField] private AudioMixer audioMixer;

            private void Start()
            {
                var volume = PlayerPrefs.GetFloat(volumeParam, 0f);
                audioMixer.SetFloat(volumeParam, volume);
            }
        }
    }
}