using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    
    public partial class Form1 : Form
    {
        public Bitmap Handlertexture = Resource1.H, // картинка первого квадрата 
                      Target = Resource1.T; // картинка второго квадрата 
        private Point _targetPosition = new Point(200, 200); // изначальная позиция квадрата , который нужно догонять 
        private Point _direction = Point.Empty; // некое направление 
        private int _score = 0; // начальное кол-во очков 
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true); // для того , чтобы не мерцала картинка 
            UpdateStyles();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e) // второй таймер , который вызывает рандомное движение квадрата 
        {
            Random r = new Random();
            timer2.Interval = r.Next(25, 1000); // случайный интервал от 25 до 1000 миллисекунд 
            _direction.X = r.Next(-1, 2); // изменение движения на случайное число от -1 до 2 
            _direction.Y = r.Next(-1, 2);
        }

        private void score_Click(object sender, EventArgs e)
        {

        }
         
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var LocalPosition = this.PointToClient(Cursor.Position); // перевести точку на экране в точку на форме  


            _targetPosition.X += _direction.X * 15; // скорость, умножая на число , будет меняеться скорость движения квадрата 
            _targetPosition.Y += _direction.Y * 15;

            if (_targetPosition.X < 50 || _targetPosition.X > 300) // границы за которые не выходит квадрат по x
            {
                _direction.X *= -1; // меняет направление 
            }
            if (_targetPosition.Y < 50 || _targetPosition.Y > 300)// границы за которые не выходит квадрат по y
            {
                _direction.Y *= -1;
            }

            Point between = new Point(LocalPosition.X - _targetPosition.X, LocalPosition.Y - _targetPosition.Y);
            float distance = (float)Math.Sqrt((between.X * between.X) * (between.Y * between.Y));

            if (distance< 20) // дистанция при которой начисляются очки 
            {
                AddScore(1);
            }

            var handlerRect = new Rectangle(LocalPosition.X - 25, LocalPosition.Y - 25, 50, 50);    // квадрат который управляется мышью , также тут задаем размер этого квадрата и его позицию.
            var targetrect = new Rectangle(_targetPosition.X - 25, _targetPosition.Y - 25 , 50, 50); // квадрат который управляется мышью , также его размер 
            g.DrawImage(Handlertexture, handlerRect);
            g.DrawImage(Target, targetrect);

        }
        private  void AddScore(int score) // метод , который добавляет очки
        {
            _score += score;
            scoreLabel.Text = _score.ToString();  
        }
    }
}
