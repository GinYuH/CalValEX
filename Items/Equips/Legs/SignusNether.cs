using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Items.Equips.Legs
{
    [AutoloadEquip(EquipType.Legs)]
    public class SignusNether : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = CalamityID.CalRarityID.Turquoise;
            //Item.accessory = true;
            Item.vanity = true;
            ArmorIDs.Legs.Sets.OverridesLegs[Item.legSlot] = true;
        }
    }
}