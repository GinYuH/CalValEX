using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Items.Equips.Legs
{
    [AutoloadEquip(EquipType.Legs)]
    public class DraedonLegs : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            ArmorIDs.Legs.Sets.OverridesLegs[Item.legSlot] = true;
        }
    }
}