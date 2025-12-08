global using Microsoft.Xna.Framework;
using System;
using System.IO;
using CalValEX.ExtraTextures.ChristmasPets;
using CalValEX.Biomes;
using Terraria.ModLoader;
using CalValEX.Items.Pets;
using CalValEX.NPCs.Oracle;
using CalValEX.NPCs.JellyPriest;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using ReLogic.Content;
using CalValEX.Items;
using CalValEX.Projectiles;
using CalValEX.Items.Tiles;

namespace CalValEX
{
    public partial class CalValEX : Mod
    {
        public enum MessageType
        {
            SyncOraclePlayer = 0,
            PlayerBagChanged,
            SyncCalValEXPlayer,
            SyncSCalHits
        }

        public static CalValEX instance;
        public Mod herosmod;
        public Mod ortho;
        public Mod bossChecklist;
        public Mod cata;
        public Mod fables;
        public Mod wotg;
        public Mod hunt;
        public Mod infernum;
        public Mod cremix;
        public Mod subworldLibrary;
        public Mod sloome;
        public Mod souls;
        public Mod soulsDLC;

        public const string heropermission = "CalValEX";
        public const string heropermissiondisplayname = "Calamity's Vanities";
        public bool hasPermission;

        public static bool Bumble;

        public static bool AprilFoolMonth;
        public static bool AprilFoolWeek;
        public static bool AprilFoolDay;

        public static string currentDate;
        public static int day;
        public static int month;
        public static Texture2D AstralSky;

        public static bool InfernumActive()
        {
            if (instance.infernum != null)
            {
                if ((bool)instance.infernum.Call("GetInfernumActive"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public static bool CalEModeActive()
        {
            if (instance.souls != null && instance.soulsDLC != null)
            {
                if ((bool)instance.souls.Call("EMode"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }


        public override void Load()
        {
            instance = this;

            calamityActive = ModLoader.TryGetMod("CalamityMod", out calamity);

            ModLoader.TryGetMod("HEROsMod", out herosmod);
            ModLoader.TryGetMod("CalValPlus", out ortho);
            ModLoader.TryGetMod("BossChecklist", out bossChecklist);
            ModLoader.TryGetMod("NoxusBoss", out wotg);
            ModLoader.TryGetMod("InfernumMode", out infernum);
            ModLoader.TryGetMod("CalamityHunt", out hunt);
            ModLoader.TryGetMod("CalRemix", out cremix);
            ModLoader.TryGetMod("SubworldLibrary", out subworldLibrary);
            ModLoader.TryGetMod("Bloopsitems", out sloome);
            ModLoader.TryGetMod("CalamityFables", out fables);
            ModLoader.TryGetMod("FargowiltasSouls", out souls);
            ModLoader.TryGetMod("FargowiltasCrossmod", out soulsDLC);

            DateTime dateTime = DateTime.Now;
            currentDate = dateTime.ToString("dd/MM/yyyy");
            day = dateTime.Day;
            month = dateTime.Month;

            AprilFoolWeek = ortho != null || (DateTime.Now.Month == 4 && DateTime.Now.Day <= 7);
            AprilFoolDay = ortho != null || (DateTime.Now.Month == 4 && DateTime.Now.Day == 1);
            AprilFoolMonth = ortho != null || (DateTime.Now.Month == 4);

            AstralSky = ModContent.Request<Texture2D>("CalValEX/Biomes/AstralSky", AssetRequestMode.ImmediateLoad).Value;
            if (!Main.dedServ)
                AddBossHeadTexture("CalValEX/NPCs/AstrumDeus/AstrumDeusMap", -1);

            if (Main.dedServ)
                return;
            SkyManager.Instance["CalValEX:AstralBiome"] = new AstralSky();

            //ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            //if (wikithis != null)
                //wikithis.Call("AddModURL", this, "terrariamods.fandom.com$Calamity%27s_Vanities");
        }

        public override void Unload()
        {
            instance = null;
            herosmod = null;
            ortho = null;
            bossChecklist = null;
            cata = null;
            infernum = null;
            hasPermission = false;
            wotg = null;
            hunt = null;
            cremix = null;
            subworldLibrary = null;
            sloome = null;
            fables = null;

            currentDate = null;
            Bumble = false;
            day = -1;
            month = -1;
            AstralSky = null;
            AprilFoolDay = false;
            AprilFoolWeek = false;
            AprilFoolMonth = false;

            if (Main.dedServ)
                return;

            ChristmasTextureChange.Unload();
        }

        public override void PostSetupContent()
        {
            if (CalamityActive)
            {
                Mod cal = ModLoader.GetMod("CalamityMod");
                cal.Call("MakeItemExhumable", ModContent.ItemType<RottingCalamitousArtifact>(), ModContent.ItemType<CalamitousSoulArtifact>());
                cal.Call("MakeItemExhumable", ModContent.ItemType<HeartoftheCommunity>(), ModContent.ItemType<ShatteredHeartoftheCommunity>());
                cal.Call("RegisterNPCShop", ModContent.NPCType<JellyPriestNPC>(), (Player player) => Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().jellyInv, (Player player, bool enabled) => Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().jellyInv = enabled);
                cal.Call("RegisterNPCShop", ModContent.NPCType<OracleNPC>(), (Player player) => Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().oracleInv, (Player player, bool enabled) => Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().oracleInv = enabled);
                cal.Call("AddAndrombaSolution", ModContent.ItemType<XenoSolution>(), "CalValEX/ExtraTextures/AndroombaBlight", (NPC npc) => AstralSolutionProj.Convert((int)(npc.position.X + npc.width / 2) / 16, (int)(npc.position.Y + npc.height / 2) / 16, 3));
            }

            //Christmas textures
            ChristmasTextureChange.Load();
        }

        public override object Call(params object[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
            }

            if (args.Length == 0)
            {
                throw new ArgumentException("Arguments cannot be empty!");
            }

            if (args[0] is string content)
            {
                switch (content)
                {
                    case "downedMeldosaurus":
                    case "meldosaurus":
                    case "downedmeldosaurus":
                    case "Meldosaurus":
                        return CalValEXWorld.downedMeldosaurus;
                    case "downedStratusApocalypse":
                    case "downedstratusapocalypse":
                    case "amogus":
                    case "downedamogus":
                        return CalValEXWorld.amogus;
                    case "inAstralBlight":
                    case "AstralBlight":
                    case "inastralblight":
                    case "oldastral":
                    case "astralblight":
                        return Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().ZoneAstral;
                }
            }

            return false;
        }

        public static void MountNerf(Player player, float reduceDamageBy, float reduceHealthBy)
        {
            bool bossIsAlive = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.boss)
                {
                    bossIsAlive = true;
                }
            }

            if (bossIsAlive && !CalValEXConfig.Instance.GroundMountLol)
            {
                int calculateLife = (int)(player.statLifeMax2 * reduceHealthBy);
                player.statLifeMax2 -= calculateLife;
                player.GetDamage(DamageClass.Generic) -= reduceDamageBy;
            }
        }

        public static bool DetectProjectile(int proj)
        {
            bool bossIsAlive = false;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile pro = Main.projectile[i];
                if (pro.type == proj && pro.active && pro != null)
                {
                    bossIsAlive = true;
                }
            }
            return bossIsAlive;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();
            byte playerNumber;
            OraclePlayer oraclePlayer;
            CalValEXPlayer calValEXPlayer;
            int SCalHits;
            switch (msgType)
            {
                case MessageType.SyncOraclePlayer:
                    playerNumber = reader.ReadByte();
                    oraclePlayer = Main.player[playerNumber].GetModPlayer<OraclePlayer>();
                    oraclePlayer.playerHasGottenBag = reader.ReadBoolean();
                    break;

                case MessageType.PlayerBagChanged:
                    playerNumber = reader.ReadByte();
                    oraclePlayer = Main.player[playerNumber].GetModPlayer<OraclePlayer>();
                    oraclePlayer.playerHasGottenBag = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.PlayerBagChanged);
                        packet.Write(playerNumber);
                        packet.Write(oraclePlayer.playerHasGottenBag);
                        packet.Send(-1, playerNumber);
                    }

                    break;

                case MessageType.SyncCalValEXPlayer:
                    playerNumber = reader.ReadByte();
                    calValEXPlayer = Main.player[playerNumber].GetModPlayer<CalValEXPlayer>();
                    SCalHits = reader.ReadInt32();
                    calValEXPlayer.SCalHits = SCalHits;
                    break;

                case MessageType.SyncSCalHits:
                    playerNumber = reader.ReadByte();
                    calValEXPlayer = Main.player[playerNumber].GetModPlayer<CalValEXPlayer>();
                    SCalHits = reader.ReadInt32();
                    calValEXPlayer.SCalHits = SCalHits;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte)MessageType.SyncSCalHits);
                        packet.Write(playerNumber);
                        packet.Write(calValEXPlayer.SCalHits);
                        packet.Send(-1, playerNumber);
                    }

                    break;

                default:
                    Logger.WarnFormat("CalValEX: Unknown Message type: {0}", msgType);
                    break;
            }
        }

        public void SetupHerosMod()
        {
            if (herosmod != null)
            {
                herosmod.Call(
                    // Special string
                    "AddPermission",
                    // Permission Name
                    heropermission,
                    // Permission Display Name
                    heropermissiondisplayname);
            }
        }

        public bool getPermission()
        {
            return hasPermission;
        }

        internal static bool InAnySubworld()
        {
            if (instance.subworldLibrary is null)
                return false;

            foreach (Mod mod in ModLoader.Mods)
            {
                if (mod.Name.Equals(instance.subworldLibrary.Name))
                    continue;

                bool anySubworldForMod = (instance.subworldLibrary.Call("AnyActive", mod) as bool?) ?? false;
                if (anySubworldForMod)
                    return true;
            }
            return false;
        }
    }
}