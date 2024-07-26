using UnityEngine;

namespace Script.Scripts_HotUpdate.Sensei
{
    public class TestSensei : SenseiCard
    {
        protected override StudentData BeforeAttack(StudentData newData, StudentData original)
        {
            newData.attack = original.attack + 10;
            return newData;
        }
    }
}