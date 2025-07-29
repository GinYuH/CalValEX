using Terraria;
using Terraria.GameContent.ItemDropRules;


namespace CalValEX
{
    public class MasterRevCondition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return Main.masterMode;
                return CalValEX.CalamityActive && ((bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance") || Main.masterMode);
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class FogboundCondition : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && (bool)CalValEX.Calamity.Call("GetBossDowned", "adulteidolonwyrm") && !CalValEX.AprilFoolMonth;
            }

            public bool CanShowItemDropInUI()
            {
                return false;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class FogboundCondition2 : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && !(bool)CalValEX.Calamity.Call("GetBossDowned", "adulteidolonwyrm") && !CalValEX.AprilFoolMonth;
            }

            public bool CanShowItemDropInUI()
            {
                return false;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class Polteralive : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return (CalValEX.CalamityActive && !(bool)CalValEX.Calamity.Call("GetBossDowned", "polterghast") && ((bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance") || Main.masterMode)) || (!CalValEX.CalamityActive && Main.masterMode);
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class Polterdead : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return (CalValEX.CalamityActive && (bool)CalValEX.Calamity.Call("GetBossDowned", "polterghast") && ((bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance") || Main.masterMode)) || (!CalValEX.CalamityActive && Main.masterMode);
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class YharonDowned : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && (bool)CalValEX.Calamity.Call("GetBossDowned", "yharon");
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class BlockDrops : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return !CalValEXConfig.Instance.ConfigBossBlocks && CalValEX.CalamityActive;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class SilvaCrystal : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && !CalValEXConfig.Instance.ConfigBossBlocks && (bool)CalValEX.Calamity.Call("GetBossDowned", "yharon");
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class OtherworldlyStoneDrop : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && !CalValEXConfig.Instance.ConfigBossBlocks && (bool)CalValEX.Calamity.Call("GetBossDowned", "devourerofgods");
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class LevihitaPlushies : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && (NPC.CountNPCS(CalValEX.CalamityNPC("Anahita")) + NPC.CountNPCS(CalValEX.CalamityNPC("Leviathan")) <= 1)
                    && (((bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance") || Main.masterMode) || (!CalValEX.CalamityActive && Main.masterMode));
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class Levihita : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && (NPC.CountNPCS(CalValEX.CalamityNPC("Anahita")) + NPC.CountNPCS(CalValEX.CalamityNPC("Leviathan")) <= 1) && !Main.expertMode;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class DeusFUCKMasorev : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {

                if (CalValEX.CalamityActive)
                {
                    float[] deusAI = (float[])CalValEX.Calamity.Call("GetCalamityAI", info.npc);
                    return ((bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance") || Main.masterMode) && CalValEX.CalamityActive && !(deusAI[0] == 0f ||
                        (((bool)CalValEX.Calamity.Call("GetDifficultyActive", "death") || (bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush")) && deusAI[0] != 3f));
                }
                return false;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class DeusFUCK : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {

                if (CalValEX.CalamityActive)
                {
                    float[] deusAI = (float[])CalValEX.Calamity.Call("GetCalamityAI", info.npc);
                    return CalValEX.CalamityActive && !(deusAI[0] == 0f || (((bool)CalValEX.Calamity.Call("GetDifficultyActive", "death") || (bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush")) && deusAI[0] != 3f));
                }
                return false;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class DeusFUCKBlight : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (CalValEX.CalamityActive)
                {
                    float[] deusAI = (float[])CalValEX.Calamity.Call("GetCalamityAI", info.npc);
                    return (Main.LocalPlayer.InModBiome<Biomes.AstralBlight>() || Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().Blok) && CalValEX.CalamityActive && !(deusAI[0] == 0f ||
                        (((bool)CalValEX.Calamity.Call("GetDifficultyActive", "death") || (bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush")) && deusAI[0] != 3f));
                }
                return false;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class Exodrop : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && NPC.CountNPCS(CalValEX.CalamityNPC("ThanatosHead")) +
                NPC.CountNPCS(CalValEX.CalamityNPC("AresBody")) +
                NPC.CountNPCS(CalValEX.CalamityNPC("Apollo")) <= 1;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class ExoPlush : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && ((bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance") || Main.masterMode) && NPC.CountNPCS(CalValEX.CalamityNPC("ThanatosHead")) +
                NPC.CountNPCS(CalValEX.CalamityNPC("AresBody")) +
                NPC.CountNPCS(CalValEX.CalamityNPC("Apollo")) <= 1;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class ExoPlating : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && (NPC.CountNPCS(CalValEX.CalamityNPC("ThanatosHead")) +
                NPC.CountNPCS(CalValEX.CalamityNPC("AresBody")) +
                NPC.CountNPCS(CalValEX.CalamityNPC("Apollo")) <= 1) && !Main.expertMode && !CalValEXConfig.Instance.ConfigBossBlocks;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class Fogdowned : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return CalValEX.CalamityActive && !CalValEXWorld.downedFogbound; // Calamity must be active since the lore item relies on Calamity
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class MeldosaurusDowned : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                return !CalValEXWorld.downedMeldosaurus;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class JunkoDrop : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return info.npc.GetGlobalNPC<CalValEXGlobalNPC>().junkoReference && CalValEX.CalamityActive;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class RoverDrop : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return info.npc.GetGlobalNPC<CalValEXGlobalNPC>().wolfram && CalValEX.CalamityActive;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class GeldonDrop : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return info.npc.GetGlobalNPC<CalValEXGlobalNPC>().geldonSummon && CalValEX.CalamityActive;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
        }
        public class DogeDrop : IItemDropRuleCondition
        {
            public bool CanDrop(DropAttemptInfo info)
            {
                if (!CalValEX.CalamityActive)
                    return false;
                return info.npc.GetGlobalNPC<CalValEXGlobalNPC>().bdogeMount && CalValEX.CalamityActive;
            }

            public bool CanShowItemDropInUI()
            {
                return true;
            }

            public string GetConditionDescription()
            {
                return null;
            }
    }
    public class IsNightDrop : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return !Main.dayTime;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class DropsEnabled : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return !CalValEXConfig.Instance.DisableVanityDrops;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class ProvidenceDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!CalValEX.CalamityActive)
                return false;
            return (bool)CalValEX.Calamity.Call("GetBossDowned", "providence");
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class MidhardmodeDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!CalValEX.CalamityActive)
                return NPC.downedPlantBoss;
            return (bool)CalValEX.Calamity.Call("GetBossDowned", "calamitasclone") || NPC.downedPlantBoss;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class PolterDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!CalValEX.CalamityActive)
                return false;
            return (bool)CalValEX.Calamity.Call("GetBossDowned", "polterghast");
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class AquaDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!CalValEX.CalamityActive)
                return false;
            return (bool)CalValEX.Calamity.Call("GetBossDowned", "aquaticscourge");
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class ThanatosDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!CalValEX.CalamityActive)
                return false;
            return (bool)CalValEX.Calamity.Call("GetBossDowned", "thanatos");
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class AresDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!CalValEX.CalamityActive)
                return false;
            return (bool)CalValEX.Calamity.Call("GetBossDowned", "ares");
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class TwinsDowned : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!CalValEX.CalamityActive)
                return false;
            return (bool)CalValEX.Calamity.Call("GetBossDowned", "apollo");
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class CalamityDay : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalValEX.month == 6 && CalValEX.day == 1;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class April14 : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return CalValEX.month == 4 && CalValEX.day == 14;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class MoonLord : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return NPC.downedMoonlord;
        }

        public bool CanShowItemDropInUI()
        {
            return NPC.downedMoonlord;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class Infernum : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (CalValEX.instance.infernum == null)
                return false;
            return (bool)CalValEX.instance.infernum.Call("GetInfernumActive");
        }

        public bool CanShowItemDropInUI()
        {
            return CalValEX.instance.infernum != null;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
}