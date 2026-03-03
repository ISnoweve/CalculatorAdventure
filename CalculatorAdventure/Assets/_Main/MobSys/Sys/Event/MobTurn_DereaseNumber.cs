namespace _Main.MobSys.Sys.Event
{
    public readonly struct MobTurn_DereaseNumber
    {
        private readonly int mobAttackSkillCountDown;
        public int MobAttackSkillCountDown => mobAttackSkillCountDown;
        public MobTurn_DereaseNumber(int mobAttackSkillCountDown)
        {
            this.mobAttackSkillCountDown = mobAttackSkillCountDown;
        }
    }
}