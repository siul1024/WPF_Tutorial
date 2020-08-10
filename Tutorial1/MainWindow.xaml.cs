using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tutorial1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The description is '{this.DescriptionText.Text}'");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.ChkBoxItem_Weld.IsChecked
            = this.ChkBoxItem_Assembly.IsChecked
            = this.ChkBoxItem_Plasma.IsChecked
            = this.ChkBoxItem_Laser.IsChecked
            = this.ChkBoxItem_Purchase.IsChecked
            = this.ChkBoxItem_Lathe.IsChecked
            = this.ChkBoxItem_Drill.IsChecked
            = this.ChkBoxItem_Fold.IsChecked
            = this.ChkBoxItem_Roll.IsChecked
            = this.ChkBoxItem_Saw.IsChecked
            = false;

            this.TextBoxLength.Text = string.Empty;
        }

        private void ChkBox_Checked(object sender, RoutedEventArgs e)
        {
            this.TextBoxLength.Text += (string)((CheckBox)sender).Content;
        }

        private void ChkBox_Unhecked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxFinish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.TextBoxNote == null)
            {
                return;
            }
            var combo = (ComboBox)sender;
            var value = (ComboBoxItem)combo.SelectedValue;
            this.TextBoxNote.Text = (string)value.Content;
        }

        private void TextBoxSupplierName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.TextBoxMass.Text = this.TextBoxSupplierName.Text;
        }
    }
}
