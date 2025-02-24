using System;

namespace Structs
{
    [Serializable]
    public struct Level
    {
        public string platformColor;
        public string playerColor;
        public string normalColor;
        public string obstacleColor;
        public LevelIndices[] indices;
    }
}