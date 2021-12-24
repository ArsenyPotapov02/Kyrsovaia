using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Курсовая_C
{
    class Particle
    {
        public int Radius; // радиус частицы
        public float X; // X координата положения частицы в пространстве
        public float Y; // Y координата положения частицы в пространстве

        public float SpeedX; // скорость перемещения по оси X
        public float SpeedY; // скорость перемещения по оси Y
        public float Life; // запас здоровья частицы

        public static Random rand = new Random();
        public bool check;
        
        public Particle()
        {
            var direction = (double)rand.Next(360);
            var speed = 1 + rand.Next(10);

            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            Radius = 2 + rand.Next(10);
            Life = 20 + rand.Next(100);
        }
        public virtual void Draw (Graphics g)
        {
            float k = Math.Min(1f, Life / 100);
            int alpha = (int)(k * 255);
            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);

            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose();
        }
        public class ParticleColorful : Particle
        {
            public Color FromColor;
            public Color ToColor;
  
            public static Color MixColor(Color color1, Color color2, float k)
            {
                if (k < 0) // проверяем чтобы коэф не уходил в минус
                {
                    k = 0;
                }
                return Color.FromArgb(
                    (int)(color2.A * k + color1.A * (1 - k)),
                    (int)(color2.R * k + color1.R * (1 - k)),
                    (int)(color2.G * k + color1.G * (1 - k)),
                    (int)(color2.B * k + color1.B * (1 - k))
                );
            }
            public override void Draw(Graphics g)
            {
                float k = Math.Min(1f, Life / 100);
                var color = MixColor(ToColor, FromColor, k);
                var b = new SolidBrush(color);
                g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

                if (check)
                {
                    g.DrawLine(new Pen(Color.White), X , Y , X  + SpeedX*5 , Y  + SpeedY*5 );
                    // нарисовал вектор скорости 
                  
                }
                b.Dispose();

            }
        }
    }
}
