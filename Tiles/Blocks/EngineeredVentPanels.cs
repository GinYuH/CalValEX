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
            Main.tileMergeDirt[Type] = false;
            Main.tileBrick[Type] = true;
            Main.tileShine2[Type] = true;
            AddMapEntry(new Color(115, 123, 138));
            TileID.Sets.GemsparkFramingTypes[Type] = Type;
            HitSound = SoundID.Tink;
            DustType = DustID.Meteorite;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
