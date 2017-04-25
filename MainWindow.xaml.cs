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

namespace NetCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        bool checkInputFlag = true;





        //Переводим из десятичной в двоичную
        string decToBin(string input)
        {
            string output = "";
            if ("" == input) return "";
            int x = int.Parse(input);
            while (x > 0)
            {
                output = x % 2 + output;
                x /= 2;
            }
            if ("" == output)
            {
                output = "0";
            }
            if (output.Length < 8)
            {
                int l = output.Length;
                for (int i = 0; i < 8 - l; i++)
                {
                    output = "0" + output;
                }
            }
            return output;
        }

        //Переводим из двоичной в десятичную
        string binToDec(string input)
        {
            string output = "";
            int temp = 0;
            for (int i = 0; i < input.Length; i++)
            {
                temp += int.Parse(input[i].ToString()) * (2 ^ i);
            }

            return temp.ToString();
        }

        string decimalCut(string input)
        {
            try
            {
                if (int.Parse(input) > 255)
                    return "255";
            }
            catch
            {
                return "";
            }
            return input;
        }

        string binaryCut(string input)
        {
            string output = null;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '0' || input[i] != '1')
                {
                    output += "1";
                }
            }
            if (output.Length > 8)
                return "11111111";
            else
            {
                return output;
            }


        }


        void setSelection(TextBox tb)
        {
            tb.Select(0, tb.Text.Length);
        }



        //Секция десятичного вида IP адреса
        private void ipv4_dec_1oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(ipv4_dec_1oct);
        }

        private void ipv4_dec_1oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(ipv4_dec_1oct);
        }

        private void ipv4_dec_1oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {


                ipv4_dec_1oct.Text = decimalCut(ipv4_dec_1oct.Text);
                ipv4_dec_1oct.SelectionStart = ipv4_dec_1oct.Text.Length;

                if (null != ipv4_bin_1oct)
                {
                    ipv4_bin_1oct.Text = decToBin(ipv4_dec_1oct.Text);
                }
            }
        }

        private void ipv4_dec_2oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(ipv4_dec_2oct);
        }

        private void ipv4_dec_2oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(ipv4_dec_2oct);
        }
        private void ipv4_dec_2oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {
                ipv4_dec_2oct.Text = decimalCut(ipv4_dec_2oct.Text);
                ipv4_dec_2oct.SelectionStart = ipv4_dec_2oct.Text.Length;

                if (null != ipv4_bin_2oct)
                {
                    ipv4_bin_2oct.Text = decToBin(ipv4_dec_2oct.Text);
                }
            }
        }
        private void ipv4_dec_3oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(ipv4_dec_3oct);
        }

        private void ipv4_dec_3oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(ipv4_dec_3oct);
        }
        private void ipv4_dec_3oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {
                ipv4_dec_3oct.Text = decimalCut(ipv4_dec_3oct.Text);
                ipv4_dec_3oct.SelectionStart = ipv4_dec_3oct.Text.Length;

                if (null != ipv4_bin_3oct)
                {
                    ipv4_bin_3oct.Text = decToBin(ipv4_dec_3oct.Text);
                }
            }
        }
        private void ipv4_dec_4oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(ipv4_dec_4oct);
        }

        private void ipv4_dec_4oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(ipv4_dec_4oct);
        }
        private void ipv4_dec_4oct_GotFocus(object sender, RoutedEventArgs e)
        {
            setSelection(ipv4_dec_4oct);
        }
        private void ipv4_dec_4oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {
                ipv4_dec_4oct.Text = decimalCut(ipv4_dec_4oct.Text);
                ipv4_dec_4oct.SelectionStart = ipv4_dec_4oct.Text.Length;

                if (null != ipv4_bin_4oct)
                {
                    ipv4_bin_4oct.Text = decToBin(ipv4_dec_4oct.Text);
                }
            }
        }


        private void mask_dec_1oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(mask_dec_1oct);
        }

        private void mask_dec_1oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(mask_dec_1oct);
        }

        private void mask_dec_1oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {
                mask_dec_1oct.Text = decimalCut(mask_dec_1oct.Text);
                mask_dec_1oct.SelectionStart = mask_dec_1oct.Text.Length;

                if (null != mask_bin_1oct)
                {
                    mask_bin_1oct.Text = decToBin(mask_dec_1oct.Text);
                }
            }
        }

        private void mask_dec_2oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(mask_dec_2oct);
        }

        private void mask_dec_2oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(mask_dec_2oct);
        }
        private void mask_dec_2oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {
                mask_dec_2oct.Text = decimalCut(mask_dec_2oct.Text);
                mask_dec_2oct.SelectionStart = mask_dec_2oct.Text.Length;

                if (null != mask_bin_2oct)
                {
                    mask_bin_2oct.Text = decToBin(mask_dec_2oct.Text);
                }
            }
        }
        private void mask_dec_3oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(mask_dec_3oct);
        }

        private void mask_dec_3oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(mask_dec_3oct);
        }
        private void mask_dec_3oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {
                mask_dec_3oct.Text = decimalCut(mask_dec_3oct.Text);
                mask_dec_3oct.SelectionStart = mask_dec_3oct.Text.Length;

                if (null != mask_bin_3oct)
                {
                    mask_bin_3oct.Text = decToBin(mask_dec_3oct.Text);
                }
            }
        }
        private void mask_dec_4oct_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(mask_dec_4oct);
        }

        private void mask_dec_4oct_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(mask_dec_4oct);
        }
        private void mask_dec_4oct_GotFocus(object sender, RoutedEventArgs e)
        {
            setSelection(mask_dec_4oct);
        }
        private void mask_dec_4oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkInputFlag)
            {
                mask_dec_4oct.Text = decimalCut(mask_dec_4oct.Text);
                mask_dec_4oct.SelectionStart = mask_dec_4oct.Text.Length;

                if (null != mask_bin_4oct)
                {
                    mask_bin_4oct.Text = decToBin(mask_dec_4oct.Text);
                }
            }
        }
        //Редактирование ip адреса в двоичном виде

        private void ipv4_bin_1oct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (null != ipv4_dec_1oct)
            {
                ipv4_dec_1oct.Text = binToDec(binaryCut(ipv4_bin_1oct.Text));
            }

        }

        private void BinChecker_Checked(object sender, RoutedEventArgs e)
        {
            ipv4_dec_1oct.IsEnabled = true;
            ipv4_dec_2oct.IsEnabled = true;
            ipv4_dec_3oct.IsEnabled = true;
            ipv4_dec_4oct.IsEnabled = true;
            mask_dec_1oct.IsEnabled = true;
            mask_dec_2oct.IsEnabled = true;
            mask_dec_3oct.IsEnabled = true;
            mask_dec_4oct.IsEnabled = true;

            ipv4_bin_1oct.IsEnabled = false;
            ipv4_bin_2oct.IsEnabled = false;
            ipv4_bin_3oct.IsEnabled = false;
            ipv4_bin_4oct.IsEnabled = false;
            mask_bin_1oct.IsEnabled = false;
            mask_bin_2oct.IsEnabled = false;
            mask_bin_3oct.IsEnabled = false;
            mask_bin_4oct.IsEnabled = false;
            checkInputFlag = !BinChecker.IsChecked.Value;
        }

        private void DecCheker_Checked(object sender, RoutedEventArgs e)
        {
            ipv4_bin_1oct.IsEnabled = true;
            ipv4_bin_2oct.IsEnabled = true;
            ipv4_bin_3oct.IsEnabled = true;
            ipv4_bin_4oct.IsEnabled = true;
            mask_bin_1oct.IsEnabled = true;
            mask_bin_2oct.IsEnabled = true;
            mask_bin_3oct.IsEnabled = true;
            mask_bin_4oct.IsEnabled = true;

            ipv4_dec_1oct.IsEnabled = false;
            ipv4_dec_2oct.IsEnabled = false;
            ipv4_dec_3oct.IsEnabled = false;
            ipv4_dec_4oct.IsEnabled = false;
            mask_dec_1oct.IsEnabled = false;
            mask_dec_2oct.IsEnabled = false;
            mask_dec_3oct.IsEnabled = false;
            mask_dec_4oct.IsEnabled = false;
            checkInputFlag = !BinChecker.IsChecked.Value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
