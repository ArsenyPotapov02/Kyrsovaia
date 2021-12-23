using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            public float angle = 0;
         

           
            public override void ImpactParticle(Particle particle)
            {
                float gX = X - particle.X;
                float gY = Y - particle.Y;
                
                float invLenght;
                
                float normalVectLenght = (float)Math.Sqrt(gX * gX + gY * gY);
                float nX = gX / normalVectLenght;
                float nY = gY / normalVectLenght;


                angle = (float)(Math.Cos((gX * nX + gY * nY) / (Math.Sqrt(gX * gX + nX * nX) + Math.Sqrt(gY * gY + nY * nY))));
                if (normalVectLenght  < 40 )
                {
                    if (particle is ParticleColorful)// Если частица разноцветная 
                    {
                        var p = (particle as ParticleColorful);
                        float dot = p.SpeedX * nX + p.SpeedY * nY;
                        p.SpeedX = p.SpeedX - 2 *dot *nX;
                        p.SpeedY = p.SpeedY - 2 *dot * nY;

                        p.X += p.SpeedX; 
                        p.Y += p.SpeedY;

                    }

                }

            }
            public override void Render(Graphics g)
            {
                g.DrawEllipse(
                    new Pen(Color.Lime),
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
                if (r + particle.Radius < 130 / 2)
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
