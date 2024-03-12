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
            /// ������������� ��������� ����� � ������������ �� ���������.
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

                //Debug.Log("������ Log10 = " + (Mathf.Pow(10f, volumeValue/MULTIPLIER)));
                // ���������� f ���������� � p, �� ���� ������ �������� ���������� ����������������.
                slider.value = Mathf.Pow(10f, volumeValue/MULTIPLIER);
            }

            private void ParameterSetter(float value)
            {
                //Debug.Log("�������� = " + Mathf.Log10(value));
                //Debug.Log("�������� + ��������� = " + (Mathf.Log10(value)*MULTIPLIER));
                // ��������� �������� �� �������� (�� 0 �� 1) � ��������� � ���������� �������� ������� ���������(�� 0 �� -80)
                // �������� �������� �������� � ���������� �������� � �������� �� ���������. 20 - ������� ��������.
                volumeValue = Mathf.Log10(value) * MULTIPLIER;
                audioMixer.SetFloat(volumeParam, volumeValue);
            }

            private void OnDisable() { PlayerPrefs.SetFloat(volumeParam, volumeValue); }
        }
    }
}