using CalValEX.CalamityID;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Mounts.Minecart
{
    public class DraedonCart : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = CalRarityID.Exo;
            Item.UseSound = SoundID.NPCHit4;
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<DraedonCartMount>();
        }
    }
}