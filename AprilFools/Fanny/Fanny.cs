using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using CalValEX.Buffs.Pets;
using CalValEX.Biomes;
using CalValEX.NPCs.Oracle;
using CalValEX.NPCs.JellyPriest;
using CalValEX.Items.Mounts.Ground;
using CalValEX.Items.Mounts.InfiniteFlight;
using CalValEX.CalamityID;
using System.Diagnostics;

namespace CalValEX.AprilFools.Fanny
{
    public class Fanny : UIElement
    {
        public Vector2 BasePosition => GetDimensions().ToRectangle().Bottom();
        public FannyTextbox SpeechBubble;
        public SoundStyle speakingSound;
        public FannyTextboxPalette textboxPalette;

        public string textboxHoverText;

        public float fadeIn;
        public bool flipped;
        public bool idlesInInventory;

        public float distanceFromEdge = 240;

        private int fanFrame;
        private int fanFrameCounter;

        //Shake wwhen saying a new message in the inventory
        public bool needsToShake;
        public float shakeTime;

        //Bounce and tickle anims when hovered / clicked
        public float bounce;
        public float tickle;

        //Small break between messages
        public int talkCooldown;

        //Default placeholder message used when not speaking
        public FannyMessage NoMessage = new FannyMessage("None", "", "Idle", displayOutsideInventory: false);

        //Message currently being spoken
        private FannyMessage currentMessage;

        public bool Speaking => currentMessage != null && !currentMessage.InDelayPeriod;

        public FannyMessage UsedMessage
        {
            get => Speaking ? currentMessage : NoMessage;
            set
            {
                currentMessage = value;
                SpeechBubble.Recalculate();
            }
        }

        public bool CanSpeak => currentMessage == null && talkCooldown <= 0;

        /// <summary>
        /// Plays the desired message, ignoring any condition the message may have
        /// </summary>
        public void TalkAbout(FannyMessage message)
        {
            if (!CanSpeak || !message.CanPlayMessage())
                return;

            message.PlayMessage(this);
        }

        /// <summary>
        /// Stops talking about the current message and goes on cooldown for that message
        /// </summary>
        public void StopTalking()
        {
            currentMessage = null;
            talkCooldown = 60;
        }

        public FannyTextboxPalette UsedPalette => (currentMessage != null && currentMessage.paletteOverride.HasValue) ? currentMessage.paletteOverride.Value : textboxPalette;

        public override void OnInitialize()
        {
            OnLeftClick += TickleTheRepugnantFuck;
        }

        private void TickleTheRepugnantFuck(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!FannyManager.fannyEnabled)
                return;
            tickle = Math.Max(tickle, 0) + 1;
            SoundEngine.PlaySound(SoundID.DD2_GoblinScream with { MaxInstances = 0 });
        }

        public override void Update(GameTime gameTime)
        {
            if (!FannyManager.fannyEnabled)
            {
                StopTalking();
            }
            //Tick down the talk cooldown
            talkCooldown--;

            //Slide
            if (!flipped)
                Left.Set(-(80 + distanceFromEdge * MathF.Pow(fadeIn, 0.4f)), 1);
            else
                Left.Set(distanceFromEdge * MathF.Pow(fadeIn, 0.4f), 0);

            Recalculate();


            //Show show if there's currently a message (if it's a message only shown in the inventory, only show it there)
            //Additionally, always show with the inventory open as long as its not hidden by default
            bool shouldShow = (Main.playerInventory && idlesInInventory) ||
                (currentMessage != null && (Main.playerInventory || UsedMessage.DisplayOutsideInventory) && !currentMessage.InDelayPeriod);


            //Slides in and out
            if (!shouldShow)
            {
                fadeIn -= 0.05f;
                if (fadeIn < 0)
                    fadeIn = 0;
            }
            else
            {
                fadeIn += 0.05f;
                if (fadeIn > 1)
                    fadeIn = 1;
            }

            //Shake if fanny choses a new message while already out there in the inventory
            if (needsToShake)
            {
                needsToShake = false;
                if (Main.playerInventory && fadeIn == 1)
                    shakeTime = 1f;
            }
            if (shakeTime > 0)
                shakeTime -= 1 / (60f * 1f);

            //Tick down bounce & tickle anims
            bounce -= 1 / (60f * 0.4f);

            tickle -= 1 / (60f * 0.4f);
            if (tickle > 4)
                tickle -= 1 / (60f * 0.4f);

            base.Update(gameTime);
        }

        #region Drawing
        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!FannyManager.fannyEnabled)
            {
                return;
            }
            // find the player's latest discord chat while the game isn't opened. doesn't work in multiplayer for sanity reasons
            if (Main.netMode == NetmodeID.SinglePlayer && NPC.downedBoss1 && !(FannyManager.discord1.alreadySeen || FannyManager.discord2.alreadySeen) && !Main.hasFocus)
            {
                FannyManager.GetDiscord();
            }
            AnimateFanny();

            // finally draw Fanny
            Texture2D fannySprite = UsedMessage.Portrait.Texture.Value;
            Rectangle frame = fannySprite.Frame(1, UsedMessage.Portrait.frameCount, 0, fanFrame);
            Vector2 position = BasePosition;

            if (shakeTime > 0)
            {
                position += Main.rand.NextVector2Circular(1f, 1f) * 5f * Utils.GetLerpValue(0.6f, 1f, shakeTime, true);

                float bounceTime = Utils.GetLerpValue(0.6f, 1f, shakeTime, true);
                position.Y -= Math.Abs(MathF.Sin(bounceTime * MathHelper.TwoPi)) * 62f * MathF.Pow(bounceTime, 0.6f);
            }

            else if (ContainsPoint(Main.MouseScreen) || bounce > 0)
            {
                if (bounce < 0)
                    bounce = 1;

                position.Y -= MathF.Pow(Math.Abs(MathF.Sin(bounce * MathHelper.Pi)), 0.6f) * 22f;
            }

            if (tickle >= 1)
                position += Main.rand.NextVector2Circular(3f, 3f) * tickle;


            spriteBatch.Draw(fannySprite, position, frame, Color.White * fadeIn, 0, new Vector2(frame.Width / 2f, frame.Height), 1f, 0, 0);
            if (Speaking && UsedMessage.ItemType != -22)
                DrawItem();
        }

        public void AnimateFanny()
        {
            fanFrameCounter++;
            if (fanFrameCounter > UsedMessage.Portrait.animationSpeed)
            {
                fanFrame++;
                fanFrameCounter = 0;
            }

            if (fanFrame >= UsedMessage.Portrait.frameCount)
                fanFrame = 0;
        }

        public void DrawItem()
        {
            Main.instance.LoadItem(UsedMessage.ItemType);
            Texture2D itemSprite = TextureAssets.Item[UsedMessage.ItemType].Value;
            int count = Main.itemAnimations[UsedMessage.ItemType] != null ? Main.itemAnimations[UsedMessage.ItemType].FrameCount : 1;
            Rectangle nframe = itemSprite.Frame(1, count, 0, 0);
            Vector2 origin = nframe.Size() / 2f;
            Vector2 itemPosition = BasePosition + new Vector2(60 * (flipped ? -1 : 1), -60) + UsedMessage.ItemOffset + Vector2.UnitY * MathF.Sin(Main.GlobalTimeWrappedHourly * 2) * 4;

            Main.EntitySpriteDraw(itemSprite, itemPosition, nframe, Color.White * fadeIn, MathF.Sin(Main.GlobalTimeWrappedHourly * 2 + MathHelper.PiOver2) * 0.2f, origin, UsedMessage.ItemScale, SpriteEffects.None);
        }
        #endregion
    }

    public class FannyTextbox : UIElement
    {
        public Fanny ParentFanny;
        public int outlineThickness = 3;
        public int backgroundPadding = 10;

        public override void LeftClick(UIMouseEvent evt)
        {
            base.LeftClick(evt);

            //Set the timeleft of the message to 30
            if (ParentFanny.Speaking && ParentFanny.fadeIn == 1 && ParentFanny.UsedMessage.NeedsToBeClickedOff && ParentFanny.UsedMessage.TimeLeft > 30)
                ParentFanny.UsedMessage.TimeLeft = 30;
        }

        public override void Recalculate()
        {
            Vector2 offset = new Vector2(50, 90);
            if (ParentFanny.flipped)
                offset.X *= -1;

            Vector2 basePosition = ParentFanny.BasePosition - offset;
            Vector2 textSize = FontAssets.MouseText.Value.MeasureString(ParentFanny.UsedMessage.Text) * ParentFanny.UsedMessage.textSize;
            Vector2 cornerPosition = basePosition - textSize;

            if (ParentFanny.flipped)
                cornerPosition = basePosition - Vector2.UnitY * textSize.Y;

            //Account for padding
            textSize += Vector2.One * (backgroundPadding + outlineThickness) * 2;
            cornerPosition -= Vector2.One * (backgroundPadding + outlineThickness);

            //Fade out
            cornerPosition.Y -= MathF.Pow(Utils.GetLerpValue(30, 0, ParentFanny.UsedMessage.TimeLeft, true), 1.6f) * 30f;
            //Fade in
            cornerPosition.Y += MathF.Pow(1 - ParentFanny.fadeIn, 2f) * 40;

            Width.Set(textSize.X, 0);
            Height.Set(textSize.Y, 0);
            Left.Set(cornerPosition.X, 0);
            Top.Set(cornerPosition.Y, 0);
            base.Recalculate();
        }


        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!ParentFanny.Speaking)
                return;

            FannyTextboxPalette palette = ParentFanny.UsedPalette;

            // a shit ton of variables
            var font = FontAssets.MouseText.Value;
            string text = ParentFanny.UsedMessage.Text;

            Rectangle dimensions = GetDimensions().ToRectangle();

            Vector2 outlineDrawPosition = dimensions.TopLeft();
            Vector2 backgroundDrawPosition = outlineDrawPosition + Vector2.One * outlineThickness;
            Vector2 textDrawPosition = backgroundDrawPosition + Vector2.One * backgroundPadding;

            Vector2 outlineSize = dimensions.Size();
            Vector2 backgroundSize = outlineSize - Vector2.One * outlineThickness * 2;

            Texture2D squareTexture = TextureAssets.MagicPixel.Value;
            float opacity = ParentFanny.fadeIn * Utils.GetLerpValue(0, 30, ParentFanny.UsedMessage.TimeLeft, true);

            // draw the border as a large rectangle behind, and the inners as a slightly smaller rectangle infront
            Main.spriteBatch.Draw(squareTexture, outlineDrawPosition, null, palette.outline * opacity, 0, Vector2.Zero, outlineSize / squareTexture.Size(), 0, 0);
            Main.spriteBatch.Draw(squareTexture, backgroundDrawPosition, null, palette.background * opacity, 0, Vector2.Zero, backgroundSize / squareTexture.Size(), 0, 0);


            if (ContainsPoint(Main.MouseScreen) && ParentFanny.fadeIn == 1 && ParentFanny.UsedMessage.NeedsToBeClickedOff && ParentFanny.UsedMessage.TimeLeft > 30)
            {
                Main.spriteBatch.Draw(squareTexture, backgroundDrawPosition, null, palette.backgroundHover with { A = 0 } * (0.4f + 0.2f * MathF.Sin(Main.GlobalTimeWrappedHourly * 4f)) * opacity, 0, Vector2.Zero, backgroundSize / squareTexture.Size(), 0, 0);
                Main.LocalPlayer.mouseInterface = true;

                string hoverText = ParentFanny.textboxHoverText;
                if (ParentFanny.UsedMessage.hoverTextOverride != "")
                    hoverText = ParentFanny.UsedMessage.hoverTextOverride;
                Main.instance.MouseText(hoverText);
            }

            // finally draw the text
            Utils.DrawBorderStringFourWay(Main.spriteBatch, font, text, textDrawPosition.X, textDrawPosition.Y, palette.text * (Main.mouseTextColor / 255f) * opacity, palette.textOutline * opacity, Vector2.Zero, ParentFanny.UsedMessage.textSize);
        }
    }

    public class FannyUIState : UIState
    {
        public static Fanny FannyTheFire = new Fanny();
        public static Fanny EvilFanny = new Fanny();
        public static Fanny WonderFlower = new Fanny();

        public override void OnInitialize()
        {
            LoadFanny(FannyTheFire, "Thank you for the help, Fanny!", false, true, SoundID.Cockatiel with { MaxInstances = 0, Volume = 0.3f, Pitch = -0.8f }, "Idle");
            LoadFanny(EvilFanny, "Get away, Evil Fanny!", true, false, SoundID.DD2_DrakinShot with { MaxInstances = 0, Volume = 0.3f, Pitch = 0.8f }, "EvilIdle", distanceFromEdge: 120,
                textboxPalette: new FannyTextboxPalette(Color.Black, Color.Red, Color.Indigo, Color.DeepPink, Color.Tomato));
        }

        public Fanny LoadFanny(Fanny fanny, string hoverText, bool flipped, bool idlesInInventory, SoundStyle voice, string emptyMessagePortrait, float verticalOffset = 0f, float distanceFromEdge = 240f, FannyTextboxPalette? textboxPalette = null)
        {
            fanny.Left.Set(-80, 1);
            fanny.Top.Set(-160, 1 - verticalOffset);
            fanny.Height.Set(80, 0f);
            fanny.Width.Set(80, 0f);

            Append(fanny);

            FannyTextbox textbox = new FannyTextbox();

            textbox.Height.Set(0, 0f);
            textbox.Width.Set(0, 0f);
            textbox.ParentFanny = fanny;
            Append(textbox);
            fanny.SpeechBubble = textbox;

            fanny.textboxHoverText = hoverText;
            fanny.flipped = flipped;
            fanny.idlesInInventory = idlesInInventory;
            fanny.speakingSound = voice;
            fanny.NoMessage = new FannyMessage("", "", emptyMessagePortrait, displayOutsideInventory: false);
            fanny.distanceFromEdge = distanceFromEdge;

            if (textboxPalette.HasValue)
                fanny.textboxPalette = textboxPalette.Value;
            else
                fanny.textboxPalette = new FannyTextboxPalette();

            return fanny;
        }

        public void StopAllDialogue()
        {
            foreach (UIElement element in Elements)
            {
                if (element is Fanny fanny)
                    fanny.StopTalking();
            }
        }

        public bool AnyAvailableFanny()
        {
            return Elements.Any(ui => ui is Fanny fanny && fanny.CanSpeak);
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class FannyUISystem : ModSystem
    {
        private UserInterface FannyInterface;
        internal static FannyUIState UIState;

        public override void Load()
        {
            FannyInterface = new UserInterface();
            UIState = new FannyUIState();
            FannyInterface.SetState(UIState);
        }

        public override void UpdateUI(GameTime gameTime)
        {
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "CalValEX: Fanny",
                    delegate {

                        FannyInterface?.Update(Main._drawInterfaceGameTime);
                        FannyInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }

    public class FannySavePlayer : ModPlayer
    {
        public bool[] readMessages;

        public bool readTroughFannyDogDialogue = false;


        public override void SaveData(TagCompound tag)
        {
            for (int i = 0; i < FannyManager.fannyMessages.Count; i++)
            {
                FannyMessage msg = FannyManager.fannyMessages[i];
                if (msg.alreadySeen && msg.PersistsThroughSaves)
                    tag["FannyDialogueCV" + msg.Identifier] = true;
            }
        }

        public override void LoadData(TagCompound tag)
        {
            readMessages = new bool[FannyManager.fannyMessages.Count];
            for (int i = 0; i < FannyManager.fannyMessages.Count; i++)
            {
                FannyMessage msg = FannyManager.fannyMessages[i];
                readMessages[i] = tag.ContainsKey("FannyDialogueCV" + msg.Identifier);
            }
        }

        public override void OnEnterWorld()
        {
            if (Main.mouseRight && Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) && Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                SoundEngine.PlaySound(SoundID.Cockatiel);
                SoundEngine.PlaySound(SoundID.DD2_GoblinScream);
                SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion);
                return;
            }

            if (readMessages is null || readMessages.Length < FannyManager.fannyMessages.Count)
            {

                for (int i = 0; i < FannyManager.fannyMessages.Count; i++)
                {
                    FannyMessage msg = FannyManager.fannyMessages[i];
                    msg.alreadySeen = false;
                }
                return;
            }


            for (int i = 0; i < FannyManager.fannyMessages.Count; i++)
            {
                FannyMessage msg = FannyManager.fannyMessages[i];
                msg.alreadySeen = readMessages[i];
            }
        }
    }

    [Autoload(Side = ModSide.Client)]
    public partial class FannyManager : ModSystem
    {
        public static List<FannyMessage> fannyMessages = new List<FannyMessage>();
        public static Dictionary<string, FannyPortrait> Portraits = new Dictionary<string, FannyPortrait>();
        public static bool fannyEnabled = false;
        public static bool hasRemix = false;
        public static bool hasIsaac = false;
        public static bool hasUndertale = false;
        public static bool hasQ = false;

        #region Loading
        public override void Load()
        {
            if (ModLoader.HasMod("CalRemix"))
            {
                hasRemix = true;
            }
            else
            {
                // check if the mods folder has remix
                // in the modern day and age, this is highly unlikely, but just in case
                {
                    string modPath = Path.Combine(Main.SavePath, "Mods");
                    string[] files = Directory.GetFiles(modPath, "*.tmod", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        if (file.Contains("CalRemix"))
                        {
                            hasRemix = true;
                        }
                    }
                }
                // do           this to get every mod from the workshop
                AppId_t TMLAppID_t = new AppId_t(1281930u);
                AppId_t TerrariaAppId_t = new AppId_t(105600u);
                List<string> fuck = new AppId_t[2]
                    {
                        TMLAppID_t,
                        TerrariaAppId_t
                    }.Select((AppId_t app) => Path.Combine(Terraria.Social.Steam.WorkshopHelper.GetWorkshopFolder(app), "content", app.ToString())).Where(Directory.Exists).SelectMany(Directory.EnumerateDirectories)
                        .ToList();
                // if any of the mods contains "CalRemix" set hasRemix to true, this causes Fanny to get a bit more personal
                foreach (string modPath in fuck)
                {
                    string[] files = Directory.GetFiles(modPath, "*.tmod", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        if (file.Contains("CalRemix"))
                        {
                            hasRemix = true;
                        }
                    }
                }
                if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\The Binding of Isaac Rebirth"))
                {
                    hasIsaac = true;
                }
                if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Undertale"))
                {
                    hasUndertale = true;
                }
                if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Ungerdertale"))
                {
                    hasQ = true;
                }
                //250900
            }
            LoadFannyPortraits();
            LoadGeneralFannyMessages();
        }
        public static void LoadGeneralFannyMessages()
        {
            fannyMessages.Add(new FannyMessage("HelloNew", "Hello there! I'm Fanny the Flame, your personal guide to assist you with traversing this dangerous world. I wish you good luck on your journey and a Fan-tastic time!",
                "Idle", (FannySceneMetrics metrics) => !hasRemix));

            FannyMessage ml = new FannyMessage("HelloAgain", "Hello there! I'm Fanny the Fl-",
                "Idle", (FannySceneMetrics scene) => hasRemix && !ModLoader.HasMod("CalRemix"), 2, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true).AddDelay(0.4f);

            fannyMessages.Add(ml);

            FannyMessage ml2 = new FannyMessage("HelloAgain2", "Wait... I remember!",
                "Awooga", FannyMessage.AlwaysShow, 3, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true)
                .NeedsActivation(0f);

            ml.AddStartEvent(() => ml2.ActivateMessage());

            fannyMessages.Add(ml2);

            FannyMessage ml3 = new FannyMessage("HelloAgain3", "Hello again $0! It seems we have been brought together again through strange circumstances! You're back, I'm back, let's make the most of it! Fire away with your questions, and I'll do my best to illuminate your path... or at least crack a few jokes along the way. Let's get this fiery show on the road!",
                "Idle", FannyMessage.AlwaysShow, 6, needsToBeClickedOff: true, onlyPlayOnce: true, displayOutsideInventory: true)
                .NeedsActivation(2f).AddDynamicText(Steamworks.SteamFriends.GetPersonaName).SetHoverTextOverride("...");

            ml2.AddStartEvent(() => ml3.ActivateMessage());

            fannyMessages.Add(ml3);


            fannyMessages.Add(new FannyMessage("Raito", "Oh yeah! Something else, if for whatever reason you don't think you need my help, you can grab a \"Friend Extinguisher\" or use the mod's April Fools Content configuration setting, and i'll go poof! But why would you want to do that?",
                "Nuhuh").AddItemDisplay(ModContent.ItemType<FriendExtinguisher>()).AddEndEvent(() => Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), ModContent.ItemType<FriendExtinguisher>())));

            fannyMessages.Add(new FannyMessage("ShrineSnow", "Woah, is that a snow shrine? You better go loot it for its one-of-a-kind treasure! It gave you a really cool item that you'll use forever I think?",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneSnow && Main.LocalPlayer.ZoneRockLayerHeight && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineDesert", "Woah, is that a desert shrine? You better go loot it for its one-of-a-kind treasure! It gave you a tile-matching game called Luxor I think?",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneUndergroundDesert && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineCorruption", "Woah, is that a corruption shrine? You better go loot it for its one-of-a-kind treasure! It caused pebbles to rain from the sky I think?",
                "Awooga", (FannySceneMetrics scene) => !WorldGen.crimson && Main.LocalPlayer.ZoneCorrupt && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineCrimson", "Woah, is that a crimson shrine? You better go loot it for its one-of-a-kind treasure! It caused pebbles to rain from the sky I think?",
                "Awooga", (FannySceneMetrics scene) => WorldGen.crimson && Main.LocalPlayer.ZoneCrimson && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineUg", "Woah, is that an underground shrine? You better go loot it for its one-of-a-kind treasure! It caused you to gain defense while standing still I think?",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneNormalUnderground && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineHallow", "Woah, is that a hallow shrine? You better go loot it for its one-of-a-kind treasure! No seriously, it's the only thing exclusive to the Hallow!",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneHallow && Main.LocalPlayer.ZoneRockLayerHeight && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineGranite", "Woah, is that a granite shrine? You better go loot it for its one-of-a-kind treasure! It caused sparks to fly out of enemies when hit I think?",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneGranite && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineMarble", "Woah, is that a marble shrine? You better go loot it for its one-of-a-kind treasure! It summoned cool orbital swords I think?",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneMarble && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("ShrineMushroom", "Woah, is that a mushroom shrine? You better go loot it for its one-of-a-kind treasure! It imbued true melee weapons with fungi I think?",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneGlowshroom && Main.rand.NextBool(116000)));

            fannyMessages.Add(new FannyMessage("Arsenic", "This place is a lot more out of this world than when I was last here! Try breaking through walls to find the rare and precious Arsenic Ore which can be used for highly advanced robotics!",
                "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneDungeon));

            fannyMessages.Add(new FannyMessage("Snowbr", "It's quite chilly here, maybe you should invest some time in gathering some cold-protective gear before you freeze to death!",
                "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneSnow));

            fannyMessages.Add(new FannyMessage("Cavern", "It's quite dark down here. You should go get some more torches before further exploration or you may fall into a pit full of lice!",
                "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneRockLayerHeight).AddItemDisplay(ItemID.Torch));

            fannyMessages.Add(new FannyMessage("Granite", "Woah, this place looks so cool and futuristic! It's almost like an entirely different dimension here!",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneGranite));

            fannyMessages.Add(new FannyMessage("Marble", "Marble? I LOVE playing with marbles! A few hundred years ago I was an avid marble collector, collecting marbles of various shapes, colors, and sizes. But, one day, I lost my marbles.",
                "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneMarble));

            fannyMessages.Add(new FannyMessage("FungalGrowths", "I know a quick get rich quick scheme. See those Glowing Mushrooms? They sell for a lot! Go destroy that ecosystem for some quick cash!",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneGlowshroom).AddItemDisplay(ItemID.GlowingMushroom));

            fannyMessages.Add(new FannyMessage("GemCave", "So many gemstones! Make sure to keep some Emeralds handy. Apparently a lot of people like to search for them to make crates for some reason!",
                "Nuhuh", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneGemCave).AddItemDisplay(ItemID.Emerald));

            fannyMessages.Add(new FannyMessage("Hell", "Welcome to hell! This place is flaming hot just like me, so you better get some gear to protect you aganist the heat!", "Nuhuh",
                (FannySceneMetrics scene) => Main.LocalPlayer.ZoneUnderworldHeight && !Main.hardMode));

            fannyMessages.Add(new FannyMessage("Blight", "How blightful.", "Idle",
                (FannySceneMetrics scene) => Main.LocalPlayer.InModBiome(ModContent.GetInstance<AstralBlight>())));

            fannyMessages.Add(new FannyMessage("ShimmerNothing", "You should consider throwing that item you're holding in Shimmer! You may get something powerful!",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneShimmer && !Main.LocalPlayer.HeldItem.CanShimmer(), onlyPlayOnce: false, cooldown: 600));

            fannyMessages.Add(new FannyMessage("Meteore", "A Fallen Star!",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneMeteor).AddItemDisplay(ItemID.FallenStar));

            fannyMessages.Add(new FannyMessage("Temple", "I love house invasion!",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneLihzhardTemple).SetHoverTextOverride("Me too Fanny!"));

            fannyMessages.Add(new FannyMessage("TempleWires", "Aw man, there's so many booby traps in here! Try using that fancy gadget of yours to disable them!",
                "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneLihzhardTemple && (Main.LocalPlayer.HasItem(ItemID.WireCutter) || Main.LocalPlayer.HasItem(ItemID.MulticolorWrench) || Main.LocalPlayer.HasItem(ItemID.WireKite))).AddItemDisplay(ItemID.WireCutter));

            fannyMessages.Add(new FannyMessage("Altars", "Smashing demon altars is no longer guaranteed to bless your world with ores. But it’s still worth a shot!",
               "Idle", (FannySceneMetrics scene) => (Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson) && Main.hardMode && !Main.LocalPlayer.ZoneUnderworldHeight));

            fannyMessages.Add(new FannyMessage("Jungleabyss", "I’ve heard word that there’s incredible treasures in the mysterious depths of the ocean, the one past the jungle!",
                "Nuhuh", (FannySceneMetrics scene) => !NPC.downedBoss3 && Main.LocalPlayer.ZoneJungle && Main.rand.NextBool(600)));

            fannyMessages.Add(new FannyMessage("Bee", "According to all known laws of aviation, there is no way that a bee should be able to fly. Its wings are too small to get its fat little body off the ground. The bee, of course, flies anyways. Because bees don't care what humans think is impossible.",
                "Nuhuh", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == NPCID.Bee || n.type == NPCID.BeeSmall)));

            fannyMessages.Add(new FannyMessage("Fairy", "That thing is hurting my eyes! Kill it, quick!",
                "Sob", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == NPCID.EmpressButterfly)));

            fannyMessages.Add(new FannyMessage("Cultists", "Looks like some blue robe-wearing hooligans are worshiping a coin! Try not to interrupt them, they seem to be having a good time.",
                "Nuhuh", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == NPCID.CultistDevote)));

            fannyMessages.Add(new FannyMessage("AncientDom", "Who is this guy???",
                "Sob", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == NPCID.AncientCultistSquidhead)));

            fannyMessages.Add(new FannyMessage("Oracle", "The Oracle has a variety of adorable pets you can buy to keep you company, just like me!",
                "Idle", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == ModContent.NPCType<OracleNPC>())));

            fannyMessages.Add(new FannyMessage("Jelly", "I see a jellyfish tied up, try freeing her by hitting her, surely she can take it right? Jellyfish are known for their immortality!",
                "Awooga", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == ModContent.NPCType<JellyPriestBound>())));

            fannyMessages.Add(new FannyMessage("FungusGarden", "Careful when exploring the Shroom Garden. I hear some rather large crustaceans make their home there. Wouldn't want to be turned into Delicious Meat!",
                "Nuhuh", (FannySceneMetrics scene) => Main.rand.NextBool(2160000) && !NPC.downedBoss1, cooldown: 120));

            fannyMessages.Add(new FannyMessage("FalseRef", "WHOA! Is that a reference to another of my favorite games?????",
                "Nuhuh", (FannySceneMetrics scene) => Main.rand.NextBool(1500000)));

            fannyMessages.Add(new FannyMessage("ProbablyYakuza", "One time, I saw someone being dragged into a car by three men. The men took around 10 minutes and 23 seconds to subdue their victim, and 2 more minutes to drive away. I did nothing to stop it.",
                "Nuhuh", (FannySceneMetrics scene) => Main.rand.NextBool(1500000)));

            fannyMessages.Add(new FannyMessage("Fuckyou", "You are now manually breathing.",
               "Nuhuh", (FannySceneMetrics scene) => Main.rand.NextBool(4000000)));

            fannyMessages.Add(new FannyMessage("Mount", "Do a barrel roll on that thing you're riding!",
               "Awooga", (FannySceneMetrics scene) => Main.rand.NextBool(1000) && Main.LocalPlayer.mount.Type != MountID.None));

            fannyMessages.Add(new FannyMessage("ProfanedBike", "Why were those chicken wings carrying parts for a motorcycle anyways? Was it too heavy for their leader so they hacked it apart or something?",
               "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.mount.Type == ModContent.MountType<ProfanedCycle>()));

            fannyMessages.Add(new FannyMessage("YharimCar", "YharimCar",
               "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.mount.Type == ModContent.MountType<YharimCar>()).SetHoverTextOverride("YharimCar"));

            fannyMessages.Add(new FannyMessage("Yharite", "No way! You found yourself a dragon!? You're just one step away from becoming a God my friend, just absorb its soul and kablam! ...I'm not sure myself how to do that though.",
               "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.mount.Type == ModContent.MountType<YharonMount>()));

            fannyMessages.Add(new FannyMessage("Creepy", Main.rand.Next(1000000) + " remaining...",
                "Cryptid", (FannySceneMetrics scene) => Main.rand.NextBool(100000000), duration: 60, needsToBeClickedOff: false));

            fannyMessages.Add(new FannyMessage("Mhage", "Be careful when using magic weapons. Drinking too many mana potions can drain your health, and leave you vulnerable to enemy attacks.",
               "Nuhuh", (FannySceneMetrics scene) => Main.rand.NextBool(2160000) && Main.LocalPlayer.HeldItem.DamageType == DamageClass.Magic, cooldown: 300, onlyPlayOnce: false));

            fannyMessages.Add(new FannyMessage("Thrust", "Did you know you can parry enemy attacks with your sword? Just right click the moment something is about to hit you, and you'll block it with ease!",
               "Idle", (FannySceneMetrics scene) => Main.rand.NextBool(2160000) && Main.LocalPlayer.HeldItem.DamageType == DamageClass.Melee, cooldown: 300, onlyPlayOnce: false));

            fannyMessages.Add(new FannyMessage("Wood", "Wood? Yummy!",
               "Awooga", (FannySceneMetrics scene) => Main.LocalPlayer.HasItem(ItemID.Wood)));

            fannyMessages.Add(new FannyMessage("HallowedBar", "What you hold now is a bar of extraordinary power infused with the essence of Heaven itself! That's a biome right?",
               "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.HasItem(ItemID.HallowedBar)).SetHoverTextOverride("It sure is Fanny, it sure is."));

            fannyMessages.Add(new FannyMessage("LifeCrystal", "Ah, digging up life crystals, are we? Remember, a crystal a day keeps the.. uhh... enemies away! See, I'm good with rhymes!",
               "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.HasItem(ItemID.LifeCrystal)));

            fannyMessages.Add(new FannyMessage("Desert", "Oh, look at you, venturing into the sandy abyss! Remember, in the desert, the sand's as hot as a freshly microwaved burrito! So don't forget your sunscreen... or your water... or your sanity. ",
               "Nuhuh", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneDesert));

            fannyMessages.Add(new FannyMessage("Corpution", "Ah, the Corruption, where the grass is as dark as my soul after what I did to that sleeping homeless person on Feburary 2nd at 2:35 AM. Just watch out for those pesky corruption monsters, they'll nibble you right up!",
               "Nuhuh", (FannySceneMetrics scene) => Main.LocalPlayer.ZoneCorrupt));

            fannyMessages.Add(new FannyMessage("Nite", "Nighttime is when the real party starts! But watch out for those nocturnal nasties, they're like uninvited guests who never leave. Keep a torch handy, it's like bringing a flashlight to a ghost story.",
               "Idle", (FannySceneMetrics scene) => !Main.dayTime));

            fannyMessages.Add(new FannyMessage("Slimerain", "Ooh, a slime rain! It's like a colorful meteor shower, except instead of making wishes, you're dodging slimy projectiles! Better grab an umbrella, or at least a slicker. Don't want to end up looking like a walking slime ball! wink wink",
               "Awooga", (FannySceneMetrics scene) => Main.slimeRain));

            fannyMessages.Add(new FannyMessage("Mechs", "Congrats on taking down those clanky contraptions! It's like defeating a bunch of oversized kitchen appliances. Just remember, don't get too cocky or they might just hit you with their spatulas of doom!",
               "Idle", (FannySceneMetrics scene) => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3));

            FannyMessage plant = new FannyMessage("Plantoreum1", "Hey, have you seen my precious pink flower that I've been growing for 15 years? I left her around here. She's been my best friend for years now, and I could never fathom what I'd do if I had lost h",
               "Idle", (FannySceneMetrics scene) => NPC.downedPlantBoss);

            fannyMessages.Add(plant);
            FannyMessage plant2 = new FannyMessage("Plantoreum2", "Oh.",
               "Sob", FannyMessage.AlwaysShow, needsToBeClickedOff: false, duration: 30)
                .NeedsActivation();

            plant.AddStartEvent(() => plant2.ActivateMessage());

            fannyMessages.Add(plant2);

            fannyMessages.Add(new FannyMessage("PlantDungeon", "Welcome to the dungeon, where skeletons have more bones than a Halloween decoration aisle! Just be careful not to wake the sleeping spirits, they're grumpier than a cat without its afternoon nap.",
               "Idle", (FannySceneMetrics scene) => NPC.downedPlantBoss && Main.LocalPlayer.ZoneDungeon));

            fannyMessages.Add(new FannyMessage("Golem", "Good job defeating that pile o' bricks! You sure.. cough cough wow, the air sure is du- cough cough",
               "Nuhuh", (FannySceneMetrics scene) => NPC.downedGolemBoss));

            fannyMessages.Add(new FannyMessage("Towers", "Ah, the lunar events! It's like hosting a cosmic tea party, except instead of sipping tea, you're dodging death rays from space! Just remember to RSVP with your best battle gear and a side of moon cheese.",
               "Idle", (FannySceneMetrics scene) => NPC.downedAncientCultist));


            FannyMessage rock1 = new FannyMessage("RockLobster1", "Oh, hot tamales! You've gone and done it! You've reeled in a lobster, my friend! Can you believe it? A rock lobster, right here in the oasis! I mean, who would've thought? Rock Lobsters are like the rockstars of the desert, and you just snagged one. You're a fishing master, my friend! But hey, let me tell you a tale about lobsters that'll have you crackling with laughter. So there I was, with my good pal Dron, you know. We decided to hit up this fancy lobster restaurant downtown. Now, you might be wondering, \"Fanny, why would a flame like you even eat lobster?\" Well, my friend, curiosity burns bright, and I figured it was worth a shot. We waltz into this posh place, me flickering with excitement, Dron rolling in like a boss. We get seated, and the waiter hands us these bibs – you know, the ones with the goofy lobster design? I, of course, couldn't wear one, being made of flame and all, but Dron struggled a bit. Picture it: Dron, bib askew, trying to maneuver it with no arms. Hilarious, right? Now, we dive into the menu. Lobster this, lobster that – it was like a seafood carnival! Dron's eyes were wide, no arms to shield them from the sea of options. We decided to go for the lobster feast, the whole shebang. And let me tell you, it was a feast fit for kings! But here comes the twist! The waiter brings out this succulent lobster dish, steam rising like the flames on my head. Dron takes a bite, a hearty one, and suddenly, his face turns redder than a ripe tomato. Turns out, he's allergic to shellfish! Who would've thought? Poor Dron, armless and allergic – the universe sure has a sense of humor. Which reminds me of the spicy saga of my friend Green Demon and his salsa extravaganza. Now, Green Demon is quite the character – always pushing the boundaries of fiery flavors, forever on a quest to grow the spiciest peppers in our little corner of the world. One day, the sun was blazing overhead, and Green Demon excitedly invited our motley crew – me, La Ruga, Ogscule, Tim, Cnidrion, and Pyrogen – for a salsa fiesta at his fiery abode. Oh, the anticipation was palpable! I flickered with excitement, eager to see what kind of fiery concoction he had in store for us. As we approached, the scent of peppers wafted through the air like a zesty dance. Rows upon rows of vibrant, fiery red and green peppers swayed in the breeze, basking in the sun's warm embrace. Green Demon, with his devilish grin, welcomed us to his spicy paradise. \"Behold, my friends! The harvest of the spiciest peppers in the land!\" he declared, his eyes gleaming with mischievous delight. We gathered around as Green Demon plucked peppers with such finesse, it was like he was orchestrating a spicy symphony. With a basket brimming with peppers, he led us to his fiery kitchen, a cauldron bubbling away with a mysterious potion – or rather, his special salsa. The kitchen was alive with the rhythmic chopping of peppers and the sizzling melody of ingredients harmonizing in the pan. I, being a flame myself, felt right at home amidst the culinary inferno. Green Demon's hands moved with the precision of a seasoned chef, his eyes gleaming with the promise of a taste explosion. Now, my friends, you must understand – this salsa wasn't just your average dip. It was a potion of pure heat, a symphony of spices that would make even the bravest tongues tremble. Green Demon was a maestro, and his salsa was his fiery masterpiece. As the salsa simmered, the aroma grew more intense. It was like a spicy enchantment had taken over the kitchen. We were all eagerly awaiting the taste test, our excitement building like a rising flame. Finally, the moment of truth arrived. Green Demon scooped up a generous amount of salsa and handed us each a tortilla chip. We stared at each other, eyes wide with anticipation, and took a bite simultaneously. Flames! The heat hit us like a spicy meteor shower.", "Idle"
                , (FannySceneMetrics scene) => Main.LocalPlayer.HasItem(ItemID.RockLobster), maxWidth: 1000, fontSize: 0.8f).AddItemDisplay(ItemID.RockLobster);
            fannyMessages.Add(rock1);
            FannyMessage rock2 = new FannyMessage("RockLobster2", "Laughter erupted as we danced around, fanning our tongues with imaginary flames. Even I, a flame myself, felt the burn in the most delightful way. Green Demon stood there, triumphant, his devilish grin widening. \"What did I tell you, my friends? The spiciest salsa in the realm!\" But my pal La Ruga took a cautious bite, realizing it was the perfect companion to his daily tea sessions. Oh La Ruga. Every Tuesday, without fail, we embark on a literary adventure to our favorite place in the world – the cozy little bookstore at the corner of the Exosphere. We strut into the bookstore like we own the place, La Ruga and me. Well, La Ruga doesn't really strut; more like gracefully scuttles with its long legs. I'm there, flickering and crackling, like a flame on a mission – a mission for some good reads. The moment we step inside, the scent of old paper and ink wafts through the air. It's like the very essence of knowledge, a fragrance that fuels my fiery enthusiasm. La Ruga seems to breathe it all in, its shadowy arms flickering with delight. Now, you might wonder, \"Fanny, why a bookstore?\" Well, let me tell you, there's something magical about flipping through the pages of a good book. The way the words dance off the paper, creating a world only limited by the imagination. It's a burning passion of mine, and La Ruga, with its ancient wisdom, appreciates the art of storytelling too. We start our journey through the aisles, and La Ruga guides me to the classics – timeless tales of adventure, romance, and mystery. Picture this: a flame flickering next to a T⍑ᒷ rᒷᓵꖌ𝙹リ, engrossed in the ancient wisdom of literature. If anyone happened to stroll by, they'd probably think they'd stumbled into a fantastical scene from a storybook. But, oh, the joy of finding a hidden gem! We once stumbled upon a dusty old tome, forgotten by time. It had a worn cover, and the pages whispered secrets of a bygone era. La Ruga and I exchanged excited glances – this was a treasure trove waiting to be explored! We huddled in a cozy reading nook, La Ruga spreading its legs out, making a snug spot for me to flicker comfortably. As I read aloud, the words came alive, creating vivid images in our minds. The world outside faded away, and it was just me, La Ruga, and the enchanting tales within those pages. Of course, being a flame, I have to be careful not to singe the pages. La Ruga occasionally gives me a gentle nudge if I get too close, a silent reminder to keep my flames in check. We've developed quite the teamwork, I must say. Now, I pick out a few books at the bookstore, and head to the park for some extra reading. Usually, my friend Tim joins me at the park, but this time, my other friend Ogscule decided to join me instead.  Now picture this: a sunny day at the park, the trees swaying gently, and the air filled with the delightful scent of nature. I'm there, Fanny the Flame, and by my side is none other than my trusty friend Ogscule. We come across a patch of grass, and Ogscule suggests we feed the birds. Now, considering that Ogscule, like Dron, doesn't have hands – or any appendages, for that matter – I'm intrigued. How on earth is this going to work? But I'm all for adventure, so we decide to give it a shot. Ogscule plops down on the grass, and I hover beside him, flames flickering in anticipation. We've got a bag of bird feed, and Ogscule looks at me with those fleshy eyes, ready for action. Without arms, he leans toward the bag, trying to nudge it open with the tip of his prongs. It's like watching a determined utensil on a mission. Meanwhile, I'm providing some fiery commentary, encouraging Ogscule like a cheerleader at a sports game. \"Go, Ogscule, you got this! Show those birds your fleshy finesse!\" I can't help but chuckle at the absurdity of the situation. We're the dynamic duo of the park, making memories that'll have the birds tweeting about us for days. Finally, after a bit of a struggle, Ogscule manages to spill some bird feed on the ground. The birds, sensing a feast, swoop down like they've just won the avian lottery. But here's the twist – since Ogscule doesn't have hands, the birds are pecking at the bird feed, and Ogscule's prongs. It's a hilarious sight, a bizarre dance of feathers and fork. I'm laughing so hard; my flames are dancing with delight. Ogscule doesn't seem to mind; he's embracing the chaos, embracing the absurdity of our friendship. We may not be the most conventional pair, but we sure know how to turn a simple day at the park into a sidesplitting comedy. Life with these friends is like a colorful cartoon, each day filled with laughter and unexpected twists. So here's to more adventures, more lobsters, and more tales to share. Thanks for being with me on this fiery journey, my friend!",
                "Idle", FannyMessage.AlwaysShow, maxWidth: 1000, fontSize: 0.8f)
                .NeedsActivation();

            rock1.AddStartEvent(() => rock2.ActivateMessage());

            fannyMessages.Add(rock2);


            {
                FannyMessage introLore = new FannyMessage("IntroducingEvilFanny", "My friend, we've made it to Hardmode! Plenty of new opportunities have popped up and plenty of dangerous new foes now lurk about.",
                    "Idle", (FannySceneMetrics scene) => Main.hardMode && !hasRemix, 8, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true).AddDelay(5);
                fannyMessages.Add(introLore);

                FannyMessage introEvilLore = new FannyMessage("IntroducingEvilFanny2", "'Sup",
                    "EvilIdle", FannyMessage.AlwaysShow, 6, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true)
                    .NeedsActivation(4f).SpokenByEvilFanny();

                introLore.AddStartEvent(() => introEvilLore.ActivateMessage());

                fannyMessages.Add(introEvilLore);

                FannyMessage introLore2 = new FannyMessage("IntroducingEvilFanny3", "E-evil Fanny!? I thought you moved away to the Yukon!",
                    "Sob", FannyMessage.AlwaysShow, 8, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true)
                    .NeedsActivation();
                introLore.AddEndEvent(() => introLore2.ActivateMessage());

                fannyMessages.Add(introLore2);

                FannyMessage introEvilLore2 = new FannyMessage("IntroducingEvilFanny4", "Yeah. Got cold.",
                   "EvilIdle", FannyMessage.AlwaysShow, 5, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true)
                   .NeedsActivation().SpokenByEvilFanny();

                introEvilLore.AddEndEvent(() => introEvilLore2.ActivateMessage());

                fannyMessages.Add(introEvilLore2);

                FannyMessage introLore3 = new FannyMessage("IntroducingEvilFanny5", "$0, it seems my evil counterpart, Evil Fanny, has returned! Don't trust a thing they say, and hopefully they'll leave..",
                   "Idle", FannyMessage.AlwaysShow, 8, onlyPlayOnce: true, displayOutsideInventory: true)
                   .NeedsActivation().AddDynamicText(FannyMessage.GetPlayerName);

                introLore2.AddEndEvent(() => introLore3.ActivateMessage());

                fannyMessages.Add(introLore3);

            }


            {
                FannyMessage introLore = new FannyMessage("IntroducingEvilFannyCV", "My friend, we've made it to Hardmode! Plenty of new opportunities have popped up and plenty of dangerous new foes now lurk about.",
                    "Idle", (FannySceneMetrics scene) => Main.hardMode && hasRemix, 8, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true).AddDelay(5);
                fannyMessages.Add(introLore);

                FannyMessage introEvilLore = new FannyMessage("IntroducingEvilFanny2CV", "'Sup",
                    "EvilIdle", FannyMessage.AlwaysShow, 6, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true)
                    .NeedsActivation(4f).SpokenByEvilFanny();

                introLore.AddStartEvent(() => introEvilLore.ActivateMessage());

                fannyMessages.Add(introEvilLore);

                FannyMessage introLore2 = new FannyMessage("IntroducingEvilFanny3CV", "E-evil Fanny!? You're here too!?",
                    "Sob", FannyMessage.AlwaysShow, 6, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true)
                    .NeedsActivation();
                introLore.AddEndEvent(() => introLore2.ActivateMessage());

                fannyMessages.Add(introLore2);

                FannyMessage introEvilLore2 = new FannyMessage("IntroducingEvilFanny4CV", "Yeah.",
                   "EvilIdle", FannyMessage.AlwaysShow, 5, needsToBeClickedOff: false, onlyPlayOnce: true, displayOutsideInventory: true)
                   .NeedsActivation().SpokenByEvilFanny();

                introEvilLore.AddEndEvent(() => introEvilLore2.ActivateMessage());

                fannyMessages.Add(introEvilLore2);

                FannyMessage introLore3 = new FannyMessage("IntroducingEvilFanny5CV", "$0, Evil Fanny has once again returned! How is this even possible!?",
                   "Idle", FannyMessage.AlwaysShow, 8, onlyPlayOnce: true, displayOutsideInventory: true)
                   .NeedsActivation().AddDynamicText(Steamworks.SteamFriends.GetPersonaName);

                introLore2.AddEndEvent(() => introLore3.ActivateMessage());

                fannyMessages.Add(introLore3);

                fannyMessages.Add(new FannyMessage("AbyssBegin", "Every 60 seconds in the Abyss a hour passes by, truly wonderful!",
                   "Nuhuh", (FannySceneMetrics scene) => !Main.zenithWorld && NPC.downedBoss3).SetHoverTextOverride("Very interesting Fanny!"));

                fannyMessages.Add(new FannyMessage("TorchGod", "A fellow being of the flames! It seems you played with a bit too much fire and now you are facing the wrath of the almighty Torch God! Don't worry though, he's impervious to damage, so you won't be able to hurt him.",
                  "Awooga", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == NPCID.TorchGod)));

                FannyMessage pumpking1 = new FannyMessage("Pumpking1", "Wh- Ahh! AAAAAAAAAAAAH!!",
                    "Awooga", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == NPCID.Pumpking), 3, needsToBeClickedOff: false).SetHoverTextOverride("What?");

                fannyMessages.Add(pumpking1);

                FannyMessage pumpking2 = new FannyMessage("Pumpking2", "I told Fanny as a joke that jack-o-lanterns get their lights by eating flames. Don't tell him, though. It's funnier this way.",
                    "EvilIdle", FannyMessage.AlwaysShow).AddDelay(2).NeedsActivation().SetHoverTextOverride("Sure? I might tell Fanny later...");

                pumpking1.AddEndEvent(() => pumpking2.ActivateMessage());

                fannyMessages.Add(pumpking2);

                // Empress
                FannyMessage eol1 = new FannyMessage("EoL1", "So, there's a second boss for the Hallow... Then where's the second boss for the other biomes? Did they just like this one more than the others?",
                    "EvilIdle", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == NPCID.HallowBoss), 8, needsToBeClickedOff: false, displayOutsideInventory: true).SpokenByEvilFanny();

                fannyMessages.Add(eol1);

                FannyMessage eol2 = new FannyMessage("EoL2", "... then again, the other boss is a recolor. Maybe for the best.",
                    "EvilIdle", FannyMessage.AlwaysShow, 6, needsToBeClickedOff: false, displayOutsideInventory: true).SpokenByEvilFanny().NeedsActivation();

                eol1.AddEndEvent(() => eol2.ActivateMessage());

                fannyMessages.Add(eol2);

                FannyMessage eol3 = new FannyMessage("EoL3", "Just like you!",
                    "Nuhuh", FannyMessage.AlwaysShow, 5, needsToBeClickedOff: false, displayOutsideInventory: true).NeedsActivation();

                eol2.AddEndEvent(() => eol3.ActivateMessage());

                fannyMessages.Add(eol3);

                FannyMessage eol4 = new FannyMessage("EoL4", "...",
                    "EvilIdle", FannyMessage.AlwaysShow, 5, needsToBeClickedOff: false, displayOutsideInventory: true).SpokenByEvilFanny().NeedsActivation();

                eol3.AddEndEvent(() => eol4.ActivateMessage());

                fannyMessages.Add(eol4);

                FannyMessage eol5 = new FannyMessage("EoL5", "Bitch",
                    "EvilIdle", (FannySceneMetrics scene) => ChildSafety.Disabled, 5, onlyPlayOnce: false, displayOutsideInventory: true).SpokenByEvilFanny().NeedsActivation();

                eol4.AddEndEvent(() => eol5.ActivateMessage());

                fannyMessages.Add(eol5);



                FannyMessage babil1 = new FannyMessage("Babil1", "Hey there, adventurer! Have you heard about the Essence of Babil? It's this amazing crafting material that drops from certain jungle creatures. Let me give you some tips on how to find it!",
                    "Nuhuh", (FannySceneMetrics scene) => Main.hardMode && Main.LocalPlayer.ZoneJungle).SetHoverTextOverride("Umm, Essence of Babil?");

                fannyMessages.Add(babil1);

                FannyMessage babil2 = new FannyMessage("Babil2", "Oh you sweet summer child! The Essence of Babil is this incredible, mystical substance you can gather from jungle enemies. It's a key ingredient for crafting some seriously awesome gear. You should definitely try to collect it!",
                    "Nuhuh", FannyMessage.AlwaysShow).SetHoverTextOverride("Huh, okay. So, where do I find it?").AddDelay(2).NeedsActivation();

                babil1.AddEndEvent(() => babil2.ActivateMessage());

                fannyMessages.Add(babil2);

                FannyMessage babil3 = new FannyMessage("Babil3", "Seriously? I just told you, it drops from jungle creatures. You know, those critters lurking around in the jungle biome? Go hunt them down, and you might get your hands on some Essence of Babil!",
                    "Nuhuh", FannyMessage.AlwaysShow).SetHoverTextOverride("Jungle creatures... got it!").NeedsActivation();

                babil2.AddEndEvent(() => babil3.ActivateMessage());

                fannyMessages.Add(babil3);

                FannyMessage babil4 = new FannyMessage("Babil4", "The Essence of Babil used for crafting powerful items. You can create some fantastic air-themed equipment with it. Seriously, just check the crafting menu, it's all there!",
                    "Nuhuh", FannyMessage.AlwaysShow).SetHoverTextOverride("I'm not sure I understand...").AddDelay(5).NeedsActivation();

                babil3.AddEndEvent(() => babil4.ActivateMessage());

                fannyMessages.Add(babil4);

                FannyMessage babil5 = new FannyMessage("Babil5", "Okay, let me spell it out for you one more time. Essence of Babil is a crafting material. You find it in the jungle. You use it to make cool stuff. Got it now? Good!",
                    "Nuhuh", FannyMessage.AlwaysShow).SetHoverTextOverride("Uh, thanks, Fanny. I think I get it now.").NeedsActivation();

                babil4.AddEndEvent(() => babil5.ActivateMessage());

                fannyMessages.Add(babil5);

                FannyMessage babil6 = new FannyMessage("Babil6", "Hey there, adventurer... Have you heard about the Essence of Babil? It's this... remarkable crafting material that drops from such unworthy jungle creatures. Let me grace you with some information, whether you appreciate it or not.",
                    "EvilIdle", (FannySceneMetrics scene) => Main.hardMode && Main.LocalPlayer.ZoneJungle).SetHoverTextOverride("Umm, Essence of Babil? What's that?").AddDelay(3).SpokenByEvilFanny();

                fannyMessages.Add(babil6);

                FannyMessage babil7 = new FannyMessage("Babil7", "Oh, how utterly clueless. The Essence of Babil is this incredibly mundane substance you can get from jungle enemies. You might even consider it somewhat important for crafting marginally useful gear. But, hey, who cares, right?",
                    "EvilIdle", FannyMessage.AlwaysShow).SetHoverTextOverride("Huh, okay. So, where do I find it?").NeedsActivation().SpokenByEvilFanny();

                babil6.AddEndEvent(() => babil7.ActivateMessage());

                fannyMessages.Add(babil7);

                FannyMessage babil8 = new FannyMessage("Babil8", "Seriously? I can't believe I have to repeat myself. It drops from those jungle creatures, assuming you can manage to defeat them, of course. Go ahead, give it a shot. Not like it matters.",
                    "EvilIdle", FannyMessage.AlwaysShow).SetHoverTextOverride("Jungle creatures... got it. But what can I make with it?").NeedsActivation().SpokenByEvilFanny().AddDelay(5);

                babil7.AddEndEvent(() => babil8.ActivateMessage());

                fannyMessages.Add(babil8);

                FannyMessage babil9 = new FannyMessage("Babil9", "You're really pushing your limits here, aren't you? It's used to craft... well, whatever. You can create some totally average air-themed equipment. But, honestly, who cares about that, right?",
                    "EvilIdle", FannyMessage.AlwaysShow).SetHoverTextOverride("I'm not sure I understand...").NeedsActivation().SpokenByEvilFanny();

                babil8.AddEndEvent(() => babil9.ActivateMessage());

                fannyMessages.Add(babil9);

                FannyMessage babil10 = new FannyMessage("Babil10", "Of course, you don't!!! Why would I expect any different? Essence of Babil is just a crafting material. You find it in the jungle. You use it to make \"cool\" stuff, if you're into that sort of thing. But, frankly, I couldn't care less.",
                    "EvilIdle", FannyMessage.AlwaysShow).SetHoverTextOverride("Uh, thanks, Evil Fanny. I think I get it now.").NeedsActivation().SpokenByEvilFanny().AddDelay(2);

                babil9.AddEndEvent(() => babil10.ActivateMessage());

                fannyMessages.Add(babil10);

                FannyMessage babil11 = new FannyMessage("Babil11", "You think you \"get it\"? You're beyond hopeless! There, now you're truly enlightened. Enjoy your essence... of oblivion!",
                    "EvilIdle", FannyMessage.AlwaysShow, duration: 20).NeedsActivation().SpokenByEvilFanny().AddStartEvent(() => Main.LocalPlayer.AddBuff(BuffID.Suffocation, 222222));

                babil10.AddEndEvent(() => babil11.ActivateMessage());

                fannyMessages.Add(babil11);



                FannyMessage polter1 = new FannyMessage("PolterChan", "Poooolteeer Chaaaaaaaaaaannnn",
                    "Idle", (FannySceneMetrics scene) => Main.LocalPlayer.HasBuff(ModContent.BuffType<PolterBuff>())).SetHoverTextOverride("Poooolteeer Chaaaaaaaaaaannnn");
                
                fannyMessages.Add(polter1);

                FannyMessage polter2 = new FannyMessage("EvilPolter", "You people need help.",
                    "EvilIdle", FannyMessage.AlwaysShow, needsToBeClickedOff: true).NeedsActivation().SpokenByEvilFanny().AddDelay(3);

                polter1.AddEndEvent(() => polter2.ActivateMessage());
                fannyMessages.Add(polter2);

                Mod calamity;
                bool calamityActive = ModLoader.TryGetMod("CalamityMod", out calamity);
                if (calamityActive)
                {
                    LoadCalamityDependantMessages();
                }

                try
                {
                    if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\"))
                    {
                        List<string> games = Directory.GetDirectories("C:\\Program Files (x86)\\Steam\\steamapps\\common\\").ToList();
                        if (games.Count > 0)
                        {
                            List<string> noTerraria = new List<string>();
                            if (games != null && games.Count > 0)
                            {
                                for (int i = 0; i < games.Count; i++)
                                {
                                    string b = games[i].Remove(0, 46);
                                    if (!(b.Contains("Terraria") || b.Contains("tModLoader") || b.Contains("Steamworks")))
                                    {
                                        noTerraria.Add(b);
                                    }
                                }
                            }
                            if (noTerraria.Count > 0)
                            {
                                fannyMessages.Add(new FannyMessage("StraightUpEvil", "By the way, $0, I see everything. Like how you have played " + noTerraria[Main.rand.Next(0, noTerraria.Count - 1)],
                               "Cryptid", (FannySceneMetrics m) => NPC.downedMoonlord).AddDynamicText(SteamFriends.GetPersonaName).SetHoverTextOverride("Oh golly gee Fanny!"));
                            }
                        }
                    }
                }
                catch
                {

                }

                discord1 = new FannyMessage("DiscordianHash", "Oh you're on Discord? What are they talking about in $0? I wanna see!",
                "Idle", (FannySceneMetrics m) => DiscordChat != "" && DiscordChat.Contains('#') && NPC.downedBoss1 && !discord2.alreadySeen).AddDynamicText(()=> DiscordChat).SetHoverTextOverride("Nothing much Fanny!");

                fannyMessages.Add(discord1);

                discord2 = new FannyMessage("DiscordianAt", "Oh you're on Discord? What are you and $0 talking about? I wanna see!",
                "Idle", (FannySceneMetrics m) => DiscordChat != "" && !DiscordChat.Contains('#') && NPC.downedBoss1 && !discord1.alreadySeen).AddDynamicText(() => DiscordChat).SetHoverTextOverride("Nothing much Fanny!");

                fannyMessages.Add(discord2);
            }
        }

        public static FannyMessage discord1 = null;
        public static FannyMessage discord2 = null;
        public static string DiscordChat = "";

        public static string GetDiscord()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                string discord = "";
                foreach (Process p in processes)
                {
                    if (!String.IsNullOrEmpty(p.MainWindowTitle))
                    {
                        if (p.MainWindowTitle.Contains("Discord"))
                        {
                            discord = p.MainWindowTitle;
                            break;
                        }
                    }
                }
                if (discord != "")
                {
                    string finalDiscord = "";
                    bool foundHash = false;
                    for (int i = 0; i < discord.Length; i++)
                    {
                        if (!foundHash)
                        {
                            if (discord[i] == '#' || discord[i] == '@')
                            {
                                foundHash = true;
                                if (discord[i] == '@')
                                    continue;
                            }
                        }
                        if (foundHash)
                        {
                            if (discord[i] != ' ')
                            {
                                finalDiscord += discord[i];
                            }
                            else
                            {
                                DiscordChat = finalDiscord;
                                return finalDiscord;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return "";
        }



        [JITWhenModsEnabled("CalamityMod")]
        public static void LoadCalamityDependantMessages()
        {
            Mod calamity = ModLoader.GetMod("CalamityMod");
            fannyMessages.Add(new FannyMessage("Cryodeath", "Ha! Snow's over, Cryogen! Wasn't that pretty cool?",
               "Idle", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "cryogen")));

            fannyMessages.Add(new FannyMessage("FollyDeath", "You finally beat the Dragonfolly! Though, I don't know why you'd need pheromones though. I mean, you're already a handsome fellow...",
               "Nuhuh", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "bumblebirb")).SetHoverTextOverride("Uhm.... Thanks for the help, Fanny...?"));

            fannyMessages.Add(new FannyMessage("CalcloneDeath", "Oh it was just a clone.",
                "Sob", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "calamitasclone")));

            fannyMessages.Add(new FannyMessage("Yharore", "Looks like the caverns have been laced with Auric Ore! The ore veins are pretty massive, so I’d say it’s best that you get up close and go hog wild!",
               "Idle", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "yharon")));

            fannyMessages.Add(new FannyMessage("DraedonExit", "Good golly! You did it! Though I'd HATE to imagine the financial losses caused by the destruction of those machines.",
                "Awooga", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "exomechs")));

            fannyMessages.Add(new FannyMessage("SCalDie", "That was exhilarating! Though that means the end of our adventure is upon us. What a Calamity as one may say!",
                "Awooga", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "supremecalamitas")));

            fannyMessages.Add(new FannyMessage("Deus", "It appears that you are once again fighting a large serpentine creature. Therefore, it's advisable to do what you've done with them before and blast it with fast single target weaponry!",
               "Idle", (FannySceneMetrics scene) => !Main.zenithWorld && scene.onscreenNPCs.Any(n => n.type == CalNPCID.AstrumDeus)));

            fannyMessages.Add(new FannyMessage("DeusSplitMod", "This is getting out of hand! Now there are two of them!",
               "Awooga", (FannySceneMetrics scene) => !Main.zenithWorld && NPC.CountNPCS(CalNPCID.AstrumDeus) > 1));

            fannyMessages.Add(new FannyMessage("PGuardians", "It seems like these mischievous scoundrels are up to no good, and plan to burn all the delicious meat! We gotta go put an end to their plan of calamity!",
                "Nuhuh", (FannySceneMetrics scene) => !Main.zenithWorld && scene.onscreenNPCs.Any(n => n.type == CalNPCID.GuardianCommander)));

            fannyMessages.Add(new FannyMessage("NewYork", "Oh, I saw that sky somewhere in my dreams! the place was called uhhh... New Yuck... Nu Yok.... New Yok.... yea something like that!",
                "Nuhuh", (FannySceneMetrics scene) => !Main.zenithWorld && scene.onscreenNPCs.Any(n => n.type == CalNPCID.Yharon)).SetHoverTextOverride("It's called New York, Fanny! I'll take you there one day."));

            fannyMessages.Add(new FannyMessage("DraedonEnter", "Gee willikers! It's the real Draedon! He will soon present you with a difficult choice between three of your previous foes but with new attacks and increased difficulty. This appears to be somewhat of a common theme with this world dontcha think?",
               "Awooga", (FannySceneMetrics scene) => !Main.zenithWorld && scene.onscreenNPCs.Any(n => n.type == CalNPCID.Draedon)));

            fannyMessages.Add(new FannyMessage("ExoMayhem", "Wow! What a mayhem! Don't panic though, if you focus on dodging, you will be less likely to get hit. A common strategy for these tin cans is to \" fall god \", which I believe means summoning other gods like the Slime God and killing them for extra health. You should also pay extra attention to Ares' red cannon, because sometimes it can sweep across the screen, ruining your dodge flow. As for the twins, keep a close eye on the right one, as it has increased fire rate. There is no saving you from Thanatos, it isn't synced and breaks the structure these guys are allegedly supposed to have. Like seriously, why do the twins and Ares hover to the sides and above you while that robo-snake just does whatever the heckle heckity heckering hecky heck he wants? It would be significantly more logical if it tried to like stay below you, but no. Anyways, good luck buddy! You're almost at the end, you can do this!",
                "Idle", (FannySceneMetrics scene) => !Main.zenithWorld && scene.onscreenNPCs.Any(n => n.type == CalNPCID.Thanatos) && scene.onscreenNPCs.Any(n => n.type == CalNPCID.Ares) && scene.onscreenNPCs.Any(n => n.type == CalNPCID.Apollo), needsToBeClickedOff: false, duration: 22));

            fannyMessages.Add(new FannyMessage("Scaldie", "Are you hurt? That was calamitous!",
                "Sob", (FannySceneMetrics scene) => scene.onscreenNPCs.Any(n => n.type == CalNPCID.SupremeCalamitas) && Main.LocalPlayer.dead));

            fannyMessages.Add(new FannyMessage("Provideath", "Hah, take that, Providence! Who knew a pile of rocks with flame wings could be so evil? It's like crushing a grumpy boulder. Now, who's up for some rocky road ice cream? Too soon? Nah, it's never too soon for dessert!",
                "Idle", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "providence")));

            fannyMessages.Add(new FannyMessage("Poltah", "Polterghast, more like... Polter-gone! We scattered those swirling spirits of cyan and pink like confetti at a victory parade. Now, who's ready to celebrate with some real fireworks? I've got the sparklers ready to light up the night sky!",
                "Idle", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("GetBossDowned", "polterghast")));

            fannyMessages.Add(new FannyMessage("AcidRain", "Acid rain? More like lemonade from the heavens! Just kidding, it's actually quite deadly. Make sure you have an umbrella made of titanium or something, unless you want to dissolve faster than a sugar cube in hot tea! And watch out for those pesky exploding toads!",
                "Idle", (FannySceneMetrics scene) => !Main.zenithWorld && (bool)calamity.Call("IsAcidRainActive")));


        }


        public static void LoadFannyPortraits()
        {
            FannyPortrait.LoadPortrait("Idle", 8, 5);
            FannyPortrait.LoadPortrait("Awooga", 4, 5);
            FannyPortrait.LoadPortrait("Cryptid", 1, 6);
            FannyPortrait.LoadPortrait("Sob", 4, 4);
            FannyPortrait.LoadPortrait("Nuhuh", 19, 4);

            FannyPortrait.LoadPortrait("EvilIdle", 1, 2);
        }

        /// <summary>
        /// Registers a message for fanny to speak
        /// You can either provide a condition to the message, in which case the message will automatically play when the condition is met <br/>
        /// Alternatively, you could cache the message, and play it yourself when needed using <see cref="Fanny.TalkAbout(FannyMessage)"/>
        /// </summary>
        public static FannyMessage LoadFannyMessage(FannyMessage message)
        {
            fannyMessages.Add(message);
            return message;
        }
        #endregion

        public override void PostUpdateEverything()
        {
            if (Main.dedServ)
                return;

            //Tick down message times
            for (int i = 0; i < fannyMessages.Count; i++)
            {
                FannyMessage msg = fannyMessages[i];

                //Fallback
                if (msg.DesiredFanny == null)
                    msg.DesiredFanny = FannyUIState.FannyTheFire;

                if (msg.timerToPlay > 0 && msg.timeToWaitBeforePlaying > msg.timerToPlay)
                    msg.timerToPlay++;

                //Tick down the cooldown
                if (msg.CooldownTime > 0)
                    msg.CooldownTime--;

                //Otherwise tick down the timer
                else if (msg.TimeLeft > 0)
                {
                    msg.TimeLeft--;

                    //Message stays in stasis if it needs to be clicked off
                    if (msg.NeedsToBeClickedOff && msg.TimeLeft == 30)
                        msg.TimeLeft = 31;
                }

                //Stop talking
                else if (msg.speakingFanny != null)
                {
                    msg.speakingFanny.StopTalking();
                    msg.StartCooldown();
                }

                //Do the start effects with a delay
                if (msg.delayTime > 0 && msg.TimeLeft == msg.MessageDuration)
                    msg.OnMessageStart();
            }

            //Don't even try looking for a new message if speaking already / On speak cooldown
            if (FannyUISystem.UIState.AnyAvailableFanny())
            {

                FannySceneMetrics scene = new FannySceneMetrics();
                //Precalculate screen NPCs to avoid repeated checks over all npcs everytime
                Rectangle screenRect = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
                scene.onscreenNPCs = Main.npc.Where(n => n.active && n.Hitbox.Intersects(screenRect));

                //Split the messages per fanny speaking
                var messageGroups = fannyMessages.GroupBy(m => m.DesiredFanny);
                foreach (var messageGroup in messageGroups)
                {
                    Fanny speakingFanny = messageGroup.First().DesiredFanny;

                    //Check if the fanny in question can speak
                    if (!speakingFanny.CanSpeak)
                        continue;

                    foreach (FannyMessage message in messageGroup)
                    {
                        if (message.CanPlayMessage() && message.Condition(scene))
                        {
                            message.PlayMessage(speakingFanny);
                            break;
                        }
                    }
                }
            }
        }

        #region Saving and Loading data
        public override void ClearWorld()
        {
            //Stop all fannies talking, and close every dialogue
            FannyUISystem.UIState.StopAllDialogue();
            for (int i = 0; i < fannyMessages.Count; i++)
            {
                FannyMessage msg = fannyMessages[i];
                msg.timerToPlay = 0;
                msg.speakingFanny = null;
                msg.TimeLeft = 0;
                msg.CooldownTime = 0;
                msg.alreadySeen = false;
            }
        }
        #endregion
    }

    public class FannySceneMetrics
    {
        public IEnumerable<NPC> onscreenNPCs;
    }

    public class FannyMessage
    {
        private string originalText;
        private string formattedText;
        internal int maxTextWidth;
        public float textSize;

        public string Text
        {
            get => formattedText;
            set
            {
                //Cache the original text, and format it
                originalText = value;
                FormatText(FontAssets.MouseText.Value, maxTextWidth);
            }
        }

        public string Identifier;

        public int CooldownTime { get; set; }
        private int cooldownDuration;

        public int TimeLeft { get; set; }
        internal int messageDuration;
        public int MessageDuration => messageDuration;

        //Which fanny the message wants to be spoken by
        public Fanny DesiredFanny;

        //The fanny actively speaking the message. For cases where we want one fanny to say what the other fanny says
        public Fanny speakingFanny;


        public bool alreadySeen = false;

        public bool DisplayOutsideInventory { get; set; }
        public bool OnlyPlayOnce { get; set; }
        public bool NeedsToBeClickedOff { get; set; }
        public bool PersistsThroughSaves { get; set; }

        public FannyPortrait Portrait { get; set; }

        public FannyTextboxPalette? paletteOverride = null;
        public string hoverTextOverride = "";
        public SoundStyle? voiceOverride = null;

        public delegate string DynamicFannyTextSegment();
        public static string GetPlayerName() => Main.LocalPlayer.name;
        public static string GetWorldName() => Main.worldName;

        public List<DynamicFannyTextSegment> textSegments = new List<DynamicFannyTextSegment>();


        public FannyMessage(string identifier, string message, string portrait = "", FannyMessageCondition condition = null, float duration = 5, float cooldown = 60, bool displayOutsideInventory = true, bool onlyPlayOnce = true, bool needsToBeClickedOff = true, bool persistsThroughSaves = true, int maxWidth = 380, float fontSize = 1f)
        {
            //Unique identifier for saving data
            Identifier = identifier;
            textSize = fontSize;

            maxTextWidth = maxWidth;
            Text = message;
            Condition = condition ?? NeverShow;

            //Duration and cooldown are inputted in seconds and then converted into frames by the constructor
            cooldownDuration = (int)(cooldown * 60);
            CooldownTime = 0;

            messageDuration = (int)(duration * 60);
            TimeLeft = 0;

            DisplayOutsideInventory = displayOutsideInventory;
            OnlyPlayOnce = onlyPlayOnce;
            NeedsToBeClickedOff = needsToBeClickedOff;
            PersistsThroughSaves = persistsThroughSaves;

            if (portrait == "")
                portrait = "Idle";
            Portrait = FannyManager.Portraits[portrait];

            //default
            DesiredFanny = FannyUIState.FannyTheFire;
        }

        /// <summary>
        /// Makes the message be spoken by evil fanny
        /// </summary>
        public FannyMessage SpokenByEvilFanny()
        {
            DesiredFanny = FannyUIState.EvilFanny;
            return this;
        }

        /// <summary>
        /// Makes the message get spoken by the specified fanny
        /// </summary>
        public FannyMessage SpokenByAnotherFanny(Fanny speakingFanny)
        {
            DesiredFanny = speakingFanny;
            return this;
        }

        /// <summary>
        /// Adds a custom textbox palette override for this message
        /// </summary>
        public FannyMessage SetPalette(FannyTextboxPalette palette)
        {
            paletteOverride = palette;
            return this;
        }

        public FannyMessage SetHoverTextOverride(string hoverTextOverride)
        {
            this.hoverTextOverride = hoverTextOverride;
            return this;
        }

        public FannyMessage SetSoundOverride(SoundStyle soundStyleOverride)
        {
            this.voiceOverride = soundStyleOverride;
            return this;
        }

        #region Message Condition stuff

        public delegate bool FannyMessageCondition(FannySceneMetrics sceneMetrics);
        public static bool NeverShow(FannySceneMetrics sceneMetrics) => false;
        public static bool AlwaysShow(FannySceneMetrics sceneMetrics) => true;

        public FannyMessageCondition Condition { get; set; }

        public int timerToPlay = 0;
        public int timeToWaitBeforePlaying = 0;

        /// <summary>
        /// Makes it so that the message will never play on its own, and needs both its condition to be met, and <see cref="ActivateMessage"/> to be called for it to be read
        /// If the condition for the message isn't met, the message won't play even if activated
        /// </summary>
        public FannyMessage NeedsActivation()
        {
            timeToWaitBeforePlaying = 1;
            return this;
        }

        /// <summary>
        /// Makes it so that the message doesn't play on its own, and needs to be manually called by another message or event to play, using <see cref="ActivateMessage"/><br/>
        /// If the condition for the message isn't met, the message won't play even if activated
        /// </summary>
        /// <param name="timer">Delay period after <see cref="ActivateMessage"/> is called where the message waits to play, in seconds</param>
        public FannyMessage NeedsActivation(float delay = 1)
        {
            timeToWaitBeforePlaying = (int)(delay * 60);
            return this;
        }

        /// <summary>
        /// Manually activates a message, making it able to be played if its condition is met
        /// To make a message need manual activation, use <see cref="NeedsActivation"/>
        /// </summary>
        public void ActivateMessage()
        {
            //Increases the timer by 1, which makes it start counting up
            timerToPlay++;
        }

        public int delayTime = 0;
        public bool InDelayPeriod => TimeLeft > messageDuration;

        /// <summary>
        /// Adds a delay time before the message starts playing, when its activation condition is met<br/>
        /// No other message can play while the delay period is started
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public FannyMessage AddDelay(float delay = 1)
        {
            delayTime = (int)(delay * 60);
            return this;
        }
        #endregion

        #region Playing messages
        public event Action OnStart;
        public event Action OnEnd;

        /// <summary>
        /// Adds an action that happens when the message is being read
        /// </summary>
        public FannyMessage AddStartEvent(Action action)
        {
            OnStart += action;
            return this;
        }

        /// <summary>
        /// Adds an action that happens when the message goes away
        /// </summary>
        public FannyMessage AddEndEvent(Action action)
        {
            OnEnd += action;
            return this;
        }


        //Technically the TimeLeft is not needed because when its active, no other message will try to play. But just in case
        public bool CanPlayMessage()
        {
            return CooldownTime <= 0 &&                                     //Can't play messages on cooldown
                   TimeLeft <= 0 &&                                         //Can't play messages that are already playing
                   (!OnlyPlayOnce || !alreadySeen) &&                       //Can't play messages that are only played once, more than once
                   (DisplayOutsideInventory || Main.playerInventory) &&     //Can't play messages that only display in the inventory outside of the inventory
                   timeToWaitBeforePlaying <= timerToPlay &&                  //Can't play messages with a timer before the timer is reached
                   !CalValEX.InAnySubworld();                               //Can't play messages while in a subworld
        }

        public void PlayMessage(Fanny fanny)
        {
            TimeLeft = messageDuration + delayTime;
            fanny.UsedMessage = this;
            speakingFanny = fanny;
            alreadySeen = true;

            //Recalculate the text as its played if we have dynamic text segments
            if (textSegments.Count > 0)
                FormatText(FontAssets.MouseText.Value, maxTextWidth);

            //Immediately play message start effects
            if (delayTime == 0)
                OnMessageStart();
        }

        public void OnMessageStart()
        {
            if (speakingFanny is null)
                return;

            speakingFanny.needsToShake = true;
            SoundEngine.PlaySound(SoundID.MenuOpen);
            SoundEngine.PlaySound(voiceOverride ?? speakingFanny.speakingSound);
            OnStart?.Invoke();
        }

        public void StartCooldown()
        {
            OnEnd?.Invoke();
            CooldownTime = cooldownDuration;
            speakingFanny = null;

            //Reset timer to play if we want to play it again later
            timerToPlay = 0;
        }
        #endregion

        #region Item display
        public int ItemType { get; set; } = -22;
        public float ItemScale { get; set; } = 1f;
        public Vector2 ItemOffset { get; set; } = Vector2.Zero;
        public FannyMessage AddItemDisplay(int itemType, float itemScale = 1f, Vector2? itemOffset = null)
        {
            ItemType = itemType;
            ItemScale = itemScale;
            ItemOffset = itemOffset ?? Vector2.Zero;
            return this;
        }
        #endregion

        #region Text formatting
        public void FormatText(DynamicSpriteFont font, float maxLineWidth = 380)
        {
            if (Main.dedServ)
                return;

            string currentWord = "";
            float currentLineLenght = 0;
            float spaceWidth = font.MeasureString(" ").X;

            //This is the setence as we are building it
            string formattedSetence = "";
            string baseText = originalText;
            if (textSegments.Count > 0)
            {
                int i = 0;
                foreach (DynamicFannyTextSegment dynamicText in textSegments)
                {
                    baseText = baseText.Replace("$" + i.ToString(), dynamicText());
                    i++;
                }
            }


            for (int i = 0; i < baseText.Length; i++)
            {
                //When theres a space, check if the word is short enough to not go over our width limit
                if (baseText[i] == ' ')
                {
                    CheckWord(currentWord, maxLineWidth, font, ref formattedSetence, ref currentLineLenght, spaceWidth);
                    currentWord = "";
                }

                //Continue to build the word
                else
                    currentWord += baseText[i];
            }
            //Check the final word
            if (currentWord != "")
                CheckWord(currentWord, maxLineWidth, font, ref formattedSetence, ref currentLineLenght, spaceWidth);

            formattedText = formattedSetence;
        }

        /// <summary>
        /// Checks if a word can be fitted into the current text line, and if not adds a linebreak
        /// </summary>
        /// <param name="word"></param>
        private void CheckWord(string word, float maxLineWidth, DynamicSpriteFont font, ref string formattedSetence, ref float currentLineLenght, float spaceWidth)
        {
            //Get the lenght of the word
            float wordLenght = font.MeasureString(word).X * textSize;

            //If adding the word doesn't make the line go over the max width, simply add the lenght of our last word (and the space) to the current line
            if (wordLenght + currentLineLenght <= maxLineWidth)
                currentLineLenght += wordLenght * textSize + spaceWidth;

            //If adding the word goes over the max width
            else
            {
                //Reset the line lenght to a new line (so only the word and the space
                currentLineLenght = wordLenght * textSize + spaceWidth;
                //Add a linebreak
                formattedSetence += "\n";
            }

            //Add the word and space to the end of the setence
            formattedSetence += word;
            formattedSetence += " ";
        }

        /// <summary>
        /// Tells fanny that the message spoken has a dynamic text element that is calculated on the fly. The dynamic text replaces the matching $ key. So if you have two dynamic text elements, itll replace $0 and $1
        /// </summary>
        /// <param name="customText"></param>
        /// <returns></returns>
        public FannyMessage AddDynamicText(DynamicFannyTextSegment customText)
        {
            if (originalText.Contains("$" + textSegments.Count.ToString()))
                textSegments.Add(customText);
            return this;
        }
        #endregion
    }

    public class FannyPortrait
    {
        public Asset<Texture2D> Texture;
        public int frameCount;
        public int animationSpeed;

        public static void LoadPortrait(string portraitName, int frameCount, int paintFrameCount)
        {
            FannyPortrait portrait = new FannyPortrait(portraitName, frameCount, paintFrameCount);
            //Load itself into the portrait list
            if (!FannyManager.Portraits.ContainsKey(portraitName))
                FannyManager.Portraits.Add(portraitName, portrait);
        }

        public FannyPortrait(string portrait, int frameCount, int paintFrameCount)
        {
            bool day = DateTime.Now.Day == 1 && DateTime.Now.Month == 4;
            string paint = day ? "" : "Paint";
            Texture = ModContent.Request<Texture2D>("CalValEX/AprilFools/Fanny/Fanny" + portrait + paint);
            this.frameCount = day ? frameCount : paintFrameCount;
            this.animationSpeed = 11;
        }
    }

    public struct FannyTextboxPalette
    {
        public Color background = Color.SaddleBrown;
        public Color backgroundHover = Color.SaddleBrown;
        public Color outline = Color.Magenta;
        public Color text = Color.Lime;
        public Color textOutline = Color.DarkBlue;

        public FannyTextboxPalette() { }

        public FannyTextboxPalette(Color text, Color textOutline, Color background, Color backgroundHover, Color outline)
        {
            this.text = text;
            this.textOutline = textOutline;
            this.background = background;
            this.backgroundHover = backgroundHover;
            this.outline = outline;
        }
    }
}