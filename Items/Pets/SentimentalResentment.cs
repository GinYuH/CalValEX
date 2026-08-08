using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalValEX.Items.Pets
{
    [LegacyName("CalamitousSoulArtifact")]
    public class SentimentalResentment : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<RottingResentment>();
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.UseSound = SoundID.NPCHit2;
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.SepulcherNeo.SepulcherHeadNeo>();
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = CalamityID.CalRarityID.Violet;
            Item.buffType = ModContent.BuffType<Buffs.Pets.SepulcherBuffNeo>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return false;
        }
    }
}