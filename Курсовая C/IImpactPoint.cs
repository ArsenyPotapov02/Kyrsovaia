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


                angle = (float)(Math.Cos((gX * nX + gY * nY) / (Math.Sqrt(gX * gX + nX * nX) + Math.Sqrt(gY * gY + nY * nY))))+270;
                if (normalVectLenght + particle.Radius < 130 / 2)
                {
                    if (particle is ParticleColorful)// Если частица разноцветная 
                    {
                        var p = (particle as ParticleColorful);
                        var m = new Matrix();
                        m.Rotate(angle);// Через матрицу трансформации поворацчиваем на угол

                        var points = new[] { new PointF(gX, gY), new PointF(p.SpeedX, p.SpeedY) };
                        m.TransformPoints(points);

                        p.X = X - gX;// Считаем новые координаты вылета частиц
                        p.Y = Y- gY;
                        //p.SpeedX = points[1].X ;// Считаем новый вектор частиц
                        //p.SpeedY = points[1].Y ;// Считаем новый вектор частиц

                         p.SpeedX = (gX - 2 * (gX * nX) * nX);
                         p.SpeedY = (gY - 2 * (gY * nY) * nY);

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
                g.DrawString(
                   $"{(int)angle}",
                   new Font("Verdana", 10),
                   new SolidBrush(Color.White),
                    X,
                    Y
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
