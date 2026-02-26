using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;

namespace CalValEX.Tiles.Blocks
{
    public class EngineeredVentPanels : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(115, 123, 138));
            TileID.Sets.GemsparkFramingTypes[Type] = Type;
            HitSound = SoundID.Tink;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
            return false;
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 57 + Main.rand.Next(2), 0f, 0f, 1, new Color(255, 255, 255));
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
