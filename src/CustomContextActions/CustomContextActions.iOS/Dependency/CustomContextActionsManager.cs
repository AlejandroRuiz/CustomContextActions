using System;
using System.Reflection;
using CoreGraphics;
using CustomContextActions.Dependency;
using CustomContextActions.iOS.Dependency;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(CustomContextActionsManager))]
namespace CustomContextActions.iOS.Dependency
{
    public class CustomContextActionsManager : ICustomContextActionsManager
    {
        #region ICustomContextActionsManager

        public void RestoreContextActionsViews()
        {
            SetNativeView(DestructiveBackground, true);
            SetNativeView(NormalBackground);
        }

        public void SetCustomBackgroundColor(Color color, bool isForDestructive = false)
        {
            GetDefaultValuesIfNeeded();

            //Create a default size for color background
            var rect = new CGRect(0, 0, 1, 1);
            var size = rect.Size;

            //Create a background UIImage view based on requested color
            UIGraphics.BeginImageContext(size);
            using (var context = UIGraphics.GetCurrentContext())
            {
                context.SetFillColor(color.ToCGColor());
                context.FillRect(rect);
                SetNativeView(UIGraphics.GetImageFromCurrentImageContext(), isForDestructive);
            }
        }

        public void SetCustomView(Xamarin.Forms.View backgroundView, bool isForDestructive = false)
        {
            GetDefaultValuesIfNeeded();

            //Convert Xamarin.Forms.View to a Native UIView
            var defaultSize = new CGRect(0, 0, backgroundView.WidthRequest > 0 ? backgroundView.WidthRequest : 80, backgroundView.HeightRequest > 0 ? backgroundView.WidthRequest : 120);
            var renderer = Platform.CreateRenderer(backgroundView);
            renderer.NativeView.Frame = defaultSize;
            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;
            renderer.Element.Layout(defaultSize.ToRectangle());
            var nativeView = renderer.NativeView;
            nativeView.SetNeedsLayout();

            //Convert UIView into a UIImage
            UIGraphics.BeginImageContextWithOptions(nativeView.Bounds.Size, nativeView.Opaque, 0.0f);
            nativeView.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var nativeImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            SetNativeView(nativeImage, isForDestructive);
        }

        #endregion

        UIImage DestructiveBackground;
        UIImage NormalBackground;

        void GetDefaultValuesIfNeeded()
        {
            if (DestructiveBackground == null)
            {
                DestructiveBackground = GetNativeView(true);
                NormalBackground = GetNativeView();
            }
        }

        void SetNativeView(UIImage nativeView, bool isDestructive = false)
        {
            var info = GetContextActionFieldInfo(isDestructive);
            info.SetValue(null, nativeView);
        }

        UIImage GetNativeView(bool isDestructive = false)
        {
            var info = GetContextActionFieldInfo(isDestructive);
            return (UIImage)info.GetValue(null);
        }

        FieldInfo GetContextActionFieldInfo(bool isDestructive = false)
        {
            var formsAssembly = (typeof(Xamarin.Forms.Platform.iOS.EntryRenderer)).Assembly;

            //https://github.com/xamarin/Xamarin.Forms/blob/ae92582d5acad2b8aeab9a2ed5b490561e71bd6c/Xamarin.Forms.Platform.iOS/ContextActionCell.cs#L14
            var contextActionCellType = formsAssembly.GetType("Xamarin.Forms.Platform.iOS.ContextActionsCell");

            return contextActionCellType.GetField(isDestructive ? nameof(DestructiveBackground) : nameof(NormalBackground), BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
