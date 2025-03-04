using Enums;

namespace Abstracts
{
    public interface IGround
    {
        GroundType GetGroundType();
        void SetGroundType(GroundType groundType);
    }
}