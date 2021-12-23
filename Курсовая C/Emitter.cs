using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Курсовая_C.Particle;

namespace Курсовая_C
{
    class Emitter
    {
        public float GravitationX = 0;
        public float GravitationY = 1;
        List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;
        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();
        public int ParticlesCount = 500;


        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public int SpeedMin = 1; // начальная минимальная скорость движения частицы
        public int SpeedMax = 10; // начальная максимальная скорость движения частицы
        public int RadiusMin = 2; // минимальный радиус частицы
        public int RadiusMax = 10; // максимальный радиус частицы
        public int LifeMin = 20;
        public int LifeMax = 100;

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public int ParticlesPerTick = 1;
        public bool check;
       

        public void UpdateState()
        {
            int particlesToCreate = ParticlesPerTick;


            foreach (var particle in particles)
            {
                particle.check = this.check;

                if (particle.Life <= 0)
                {
                    ResetParticle(particle);
                    if (particlesToCreate > 0)
                    {
                        /* у нас как сброс частицы равносилен созданию частицы */
                        particlesToCreate -= 1; // поэтому уменьшаем счётчик созданных частиц на 1
                        ResetParticle(particle);
                    }
                }
                else
                {

                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                    particle.Life -= 1;
                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle);
                    }
                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;

                }
            }

            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle();
                ResetParticle(particle);
                particles.Add(particle);
            }

        }
        public void Render(Graphics g)
        {
            // утащили сюда отрисовку частиц
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
            foreach (var point in impactPoints) // тут теперь  impactPoints
            {
               
                point.Render(g); // это добавили
            }
        }
        public virtual void ResetParticle(Particle particle)
        {
            if (particle is ParticleColorful)// Mеням цвет обратно на исходный
            {
                var p = (particle as ParticleColorful);
                (particle as ParticleColorful).FromColor = ColorFrom;
                p.ToColor = ColorTo;
            }

            particle.Life = Particle.rand.Next(LifeMin, LifeMax);

            particle.X = X;
            particle.Y = Y;

            var direction = Direction
                + (double)Particle.rand.Next(Spreading)
                - Spreading / 2;

            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);

        }
        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.FromColor = ColorFrom;
            particle.ToColor = ColorTo;

            return particle;
        }

        public class TopEmitter : Emitter
        {
            public int Width;
            public override void ResetParticle(Particle particle)
            {
                base.ResetParticle(particle); // вызываем базовый сброс частицы, там жизнь переопределяется и все такое

                // а теперь тут уже подкручиваем параметры движения
                particle.X = Particle.rand.Next(Width); // позиция X -- произвольная точка от 0 до Width
                particle.Y = 0;  // ноль -- это верх экрана 

                particle.SpeedY = 1; // падаем вниз по умолчанию
                particle.SpeedX = Particle.rand.Next(-2, 2); // разброс влево и вправа у частиц
            }
        
        }
    }
}
