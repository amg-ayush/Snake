using System.Collections;

namespace Snake
{
    public partial class SnakeForm : Form
    {
        private Hashtable keys;
        int speed = 20;
        int state = 0;

        public class Snake
        {
            public int m;
            public int n;
            public char na;
            public Snake(int m, int n)
            {
                this.m = m;
                this.n = n;
            }
        }

        public class Eda
        {
            public int a;
            public int b;
            public Eda(int a, int b)
            {
                this.a = a;
                this.b = b;
            }
        }

        List<Snake> l = new List<Snake>();
        List<Eda> h = new List<Eda>();

        Random rr = new Random();


        public SnakeForm()
        {
            InitializeComponent();
            for (int i = 110; i < 160; i += 20)
            {
                Snake sn = new Snake(160, i);
                l.Add(sn);
            }
            Eda f = new Eda(200, 150);
            h.Add(f);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            keys = new Hashtable();
            keys.Add(38, false); // up
            keys.Add(40, false); // down
            keys.Add(37, false); // left 
            keys.Add(39, false); // right
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.pole(e.Graphics);
            this.render(e.Graphics);
            this.havchik(e.Graphics);
        }
        public void render(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < l.Count; ++i)
            {
                Snake sn = l[i];
                Rectangle rect = new Rectangle(sn.m, sn.n, 20, 20);
                g.FillRectangle(blackBrush, rect);
            }

        }
        public void havchik(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            SolidBrush blackBrush = new SolidBrush(Color.Blue);
            for (int i = 0; i < h.Count; ++i)
            {
                Eda edaa = h[i];
                Rectangle rect = new Rectangle(edaa.a, edaa.b, 20, 20);
                g.FillRectangle(blackBrush, rect);
            }
        }
        public void pole(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            int x = 100;
            for (int i = 0; i < 15; ++i)
            {
                int y = 50;
                for (int j = 0; j < 15; ++j)
                {
                    Rectangle rect = new Rectangle(x, y, 20, 20);
                    g.DrawRectangle(p, rect);
                    y += 20;
                }
                x += 20;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;	// Turn on WS_EX_COMPOSITED
                cp.ExStyle |= 0x00080000;	//WS_EX_LAYERED    
                return cp;
            }
        }
        int aa, bb;

        public void Proverka()
        {
            if (state == 0)
            {
                this.MyUpdate();
            }
        }
        private bool Check180(char c)
        {
            if (c != l[0].na)
                return true;
            else
                return false;
        }

        private void Peredvizheniye(int p, int z, char c)
        {
            int sp = this.speed;
            sp *= z;
            if (c == 'u' || c == 'd')
            {
                if (l[0].n != p && SamSebya(l[0].m, l[0].n + sp) == false)
                {
                    l[0].n += sp;
                    Step(l[0].m, l[0].n, l[0].na);
                }
                else
                {
                    state = 1;
                    MessageBox.Show("GAME OVER !!!");
                }
            }
            else
            {
                if (l[0].m != p && SamSebya(l[0].m + sp, l[0].n) == false)
                {
                    l[0].m += sp;
                    Step(l[0].m, l[0].n, l[0].na);
                }
                else
                {
                    state = 1;
                    MessageBox.Show("GAME OVER !!!");
                }
            }

        }

        public void AddSnake()
        {
            if (h[0].a == l[l.Count - 1].m && h[0].b == l[l.Count - 1].n)
            {
                Snake tt = new Snake(h[0].a, h[0].b);
                l.Add(tt);
                h.RemoveAt(0);
            }
        }

        public void NextEda()
        {
            for (int i = 0; i < h.Count; ++i)
            {
                if (l[0].m == h[i].a && l[0].n == h[i].b)
                {
                    NextGenerateEda();
                    Eda ee = new Eda(aa, bb);
                    h.Add(ee);
                    break;
                }
            }
        }
        public void MyUpdate()
        {
            if ((bool)this.keys[38]) // up
                if (Check180('d'))
                    l[0].na = 'u';
            if ((bool)this.keys[40]) // down
                if (Check180('u'))
                    l[0].na = 'd';
            if ((bool)this.keys[37]) // left
                if (Check180('r'))
                    l[0].na = 'l';
            if ((bool)this.keys[39]) // right
                if (Check180('l'))
                    l[0].na = 'r';

            if (l[0].na == 'd')
                Peredvizheniye(330, 1, 'd');
            if (l[0].na == 'u')
                Peredvizheniye(50, -1, 'u');
            if (l[0].na == 'r')
                Peredvizheniye(380, 1, 'r');
            if (l[0].na == 'l')
                Peredvizheniye(100, -1, 'l');

            AddSnake();
            NextEda();
        }

        public bool SamSebya(int uu, int uuu)
        {
            bool nn = false;
            for (int i = 0; i < l.Count; ++i)
            {
                if (uu == l[i].m && uuu == l[i].n) // врезался в сам себя ???
                {
                    nn = true;
                    break;
                }
            }
            return nn;
        }
        public void Step(int r, int r1, char r2)
        {
            for (int i = 1; i < l.Count; ++i)
            {
                int e = l[i].m;
                int e1 = l[i].n;
                char e2 = l[i].na;
                l[i].m = r;
                l[i].n = r1;
                l[i].na = r2;
                r = e;
                r1 = e1;
                r2 = e2;
            }
        }

        private bool EdaNaZmeike(int i)
        {
            return aa == l[i].m && bb == l[i].n;
        }

        private void GenerateFirstCoord()
        {
            aa = rr.Next(10, 38);
            while (aa % 2 != 0)
                aa = rr.Next(10, 38);
            bb = rr.Next(5, 33);
            while (bb % 2 == 0)
                bb = rr.Next(5, 33);
            aa *= 10;
            bb *= 10;
        }
        public void NextGenerateEda()
        {
            GenerateFirstCoord();
            int yy = 0;
            while (yy == 0)
            {
                yy = 0;
                int hh = 0;
                for (int i = 0; i < l.Count; ++i)
                {
                    if (EdaNaZmeike(i))
                    {
                        aa = rr.Next(10, 38);
                        while (aa % 2 != 0)
                            aa = rr.Next(10, 38);
                        bb = rr.Next(5, 33);
                        while (bb % 2 == 0)
                            bb = rr.Next(5, 33);
                        aa *= 10;
                        bb *= 10;
                        hh = 1;
                        break;
                    }
                }
                if (hh == 0)
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.keys[(int)e.KeyCode] = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            this.keys[(int)e.KeyCode] = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Proverka();
            //this.MyUpdate();
            this.Invalidate();
        }
    }
}