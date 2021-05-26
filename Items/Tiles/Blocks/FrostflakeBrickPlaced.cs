using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Items.Tiles.Blocks
{
    public class FrostflakeBrickPlaced : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            drop = ModContent.ItemType<FrostflakeBrick>();
            AddMapEntry(new Color(66, 242, 245));
            dustType = 92;
            minPick = 149;
            soundType = SoundID.Tink;
        }
    }
}