using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;

namespace CalValEX.Tiles.Blocks
{
    public class EngineeredGirder : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(177, 77, 77));
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
