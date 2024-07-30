using UnityEngine;

namespace GameRes.SO
{
    public class LevelData 
    {
        public int index;
        public string levelName;
        public int targetPoint;
        public string BgPath => "Assets/GameRes/Art/ba/LevelBG/" + index + ".png";
        public string LevelDesc => "level" + levelName;
    }
}