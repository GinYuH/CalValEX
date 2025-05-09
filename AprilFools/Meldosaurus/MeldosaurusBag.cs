﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace CalValEX.AprilFools.Meldosaurus
{
	public class MeldosaurusBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 3;
			// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Cyan;
			Item.expert = true;
		}

		public override bool CanRightClick() => true;

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<MeldosaurusMask>(), 7));

			/*if (CalValEX.CalamityActive)
			{
				itemLoot.Add(ItemDropRule.NotScalingWithLuck(CalValEX.CalamityItem("MeldBlob"), 1, 1, 2));
				itemLoot.Add(CalamityMod.DropHelper.CalamityStyle(CalamityMod.DropHelper.BagWeaponDropRateFraction, new int[]
				{
				ModContent.ItemType<Nyanthrop>(),
				ModContent.ItemType<ShadesBane>()
				}));
			}*/

            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Meldosaurus>()));
		}
	}
}