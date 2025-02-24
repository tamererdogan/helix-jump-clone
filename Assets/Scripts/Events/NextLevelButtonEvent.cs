using Abstracts;
using UnityEngine.Events;

namespace Events
{
    public class NextLevelButtonEvent : AbstractButtonEvent
    {
        public static UnityAction OnButtonClicked = delegate {};

        protected override void OnClick()
        {
            OnButtonClicked.Invoke();
        }
    }
}