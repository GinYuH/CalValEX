using CalValEX.Items.Mounts.LimitedFlight;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.Plants
{
    public class PhantoSproutPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementLarge.AddVariation(ModContent.ItemType<PhantoSprout>(), Type, 0);
            RegisterItemDrop(ModContent.ItemType<PhantoSprout>());
            TileID.Sets.SwaysInWindBasic[Type] = true;
            HitSound = SoundID.Grass;
        }
    }
}