using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace toolcad23.Models.Helpers
{
    internal static class Functions
    {
        public static async void CreateBitmapFromVisualAndCopyToClipboard(Visual target)
        {
            //if (target == null || string.IsNullOrEmpty(fileName))
            //{
            //    return;
            //}

            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);

            RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new Point(), bounds.Size));
            }

            renderTarget.Render(visual);
            await TryCopyBitmapToClipboard(renderTarget);
            //PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            //bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            //using (Stream stm = File.Create(fileName))
            //{
            //    bitmapEncoder.Save(stm);
            //}
        }

        private static async Task TryCopyBitmapToClipboard(BitmapSource bmpCopied)
        {
            var tries = 3;
            while (tries-- > 0)
            {
                try
                {
                    // This must be executed on the calling dispatcher.
                    Clipboard.SetImage(bmpCopied);
                }
                catch (COMException)
                {
                    // Windows clipboard is optimistic concurrency. On fail (as in use by another process), retry.
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                }
            }
        }
    }
}
