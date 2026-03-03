using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Tiles.Blocks
{
    public class EngineeredContructPanels : ModTile
    {   int subsheetHeight = 90;
        int subsheetWidth = 324;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBrick[Type] = true;
            Main.tileShine2[Type] = true;
            AddMapEntry(new Color(80, 88, 102));
            TileID.Sets.GemsparkFramingTypes[Type] = Type;
            HitSound = SoundID.Tink;
            DustType = DustID.Meteorite;
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
            return false;
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            int xPos = i % 6;
            int yPos = j % 6;
            frameXOffset = xPos * subsheetWidth;
            frameYOffset = yPos * subsheetHeight;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}