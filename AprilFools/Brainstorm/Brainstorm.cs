using CalamityMod.Items;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.AprilFools.Brainstorm
{
    public class Brainstorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item108 with { Pitch = 0.3f };
            Item.damage = 1;
            Item.knockBack = 0f;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<GreenNeedleProj>();
            Item.shootSpeed = 26f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));
        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.IllegalGunParts).
                AddIngredient(ModContent.ItemType<GreenNeedle>(), 100).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
