using DayVsNight.Themes;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DayVsNight
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // MessagingCenter.Subscribe<LoginMessage>(this, "successful_login", (lm) => HandleLogin(lm));
            MessagingCenter.Subscribe<ThemeMessage>(this, ThemeMessage.ThemeChanged, (tm) => UpdateTheme(tm));
        }

        private void UpdateTheme(ThemeMessage tm)
        {
            BackgroundGradient.InvalidateSurface();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<ThemeMessage>(this, ThemeMessage.ThemeChanged);
        }

        string themeName = "light";

        private void ProfileImage_Tapped(object sender, EventArgs e)
        {
            if (themeName == "light")
            {
                themeName = "dark";
            }
            else
            {
                themeName = "light";
            }

            ThemeHelper.ChangeTheme(themeName);
        }

        // background brush
        SKPaint backgroundBrush = new SKPaint()
        {
            Style = SKPaintStyle.Fill,
            Color = Color.Red.ToSKColor()
        };

        private void BackgroundGradient_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // get the brush based on the theme
            SKColor gradientStart = ((Color)Application.Current.Resources["BackgroundGradientStartColor"]).ToSKColor();
            SKColor gradientMid = ((Color)Application.Current.Resources["BackgroundGradientMidColor"]).ToSKColor();
            SKColor gradientEnd = ((Color)Application.Current.Resources["BackgroundGradientEndColor"]).ToSKColor();

            // gradient backround
            backgroundBrush.Shader = SKShader.CreateRadialGradient
                (new SKPoint(0, info.Height * .8f),
                info.Height*.8f,
                new SKColor[] { gradientStart, gradientMid, gradientEnd },
                new float[] { 0, .5f,  1 },
                SKShaderTileMode.Clamp);

            //backgroundBrush.Shader = SKShader.CreateLinearGradient(
            //                              new SKPoint(0, 0),
            //                              new SKPoint(info.Width, info.Height),
            //                              new SKColor[] {
            //                                  gradientStart, gradientEnd },
            //                              new float[] { 0, 1 },
            //                              SKShaderTileMode.Clamp);
            SKRect backgroundBounds = new SKRect(0, 0, info.Width, info.Height);
            canvas.DrawRect(backgroundBounds, backgroundBrush);


            Color color = (Color)Application.Current.Resources["TabSubTextColor"];
            for (int i = 0; i < stackParent.Children.Count; i++)
            {
                StackLayout stack = stackParent.Children[i] as StackLayout;
                if (stack.Children[0].BackgroundColor != Color.White)
                {
                    (stack.Children[0] as BoxView).BackgroundColor = color;
                    (stack.Children[1] as Label).TextColor = color;
                    (stack.Children[2] as Label).TextColor = color;
                }
            }
        }

        private void Stack_TapGesture(object sender, EventArgs e)
        {
            StackLayout stackLayout = sender as StackLayout;
            Color color = (Color)Application.Current.Resources["TabSubTextColor"];
            for(int i= 0;i < stackParent.Children.Count; i++)
            {
                StackLayout stack = stackParent.Children[i] as StackLayout;
                (stack.Children[0] as BoxView).BackgroundColor = color;
                (stack.Children[1] as Label).TextColor = color;
                (stack.Children[2] as Label).TextColor = color;
            }
            (stackLayout.Children[0] as BoxView).BackgroundColor = Color.White;
            (stackLayout.Children[1] as Label).TextColor = Color.White;
            (stackLayout.Children[2] as Label).TextColor = Color.White;
        }
    }
}
