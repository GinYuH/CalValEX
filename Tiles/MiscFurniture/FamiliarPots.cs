using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.MiscFurniture
{
    public class FamiliarPotPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 2);
            HitSound = SoundID.Shatter;
        }
    }
    public class FamiliarTallPotPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 3);
            HitSound = SoundID.Shatter;
        }
    }
    public class FamiliarUrnPotPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 2);
            HitSound = SoundID.Shatter;
        }
    }
    public class FamiliarSkinnyPotPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 2);
            HitSound = SoundID.Shatter;
        }
    }
}