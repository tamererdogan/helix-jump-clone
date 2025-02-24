using Abstracts;
using UnityEngine.Events;

namespace Events
{
    public class TapToStartButtonEvent : AbstractButtonEvent
    {
        public static UnityAction OnButtonClicked = delegate {};

        protected override void OnClick()
        {
            OnButtonClicked.Invoke();
        }
    }
}