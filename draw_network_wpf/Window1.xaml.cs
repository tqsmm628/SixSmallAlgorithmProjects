using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace draw_network
{
    public partial class Window1 : Window
    {
        private Network MyNetwork = new();
        
        public Window1()
        {
            InitializeComponent();
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    DefaultExt = ".net",
                    Filter = "Network Files|*.net|All Files|*.*"
                };

                if (dialog.ShowDialog() == true)
                {
                    MyNetwork.ReadFromFile(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyNetwork = new Network();
            }

            DrawNetwork();
        }

        private void DrawNetwork()
        {
            mainCanvas.Children.Clear();
            MyNetwork.Draw(mainCanvas);
        }

        private void ExitCommand_Executed(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyNetwork.ReadFromFile("net3.net");
            MyNetwork.Draw(mainCanvas);
        }
    }
}
