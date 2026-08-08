using Terraria.ModLoader;
using CalValEX.Tiles.Paintings;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ReLogic.Content;

namespace CalValEX.Items.Tiles.Paintings
{
    public class Ohio : ModItem
    {
        public int frameCounter = 0;
        public int frame = 0;
        public bool playingAnimation = false;
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Terraria.Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<OhioPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = CalamityID.CalRarityID.Exo;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> texture;
            //0 = 6 frames, 8 = 3 frames]
            texture = ModContent.Request<Texture2D>(Texture);
            spriteBatch.Draw(texture.Value, position, texture.Frame(1, 4, 0, frame), Color.White, 0f, origin, scale, SpriteEffects.None, 0);
            
            return false;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Asset<Texture2D> texture;
            texture = ModContent.Request<Texture2D>(Texture);
            spriteBatch.Draw(texture.Value, Item.position - Main.screenPosition, texture.Frame(1, 4, 0, frame), lightColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            UpdateFrames();
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            UpdateFrames();
        }

        public void UpdateFrames()
        {
            if (Main.rand.NextBool(22222))
            {
                playingAnimation = true;
            }
            if (playingAnimation)
            {
                frameCounter++;
                if (frameCounter > 6)
                {
                    frameCounter = 0;
                    frame++;
                }
                if (frame > 3)
                {
                    frame = 0;
                    playingAnimation = false;
                }
            }
        }
    }
}