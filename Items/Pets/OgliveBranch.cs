﻿using CalValEX.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Pets
{
    public class OgliveBranch : ModItem
    {
        public override void SetStaticDefaults()
        {
            if (CalValEX.CalamityActive)
                ItemID.Sets.ShimmerTransformToItem[CalValEX.CalamityItem("BloodyVein")] = Type;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.UseSound = SoundID.NPCDeath4;
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.Birdscule>();
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<Aqua>();
            Item.buffType = ModContent.BuffType<Buffs.Pets.BirdsculeBuff>();
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return false;
        }
    }
}