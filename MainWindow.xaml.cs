using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadListOfTeams
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string readfileName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPickFileRead_Click(object sender, RoutedEventArgs e)
        {
            //add Reference: - System.Windows.Forms
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK) {
                readfileName = openFileDialog.FileName;
                lblFileRead.Content = readfileName;
                btnPickFileWrite.IsEnabled = true;
            }
           
        }

        private void btnPickFileWrite_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            System.Windows.Forms.DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                try {
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(readfileName);

                    /*
                     * Loop through file, we are going to pull data for the following:
                     * "city":
                        "key":
                        "postal_code":
                        "team_number":
                    */
                    String teams = "";
                    while (!streamReader.EndOfStream)
                    {
                        
                        string line = streamReader.ReadLine();
                        if (line.Contains("\"city\":"))
                        {
                            //Get rid of front - needed to add 6 to deal with spaces
                            line = line.Remove(0, "\"city\":".Length+6);
                            //Get rid of ", at the end
                            line = line.Substring(0, line.Length - 3);
                            //MessageBox.Show(line);//troubleshooting
                            teams += line + "\n";
                        }
                        
                    }
                    MessageBox.Show(teams);
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
 
