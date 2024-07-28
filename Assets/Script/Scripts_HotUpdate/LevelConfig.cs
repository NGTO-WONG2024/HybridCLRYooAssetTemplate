using UnityEngine;

namespace GameRes.SO
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public int index;
        public string levelName;
        public string levelDesc=> "level"+levelName;
        public int targetPoint;
        public string bgPath => "Assets/GameRes/Art/ba/LevelBG/" + index + ".png";

    }
}