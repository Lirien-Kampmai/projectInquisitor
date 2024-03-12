using UnityEngine;
using UnityEngine.UI;

namespace FirePaw
{
    namespace UI
    {
        public class UISettingsButton : UISelectableButton
        {
            [SerializeField] private Settings setting;

            public void SetNextSetting()
            {
                setting?.SetNextValue();
                setting?.Apply();
            }

            public void SetPreviosSetting()
            {
                setting?.SetPreviosValue();
                setting?.Apply();
            }

            public void SetSetting()
            {
                setting?.SetFloatValue();
                setting?.Apply();
            }

            private void Start()
            {
                ApplyProperty(setting);
            }

            public void ApplyProperty(Settings property)
            {
                if (property == null) return;

                setting = property;
            }
        }
    }
}