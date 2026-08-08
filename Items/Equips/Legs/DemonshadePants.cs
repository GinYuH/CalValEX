using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Items.Equips.Legs
{
    [AutoloadEquip(EquipType.Legs)]
    public class DemonshadePants : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.rare = CalamityID.CalRarityID.CalamityRed;
            Item.vanity = true;
            Item.value = Item.sellPrice(0, 3, 0, 0);
        }
    }
}