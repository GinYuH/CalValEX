using CalValEX.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Projectiles
{
    public class ChaoticPuffballThrown : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.aiStyle = ProjAIStyleID.BeachBall;
            Projectile.friendly = true;
        }

        public override void OnKill(int timeLeft)
        {
            Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ModContent.ItemType<ChaoticPuffball>());
            if (Main.rand.NextBool(10))
            {
                Terraria.Audio.SoundEngine.PlaySound(Terraria.ID.SoundID.Item20, Projectile.position);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(0, 0), Terraria.ID.ProjectileID.InfernoHostileBlast, 222, 10, Main.myPlayer);
            }
        }
    }
}