using CalValEX.CalamityID;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX
{
    public static class CVUtils
    {
        public const string AstrageldonRarity = "SuperbossRarity";
        public const string AvatarRarity = "AvatarRarity";
        public const string NamelessRarity = "NamelessDeityRarity";
        public const string MarsRarity = "GenesisComponentRarity";


        public static int SetRarity(int Rarity, string modRarity = "")
        {
            if (modRarity != "")
            {
                return CrossmodRarity(modRarity);
            }
            int ret = Rarity;
            // CalRarityID isn't done by load time, so unfortunately, this has to be done like this
            if (Rarity > 11)
            {
                ret = CalRarityID.Turquoise;
                switch (Rarity)
                {
                    case 13:
                        ret = CalRarityID.PureGreen;
                        break;
                    case 14:
                        ret = CalRarityID.DarkBlue;
                        break;
                    case 15:
                        ret = CalRarityID.Violet;
                        break;
                    case 16:
                        ret = CalRarityID.HotPink;
                        break;
                }
            }
            return ret;
        }


        // Gives the plush a rarity from another mod
        public static int CrossmodRarity(string rarity)
        {
            string modName = "";
            switch (rarity)
            {
                case AstrageldonRarity:
                    modName = "CatalystMod";
                    break;
                case MarsRarity:
                case NamelessRarity:
                case AvatarRarity:
                    modName = "NoxusBoss";
                    break;
            }

            if (ModLoader.TryGetMod(modName, out Mod modInstance))
            {
                return modInstance.Find<ModRarity>(rarity).Type;
            }
            return ItemRarityID.Gray;
        }


        public static void PetBuff(Player player, int buffIndex, int projType, int amount = 1)
        {
            bool petProjectileNotSpawned = player.ownedProjectileCounts[projType] < amount;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, projType, 0, 0f, player.whoAmI, 0f, 0f);
            }
        }

        public static void CritterBestiary(NPC n, int NPCType)
        {
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player is null || !player.active)
                    continue;

                if (n.Hitbox.Intersects(player.HitboxForBestiaryNearbyCheck))
                {
                    NPC nPC = new NPC();
                    nPC.SetDefaults(NPCType);
                    Main.BestiaryTracker.Kills.RegisterKill(nPC);
                    break;
                }
            }
        }
        public static void PlatformHangOffset(int i, int j, ref int offsetY)
        {
            Tile tile = Main.tile[i, j];
            TileObjectData data = TileObjectData.GetTileData(tile);
            int topLeftX = i - tile.TileFrameX / 18 % data.Width;
            int topLeftY = j - tile.TileFrameY / 18 % data.Height;
            if (WorldGen.IsBelowANonHammeredPlatform(topLeftX, topLeftY))
            {
                offsetY -= 8;
            }
        }

        public static void SetupFurniture(this ModTile tile, int width, int height, bool wall = false, Color map = default, bool frame18 = false, bool lighted = false)
        {
            Main.tileFrameImportant[tile.Type] = true;
            if (lighted)
                Main.tileLighted[tile.Type] = true;
            Main.tileLavaDeath[tile.Type] = true;
            TileID.Sets.DisableSmartCursor[tile.Type] = true;
            if (wall)
            {
                TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
                TileID.Sets.FramesOnKillWall[tile.Type] = true;
            }
            else
            {
                TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            }
            TileObjectData.newTile.Width = width;
            TileObjectData.newTile.Height = height;
            List<int> heightList = new List<int>();
            for (int i = 0; i < height; i++)
            {
                if (i == 0 && frame18)
                {
                    heightList.Add(18);
                }
                else
                {
                    heightList.Add(16);
                }
            }
            TileObjectData.newTile.CoordinateHeights = heightList.ToArray();
            TileObjectData.addTile(tile.Type);
            if (map != default)
            {
                LocalizedText name = tile.CreateMapEntryName();
                tile.AddMapEntry(map, name);
            }
        }
    }
}