using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace CalValEX
{
    [BackgroundColor(49, 32, 36, 216)]
    public class CalValEXConfig : ModConfig
    {
        public static CalValEXConfig Instance;
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("$Mods.CalValEX.Configs.Drops")]

        [BackgroundColor(192, 54, 64, 192)]
        [ReloadRequired]
        [DefaultValue(false)]
        public bool DisableVanityDrops { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool ConfigBossBlocks { get; set; }

        [Header("$Mods.CalValEX.Configs.Pets")]

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool FatDog { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool SupCombo { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool DragonballName { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool Polterskin { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool Pitbul { get; set; }

        [Header("$Mods.CalValEx.Configs.Gameplay")]

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool TownNPC { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool IsopodBait { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool CritterSpawns { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool GroundMountLol { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(true)]
        [ReloadRequired()]
        public bool UseFables { get; set; }

        [Header("$Mods.CalValEx.Configs.OtherHeader")]

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(true)]
        [ReloadRequired()]
        public bool AprilFoolsContent { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(true)]
        [ReloadRequired()]
        public bool DiscordRichPresence { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool OwnerOnly { get; set; }

        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(false)]
        public bool HerosPerm { get; set; }

        /// <summary>
        /// Checks to see if the player is the current server host. Thanks Jopojelly.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool IsPlayerLocalServerOwner(Player player)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return Netplay.Connection.Socket.GetRemoteAddress().IsLocalHost();
            }

            for (int plr = 0; plr < Main.maxPlayers; plr++)
                if (Netplay.Clients[plr].State == 10 && Main.player[plr] == player && Netplay.Clients[plr].Socket.GetRemoteAddress().IsLocalHost())
                    return true;
            return false;
        }

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            string accept = Language.GetTextValue("Mods.CalValEX.Configs.ChangeGood");

            if (OwnerOnly && IsPlayerLocalServerOwner(Main.player[whoAmI]))
            {
                message = accept;
                return true;
            }
            else if (OwnerOnly && CalValEX.instance.herosmod == null)
            {
                message = Language.GetTextValue("Mods.CalValEX.Configs.HostOnly");
                return false;
            }

            if (HerosPerm && CalValEX.instance.herosmod != null)
            {
                if (CalValEX.instance.herosmod.Call("HasPermission", whoAmI, CalValEX.heropermission) is bool result && result)
                {
                    message = accept;
                    return true;
                }
                message = Language.GetTextValue("Mods.CalValEX.Configs.NoPerms");
                return false;
            }

            message = accept;
            return true;
        }
    }
}
