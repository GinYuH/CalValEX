using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using CalValEX.NPCs.Critters;
using Terraria.ID;

namespace CalValEX.Tiles.Statues
{
    public class EyedolStatuePlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Terraria.ID.TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Eyedol Statue");
            AddMapEntry(new Color(144, 148, 144), name);
            DustType = DustID.Silver;
            
        }

        public override void HitWire(int i, int j)
        {
            int y = j - Main.tile[i, j].TileFrameY / 18;
            int x = i - Main.tile[i, j].TileFrameX / 18;

            Wiring.SkipWire(x, y);
            Wiring.SkipWire(x, y + 1);
            Wiring.SkipWire(x, y + 2);
            Wiring.SkipWire(x + 1, y);
            Wiring.SkipWire(x + 1, y + 1);
            Wiring.SkipWire(x + 1, y + 2);

            int spawnX = x * 16 + 16;
            int spawnY = (y + 3) * 16;

            int npcIndex = -1;
            if (Wiring.CheckMech(x, y, 30) && NPC.MechSpawn((float)spawnX, (float)spawnY, NPCType<Eyedol>()))
            {
                npcIndex = NPC.NewNPC(new Terraria.DataStructures.EntitySource_TileBreak(i, j), spawnX, spawnY - 12, NPCType<Eyedol>());
            }
            if (npcIndex >= 0)
            {
                Main.npc[npcIndex].value = 0f;
                Main.npc[npcIndex].npcSlots = 0f;
                Main.npc[npcIndex].SpawnedFromStatue = true;
            }
        }
    }
}