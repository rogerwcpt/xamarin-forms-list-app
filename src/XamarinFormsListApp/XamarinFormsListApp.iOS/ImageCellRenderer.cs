using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace XamarinFormsListApp.iOS
{
    // Bugfix for this class has been sumbitted to Xamarin:  https://github.com/xamarin/Xamarin.Forms/issues/4711
    public class ImageCellRenderer : TextCellRenderer
    {

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var result = (CellTableViewCell)base.GetCell(item, reusableCell, tv);

            var imageCell = (ImageCell)item;

            WireUpForceUpdateSizeRequested(item, result, tv);

            SetImage(imageCell, result);

            return result;
        }

        async void SetImage(ImageCell cell, CellTableViewCell target)
        {
            var source = cell.ImageSource;

            target.ImageView.Image = null;

            IImageSourceHandler handler;

            if (source != null && (handler = Xamarin.Forms.Internals.Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(source)) != null)
            {
                UIImage uiimage;
                try
                {
                    uiimage = await handler.LoadImageAsync(source).ConfigureAwait(false);
                }
                catch (TaskCanceledException)
                {
                    uiimage = null;
                }

                // Start of BugFix
                if (NSThread.Current.IsMainThread)
                {
                    SetImage(target, uiimage).Invoke();
                }
                else
                {
                    NSRunLoop.Main.BeginInvokeOnMainThread(SetImage(target, uiimage));
                }
                // end of bugfix

            }
            else
            {
                target.ImageView.Image = null;
            }
        }

        // Start of BugFix
        static Action SetImage(CellTableViewCell target, UIImage uiimage)
        {
            return () =>
            {
                if (target.Cell != null)
                {
                    target.ImageView.Image = uiimage;
                    target.SetNeedsLayout();
                }
                else
                    uiimage?.Dispose();
            };
        }
        //end of bugfix
    }
}