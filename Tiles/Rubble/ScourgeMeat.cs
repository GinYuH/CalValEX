using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.Rubble
{
    public class ScourgeMeatLarge : ModTile
    {
        // Item relations arent defined until postsetup
        public static int Mandible => CalamityID.CalamityID.ItemRelation("StormlionMandible", "Electrocells", ItemID.AntlionMandible);
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(3, 2);
            FlexibleTileWand.RubblePlacementLarge.AddVariation(Mandible, Type, 0);
            RegisterItemDrop(Mandible);
        }
    }
    public class ScourgeMeatLarge2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(3, 2);
            FlexibleTileWand.RubblePlacementLarge.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatMedium : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 2);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatMedium2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 1);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatMedium3 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 1);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatMedium4 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 2);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatSmall : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatSmall2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatSmall3 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatSmall4 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
    public class ScourgeMeatSmall5 : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(1, 1);
            FlexibleTileWand.RubblePlacementSmall.AddVariation(ScourgeMeatLarge.Mandible, Type, 0);
            RegisterItemDrop(ScourgeMeatLarge.Mandible);
        }
    }
}