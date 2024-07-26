using UnityEngine;

namespace Script
{
    [CreateAssetMenu(menuName = "studentData",fileName = "studentData")]
    public class StudentData : ScriptableObject
    {
        public string studentName;
        public Sprite headIcon;
    }
}