using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Remnants.Projectiles.Enemy
{
    public class fireball : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BallofFire;
        public override string GlowTexture => "Terraria/Images/Projectile_" + ProjectileID.BallofFire;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fireball");
            //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BallofFire);
            Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = true;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
        }

        public override void AI()
        {
            Projectile.rotation += MathHelper.PiOver4;

            int dustIndex;
            if (Main.rand.NextBool(10))
            {
                dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Scale: Main.rand.NextFloat(1, 2));
                Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(2, 2);
            }
            for (int i = 0; i < 3; i++)
            {
                dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Scale: Main.rand.NextFloat(1, 2));
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(2, 2);
            }

            if (Projectile.wet && !Projectile.lavaWet)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 10 * 60);
            Projectile.Kill();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 10 * 60);
            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Scale: Main.rand.NextFloat(1, 2));
                Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(4, 4);
            }
            for (int i = 0; i < 30; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Scale: Main.rand.NextFloat(1, 2));
                Main.dust[dustIndex].velocity = Main.rand.NextVector2Circular(4, 4);
            }

            for (int i = 0; i < 5; i++)
            {
                int projectile = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Main.rand.NextVector2Circular(4, 4), ModContent.ProjectileType<burn>(), 20, 0f, Projectile.owner);
                Main.projectile[projectile].friendly = false;
                Main.projectile[projectile].hostile = true;
            }

            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
        }
    }
}