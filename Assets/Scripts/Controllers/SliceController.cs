using Abstracts;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class SliceController : MonoBehaviour, IGround
    {
        private GroundType _type = GroundType.Normal;

        public GroundType GetGroundType()
        {
            return _type;
        }

        public void SetGroundType(GroundType type)
        {
            _type = type;
        }
    }
}