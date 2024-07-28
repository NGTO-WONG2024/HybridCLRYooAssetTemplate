using System.Threading.Tasks;
using UnityEngine;

namespace Script.Scripts_HotUpdate
{
    public class SenseiCard : CardBase
    {
        private ISensei sensei;
        public async void SetUp(ISensei s, Transform parent = null)
        {
            sensei = s;
            if (parent != null)
            {
                transform.SetParent(parent);
                transform.localPosition = Vector3.zero;
            }
            var path = "Assets/GameRes/Art/ba/sensei/" + sensei.Name + ".png";
            images["headIcon"].sprite = await ResManager.Instance.Load<Sprite>(path);
            texts["name"].text = sensei.Name;
        }

        public async Task<StudentData> Buff_BeforeAttack(StudentData data)
        {
            await PlayFeelAsync("beforeAttackFeel");
            return sensei.Buff_BeforeAttack(data);
        }

        public override void OnClick()
        {
            base.OnClick();
            if (Balatro.Instance.Coin > sensei.Cost)
            {
                transform.SetParent(Balatro.Instance.senseiArea);
                transform.localPosition = Vector3.zero;
                Balatro.Instance.Coin -= sensei.Cost;
            }
        }
    }
    
}