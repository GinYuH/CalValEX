using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Projectiles.Pets;
using CalValEX.Buffs.Pets;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using System;

namespace CalValEX.Items.Pets
{
    public class BrimberryItem : ModItem 
    {
        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.UseSound = SoundID.NPCHit1;
            Item.shoot = ProjectileType<BrimberryPet>();
            Item.value = Item.sellPrice(0, 2, 9, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.buffType = BuffType<BrimberryBuff>();
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
            spriteBatch.Draw(Assets().Item1, position, new Rectangle(Assets().Item2 ? 22 : 0, 0, 20, 22), drawColor, 0f, Assets().Item1.Size() / new Vector2(4, 2), 1, SpriteEffects.None, 0f);

            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) {
            var pos = new Vector2(Item.position.X, Item.position.Y);
            spriteBatch.Draw(Assets().Item1, pos - Main.screenPosition, new Rectangle(Assets().Item2 ? 22 : 0, 0, 20, 22),
                alphaColor, rotation, Assets().Item1.Size() / new Vector2(4, 2), 1, SpriteEffects.None, 0f);

            return false;
        }

        private Tuple<Texture2D, bool> Assets() {
            return Tuple.Create(Request<Texture2D>("CalValEX/Items/Pets/BrimberryItem").Value, Main.LocalPlayer.HasItem(CalamityID.CalItemID.DormantBrimseeker));
        }
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return false;
        }
    }
}