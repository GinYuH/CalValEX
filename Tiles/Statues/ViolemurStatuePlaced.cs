using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using CalValEX.NPCs.Critters;
using Terraria.ID;

namespace CalValEX.Tiles.Statues
{
    public class ViolemurStatuePlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            Terraria.ID.TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Violemur Statue");
            AddMapEntry(new Color(144, 148, 144), name);
            DustType = DustID.Silver;
            
        }

        public override void HitWire(int i, int j)
        {
            // Find the coordinates of top left tile square through math
            int y = j - Main.tile[i, j].TileFrameY / 18;
            int x = i - Main.tile[i, j].TileFrameX / 18;

            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x, y + 2);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
            Wiring.SkipWire(x + 1, y + 2);

            // We add 16 to x to spawn right between the 2 tiles. We also want to right on the ground in the y direction.
            int spawnX = x * 16 + 16;
            int spawnY = (y + 3) * 16;

            // This example shows both item spawning code and npc spawning code, you can use whichever code suits your mod

            // If you want to make an NPC spawning statue, see below.
            int npcIndex = -1;
            // 30 is the time before it can be used again. NPC.MechSpawn checks nearby for other spawns to prevent too many spawns. 3 in immediate vicinity, 6 nearby, 10 in world.
            int critter;
            if (Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().ZoneAstral)
            {
                critter = NPCType<Blightolemur>();
            }
            else
            {
                critter = NPCType<Violemur>();
            }
            if (Wiring.CheckMech(x, y, 30) && NPC.MechSpawn((float)spawnX, (float)spawnY, (critter)))
            {
                npcIndex = NPC.NewNPC(new Terraria.DataStructures.EntitySource_SpawnNPC(),spawnX, spawnY - 12, (critter));
            }
            if (npcIndex >= 0)
            {
                Main.npc[npcIndex].value = 0f;
                Main.npc[npcIndex].npcSlots = 0f;
                // Prevents Loot if NPCID.Sets.NoEarlymodeLootWhenSpawnedFromStatue and !Main.HardMode or NPCID.Sets.StatueSpawnedDropRarity != -1 and NextFloat() >= NPCID.Sets.StatueSpawnedDropRarity or killed by traps.
                // Prevents CatchNPC
                Main.npc[npcIndex].SpawnedFromStatue = true;
            }
        }
    }
}