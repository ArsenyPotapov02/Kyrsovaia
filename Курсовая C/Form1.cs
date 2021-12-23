using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static Курсовая_C.Emitter;
using static Курсовая_C.IImpactPoint;
using static Курсовая_C.Particle;

namespace Курсовая_C
{
    public partial class Form1 : Form
    {
        
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;
        PaintPoint point1;
        PaintPoint point2;
        PaintPoint point3;
        PaintPoint point4;
        PaintPoint point5;
        ReboundPoint point6;
        ReboundPoint point7;
        ReboundPoint point8;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new Emitter 
            {
                Direction = 0,
                Spreading = 1,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 20,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 4 ,
            };

            emitters.Add(this.emitter);
            point1 = new PaintPoint // создали точки расскрашивания
            {
               color = Color.DarkBlue,
               X = picDisplay.Width / 2,
               Y = picDisplay.Height / 2,
            };
            emitter.impactPoints.Add(point1);
            point2 = new PaintPoint // создали точки расскрашивания
            {
                color = Color.LightYellow,
                X = picDisplay.Width / 4,
                Y = picDisplay.Height / 2,
            };
            emitter.impactPoints.Add(point2);
            point3 = new PaintPoint // создали точки расскрашивания
            {
                color = Color.Magenta,
                X = picDisplay.Width / 4 *3,
                Y = picDisplay.Height / 2,
            };
            emitter.impactPoints.Add(point3);
            point4 = new PaintPoint // создали точки расскрашивания
            {
                color = Color.Red,
                X = picDisplay.Width / 6 * 5,
                Y = picDisplay.Height / 4 *3,
            };
            emitter.impactPoints.Add(point4);
            point5 = new PaintPoint // создали точки расскрашивания
            {
                color = Color.Gold,
                X = picDisplay.Width / 6 ,
                Y = picDisplay.Height / 4 *3,
            };
            emitter.impactPoints.Add(point5);
            point6 = new ReboundPoint 
            {
                
                X = picDisplay.Width / 5 ,
                Y = picDisplay.Height / 4 * 3,
            };
            emitter.impactPoints.Add(point6); 
            point7 = new ReboundPoint 
            {
               
                X = picDisplay.Width / 6 +200,
                Y = picDisplay.Height / 4 * 3,
            };
            emitter.impactPoints.Add(point7);
            point8 = new ReboundPoint { 
                
                X = picDisplay.Width / 6 *5 -200,
                Y = picDisplay.Height / 4 * 3,
            };
            emitter.impactPoints.Add(point8);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // тут теперь обновляем эмиттер

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g); // а тут теперь рендерим через эмиттер
            }

            picDisplay.Invalidate();
        }


        // добавляем переменные для хранения положения мыши
        private int MousePositionX = 0;
        private int MousePositionY = 0;
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // а тут в эмиттер передаем положение мыфки
            point6.X= e.X;
            point6.Y = e.Y;
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"Направление {tbDirection.Value}°";
            emitter.Direction = tbDirection.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = $"Разброс {trackBar1.Value}°";
            emitter.Spreading = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label3.Text = $"Количество частиц {trackBar2.Value}";
            emitter.Spreading = trackBar2.Value;
            emitter.ParticlesPerTick = trackBar2.Value;
        }
    }
}
