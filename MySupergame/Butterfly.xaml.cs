using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MySupergame
{
    public sealed partial class Butterfly : UserControl
    {
        // animate butterfly timer
        private DispatcherTimer timer;

        // offset to show
        private int currentFrame = 0;
        private int direction = 1; // 1 or -1
        private int frameheight = 132;

        // speed
        private readonly double MaxSpeed = 10;
        private readonly double Accelerate = 0.5;
        private double speed;

        // angle
        private readonly double AngleStep = 5;
        private double Angle = 0;

        public double LocationX { get; set; }
        public double LocationY { get; set; }

        public Butterfly()
        {
            this.InitializeComponent();

            // animate
            timer = new DispatcherTimer();
            // 125 milliseconds
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += Timer_Tick; // press tab
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // currentFrame 0,1,2,3,4
            if (direction == 1) currentFrame++;
            else currentFrame--;
            if (currentFrame == 0 || currentFrame == 4)
            {
                direction = -1 * direction; // 1 or -1
            }
            // set offset
            SpriteSheetOffset.Y = currentFrame * -frameheight;
        }

        // move-method
        public void Move()
        {
            // more speed
            speed += Accelerate;
            if (speed > MaxSpeed) speed = MaxSpeed;


            // set location values (angle, speed)
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle + 90))) * speed; 
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle + 90))) * speed;
        }

        // rotate
        public void Rotate(int direction)
        {
            Angle += direction * AngleStep; // -1*5 or 1*5
            ButterflyRotateAngle.Angle = Angle;
        }

        // update location
        public void SetLocation()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

    }
}
