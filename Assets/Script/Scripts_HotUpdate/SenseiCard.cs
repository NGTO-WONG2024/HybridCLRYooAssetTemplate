namespace Script.Scripts_HotUpdate
{
    public abstract class SenseiCard : CardBase
    {
        public StudentData Buff_BeforeAttack(StudentData original)
        {
            var newData = new StudentData();
            newData = BeforeAttack(newData,original);
            return newData;
        }

        protected abstract StudentData BeforeAttack(StudentData newData,StudentData original);
    }
    
}