using UnityEngine;

namespace FirePaw
{
    namespace UI
    {
        [RequireComponent(typeof(AudioSource))]
        public class UISound : MonoBehaviour
        {
            [SerializeField] private AudioClip click;

            private new AudioSource audio;

            private UIButton[] uiButton;

            private void Start()
            {
                audio    = GetComponent<AudioSource>();
                uiButton = GetComponentsInChildren<UIButton>(true);

                for (int i = 0; i < uiButton.Length; i++)
                    uiButton[i].PointerClick += OnPointClicked;
            }

            private void OnDestroy()
            {
                for (int i = 0; i < uiButton.Length; i++)
                    uiButton[i].PointerClick -= OnPointClicked;
            }

            private void OnPointClicked(UIButton arg0) { audio.PlayOneShot(click); }
        }
    }
}