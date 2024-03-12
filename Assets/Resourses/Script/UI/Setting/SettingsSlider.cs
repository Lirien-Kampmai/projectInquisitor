using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FirePaw
{
    namespace SettingSystem
    {
        public class SettingsSlider : MonoBehaviour
        {
            /// <summary>
            /// устанавливает параметры звука в соответствии со слайдером.
            /// </summary>
            [SerializeField] private string volumeParam = "Set_Name_Exposed_Parameters";
            [SerializeField] private AudioMixer audioMixer;

            private Slider slider;
            private float  volumeValue;

            private const float MIN_VALUE  = 0.000001f;
            private const float MAX_VALUE  = 0.990000f;
            private const float MULTIPLIER = 20f;

            private void Awake()
            {
                slider = GetComponent<Slider>();
                slider.onValueChanged.AddListener(ParameterSetter);

                slider.minValue = MIN_VALUE;
                slider.maxValue = MAX_VALUE;
            }

            private void Start()
            {
                volumeValue = PlayerPrefs.GetFloat(volumeParam, Mathf.Log10(slider.value) * MULTIPLIER);

                //Debug.Log("реверс Log10 = " + (Mathf.Pow(10f, volumeValue/MULTIPLIER)));
                // возвращаем f возведённое в p, то есть делаем обратное десятичное логарифмирование.
                slider.value = Mathf.Pow(10f, volumeValue/MULTIPLIER);
            }

            private void ParameterSetter(float value)
            {
                //Debug.Log("логарифм = " + Mathf.Log10(value));
                //Debug.Log("логарифм + константа = " + (Mathf.Log10(value)*MULTIPLIER));
                // принимаем значение от слайдера (от 0 до 1) и переводим в комфортные значения миксера децибелов(от 0 до -80)
                // заливаем значение слайдера в десятичный логарифм и умножаем на константу. 20 - удобное значение.
                volumeValue = Mathf.Log10(value) * MULTIPLIER;
                audioMixer.SetFloat(volumeParam, volumeValue);
            }

            private void OnDisable() { PlayerPrefs.SetFloat(volumeParam, volumeValue); }
        }
    }
}