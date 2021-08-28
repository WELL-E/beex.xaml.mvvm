using Beex.Xaml.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Beex.VFirst.Sample.Services
{
    public class ViewService : ServiceBase, IViewService
    {
        public static readonly DependencyProperty ViewProperty =
           DependencyProperty.Register("View", typeof(ListView), typeof(ViewService), new PropertyMetadata(null));
        public ListView View
        {
            get { return (ListView)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        public void PrintView()
        {
            FrameworkElement e = View as FrameworkElement;
            if (View == null) return;

            var printDlg = new PrintDialog();
            System.Printing.PrintCapabilities capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);
            double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                           e.ActualHeight);
            var originalScale = e.LayoutTransform;
            e.LayoutTransform = new ScaleTransform(scale, scale);
            Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            e.Measure(sz);
            e.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));
            printDlg.PrintVisual(e, "PrintView");
            e.LayoutTransform = originalScale;
            MessageBox.Show("打印完成.");
        }
    }
}
