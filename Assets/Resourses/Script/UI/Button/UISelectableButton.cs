using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace FirePaw
{
    namespace UI
    {
        public class UISelectableButton : UIButton
        {
            /// <summary>
            /// скрипт на будущее. либо он, либо прописывать в UIButton эффект селекта и не страдать хернёй с наследованием
            /// </summary>
            [SerializeField] private Image selectImage;

            public UnityEvent OnSelect;
            public UnityEvent OnUnSelect;

            public override void SetOnFocus()
            {
                base.SetOnFocus();
                selectImage.enabled = true;
                OnSelect?.Invoke();
            }

            public override void SetOffFocus()
            {
                base.SetOffFocus();
                selectImage.enabled = false;
                OnUnSelect?.Invoke();
            }
        }
    }
}