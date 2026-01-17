using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.AprilFools
{
    public class ShadesBane : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 47;
            Item.DamageType = DamageClass.Melee;
            Item.value = Item.sellPrice(0, 2);
            Item.rare = ItemRarityID.Cyan;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!CalValEX.AprilFoolMonth)
            {
            return false;
            }
            else
            {
                return true;
            }
		}

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (player.controlUseItem)
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.position.X + target.Size.X * 0.5f, target.position.Y + target.Size.Y * 0.5f, Main.rand.Next(-13,13), Main.rand.Next(-13, 13), ProjectileID.CursedFlameFriendly, 201, 0.1f);
        }
        public override bool AltFunctionUse(Player player)
        {
            if (!CalValEX.AprilFoolMonth)
            {
            return false;
            }
		
            else
            {
            int twidir = Main.rand.Next(-36, 36);
            int twidir2 = Main.rand.Next(-36, 36);
            for (int counter = 0; counter < 16; counter++)
            {
                int twidir3 = Main.rand.Next(-36, 36);
                int twidir4 = Main.rand.Next(-36, 36);
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.position.X, player.position.Y, twidir3, twidir4, ProjectileID.CursedFlameFriendly, 201, 0.1f);
            }
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.position.X, player.position.Y, twidir, twidir2, ProjectileID.BlackBolt, 401, 0.1f);
            return true;
            }
        }
    }
}
