using CalValEX.Buffs.Transformations;
using CalValEX.Items.Equips.Transformations;
using CalValEX.Items.Equips.Hats;
using CalValEX.Items.Mounts.Morshu;
using CalValEX.Items.Equips.Shirts.AresChestplate;
using CalValEX.Items.Equips.Balloons;
using CalValEX.Projectiles.Pets;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Equips.Shirts;
using CalValEX.Items.Equips.Wings;
using CalValEX.Items.Mounts.Ground;
using CalamityMod.Particles;
using CalValEX.Projectiles;
using Terraria.ModLoader.IO;
using CalValEX.Items.Mounts.InfiniteFlight;
using CalValEX.Items.Equips.Hats.Draedon;
using CalValEX.Items.Equips.Shirts.Draedon;

namespace CalValEX
{
    public class CalValEXPlayer : ModPlayer {
        //public static readonly ClassSpecificPlayerLayer DraedonSet = new ClassSpecificPlayerLayer("CalValEX/Items/Equips/Shirts/Draedon/", "CalValEX/Items/Equips/Hats/Draedon/", "DraedonChestplate", "DraedonHelmet");

        private const int saveVersion = 0;

        public bool aero;
        public bool andro;
        public bool Angrypup;
        public bool asPet;
        public bool AstPhage;
        public bool BabyCnidrion;
        public bool babywaterclone;
        public bool bDoge;
        public bool BoldLizard;
        public bool CalamityBABYBool;
        public bool CalamityBabyGotHit;
        public bool calMonolith = false;
        public bool catfish;
        public bool Chihuahua;
        public bool cloudmini;
        public bool cr;
        public bool Cryokid;
        public bool darksunSpirits;
        public bool dBall;
        public bool deusmain;
        public bool deussmall;
        public bool dogMonolith = false;
        public bool drone;
        public bool dsPet;
        public bool Dstone;
        public bool eb;
        public bool eidolist;
        public bool Enredpet;
        public bool euros;
        public bool EWyrm;
        public bool excal;
        public bool feel;
        public bool fog;
        public bool George;
        public bool GeorgeII;
        public bool goozmaPet;
        public bool hDoge;
        public bool HellLab;
        public bool hover;
        public bool jared;
        public bool junsi;
        public bool leviMonolith = false;
        public bool Lightshield;
        public bool mAero;
        public bool mAmb;
        public bool mAme;
        public bool mArmored;
        public bool mBirb;
        public bool mBirb2;
        public bool mChan;
        public bool mClam;
        public bool mCry;
        public bool mDebris;
        public bool mDia;
        public bool mDoge;
        public bool mDuke;
        public bool MechaGeorge;
        public bool mEme;
        public bool mFolly;
        public bool mHeat;
        public bool mHeat2;
        public bool mHive;
        public bool mImp;
        public bool MiniCryo;
        public bool mNaked;
        public bool moistPet;
        public bool conejo;
        public bool ohio;
        public bool hasOhiod = false;

        public int morshuscal = 0;
        public int morshuTimer;
        public bool mPerf;
        public bool mPhan;
        public bool mRav;
        public bool mRub;
        public bool mSap;
        public bool mScourge;
        public bool mShark;
        public bool mSkater;
        public bool mSlime;
        public bool mTop;
        public bool Nugget;
        public bool oSquid;
        public bool PBGmini;
        public bool pbgMonolith = false;
        public bool ProGuard1;
        public bool ProGuard2;
        public bool ProGuard3;
        public bool ProviPet;
        public bool provMonolith = false;
        public bool pylon;
        public bool rarebrimling;
        public bool raresandmini;
        public bool RepairBot;
        public bool rover;
        public bool rPanda;
        public bool rusty;
        public bool sandmini;
        public bool sBun;
        public int SCalHits;
        public bool scalMonolith = false;
        public bool sDuke;
        public bool seerL;
        public bool seerM;
        public bool seerS;
        public bool sepet;
        public bool SignusMini;
        public bool sirember;
        public bool Skeetyeet;
        public bool SmolCrab;
        public bool squid;
        public bool sSignus;
        public bool StarJelly;
        public bool strongWeeb;
        public bool sVoid;
        public bool sWeeb;
        public bool SWPet;
        public bool TerminalRock;
        public bool tub;
        public bool uSerpent;
        public bool voidling;
        public bool VoidOrb;
        public bool voreworm;
        public bool worb;
        public bool yharonMonolith = false;
        public bool cryoMonolith = false;
        public bool brMonolith = false;
        public bool exoMonolith = false;
        public bool ySquid;
        public bool amogus;
        public bool buppy;
        public bool BMonster;
        public bool hage;
        public bool Blok;
        public bool ZoneAstral;
        public bool ZoneLab;
        public bool ZoneMockDungeon;
        public bool aesthetic;
        public bool exorb;
        public bool rockhat;
        public bool prismshell;
        public bool rainbow;
        public bool avalon;
        public bool SupJ;
        public bool poltermask;
        public bool polterchest;
        public bool polterthigh;
        public bool bSignut;
        public bool bSlime;
        public bool buffboi;
        public bool smaul;
        public bool shart;
        public bool roverd;
        public bool morshu;
        public bool morshugun;
        public bool scaldown;
        public bool yharcar;
        public bool sepneo;
        public bool ogscule;
        public bool nautilus;
        //Signus Transformation bools
        public bool signutHide;
        public bool signutForce;
        public bool signutPrevious;
        public bool signutPower;
        public bool signutTrans;
        //Andromeda transformation bools
        public bool androHide;
        public bool androForce;
        public bool androPrevious;
        public bool androPower;
        public bool androTrans;
        //Classic Brimmy transformation bools
        public bool classicHide;
        public bool classicForce;
        public bool classicPrevious;
        public bool classicPower;
        public bool classicTrans;
        //Cloud transformation bools
        public bool cloudHide;
        public bool cloudForce;
        public bool cloudPrevious;
        public bool cloudPower;
        public bool cloudTrans;
        //Sand transformation bools
        public bool sandHide;
        public bool sandForce;
        public bool sandPrevious;
        public bool sandPower;
        public bool sandTrans;
        //Bloody mary bools
        public bool maryHide;
        public bool maryForce;
        public bool maryPrevious;
        public bool maryPower;
        public bool maryTrans;
        //Arsenal Soldier bools
        public bool arsenalHelmet;
        public bool arsenalChestplate;
        public double rotcounter = 0;
        public double rotdeg = 0;
        public double rotsin = 0;
        public int chopperframe = 0;
        public int choppercounter = 0;
        public int helicounter = 0;
        public int heliframe = 0;
        public int conecounter = 0;
        public int coneframe = 0;
        public int twincounter = 0;
        public int twinframe = 0;
        public int stwincounter = 0;
        public int stwinframe = 0;
        public bool wulfrumjam;
        public bool cassette;
        public bool specan;
        public bool carriage;
        public bool silvajeep;
        public float bcarriagewheel = 0.0f;
        public bool twinballoon;
        public bool artballoon;
        public bool apballoon;
        public bool sapballoon;
        public bool sartballoon;
        //344
        //Pong stuff
        public bool pongactive;
        public int pongstage = 0;
        public int pongoutcome = 0;
        //Boi stuff
        public bool boiactive;
        public bool boiatlantis;
        public bool boienemy1;
        public bool boienemy2;
        public bool boienemy3;
        public int boistage;
        public int boihealth = 3;
        public int bossded;
        public bool aresarms;
        public bool lumpe;
        public bool geldonalive;
        public bool ares;
        public bool thanos;
        public bool twins;
        /*public float pongballposx;
        public float pongballposy;
        public float sliderposx;
        public float sliderposy;*/
        //Enchants
        public bool soupench = false;

        // Bootleg Calamity bools
        public bool SirenHeart;

        //Æ: Drae's bools
        public bool digger;
        public bool BestInst;
        public bool DustChime;
        public bool NurseryBell;
        public bool AlarmClock;
        public bool MaladyBells;
        public bool Harbinger;
        public bool SpiritDiner;
        public bool AltarBell;
        public bool WormBell;
        public bool Vaseline;
        public bool ScratchedGong;
        public bool TubRune;
        public bool Pandora;
        public bool profanedCultist;
        public bool profanedCultistHide;
        public bool profanedCultistForce;
        public bool perennial;
        public bool perennialHide;
        public bool perennialForce;
        public bool zygote;
        public bool brimberry;
        public bool bellaCloak;
        public bool bellaCloakHide;
        public bool bellaCloakForce;
        public bool helipack;
        public bool CalValPat;
        public bool jellyInv;
        public bool oracleInv;
        public bool bikeShred;

        public override void Initialize() {
            jellyInv = false;
            oracleInv = false;
            ResetMyStuff();
            CalamityBabyGotHit = false;
            morshuTimer = 0;
        }

        public override void ResetEffects() {
            signutPrevious = signutTrans;
            signutTrans = signutHide = signutForce = signutPower = false;
            androPrevious = androTrans;
            androTrans = androHide = androForce = androPower = false;
            classicPrevious = classicTrans;
            classicTrans = classicHide = classicForce = classicPower = false;
            cloudPrevious = cloudTrans;
            cloudTrans = cloudHide = cloudForce = cloudPower = false;
            sandPrevious = sandTrans;
            sandTrans = sandHide = sandForce = sandPower = false;
            maryPrevious = maryTrans;
            maryTrans = maryHide = maryForce = maryPower = false;
            profanedCultist = profanedCultistHide = profanedCultistForce = false;
            perennial = perennialHide = perennialForce = false;
            bellaCloak = bellaCloakHide = bellaCloakForce = false;
            arsenalChestplate = false;
            arsenalHelmet = false;
            ResetMyStuff();
        }

        //Update vanity is fucking broken so they are detected here
        public override void UpdateEquips() {
            if (Player.armor[10].type == ItemType<Aestheticrown>()) {
                aesthetic = true;
            }
            if (Player.armor[10].type == ItemType<StonePile>()) {
                rockhat = true;
            }
            if (Player.armor[10].type == ItemType<TrueCosmicCone>()) {
                conejo = true;
            }
            if (Player.armor[10].type == ItemType<SpectralstormHat>()) {
                specan = true;
            }
            if (Player.armor[10].type == ItemType<DraedonHelmet>())
            {
                arsenalHelmet = true;
            }
            if (Player.armor[11].type == ItemType<DraedonChestplate>())
            {
                arsenalChestplate = true;
            }
            if (Player.armor[11].type == ItemType<AresChestplate>()) {
                bool gausspawned = Player.ownedProjectileCounts[ProjectileType<GaussArm>()] <= 0;
                bool laserpawned = Player.ownedProjectileCounts[ProjectileType<LaserArm>()] <= 0;
                bool teslaspawned = Player.ownedProjectileCounts[ProjectileType<TeslaArm>()] <= 0;
                bool plasmaspawned = Player.ownedProjectileCounts[ProjectileType<PlasmaArm>()] <= 0;
                if (gausspawned && Player.whoAmI == Main.myPlayer) {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.armor[11]), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                        0, 0, ProjectileType<GaussArm>(), 0, 0f, Player.whoAmI);
                }
                if (laserpawned && Player.whoAmI == Main.myPlayer) {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.armor[11]), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                       0, 0, ProjectileType<LaserArm>(), 0, 0f, Player.whoAmI);
                }
                if (teslaspawned && Player.whoAmI == Main.myPlayer) {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.armor[11]), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                       0, 0, ProjectileType<TeslaArm>(), 0, 0f, Player.whoAmI);
                }
                if (plasmaspawned && Player.whoAmI == Main.myPlayer) {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.armor[11]), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                       0, 0, ProjectileType<PlasmaArm>(), 0, 0f, Player.whoAmI);
                }
                aresarms = true;
            }
        }

        public override void UpdateVisibleVanityAccessories() {
            Mod antisocial;
            ModLoader.TryGetMod("Antisocial", out antisocial);
            Item itemVan = Player.armor[11];
            if (itemVan.type == ItemType<BloodyMaryDress>()) {
                maryHide = false;
                maryForce = true;
            }
            if (itemVan.type == ItemType<ProfanedCultistRobes>()) {
                profanedCultistHide = false;
                profanedCultistForce = true;
            }
            if (itemVan.type == ItemType<PerennialDress>())
            {
                perennialHide = false;
                perennialForce = true;
            }
            if (itemVan.type == ItemType<BelladonnaCloak>()) {
                bellaCloakHide = false;
                bellaCloakForce = true;
            }
            for (int n = 13; n < 18 + Player.extraAccessorySlots; n++) {
                Item item = Player.armor[n];
                if (item.type == ItemType<Signus>()) {
                    signutHide = false;
                    signutForce = true;
                } else if (item.type == ItemType<ProtoRing>()) {
                    androHide = false;
                    androForce = true;
                } else if (item.type == ItemType<BurningEye>()) {
                    classicHide = false;
                    classicForce = true;
                } else if (item.type == ItemType<CloudWaistbelt>()) {
                    cloudHide = false;
                    cloudForce = true;
                } else if (item.type == ItemType<SandyBangles>()) {
                    sandHide = false;
                    sandForce = true;
                }
                //Update vanity is fucking broken so they are detected here
                else if (item.type == ItemType<Items.Equips.Backs.PrismShell>()) {
                    prismshell = true;
                } else if (item.type == ItemType<Items.Equips.ExodiumMoon>()) {
                    exorb = true;
                } else if (item.type == ItemType<ArtemisBalloonSmall>()) {
                    sartballoon = true;
                } else if (item.type == ItemType<ApolloBalloonSmall>()) {
                    sapballoon = true;
                } else if (item.type == ItemType<ApolloBalloon>())
                {
                    apballoon = true;
                    if (Player.ownedProjectileCounts[ProjectileType<ApolloBalloonProj>()] <= 0 && Player.whoAmI == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Player.GetSource_Accessory(item), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                            0, 0, ProjectileType<ApolloBalloonProj>(), 0, 0f, Player.whoAmI);
                    }
                }
                else if (item.type == ItemType<ArtemisBalloon>()) 
                {
                    artballoon = true;
                    if (Player.ownedProjectileCounts[ProjectileType<ArtemisBalloonProj>()] <= 0 && Player.whoAmI == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Player.GetSource_Accessory(item), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                            0, 0, ProjectileType<ArtemisBalloonProj>(), 0, 0f, Player.whoAmI);
                    }
                } else if (item.type == ItemType<ExoTwinsBalloon>())
                {
                    artballoon = true;
                    if (Player.ownedProjectileCounts[ProjectileType<ArtemisBalloonProj>()] <= 0 && Player.whoAmI == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Player.GetSource_Accessory(item), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                            0, 0, ProjectileType<ArtemisBalloonProj>(), 0, 0f, Player.whoAmI);
                    }
                    apballoon = true;
                    if (Player.ownedProjectileCounts[ProjectileType<ApolloBalloonProj>()] <= 0 && Player.whoAmI == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Player.GetSource_Accessory(item), Player.position.X + Player.width / 2, Player.position.Y + Player.height / 2,
                            0, 0, ProjectileType<ApolloBalloonProj>(), 0, 0f, Player.whoAmI);
                    }
                } 
                else if (item.type == ItemType<WulfrumHelipack>()) {
                    helipack = true;
                }
            }
        }
        public override void FrameEffects() {

            string classString = "Typeless";
            if (Player.HeldItem.DamageType.CountsAsClass(DamageClass.Melee))
            {
                classString = "Melee";
            }
            else if (Player.HeldItem.DamageType.CountsAsClass(DamageClass.Magic))
            {
                classString = "Magic";
            }
            else if (Player.HeldItem.DamageType.CountsAsClass(DamageClass.Summon))
            {
                classString = "Summoner";
            }
            else if (Player.HeldItem.DamageType.CountsAsClass(DamageClass.Throwing))
            {
                classString = "Rogue";
            }
            else if (Player.HeldItem.DamageType.CountsAsClass(DamageClass.Ranged))
            {
                classString = "Ranged";
            }

            if ((maryTrans || maryForce) && !maryHide)
                Player.legs = EquipLoader.GetEquipSlot(Mod, "BloodyMaryDress", EquipType.Legs);
            if ((profanedCultist || profanedCultistForce) && !profanedCultistHide)
                Player.legs = EquipLoader.GetEquipSlot(Mod, "ProfanedCultistRobes", EquipType.Legs);
            if ((perennial || perennialForce) && !perennialHide)
                Player.legs = EquipLoader.GetEquipSlot(Mod, "PerennialDress", EquipType.Legs);
            if ((bellaCloak || bellaCloakForce) && !bellaCloakHide)
                Player.legs = EquipLoader.GetEquipSlot(Mod, "BelladonnaCloak", EquipType.Legs);

            if ((signutTrans || signutForce) && !signutHide) {
                Player.head = EquipLoader.GetEquipSlot(Mod, "Signus", EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, "Signus", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "Signus", EquipType.Legs);
            } else if ((androTrans || androForce) && !androHide) {
                Player.head = EquipLoader.GetEquipSlot(Mod, "ProtoRing", EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, "ProtoRing", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "ProtoRing", EquipType.Legs);
            } else if ((classicTrans || classicForce) && !classicHide) {
                Player.head = EquipLoader.GetEquipSlot(Mod, "BurningEye", EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, "BurningEye", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "BurningEye", EquipType.Legs);
            } else if ((cloudTrans || cloudForce) && !cloudHide) {
                Player.head = EquipLoader.GetEquipSlot(Mod, "CloudWaistbelt", EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, "CloudWaistbelt", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "CloudWaistbelt", EquipType.Legs);
            } else if ((sandTrans || sandForce) && !sandHide) {
                Player.head = EquipLoader.GetEquipSlot(Mod, "SandyBangles", EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, "SandyBangles", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "SandyBangles", EquipType.Legs);
            }
            if (arsenalHelmet)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "DraedonHelmet" + classString, EquipType.Head);
            }
            if (arsenalChestplate)
            {
                Player.body = EquipLoader.GetEquipSlot(Mod, "DraedonChestplate" + classString, EquipType.Body);
            }
            if (cassette) {
                Player.armorEffectDrawShadow = true;
                Player.armorEffectDrawOutlines = true;
            }
        }
        public override void UpdateVisibleAccessories() {
            if (signutTrans)
                Player.AddBuff(BuffType<SignutTransformationBuff>(), 60, true);
            else if (androTrans)
                Player.AddBuff(BuffType<ProtoRingBuff>(), 60, true);
            else if (classicTrans)
                Player.AddBuff(BuffType<ClassicBrimmyBuff>(), 60, true);
            else if (cloudTrans)
                Player.AddBuff(BuffType<CloudTransformationBuff>(), 60, true);
            else if (sandTrans)
                Player.AddBuff(BuffType<SandTransformationBuff>(), 60, true);
        }

        public override void PreUpdateMovement() {
            /*if (pongactive)
            {
                Player.releaseHook = true;
                Player.releaseMount = true;
                Player.velocity.X = 0;
                if (Player.velocity.Y < 0)
                {
                    Player.velocity.Y = 0;
                }
            }*/
            if (!pongactive) {
                pongstage = 0;
                pongoutcome = 0;
            }
            /*if (boiactive)
            {
                Player.releaseHook = true;
                Player.releaseMount = true;
                Player.velocity.X = 0;
                if (Player.velocity.Y < 0)
                {
                    Player.velocity.Y = 0;
                }
            }*/
            if (!boiactive) {
                //boistage = 0;
                boihealth = 3;
                boienemy1 = false;
                boienemy2 = false;
                boienemy3 = false;
            }
        }

        public override void PostUpdateBuffs() {
            if (!Player.HasBuff(BuffType<MorshuBuff>())) {
                morshuTimer = 0;
            }
        }

        public override void UpdateDead() {
            ResetMyStuff();
            CalamityBabyGotHit = false;
            SCalHits = 0;
            morshuTimer = 0;
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override void PreUpdate() {
            //Custom player draw frame counters
            int coneflame = 6;
            if (conecounter >= 5) {
                conecounter = -1;
                coneframe = coneframe == coneflame - 1 ? 0 : coneframe + 1;
            }
            conecounter++;

            int twinflame = 5;
            if (twincounter >= 12) {
                twincounter = -1;
                twinframe = twinframe == twinflame - 1 ? 0 : twinframe + 1;
            }
            twincounter++;

            int stwinflame = 12;
            if (stwincounter >= 12) {
                stwincounter = -1;
                stwinframe = stwinframe == stwinflame - 1 ? 0 : stwinframe + 1;
            }
            stwincounter++;

            int wheelspeed = silvajeep ? 2 : 1;

            if (Main.LocalPlayer.velocity.X > 0) {
                bcarriagewheel += wheelspeed;
            } else if (Main.LocalPlayer.velocity.X < 0) {
                bcarriagewheel -= wheelspeed;
            }
            if (bossded > 0) {
                bossded--;
            }
            rotcounter += Math.PI / 80;
            rotdeg = Math.Cos(rotcounter);
            rotsin = -Math.Sin(rotcounter);

            if (wulfrumjam && Main.rand.NextBool(2)&& CalValEX.CalamityActive) {
                /*Vector2 smokeOffset = new Vector2(22 * Player.direction, 6);
                CalamityMod.Particles.Particle smoke = new CalamityMod.Particles.SmallSmokeParticle(Player.Center - smokeOffset, Vector2.Zero, Color.GreenYellow, new Color(40, 40, 40), Main.rand.NextFloat(0.4f, 0.8f), 145 - Main.rand.Next(50));
                smoke.Velocity = (smoke.Position - Player.Center) * 0.3f + Player.velocity;
                CalamityMod.Particles.GeneralParticleHandler.SpawnParticle(smoke);*/
            }
        }
        public override void PostUpdate()
        {
            bool flag21 = Player.onTrack;
            if (Player.mount.Type == MountType<ProfanedCycle>())
            {
                Player.fartKartCloudDelay = Math.Max(0, Player.fartKartCloudDelay - 1);
                float num18 = ((Player.ignoreWater || Player.merman) ? 1f : (Player.shimmerWet ? 0.25f : (Player.honeyWet ? 0.25f : ((!Player.wet) ? 1f : 0.5f))));
                Player.velocity *= num18;
                BitsByte bitsByte = Minecart.TrackCollision(Player, ref Player.position, ref Player.velocity, ref Player.lastBoost, Player.width, Player.height, Player.controlDown, Player.controlUp, Player.fallStart2, trackOnly: false, Player.mount.Delegations);
                if (bitsByte[0])
                {
                    Player.onTrack = true;
                    if (CalValEX.CalamityActive)
                        SpawnGrindSparks();
                }
                if (flag21 && !Player.onTrack)
                {
                    Player.mount.Delegations.MinecartJumpingSound(Player, Player.position, Player.width, Player.height);
                }
                if (bitsByte[1])
                {
                    if (Player.controlLeft || Player.controlRight)
                    {
                        if (Player.cartFlip)
                        {
                            Player.cartFlip = false;
                        }
                        else
                        {
                            Player.cartFlip = true;
                        }
                    }
                    if (Player.velocity.X > 0f)
                    {
                        Player.direction = 1;
                    }
                    else if (Player.velocity.X < 0f)
                    {
                        Player.direction = -1;
                    }
                    Player.mount.Delegations.MinecartBumperSound(Player, Player.position, Player.width, Player.height);
                }
                Player.velocity /= num18;
                if (bitsByte[2])
                {
                    Player.cartRampTime = (int)(Math.Abs(Player.velocity.X) / Player.mount.RunSpeed * 20f);
                }
                if (bitsByte[4])
                {
                    Player.trackBoost -= 4f;
                }
                if (bitsByte[5])
                {
                    Player.trackBoost += 4f;
                }
                /*int cordX = (int)(Player.Bottom.X / 16);
                int cordY = (int)(Player.Bottom.Y / 16);
                Tile t = Main.tile[cordX, cordY - 1];
                Dust d = Dust.NewDustPerfect(Player.Bottom - Vector2.UnitY * 16, DustID.GemRuby);
                d.velocity = Vector2.Zero;
                d.noGravity = true;
                if ((t.TileType == TileID.MinecartTrack || Main.tile[cordX, cordY + 1].TileType == TileID.MinecartTrack) && !Player.controlDown)
                {
                    Minecart.TrackCollision(Player, ref Player.position, ref Player.velocity, ref Player.lastBoost, Player.width, Player.height, Player.controlDown, Player.controlUp, Player.fallStart2, false, Player.mount.Delegations);
                    //Player.position.Y -= 0.5f;
                    Player.cartRampTime = (int)(Math.Abs(Player.velocity.X) / Player.mount.RunSpeed * 20f);
                    Vector2 impactPoint = Player.Bottom + Vector2.UnitX * 8 * -Player.direction;
                    Vector2 bloodSpawnPosition = Player.Bottom + Main.rand.NextVector2Circular(16, 16) * 0.04f;
                    Vector2 splatterDirection = (new Vector2(bloodSpawnPosition.X * -Player.direction, bloodSpawnPosition.Y)).SafeNormalize(Vector2.UnitY);
                    for (int i = 0; i < 2; i++)
                    {
                        int sparkLifetime = Main.rand.Next(2, 11);
                        float sparkScale = Main.rand.NextFloat(0.8f, 1f);
                        Color sparkColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.7f));
                        sparkColor = Color.Lerp(sparkColor, Color.Orange, Main.rand.NextFloat());

                        if (Main.rand.NextBool(10))
                            sparkScale *= 2f;

                        Vector2 sparkVelocity = splatterDirection.RotatedByRandom(0.6f) * Main.rand.NextFloat(12f, 25f);
                        sparkVelocity.Y -= 6f;
                        SparkParticle spark = new SparkParticle(impactPoint, sparkVelocity, true, sparkLifetime, sparkScale, sparkColor);
                        GeneralParticleHandler.SpawnParticle(spark);
                    }
                    bikeShred = true;
                }
                if ((Main.tile[cordX, cordY].TileType == TileID.MinecartTrack && Main.tile[cordX, cordY].TileFrameX > 5) || (Main.tile[cordX, cordY].TileType == TileID.MinecartTrack && Main.tile[cordX, cordY].TileFrameX > 5))
                {
                   /// Main.NewText(Main.tile[cordX, cordY].TileFrameX);
                   // Player.position.Y -= 16f;
                }*/
            }
            if (Player.mount.Type == MountType<YhogoStickMount>())
            {

                if (Player.velocity.Y == 0f)
                {
                    Player.isPerformingPogostickTricks = false;
                }
                if (Player.isPerformingPogostickTricks)
                {
                    Player.fullRotation += (float)Player.direction * ((float)Math.PI * 2f) / 30f;
                }
                else
                {
                    Player.fullRotation = (float)Math.Sign(Player.velocity.X) * Utils.GetLerpValue(0f, Player.mount.RunSpeed - 0.2f, Math.Abs(Player.velocity.X), clamped: true) * 0.4f;
                }
                Player.fullRotationOrigin = new Vector2(Player.width / 2, (float)Player.height * 0.8f);
            }
            if (Player.mount.Type == MountType<YharonMount>())
            {
                if (Math.Abs(Player.velocity.X) >= 13)
                {
                    Player.fullRotation = Utils.AngleLerp(Player.fullRotation, Player.velocity.ToRotation() + (Player.direction == -1 ? MathHelper.Pi : 0), 0.04f);
                    Player.fullRotationOrigin = new Vector2(Player.width / 2, (float)Player.height * 0.8f);
                }
                else
                {
                    Player.fullRotation = Utils.AngleLerp(Player.fullRotation, 0, 0.04f);
                    Player.fullRotationOrigin = new Vector2(Player.width / 2, (float)Player.height * 0.8f);
                }
            }
        }
        [JITWhenModsEnabled("CalamityMod")]

        public void SpawnGrindSparks()
        {

            Vector2 impactPoint = Player.Bottom + Vector2.UnitX * 8 * -Player.direction;
            Vector2 bloodSpawnPosition = Player.Bottom + Main.rand.NextVector2Circular(16, 16) * 0.04f;
            Vector2 splatterDirection = (new Vector2(bloodSpawnPosition.X * -Player.direction, bloodSpawnPosition.Y)).SafeNormalize(Vector2.UnitY);
            for (int i = 0; i < 2; i++)
            {
                int sparkLifetime = Main.rand.Next(2, 11);
                float sparkScale = Main.rand.NextFloat(0.8f, 1f);
                Color sparkColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.7f));
                sparkColor = Color.Lerp(sparkColor, Color.Orange, Main.rand.NextFloat());

                if (Main.rand.NextBool(10))
                    sparkScale *= 2f;

                Vector2 sparkVelocity = splatterDirection.RotatedByRandom(0.6f) * Main.rand.NextFloat(12f, 25f);
                sparkVelocity.Y -= 10f;
                SparkParticle spark = new(impactPoint, sparkVelocity, true, sparkLifetime, sparkScale, sparkColor);
                GeneralParticleHandler.SpawnParticle(spark);
            }
        }

        private void ResetMyStuff() {
            mBirb = false;
            mBirb2 = false;
            mDoge = false;
            mAero = false;
            mSkater = false;
            mShark = false;
            mFolly = false;
            mPerf = false;
            mHive = false;
            mPhan = false;
            mChan = false;
            mNaked = false;
            mArmored = false;
            eidolist = false;
            excal = false;
            aero = false;
            mRav = false;
            tub = false;
            andro = false;
            seerS = false;
            seerM = false;
            seerL = false;
            mImp = false;
            George = false;
            rPanda = false;
            catfish = false;
            cr = false;
            eb = false;
            mSlime = false;
            fog = false;
            mDebris = false;
            mHeat = false;
            mHeat2 = false;
            dBall = false;
            mClam = false;
            mAme = false;
            mSap = false;
            mEme = false;
            mTop = false;
            mRub = false;
            mDia = false;
            mCry = false;
            mAmb = false;
            sBun = false;
            uSerpent = false;
            GeorgeII = false;
            junsi = false;
            SignusMini = false;
            MiniCryo = false;
            SmolCrab = false;
            VoidOrb = false;
            AstPhage = false;
            StarJelly = false;
            ProGuard1 = false;
            ProGuard2 = false;
            ProGuard3 = false;
            ProviPet = false;
            Dstone = false;
            EWyrm = false;
            PBGmini = false;
            BoldLizard = false;
            Nugget = false;
            Enredpet = false;
            sandmini = false;
            raresandmini = false;
            rarebrimling = false;
            babywaterclone = false;
            cloudmini = false;
            Skeetyeet = false;
            Angrypup = false;
            Cryokid = false;
            TerminalRock = false;
            BabyCnidrion = false;
            sVoid = false;
            sSignus = false;
            sWeeb = false;
            euros = false;
            hDoge = false;
            bDoge = false;
            SWPet = false;
            jared = false;
            asPet = false;
            dsPet = false;
            mDuke = false;
            sirember = false;
            deusmain = false;
            deussmall = false;
            roverd = false;
            rusty = false;
            sepet = false;
            pylon = false;
            worb = false;
            rover = false;
            drone = false;
            hover = false;
            RepairBot = false;
            MechaGeorge = false;
            CalamityBABYBool = false;
            Lightshield = false;
            sDuke = false;
            squid = false;
            voidling = false;
            mScourge = false;
            darksunSpirits = false;
            goozmaPet = false;
            voreworm = false;
            moistPet = false;
            Chihuahua = false;
            strongWeeb = false;
            ySquid = false;
            oSquid = false;
            feel = false;
            amogus = false;
            buppy = false;
            BMonster = false;
            hage = false;
            Blok = false;
            aesthetic = false;
            exorb = false;
            poltermask = false;
            polterchest = false;
            polterthigh = false;
            rockhat = false;
            prismshell = false;
            rainbow = false;
            SupJ = false;
            bSignut = false;
            bSlime = false;
            buffboi = false;
            smaul = false;
            shart = false;
            morshu = false;
            morshugun = false;
            scaldown = false;
            avalon = false;
            wulfrumjam = false;
            conejo = false;
            cassette = false;
            specan = false;
            carriage = false;
            silvajeep = false;
            yharcar = false;
            sepneo = false;
            twinballoon = false;
            artballoon = false;
            apballoon = false;
            sapballoon = false;
            sartballoon = false;
            /*pongactive = false;
            pongoutcome = 0;
            pongstage = 0;*/
            //Boi stuff
            boiactive = false;
            boiatlantis = false;
            boienemy1 = false;
            boienemy2 = false;
            boienemy3 = false;
            boistage = 0;
            boihealth = 3;
            soupench = false;
            aresarms = false;
            lumpe = false;
            ares = false;
            thanos = false;
            twins = false;
            // Æ: Drae's bools but false !!
            digger = false;
            BestInst = false;
            DustChime = false;
            NurseryBell = false;
            AlarmClock = false;
            MaladyBells = false;
            Harbinger = false;
            SpiritDiner = false;
            AltarBell = false;
            WormBell = false;
            Vaseline = false;
            ScratchedGong = false;
            TubRune = false;
            Pandora = false;
            zygote = false;
            brimberry = false;
            helipack = false;
            CalValPat = false;
            bikeShred = false;
            ogscule = false;
            nautilus = false;
        }

        public override void OnHurt(Player.HurtInfo info) {
            DoCalamityBabyThings(info.Damage);
            if (signutTrans) {
                Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit49, Player.position);
                for (int x = 0; x < 15; x++) {
                    Dust dust;
                    Vector2 position = Main.LocalPlayer.Center;
                    dust = Main.dust[Dust.NewDust(Player.Center, 26, 15, DustID.SpookyWood, 0f, 0f, 147, new Color(255, 255, 255), 0.9868422f)];
                }
            }

            if (classicTrans || cloudTrans || sandTrans)
                Terraria.Audio.SoundEngine.PlaySound(SoundID.FemaleHit, Player.position);
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers) {
            if (signutTrans) modifiers.DisableSound();
            if (cloudTrans) modifiers.DisableSound();
            if (classicTrans) modifiers.DisableSound();
            if (sandTrans) modifiers.DisableSound();
            return;

        }
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo) {
            DoCalamityBabyThings(hurtInfo.Damage);
            if (npc.type == CalamityID.CalNPCID.SupremeCalamitas && Player.immuneTime <= 0)
            {
                SCalHits++;
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo) {
            DoCalamityBabyThings(hurtInfo.Damage);

            for (int i = 0; i < Main.maxNPCs; i++)
                if (Main.npc[i].active &&
                    Main.npc[i].type == CalamityID.CalNPCID.SupremeCalamitas) 
                    {
                    if (Player.immuneTime <= 0) 
                    {
                        SCalHits++;
                    }
                }
        }

        private void DoCalamityBabyThings(int damage) {
            if (CalamityBABYBool) {
                if (damage > 0) {
                    if (Player.whoAmI == Main.myPlayer) {
                        if (Player.name == "Kawaggy") {
                            string humiliationText = "";
                            switch (Main.rand.Next(0, 5)) {
                                case 0:
                                    humiliationText = "Why didn't you code me earlier?";
                                    break;

                                case 1:
                                    humiliationText = "Not worthy.";
                                    break;

                                case 2:
                                    humiliationText = "It's been months.";
                                    break;

                                case 3:
                                    humiliationText = "You will not be forgiven.";
                                    break;

                                case 4:
                                    humiliationText = "I hope you learn.";
                                    break;
                            }

                            CombatText.NewText(Player.getRect(), Color.White, humiliationText);
                            Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " is not worthy."), 99999999,
                                -1);
                        }

                        if (Main.rand.NextFloat() < 0.08f) {
                            if (CalamityBABY.nameList.Contains(Player.name)) {
                                Player.AddBuff(BuffID.Lovestruck, Main.rand.Next(600, 3601));
                            } else if (Player.HasBuff(BuffID.BabySlime) ||
                                       Main.LocalPlayer.HasItem(ItemID.LandMine)) {

                            } else {
                                CalamityBabyGotHit = true;
                            }
                        }
                    }
                }
            }
        }
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition) {
            if (Player.ZoneBeach && Main.rand.NextFloat() < 0.021f) {
                itemDrop = ItemType<Items.Pets.Elementals.StrangeMusicNote>();
            }
            if (ZoneAstral && Main.rand.NextFloat() < 0.0105f) {
                itemDrop = ItemType<Items.Tiles.Plants.AstralOldPurple>();
            }
            if (ZoneAstral && Main.rand.NextFloat() < 0.0105f) {
                itemDrop = ItemType<Items.Tiles.Plants.AstralOldYellow>();
            }
            if (CalValEX.CalamityActive)
            if ((bool)CalValEX.Calamity.Call("GetInZone", Player, "sunkensea") && Main.hardMode && Main.rand.NextFloat() < 0.021f) {
                itemDrop = ItemType<Items.Tiles.SailfishTrophy>();
            }
        }

        public override void PostUpdateMiscEffects() {
            bool bossIsAlive2 = false;
            for (int i = 0; i < Main.maxNPCs; i++) {
                NPC npc = Main.npc[i];
                if (npc.active && npc.boss) {
                    bossIsAlive2 = true;
                }
            }
            if (!bossIsAlive2 && CalValEX.CalamityActive) 
            {
                bool TerminalMonolith = CalValEXWorld.RockshrinEX;
                if (CalValEXWorld.RockshrinEX) 
                {
                    /*
                    CalamityMod.Skies.BossRushSky.ShouldDrawRegularly = true;
                    Player.ManageSpecialBiomeVisuals("CalamityMod:BossRush", TerminalMonolith, Player.Center);*/
                }
            }
        }

        public override void CopyClientState(ModPlayer clientClone)/* tModPorter Suggestion: Replace Item.Clone usages with Item.CopyNetStateTo */ {
            CalValEXPlayer clone = clientClone as CalValEXPlayer;
            clone.SCalHits = SCalHits;
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)CalValEX.MessageType.SyncCalValEXPlayer);
            packet.Write((byte)Player.whoAmI);
            packet.Write(SCalHits);
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer) {
            CalValEXPlayer clone = clientPlayer as CalValEXPlayer;
            if (clone.SCalHits != SCalHits) {
                var packet = Mod.GetPacket();
                packet.Write((byte)CalValEX.MessageType.SyncSCalHits);
                packet.Write((byte)Player.whoAmI);
                packet.Write(SCalHits);
                packet.Send();
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["Ohio"] = hasOhiod;
        }

        public override void LoadData(TagCompound tag)
        {
            hasOhiod = tag.GetBool("Ohio");
        }

        /*public static readonly PlayerLayer Mimigun = new PlayerLayer("CalValEX", "Mimigun", PlayerLayer.Head, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("CalValEX");
            CalValEXPlayer modPlayer = drawPlayer.GetModPlayer<CalValEXPlayer>();
            if (modPlayer.morshugun)
            {
                int gnuflip = 66 * -drawPlayer.direction;
                Texture2D texture = mod.GetTexture("Items/Mounts/Morshu/Minigun");
                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X - gnuflip);
                int drawY = (int)(drawInfo.position.Y + drawPlayer.height - Main.screenPosition.Y + 56);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height), 1f, drawPlayer.direction != -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                Main.playerDrawData.Add(data);
            }
        });

        public static readonly PlayerLayer Mimigun2 = new PlayerLayer("CalValEX", "Mimigun2", PlayerLayer.Arms, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
            {
                return;
            }
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("CalValEX");
            CalValEXPlayer modPlayer = drawPlayer.GetModPlayer<CalValEXPlayer>();
            if (modPlayer.morshugun)
            {
                int gnuflip = 36 * -drawPlayer.direction;
                Texture2D texture = mod.GetTexture("Items/Mounts/Morshu/Minigun");
                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X - gnuflip);
                int drawY = (int)(drawInfo.position.Y + drawPlayer.height - Main.screenPosition.Y + 56);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y - 4f - texture.Height / 2f) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height), 1f, drawPlayer.direction != -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                Main.playerDrawData.Add(data);
            }
        });*/

        /*public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            DraedonSet.Visible = true;
            int headLayer = layers.FindIndex(l => l == PlayerLayer.Head);
            int bodyLayer = layers.FindIndex(l => l == PlayerLayer.Body);

            if (headLayer > -1)
            {
                layers.Insert(headLayer + 1, DraedonSet.head);
            }

            if (bodyLayer > -1)
            {
                DraedonSet.Visible = true;layers.Insert(bodyLayer + 1, DraedonSet.body);
            }
        }

        public override void ModifyDrawHeadLayers(List<PlayerHeadLayer> layers)
        {
            int headLayer = layers.FindIndex(l => l == PlayerHeadLayer.Armor);

            if (headLayer > -1)
            {
                layers.Insert(headLayer + 1, DraedonSet.map);
            }
        }

        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo) //i just really dont want to fix the sprite issues.
        {
            if (drawInfo.drawPlayer.legs == EquipLoader.GetEquipSlot(Mod, "DraedonLeggings", EquipType.Legs))
            {
                drawInfo.legColor = Color.Transparent;
                drawInfo.legGlowMaskColor = Color.Transparent;
                drawInfo.pantsColor = Color.Transparent;
            }

            if (drawInfo.drawPlayer.body == EquipLoader.GetEquipSlot(Mod, "DraedonChestplate", EquipType.Body))
            {
                drawInfo.armGlowMaskColor = Color.Transparent;
                drawInfo.bodyColor = Color.Transparent;
                drawInfo.bodyGlowMaskColor = Color.Transparent;
                drawInfo.shirtColor = Color.Transparent;
                drawInfo.underShirtColor = Color.Transparent;
            }

            if (drawInfo.drawPlayer.head == EquipLoader.GetEquipSlot(Mod, "DraedonHelmet", EquipType.Head))
            {
                drawInfo.eyeColor = Color.Transparent;
                drawInfo.eyeWhiteColor = Color.Transparent;
                drawInfo.faceColor = Color.Transparent;
                drawInfo.hairColor = Color.Transparent;
                drawInfo.headGlowMaskColor = Color.Transparent;
            }
        }*/
    }
}
