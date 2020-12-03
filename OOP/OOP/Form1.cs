using System;
using System.Windows.Forms;
using System.Collections.Generic;
using OOP.Classes;
using System.Linq;
using System.Drawing;

namespace OOP 
{
    public partial class Life_Simulation : Form
    {
        private Graphics Graphics;
        private Bitmap Bitmap;
        private Square PickedSquare;
        private SimulationObject PickedObject;
        // TODO:
        //public void ChangeSeason()
        //{
        //    graphics.Clear(Map.Color);
        //    Life.UpdateSimulation();
        //    pictureBox.Refresh();
        //}

        public Life_Simulation() 
        {
            InitializeComponent();
            InitializeMap();
            RenderingNewObjects(Life.PrepareSimulation());
        }

        private void InitializeMap()
        {
            Bitmap = new Bitmap(Map.Width * Map.Multiplier, Map.Height * Map.Multiplier);
            pictureBox.Image = Bitmap;
            Graphics = Graphics.FromImage(pictureBox.Image);
            Graphics.Clear(Map.Color);
        }



        //
        // (Start & Stop) + Timer
        private void bStart_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            RenderingNewObjects(Life.UpdateSimulation());
            pictureBox.Refresh();
            UpdateInfo();
        }
        // (Start & Stop) + Timer
        //



        //
        // Scaling Map
        private void bMinus_Click(object sender, EventArgs e)
        {
            if (Map.Multiplier > 2)
            {
                bool wasEnabled = timer.Enabled;

                timer.Stop();
                --Map.Multiplier;
                Scale();

                if (wasEnabled)
                {
                    timer.Start();
                }
            }
        }

        private void bPlus_Click(object sender, EventArgs e)
        {
            if (Map.Multiplier < 8)
            {
                bool wasEnabled = timer.Enabled;

                timer.Stop();
                ++Map.Multiplier;
                Scale();

                if (wasEnabled)
                {
                    timer.Start();
                }
            }
        }

        private void Scale()
        {
            Bitmap = new Bitmap(pictureBox.Image, Map.Width * Map.Multiplier, Map.Height * Map.Multiplier);
            pictureBox.Image = Bitmap;
            Graphics = Graphics.FromImage(pictureBox.Image);
            Graphics.Clear(Map.Color);

            RenderingAllObjects();
            pictureBox.Refresh();
        }
        // Scaling Map
        //



        //
        // Rendering Map
        private void RenderingNewObjects(List<Point> Squares)   // Отрисовывает новые объекты каждый тик
        {
            foreach (var sq in Squares)
            {
                if (Map.Area[sq.Y * Map.Width + sq.X].Objects.Count != 0) {
                    SolidBrush solidBrush = new SolidBrush(Map.Area[sq.Y * Map.Width + sq.X].Objects.Last().Color);
                    DrawRectangle(solidBrush, sq);
                }
                else
                {
                    SolidBrush solidBrush = new SolidBrush(Map.Color);
                    DrawRectangle(solidBrush, sq);
                }
            }
        }

        private void RenderingAllObjects()   // Отрисовывает все объекты после скейла карты
        {
            foreach (var sq in Map.Area.Where(occupiedSquare => occupiedSquare.Objects.Count != 0))
            {
                SolidBrush solidBrush = new SolidBrush(sq.Objects.Last().Color);
                DrawRectangle(solidBrush, sq.Objects.Last().Coordinates);
            }
        }

        private void DrawRectangle(SolidBrush solidBrush, Point point)
        {
            Graphics.FillRectangle(solidBrush, Map.Multiplier * point.X, Map.Multiplier * point.Y, Map.Multiplier, Map.Multiplier);
        }
        // Rendering Map
        //



        //
        // Unit info
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int X = e.X / Map.Multiplier;
                int Y = e.Y / Map.Multiplier;

                PickedSquare = Map.Area[Y * Map.Width + X];
                PickedObject = null;
                if (PickedSquare.Objects.Count != 0)
                {
                    PickedObject = PickedSquare.Objects.Last();
                }
                UpdateInfo();
            }
        }

        private void UpdateInfo()
        {
            if (PickedSquare != null)
            {
                Info.Text = "Picked Square" + "\n" + "\n" +
                            "Position: " + PickedSquare.Coordinates.X.ToString() + " " + PickedSquare.Coordinates.Y.ToString() + "\n";

                if (PickedObject != null)
                {
                    Info.Text += "\n" + "\n";

                    if (PickedObject is Unit unit)
                    {
                        Info.Text += "UNIT" + "\n" + "\n" +
                                     "Type: " + unit.GetType().Name + "\n" +
                                     "Gender: " + unit.Gender + "\n" +
                                     "Satiety: " + unit.Satiety.ToString() + "\n" +
                                     "Position: " + unit.Coordinates.X.ToString() + " " + unit.Coordinates.Y.ToString() + "\n" +
                                     "Reprod. Ability: " + unit.ReproduceAbilityValue.ToString() + "\n";
                    }

                    if (PickedObject is Plant plant)
                    {
                        Info.Text += "PLANT" + "\n" + "\n" +
                                     "Type: " + plant.GetType().Name + "\n" +
                                     "Position: " + plant.Coordinates.X.ToString() + " " + plant.Coordinates.Y.ToString() + "\n";
                    }
                }
            }
        }
        // Unit info
        //
    }
}