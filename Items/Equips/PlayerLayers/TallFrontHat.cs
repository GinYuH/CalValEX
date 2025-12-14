using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Equips.PlayerLayers
{
    public class TallFrontHat : PlayerDrawLayer
    {
        public override bool IsHeadLayer => true;

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            bool hastallhat = false;
            if (drawInfo.drawPlayer.GetModPlayer<CalValEXPlayer>().specan || ((drawInfo.drawPlayer.GetModPlayer<CalValEXPlayer>().moldForce || drawInfo.drawPlayer.GetModPlayer<CalValEXPlayer>().moldTrans) && !drawInfo.drawPlayer.GetModPlayer<CalValEXPlayer>().moldHide))
            {
                hastallhat = true;
            }
            return hastallhat;
        }
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Player player = Main.LocalPlayer;
            CalValEXPlayer modPlayer = drawPlayer.GetModPlayer<CalValEXPlayer>();
            int secondyoffset = 0;
            float alb = (255 - drawPlayer.immuneAlpha) / 255f;
            int dyeShader = drawPlayer.dye?[0].dye ?? 0;

            Vector2 headPosition = drawInfo.Position - Main.screenPosition;

            // Using drawPlayer to get width & height and such is perfectly fine, on the other hand. Just center everything
            headPosition += new Vector2((drawPlayer.width - drawPlayer.bodyFrame.Width) / 2f, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f);

            //Convert to int to remove the jitter.
            headPosition = new Vector2((int)headPosition.X, (int)headPosition.Y);

            //Some dispalcements
            headPosition += drawPlayer.headPosition + drawInfo.headVect;

            if (modPlayer.specan)
            {
                Texture2D texture = ModContent.Request<Texture2D>("CalValEX/Items/Equips/Hats/SpectralstormHat").Value;
                int drawX = (int)(drawPlayer.position.X + drawPlayer.width / 2f - Main.screenPosition.X - (2 * drawPlayer.direction));
                int drawY = (int)(drawPlayer.position.Y + drawPlayer.height - 20 - Main.screenPosition.Y - secondyoffset);
                if (drawPlayer.mount.Active)
                    drawY += drawPlayer.mount.HeightBoost; 
                DrawData dat = new(texture, new Vector2(headPosition.X - (2 * drawPlayer.direction), headPosition.Y), null, drawInfo.colorArmorHead, 0f, new Vector2(texture.Width / 2f, texture.Height), 1f, drawPlayer.direction != -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                dat.shader = dyeShader;
                drawInfo.DrawDataCache.Add(dat);
            }
            if ((modPlayer.moldForce || modPlayer.moldTrans) && !modPlayer.moldHide)
            {
                Texture2D texture = ModContent.Request<Texture2D>("CalValEX/Items/Equips/Transformations/MoldyHoody_HeadReal").Value;
                int drawX = (int)(drawPlayer.position.X + drawPlayer.width / 2f - Main.screenPosition.X - (2 * drawPlayer.direction));
                int drawY = (int)(drawPlayer.position.Y + drawPlayer.height - 20 - Main.screenPosition.Y - secondyoffset);
                if (drawPlayer.mount.Active)
                    drawY += drawPlayer.mount.HeightBoost;
                DrawData dat = new(texture, headPosition + new Vector2(0, 1), texture.Frame(1, 20, 0, (int)(drawPlayer.bodyFrame.Y / 56f)), drawInfo.colorArmorHead, 0f, new Vector2(texture.Width / 2, texture.Height / 40), 1f, drawPlayer.direction != -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                dat.shader = dyeShader;
                drawInfo.DrawDataCache.Add(dat);
            }
        }
    }
}