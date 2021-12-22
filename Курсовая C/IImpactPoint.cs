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
         

            // а сюда по сути скопировали с минимальными правками то что было в UpdateState
            public override void ImpactParticle(Particle particle)
            {
                float gX = X - particle.X;
                float gY = Y - particle.Y;
                float nX = gX - X;
                float nY = gY - Y;
                float angle;

                double r = Math.Sqrt(gX * gX + gY * gY);

                angle = (float)(Math.Acos((gX * nX + gY * nY) / (Math.Sqrt(gX * gX + nX * nX) + Math.Sqrt(gY * gY + nY * nY))));
                if (r + particle.Radius < 130 / 2)
                {
                    if (particle is ParticleColorful)// Если частица разноцветная 
                    {
                        var p = (particle as ParticleColorful);
                        var m = new Matrix();
                        m.Rotate(angle);// Через матрицу трансформации поворацчиваем на угол

                        var points = new[] { new PointF(gX, gY), new PointF(p.SpeedX, p.SpeedY) };
                        m.TransformPoints(points);

                       // p.X = gX  ;// Считаем новые координаты вылета частиц
                      //  p.Y = gY  ;// Считаем новые координаты вылета частиц
                       // p.SpeedX = points[1].X;// Считаем новый вектор частиц
                        //p.SpeedY = points[1].Y;// Считаем новый вектор частиц

                         p.SpeedX = (gX - 2 * (gX * nX) * nX)/1000000000;
                         p.SpeedY = (gY - 2 * (gY * nY) * nY)/1000000000;

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
