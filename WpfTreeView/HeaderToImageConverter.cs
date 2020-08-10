using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfTreeView
{
    // Converts a full path to a specific image type of a drive, folder or file

    [ValueConversion(typeof(string), typeof(BitmapImage))]
    class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = (string)value;

            if(path == null)
            {
                return null;
            }

            var name = MainWindow.GetFileFolderName(path);

            var image = "Image/file.png";

            if(string.IsNullOrEmpty(name))
            {
                image = "Image/drive.png";
            }
            else if(new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                image = "Image/folder-closed.png";
            }

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
