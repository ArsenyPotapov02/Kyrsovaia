using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Курсовая_C.Particle;

namespace Курсовая_C
{
    abstract class IImpactPoint
    {
        public float X;
        public float Y;

        public abstract void ImpactParticle(Particle particle);
        // абстрактный метод с помощью которого будем изменять состояние частиц
       
        public  virtual void Render(Graphics g)
        {
            g.FillEllipse(
                    new SolidBrush(Color.Red),
                    X - 5,
                    Y - 5,
                    10,
                    10
                );
        }
        public class ReboundPoint : IImpactPoint
        {
         

            // а сюда по сути скопировали с минимальными правками то что было в UpdateState
            public override void ImpactParticle(Particle particle)
            {
                float gX = X - particle.X;
                float gY = Y - particle.Y;
                float nX = gX - X;
                float nY = gY - Y;

                double r = Math.Sqrt(gX * gX + gY * gY);
                if (r + particle.Radius < 80 / 2)
                {
                    particle.SpeedX = gX - 2 * (gX * nX) * nX;
                    particle.SpeedY = gY - 2 * (gY * nY) * nY;

                }

            }
            public override void Render(Graphics g)
            {
                g.DrawEllipse(
                    new Pen(Color.Purple),
                    X - 40,
                    Y - 40,
                    80,
                    80
                );
            }
        }
       
        public class PaintPoint : IImpactPoint // Точка перекрашивания
        {
            public Color color;
            public override void ImpactParticle(Particle particle)
            {

                float gX = X - particle.X;
                float gY = Y - particle.Y;

                double r = Math.Sqrt(gX * gX + gY * gY);
                if (r + particle.Radius < 80 / 2)
                {
                    if (particle is ParticleColorful)// Если частица разноцветная 
                    {
                        var p = (particle as ParticleColorful);
                        p.FromColor = color;// Меняем её цвет
                        p.ToColor = color;// Меняем её цвет
                    }

                }

            }

            public override void Render(Graphics g)
            {
                g.DrawEllipse(
                    new Pen(color),
                    X - 40,
                    Y - 40,
                    80,
                    80
                );
            }
        }
    }
}
