using System.Threading.Tasks;

namespace Script.Scripts_HotUpdate
{
    public interface ISensei
    {
        public string Name { get; } 
        public string Desc { get; } 
        public int Cost { get; } 
        
        public string HeadIconPath => "Assets/GameRes/Art/ba/sensei/" + Name;
        public StudentData Buff_BeforeAttack(StudentData original);
    }

    
    /// <summary>
    /// 攻击 +10
    /// </summary>
    public class Sensei1 : ISensei
    {
        public string Name { get; } = "sensei1";
        public string Desc { get; } = "攻击 +10";
        public int Cost { get; } = 1;

        public StudentData Buff_BeforeAttack(StudentData original)
        {
            var t = new StudentData();
            t.attack = original.attack + 10;
            return t;
        }
    }
    
    /// <summary>
    /// 攻击 = 学生数量
    /// </summary>
    public class Sensei2 : ISensei
    {
        public string Name { get; } = "sensei2";
        public string Desc { get; } = "攻击 = 学生数量";
        public int Cost { get; } = 10;

        public StudentData Buff_BeforeAttack(StudentData original)
        {
            var t = new StudentData();
            t.attack = Balatro.Instance.senseiCards.Length;
            return t;
        }
    }
}