using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.Plants
{
    public class CrepuscularMallowPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 5);
        }
    }
    public class ArcticWisteriaPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 5);
        }
    }
    public class EverfrostPinePlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 5);
        }
    }

    // These have special sheeting
    public class PaleblotBanePlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateWidth = 28;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.SaddleBrown, name);
        }
    }
    public class SpiderPalmPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateWidth = 28;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.SaddleBrown, name);
        }
    }
    public class MarrowWillowPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.CoordinateWidth = 20;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.SaddleBrown, name);
        }
    }
}