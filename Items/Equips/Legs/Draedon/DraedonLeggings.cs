using CalValEX.CalamityID;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Equips.Legs.Draedon
{
    [AutoloadEquip(EquipType.Legs)]
    public class DraedonLeggings : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 24;
            Item.rare = CalRarityID.DarkOrange;
            Item.vanity = true;
        }
    }
}