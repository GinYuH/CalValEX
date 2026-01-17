using CalValEX.Items.Tiles.Paintings;
using CalValEX.Tiles.Paintings;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items
{
    public class MysteryPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mystery Painting");
            // Tooltip.SetDefault("<right> for a random painting");
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 9999;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            List<int> paintings = new List<int>()
            { 
                ModContent.ItemType<TyrantFeed>(),
                ModContent.ItemType<Ohio>(),
                ModContent.ItemType<Clam>()
            };

            // Paintings that shouldn't be part of the pool
            List<string> blacklistedPaintings = new List<string>()
            {
                "Signut",
                "Scourgie"
            };

            foreach (var i in PaintingLoader.paintingItems)
            {
                if (blacklistedPaintings.Contains(i.Key))
                    continue;
                int id = i.Value;
                if (ContentSamples.ItemsByType[id].rare == ItemRarityID.Gray)
                    continue;
                paintings.Add(id);
            }

            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, paintings.ToArray()));
		}
    }
}