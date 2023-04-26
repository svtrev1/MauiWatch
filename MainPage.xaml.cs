
using System;
using Microsoft.Maui.Layouts;
using Color = Microsoft.Maui.Graphics.Color;
using Android.Util;
using AbsoluteLayout = Microsoft.Maui.Controls.AbsoluteLayout;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using System.Drawing;
using Android.Content.Res;
using Java.Security.Cert;

namespace LastWatch;



    public partial class MainPage : ContentPage
    {


        const int wDots = 43;
        const int hDots = 7;

        static readonly int[,,] numbers = new int[10, 7, 5] //длина массива =10, 7 строк по 5 элементов

        {
            { //0
                { 0, 1, 1, 1, 0}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1},
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 0, 1, 1, 1, 0}
            },
            { //1
                { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0},
                { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0}, { 0, 0, 1, 0, 0}
            },
            { //2
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 1, 1, 1, 1, 1},
                { 1, 0, 0, 0, 0}, { 1, 0, 0, 0, 0}, { 1, 1, 1, 1, 1}
            },
            { //3
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 1, 1, 1, 1, 1}
            },
            { //4
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}
            },
            { //5
                { 1, 1, 1, 1, 1}, { 1, 0, 0, 0, 0}, { 1, 0, 0, 0, 0}, { 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 1, 1, 1, 1, 1}
            },
            { //6
                { 1, 1, 1, 1, 1}, { 1, 0, 0, 0, 0}, { 1, 0, 0, 0, 0}, { 1, 1, 1, 1, 1},
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 1, 1, 1, 1, 1}
            },
            { //7
                { 1, 1, 1, 1, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}
            },
            { //8
                { 1, 1, 1, 1, 1}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 1, 1, 1, 1, 1},
                { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 1, 1, 1, 1, 1}
            },
            { //9
                { 1, 1, 1, 1, 1}, { 1, 0, 0, 0, 1}, { 1, 0, 0, 0, 1}, { 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 1}, { 0, 0, 0, 0, 1}, { 1, 1, 1, 1, 1}
            },
        };

        static readonly int[,] pause = new int[7, 2]
        {
            { 1, 1 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 0, 0 }, { 1, 1 }
        };

        BoxView[,,] BW = new BoxView[6, 7, 5];

        public MainPage()
        {
            
            InitializeComponent();

            // double h = 200.0 / hDots;
            // double w = 800.0 / wDots;
            // double posX = 860.0 / (wDots - 1);
            // double posY = 200.0 / (hDots - 1);
            
            double h = 50.0 / hDots;
            double w = 300.0 / wDots;
            double posX = 400.0 / (wDots - 1);
            double posY = 70.0 / (hDots - 1);
        
            double x = -30;
            int number = 0;
            int column = 0;
            int row = 0;

            x += posX;

            for (number = 0; number <= 5; number++)
            {
                for (column = 0; column <= 4; column++)
                {
                    double y = 0;

                    for (row = 0; row <= 6; row++)
                    {
                        BoxView boxView = new BoxView{Color = Colors.Brown};
                        BW[number, row, column] = boxView;
                        absoluteLayout.Children.Add(boxView);
                        absoluteLayout.SetLayoutBounds(boxView,
                                        new Rect(x, y, w, h));
                        y += posY;
                    }
                    x += posX;
                }
                x += posX;

                if (number != 1 && number != 3) continue;
                {
                    for (column = 0; column <= 1; column++)
                    {
                        double y = 0;

                        for (row = 0; row <= 6; row++)
                        {
                            bool isPause = pause[row, column] == 1;
                            bool IsVP = isPause ? IsVisible : !IsVisible;
                            BoxView boxView = new BoxView
                            {
                                IsVisible = IsVP,
                                Color = Colors.Brown
                                //Color = pause[row, column] == 1 ?
                                //    colorN : colorF
                            };
                            absoluteLayout.Children.Add(boxView);
                            absoluteLayout.SetLayoutBounds(boxView,
                                new Rect(x, y, w, h));

                            y += posY;
                        }
                        x += posX;
                    }
                    x += posX;
                }


            }

        Device.StartTimer(TimeSpan.FromSeconds(1), Time);
        Time();

        }
    

        bool Time()
        {
            DateTime dateTime = DateTime.Now;

            SetNumber(0, dateTime.Hour / 10);
            SetNumber(1, dateTime.Hour % 10);
            SetNumber(2, dateTime.Minute / 10);
            SetNumber(3, dateTime.Minute % 10);
            SetNumber(4, dateTime.Second / 10);
            SetNumber(5, dateTime.Second % 10);

            return true;
        }

        void Size(object sender, EventArgs args)
        {
            absoluteLayout.HeightRequest = hDots * Width / wDots;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(Width, Height);
        }

        void SetNumber(int item, int number)
        {
            int row, column;
            for (row = 0; row <= 6; row++)
            {
                for (column = 0; column <= 4; column++)
                {
                    bool isOn = numbers[number, row, column] == 1;
                    //Color color = isOn ? colorN : colorF;
                    //BW[item, row, column].Color = color;
                    bool isV = isOn ? IsVisible : !IsVisible;
                    BW[item, row, column].IsVisible = isV;
                }
            }
        }

    }


