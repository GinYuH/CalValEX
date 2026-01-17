using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.FurnitureSets.Wulfrum
{
    public class WulfrumGlobePlaced : ModTile {
        public override void SetStaticDefaults() {
            TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };

            AnimationFrameHeight = 36;
            TileObjectData.addTile(Type);

            DustType = DustID.Electric;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter) {
            frameCounter++;
            if (frameCounter > 6) {
                frameCounter = 0;
                frame++;
                if (frame > 15)
                    frame = 0;
            }
        }
    }
}