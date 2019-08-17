using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE1RSE
{
    partial class Editor
    {
        private AccordionLayout sections;
    }

    class AccordionLayout
    {
        private List<Panel> _panels;
        private List<Label> _labels;
        private List<bool> _active;
        private bool _enabledefault = false;
        private int _x = 8, _xpad = 16, _y = 8, _w = 400, _hlabel = 32, _hpanel = 48, _mlabel = 8, _mpanel = 16, _mpanelitem = 4;
        private Panel _parent;
        private Font _flabel;
        private float _flabelsize = 8f;
        private Color _lCol = Color.FromArgb(0x00, 0x00, 0x60), _lColHov = Color.Blue;

        public int Count => _labels.Count;


        public AccordionLayout(Panel p)
        {
            _parent = p;
            _panels = new List<Panel>();
            _labels = new List<Label>();
            _active = new List<bool>();
            _w = _parent.Width - _x * 2 - _xpad;
            _flabel = new Font(Control.DefaultFont.FontFamily, _flabelsize, FontStyle.Bold);
        }

        public void AddSection(string t, out Panel p)
        {
            AddSection(t);
            p = GetSection(t);
        }

        public void AddSection(string t)
        {
            if (CheckSection(t))
                throw new Exception(string.Format("Section with title {0} already exists", t));
            Initialize(new Label(), t);
            Initialize(new Panel());
            _active.Add(_enabledefault);
            Resize();
        }

        private void Initialize(Label l, string t)
        {
            l.Text = t;
            l.Font = _flabel;
            l.ForeColor = _lCol;
            l.Location = new Point(_x, _y);
            l.Width = _w;
            l.Padding = new Padding(_mlabel);
            l.AutoSize = true;
            l.MouseEnter += (o, e) => { l.Cursor = Cursors.Hand; l.ForeColor = _lColHov; };
            l.MouseLeave += (o, e) => { l.Cursor = Cursors.Default; l.ForeColor = _lCol; };
            l.Click += ToggleSection;
            _parent.Controls.Add(l);
            _labels.Add(l);
        }

        private void Initialize(Panel p)
        {
            p.Location = new Point(_x, _y);
            p.Width = _w;
            p.Height = _hpanel;
            p.Margin = new Padding(_mpanel);
            p.Padding = new Padding(0, 0, 0, _mpanel/2);
            p.AutoSize = true;
            p.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            p.Visible = _enabledefault;
            _parent.Controls.Add(p);
            _panels.Add(p);
        }

        private void ToggleSection(object l, EventArgs e)
        {
            ToggleSection(GetIndex((Label)l));
        }

        private void ToggleSection(int i)
        {
            _active[i] = !_active[i];
            _panels[i].Visible = _active[i];
            Resize();
        }

        public void ToggleSection(int i, bool on)
        {
            _active[i] = on;
            _panels[i].Visible = on;
            Resize();
        }
        public void ToggleAllSections(bool on)
        {
            for (int i = 0; i < Count; i++)
            {
                _active[i] = on;
                _panels[i].Visible = on;
            }
            Resize();
        }

        private int GetIndex(Label l)
        {
            return _labels.IndexOf(l);
        }

        public Panel GetSection(string t)
        {
            int i = 0;
            while (i < _labels.Count && _labels[i].Text != t)
                i++;
            if (i >= _labels.Count)
                throw new MissingMemberException();
            return _panels[i];
        }

        public bool CheckSection(string t)
        {
            try
            {
                GetSection(t);
            }
            catch (MissingMemberException)
            {
                return false;
            }
            return true;
        }

        public void Resize()
        {
            int pSX = _parent.HorizontalScroll.Value;
            int pSY = _parent.VerticalScroll.Value;
            for (int i = 0, htotal = 0; i < _labels.Count; i++)
            {
                _labels[i].Location = new Point(_x - pSX, _y + htotal - pSY);
                htotal += _labels[i].Height;
                if (_active[i])
                {
                    _panels[i].Location = new Point(_x - pSX, _y + htotal - pSY);
                    htotal += _panels[i].Height;
                }
            }
            _parent.Focus();
        }

        public void Reset()
        {
            while (_labels.Count > 0)
            {
                _parent.Controls.Remove(_labels.Last());
                _parent.Controls.Remove(_panels.Last());
                _labels.Remove(_labels.Last());
                _panels.Remove(_panels.Last());
                _active.Remove(_active.Last());
            }
        }
    }
}