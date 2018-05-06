

namespace OctoBot.Games.OctoGame.GameSpells
{
    public class SpellSetting
    {
        // 1 - AD
        // 2 - DEF
        // 3 - AGI
        // 4 - AP
        public ulong SpellId { get; set; }
        public string SpellName { get; set; }
        
        public int ActiveOrPassive { get; set; }
        public int SpellTree { get; set; }
        public string SpellDescriptionRu { get; set; }
        public string SpellDescriptionEn { get; set; }
        public string SpellFormula { get; set; }
        public string SpellDmgType { get; set; }
        public int SpellCd { get; set; }
        public string Onhit { get; set; }
        public string Poisen { get; set; }
        public string Buff { get; set; }
        public string DeBuff { get; set; }

        /*
        public List<CreateSpellCd> SpellCd { get; internal set; } = new List<CreateSpellCd>();

        public struct CreateSpellCd
        {
            public int CdTimeInSec;
            public DateTime SpellTimeCd;
            public int ActiveSkill;

            public CreateSpellCd(int cdTimeInSec, DateTime spellTimeCd, int activeSkill)
            {
                CdTimeInSec = cdTimeInSec;
                SpellTimeCd = spellTimeCd;
                ActiveSkill = activeSkill;
            }
        }*/

    }
}
