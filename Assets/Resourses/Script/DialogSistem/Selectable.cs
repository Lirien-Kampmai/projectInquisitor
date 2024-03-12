using UnityEngine;

namespace FirePaw
{
    namespace DialogSistem
    {
        /// <summary>
        /// вешается на префаб генерируемой кнопки выбора в истории.
        /// </summary>
        public class Selectable : MonoBehaviour
        {
            public object element;
            public void Decide() { DialogManager.SetDecision(element); }
        }
    }
}