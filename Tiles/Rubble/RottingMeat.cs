using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.Rubble
{
    public class RottingMeatLarge : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(3, 2);
            FlexibleTileWand.RubblePlacementLarge.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatLarge2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(3, 1);
            FlexibleTileWand.RubblePlacementLarge.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatMedium : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 2);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatMedium2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 1);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatMedium3 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 1);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatSmall : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatSmall2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatSmall3 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
    public class RottingMeatSmall4 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ItemID.AntlionMandible, Type, 0);
            RegisterItemDrop(ItemID.AntlionMandible);
        }
    }
}