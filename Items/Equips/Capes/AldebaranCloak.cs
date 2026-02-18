using CalValEX.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Equips.Capes
{
    [AutoloadEquip(EquipType.Front, EquipType.Back)]
    public class AldebaranCloak : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<Aqua>();
            Item.accessory = true;
            Item.vanity = true;
        }
    }
}