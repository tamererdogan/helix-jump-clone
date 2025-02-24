using UnityEngine;
using UnityEngine.UI;

namespace Abstracts
{
    public abstract class AbstractButtonEvent : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}