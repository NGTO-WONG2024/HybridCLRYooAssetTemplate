using Sirenix.OdinInspector;
using UnityEngine;
using Newtonsoft.Json;

namespace Script
{
    public class StudentData
    {
        public string id;
        public string ja;
        public string en;
        public string headIconPath => "Assets/GameRes/Art/ba/head/266px-" + en;
        public string schoolIconPath => "Assets/GameRes/Art/ba/icon/" + school.ToString();
        public School school;
        public int attack = 1;

        public enum School
        {
            /// <summary>
            /// 三一
            /// </summary>
            trinity,
            /// <summary>
            /// 千年科学
            /// </summary>
            millennium,
            /// <summary>
            /// 格黑娜
            /// </summary>
            gehenna,
            /// <summary>
            /// 阿里乌斯
            /// </summary>
            arius,
            /// <summary>
            /// 阿拜多斯
            /// </summary>
            abydos,
            /// <summary>
            /// 红冬
            /// </summary>
            red_winter,
            /// <summary>
            /// 百鬼夜行
            /// </summary>
            hyakkiyako,
            /// <summary>
            /// Valkyrie
            /// </summary>
            valkyrie,
            /// <summary>
            /// SRT
            /// </summary>
            srt,
            /// <summary>
            /// etc
            /// </summary>
            etc,
            /// <summary>
            /// shanhaijing
            /// </summary>
            shanhaijing,
        }

    }
}







