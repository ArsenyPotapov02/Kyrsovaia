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

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new Emitter // создаю эмиттер и привязываю его к полю emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            emitters.Add(this.emitter);

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
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
        }
    }
}
