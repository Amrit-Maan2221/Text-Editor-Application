//Name- Amritpal Singh
//Date of Submission - 1 October, 2021
//Project- Text Editor by Amrit
//Description- This project mimics the behaviour of notepad






using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;





namespace Text_Editor_by_Amrit
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Creating a application Similar to notepad
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _filename; //Filename of text file
        private string _opened_file_path; //The file path of the text file

        // Openly accessible Filename of txt file with get and set method
        public string Filename
        {
            get
            {
                return _filename;
            }
            set { }
        }


        // Openly accessible path of txt file with get and set method
        public string Opened_file_path
        {
            get
            {
                return _opened_file_path;
            }
            set { }
        }

        //Intialize the text Editor
        public MainWindow()
        {
            InitializeComponent();//Opening Window

            var lengthOfText = text_area.Text.Length;
            word_count.Text = "Word Count : " + lengthOfText;
            // For word Count
            this.KeyDown += new KeyEventHandler(OnButtonKeyPressed);
            this.KeyUp += new KeyEventHandler(OnButtonKeyPressed);
            if (text_area.Text.Length == 0)
            {
                word_count.Text = "Word Count : 0";
            }
        }

        //Initialize the text Editor
        public MainWindow(string filename)
        {
            InitializeComponent();//Opening Window
            try
            {
                _filename = filename;
                this.Title = _filename;
            }
            catch (Exception e)
            {
                MessageBox.Show("No file name\n" + e.Message + "\nMissing File name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // For word Count
            this.KeyDown += new KeyEventHandler(OnButtonKeyPressed);
            this.KeyUp += new KeyEventHandler(OnButtonKeyPressed);
            if (text_area.Text.Length == 0)
            {
                word_count.Text = "Word Count : 0";
            }
        }

        //Function Name- About_Option_Click
        //Parameters- object sender, RoutedEventArgs e--> System responsible
        //Return - void --> Nothing
        //Description- Displays information about text Editor when user clicks on the about menu option
        public void About_Option_Click(object sender, RoutedEventArgs e)
        {
            String aboutTextEditor = "This is Simple Text Editor that mimics the behavior of Notepad";
            MessageBox.Show(aboutTextEditor);
        }


        //Function Name- OnButtonKeyPressed
        //Parameters- object sender, RoutedEventArgs e--> System responsible
        //Return - void --> Nothing
        //Description- Display the word count of entered text to user and also show * in front of title if key gets pressed
        private void OnButtonKeyPressed(object sender, KeyEventArgs e)
        {
            var lengthOfText = text_area.Text.Length;
            word_count.Text = "Word Count : " + lengthOfText;

            string currentTitle = this.Title;


            if (currentTitle == "*Untitled- Amrit text Editor" && lengthOfText == 0)
            {
                if (currentTitle[0] == '*')
                {
                    this.Title = "Untitled- Amrit text Editor";
                }
            }
            else if (currentTitle[0] == '*')
            {
                //do nothing
            }
            else
            {
                this.Title = "*" + currentTitle;
            }
        }



        //Function Name- Save_As_File
        //Parameters- none
        //Return - void --> Nothing
        //Description- Saves the new file for the user
        public void Save_As_File()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "Untitled";
            dialog.DefaultExt = "*.txt";
            dialog.Filter = "Text Document (.txt) | *.txt";
            dialog.ShowDialog();
            File.WriteAllText(dialog.FileName, text_area.Text);
        }




        //Function Name- SaveAs_Option_Click
        //Parameters- object sender, RoutedEventArgs e
        //Return - void --> Nothing
        //Description- Saves the new file for the user when user chooses to save from menu
        public void SaveAs_Option_Click(object sender, RoutedEventArgs e)
        {
            Save_As_File();
        }



        //Function Name- New_Option_Click
        //Parameters- object sender, RoutedEventArgs e
        //Return - void --> Nothing
        //Description- Opens the new window when clicked on approriate menu option
        public void New_Option_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title[0] != '*')
            {
                MainWindow newWin = new MainWindow("Untitled- Amrit text Editor");
                newWin.Show();
                this.Close();
            }
            else
            {
                var result = MessageBox.Show("Do you want to save changes to this file ", "Save Changes", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                        Save_As_File();
                    MainWindow newWin = new MainWindow("Untitled- Amrit text Editor");
                    newWin.Show();
                    this.Close();
                }
                else if (result == MessageBoxResult.No)
                {
                    MainWindow newWin = new MainWindow("Untitled- Amrit text Editor");
                    newWin.Show();
                    this.Close();
                }
            }
        }



        //Function Name- Close_Option_Click
        //Parameters- object sender, RoutedEventArgs e
        //Return - void --> Nothing
        //Description- Closes the text Editor when user chooses to close it.
        public void Close_Option_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title[0] != '*')
            {
                this.Close();
            }
            else
            {
                var result = MessageBox.Show("Do you want to save changes to this file ", "Save Changes", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                     Save_As_File();
                    this.Close();
                }
                else if (result == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
        }




        //Function Name- Open_Option_Click
        //Parameters- object sender, RoutedEventArgs e
        //Return - void --> Nothing
        //Description- Opens the open text file of user choice when clicked on approriate menu option
        public void Open_Option_Click(object sender, RoutedEventArgs e)
        {
            if (this.Title[0] != '*')
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                MainWindow newWin = new MainWindow("Untitled- Amrit text Editor");
                newWin.text_area.AppendText(File.ReadAllText(fileDialog.FileName).ToString());
                newWin.Show();
                newWin.Title = fileDialog.FileName+ "- Amrit text Editor";
                this.Close();
            }
            else
            {
                var result = MessageBox.Show("Do you want to save changes to this file ", "Save Changes", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    Save_As_File();
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.ShowDialog();
                    MainWindow newWin = new MainWindow("Untitled- Amrit text Editor");
                    newWin.text_area.AppendText(File.ReadAllText(fileDialog.FileName).ToString());
                    newWin.Show();
                    newWin.Title = fileDialog.FileName + "- Amrit text Editor";
                    this.Close();
                }
                else if (result == MessageBoxResult.No)
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.ShowDialog();
                    MainWindow newWin = new MainWindow("Untitled- Amrit text Editor");
                    newWin.text_area.AppendText(File.ReadAllText(fileDialog.FileName).ToString());
                    newWin.Show();
                    newWin.Title = fileDialog.FileName + "- Amrit text Editor";
                    this.Close();
                }
            }
        }

    }
}
