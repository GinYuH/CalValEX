﻿using CalValEX.Items.Equips.Wings;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalValEX.Items.Equips.PlayerLayers
{
    public class Helipack : PlayerDrawLayer {
        private int frame;
        private int frameCounter;
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Wings);

        public static Asset<Texture2D> helipackTex;
        public static Asset<Texture2D> brokenHelipackTex;

        public override void Load()
        {
            if (!Main.dedServ)
            {

                helipackTex = ModContent.Request<Texture2D>("CalValEX/Items/Equips/Wings/WulfrumHelipackEquipped");
                brokenHelipackTex = ModContent.Request<Texture2D>("CalValEX/Items/Equips/Wings/WulfrumHelipackMalfunction");
            }
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
            bool heli = false;
            for (int n = 13; n < 18 + drawInfo.drawPlayer.extraAccessorySlots; n++) {
                Item item = drawInfo.drawPlayer.armor[n];
                if (item.type == ModContent.ItemType<WulfrumHelipack>())
                    heli = true;
            }

            var wingLayer = PlayerDrawLayers.Wings.Visible;

            if (wingLayer == false)
                heli = false;

            return heli || wingLayer;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            if (drawInfo.shadow != 0f)
                return;

            Player player = Main.LocalPlayer;
            Player drawPlayer = drawInfo.drawPlayer;
            CalValEXPlayer modPlayer = drawPlayer.GetModPlayer<CalValEXPlayer>();


            int secondyoffset;
            var bodFrame = drawPlayer.bodyFrame;
            if (bodFrame.Y == bodFrame.Height * 7 || bodFrame.Y == bodFrame.Height * 8 || bodFrame.Y == bodFrame.Height * 9
                || bodFrame.Y == bodFrame.Height * 14 || bodFrame.Y == bodFrame.Height * 15 || bodFrame.Y == bodFrame.Height * 16)
                secondyoffset = 2;
            else
                secondyoffset = 0;

            Vector2 packPos = new Vector2((int)(drawInfo.Position.X - (drawInfo.drawPlayer.bodyFrame.Width / 2) + (drawInfo.drawPlayer.width / 2) - (16f * drawPlayer.direction)),
                    (int)(drawInfo.Position.Y + drawInfo.drawPlayer.height) - 55f - secondyoffset)
                + drawInfo.drawPlayer.headPosition + drawInfo.headVect;

            packPos -= Main.screenPosition;

            if (player.wingTime != player.wingTimeMax) {
                frameCounter++;
                if (frameCounter > 4) {
                    frameCounter = 0;
                    frame++;
                    if (frame == 10) {
                        frame = player.wingTime != 0 ? 1 : 0;
                    }
                }
            } else {
                frame = 0;
                frameCounter = 0;
            }

            Texture2D texture;
            if (!modPlayer.wulfrumjam)
                texture = helipackTex.Value;
            else
                texture = brokenHelipackTex.Value;

            if (modPlayer.helipack) {
                int dyeShader = drawPlayer.dye?[1].dye ?? 0;
                for (int n = 0; n < 18 + drawInfo.drawPlayer.extraAccessorySlots; n++) {
                    Item item = drawInfo.drawPlayer.armor[n];
                    if (item.type == ModContent.ItemType<WulfrumHelipack>()) {
                        if (n > 9)
                            dyeShader = drawPlayer.dye?[n - 10].dye ?? 0;
                        else
                            dyeShader = drawPlayer.dye?[n].dye ?? 0;
                    }
                }
                Vector2 origin = new(texture.Width / 2f, texture.Height / 2f / 10f);
                Rectangle yFrame = texture.Frame(1, 10, 0, frame);
                DrawData dat = new(texture, packPos, yFrame, drawInfo.colorArmorBody, 0f, 
                    origin, 1, drawPlayer.direction != -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                dat.shader = dyeShader;
                drawInfo.DrawDataCache.Add(dat);
            }
        }
    }
}
