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

        string[,] maskTypes =
        {
            {"0", "0","0","0" },
            {"128", "0","0","0" },
            {"192", "0","0","0" },
            {"224", "0","0","0" },
            {"240", "0","0","0" },
            {"248", "0","0","0" },
            {"252", "0","0","0" },
            {"254", "0","0","0" },
            {"255", "0","0","0" },
            {"255", "128","0","0" },
            {"255", "192","0","0" },
            {"255", "224","0","0" },
            {"255", "240","0","0" },
            {"255", "248","0","0" },
            {"255", "252","0","0" },
            {"255", "254","0","0" },
            {"255", "255","0","0" },
            {"255", "255","128","0" },
            {"255", "255","192","0" },
            {"255", "255","224","0" },
            {"255", "255","240","0" },
            {"255", "255","248","0" },
            {"255", "255","252","0" },
            {"255", "255","254","0" },
            {"255", "255","255","0" },
            {"255", "255","255","128" },
            {"255", "255","255","192" },
            {"255", "255","255","224" },
            {"255", "255","255","240" },
            {"255", "255","255","248" },
            {"255", "255","255","252" },
            {"255", "255","255","254" },
            {"255", "255","255","255" }
        };

        //Bool section
        bool[] ipv4address = new bool[32];
        bool[] subnetmask = new bool[32];


        //Из двоичной в булевый массив

        bool[] binToBoolArr (TextBox input1, TextBox input2, TextBox input3, TextBox input4)
        {
            if ("" == input1.Text|| "" == input2.Text || "" == input3.Text || "" == input4.Text) return null;
            string temp = input1.Text + input2.Text + input3.Text + input4.Text;
            bool[] output = new bool[32];
            for(int i = 0; i<32; i++)
            {
                    output[i] = (temp[i] == '1'? true : false );
            }
            return output;
        }
        // Из булевого массива в двоичную строку
    

        string boolArrToBin (bool[] input)
        {
            if (input == null) return null;
            string output = "";
            foreach (bool i in input)
            {
                output += (i == true ? '1' : '0');
            }
            return output;
        }

        //Переводим из десятичной в двоичную
        string decToBin (string input)
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
            if (output.Length<8)
            {
                int l = output.Length;
                for(int i = 0; i < 8-l; i++)
                {
                    output = "0" + output;
                }
            }
            return output;
        }

        bool[] broadcastCounter(bool[] ip, bool[] mask)
        {
            if (ip == null||mask==null) return null;
            bool[] output = new bool[32];
            for(int i = 0; i<32; i++)
            {
                mask[i]=(mask[i] == true ? false : true);
            }
            for (int i = 0; i < 32; i++)
            {
                ip[i] = (mask[i] == true ? true:ip[i]);
            }
            return ip;
        }

        bool[] netCounter(bool[] ip, bool[] mask)
        {
            if (ip == null||mask==null) return null;
            bool[] output = new bool[32];
            for(int i = 0; i<32; i++)
            {
                output[i] = ip[i] & mask[i];
            }
            return output;
        }
        bool[] hostNumberCounter(bool[] ip, bool[] mask)
        {
            if (ip == null || mask == null) return null;
            for (int i = 0; i < 32; i++)
            {
                mask[i] = (mask[i] == true ? false : true);
            }
            for (int i = 0; i < 32; i++)
            {
                ip[i] = ip[i] & mask[i];
            }
            return ip;
        }

        string hostsInSubnetCouner(TextBox input)
        {
            ulong temp = Convert.ToUInt64(pow(2, 32 - int.Parse(input.Text))) - 2;
            return temp.ToString();
        }

        string firstUsableAddressCounter(bool[] ip, bool[] mask)
        {
            bool[] net = netCounter(ip, mask);
            string output = boolArrToBin(net);
            output = Convert.ToString(Convert.ToUInt32(output, 2) + 1, 2);
            int l = output.Length;
            if (l < 32)
            {
                for (int i = 0; i < 32 - l; i++)
                {
                    output = "0" + output;
                }
            }
                    return output;

        }
        string lastUsableAddressCount(bool[] ip, bool[] mask)
        {
            bool[] net = broadcastCounter(ip, mask);
            string output = boolArrToBin(net);
            output = Convert.ToString(Convert.ToUInt32(output, 2) - 1, 2);
            int l = output.Length;
            if (l < 32)
            {
                for(int i = 0; i<32- l; i++)
                {
                    output = "0" + output;
                }
            }
            
            return output;

        }

        //Переводим из двоичной в десятичную
        string binToDec(string input)
        {
            
            int temp = 0;
            
            for(int i = 0; i<input.Length; i++)
            {
                temp += int.Parse(input[i].ToString()) * (pow(2 ,(input.Length-i-1)));
                
            }
            
            return (temp).ToString();
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

        string maskDecimalCut(string input)
        {
            try
            {
                if (int.Parse(input) > 32)
                    return "32";
                else if (int.Parse(input) == 0)
                    return "0";
            }
            catch
            {
                return "";
            }
            return input;
        }
        /// <summary>
        /// Функция возведения в степень для int переменных
        /// </summary>
        /// <param name="x">Что возводим</param>
        /// <param name="a">Во что возводим</param>
        /// <returns>Возвращает intовое значение</returns>
        int pow(int x, int a)
        {
            int c = x;
            if (a == 0) return 1;
            for(int i=0; i<a-1; i++)
            {
                x *= c;
            }
            return x;
        }

        string binaryCut(string input)
        {
            string output = "";
            for (int i = 0; i < 8; i++)
            {
                output += input[i];
            }
            return output;
        }


        void setSelection(TextBox tb)
        {
            tb.Select(0, tb.Text.Length);
        }

        void setMask(TextBox tb)
        {
            switch (tb.Text)
            {
                case "0":
                    break;
                case "128":
                    break;
                case "192":
                    break;
                case "224":
                    break;
                case "240":
                    break;
                case "248":
                    break;
                case "252":
                    break;
                case "254":
                    break;
                case "255":
                    break;
                default:
                    tb.Text = "0";
                    break;

            }
        }

        void setHostSubnetCounter()
        {
            if (hosts_counter != null && mask_dec_slash !=null) hosts_counter.Content = hostsInSubnetCouner(mask_dec_slash);
        }

        void setHostNumber()
        {
            if (hostnumber_dec_oct1 != null & hostnumber_dec_oct2 != null & hostnumber_dec_oct3 != null & hostnumber_dec_oct4 != null & hostnumber_bin_oct1 != null & hostnumber_bin_oct2 != null & hostnumber_bin_oct3 != null & hostnumber_bin_oct4 != null)
            {

                bool[] temp = hostNumberCounter(binToBoolArr(ipv4_bin_1oct, ipv4_bin_2oct, ipv4_bin_3oct, ipv4_bin_4oct), (binToBoolArr(mask_bin_1oct, mask_bin_2oct, mask_bin_3oct, mask_bin_4oct)));
                string nettempbin = boolArrToBin(temp);
                hostnumber_bin_oct1.Content = nettempbin[0].ToString() + nettempbin[1].ToString() + nettempbin[2].ToString() + nettempbin[3].ToString() + nettempbin[4].ToString() + nettempbin[5].ToString() + nettempbin[6].ToString() + nettempbin[7].ToString();
                hostnumber_bin_oct2.Content = nettempbin[8].ToString() + nettempbin[9].ToString() + nettempbin[10].ToString() + nettempbin[11].ToString() + nettempbin[12].ToString() + nettempbin[13].ToString() + nettempbin[14].ToString() + nettempbin[15].ToString();
                hostnumber_bin_oct3.Content = nettempbin[16].ToString() + nettempbin[17].ToString() + nettempbin[18].ToString() + nettempbin[19].ToString() + nettempbin[20].ToString() + nettempbin[21].ToString() + nettempbin[22].ToString() + nettempbin[23].ToString();
                hostnumber_bin_oct4.Content = nettempbin[24].ToString() + nettempbin[25].ToString() + nettempbin[26].ToString() + nettempbin[27].ToString() + nettempbin[28].ToString() + nettempbin[29].ToString() + nettempbin[30].ToString() + nettempbin[31].ToString();
                hostnumber_dec_oct1.Content = binToDec(hostnumber_bin_oct1.Content.ToString()).ToString();
                hostnumber_dec_oct2.Content = binToDec(hostnumber_bin_oct2.Content.ToString()).ToString();
                hostnumber_dec_oct3.Content = binToDec(hostnumber_bin_oct3.Content.ToString()).ToString();
                hostnumber_dec_oct4.Content = binToDec(hostnumber_bin_oct4.Content.ToString()).ToString();
            }
        }

        void setNetAddress()
        {
            if (netaddress_dec_oct1 != null & netaddress_dec_oct2 != null & netaddress_dec_oct3 != null & netaddress_dec_oct4 != null & netaddress_bin_oct1 != null & netaddress_bin_oct2 != null & netaddress_bin_oct3 != null & netaddress_bin_oct4 != null)
            {

                bool[] temp = netCounter(binToBoolArr(ipv4_bin_1oct, ipv4_bin_2oct, ipv4_bin_3oct, ipv4_bin_4oct), (binToBoolArr(mask_bin_1oct, mask_bin_2oct, mask_bin_3oct, mask_bin_4oct)));
                string nettempbin = boolArrToBin(temp);
                netaddress_bin_oct1.Content = nettempbin[0].ToString() + nettempbin[1].ToString() + nettempbin[2].ToString() + nettempbin[3].ToString() + nettempbin[4].ToString() + nettempbin[5].ToString() + nettempbin[6].ToString() + nettempbin[7].ToString();
                netaddress_bin_oct2.Content = nettempbin[8].ToString() + nettempbin[9].ToString() + nettempbin[10].ToString() + nettempbin[11].ToString() + nettempbin[12].ToString() + nettempbin[13].ToString() + nettempbin[14].ToString() + nettempbin[15].ToString();
                netaddress_bin_oct3.Content = nettempbin[16].ToString() + nettempbin[17].ToString() + nettempbin[18].ToString() + nettempbin[19].ToString() + nettempbin[20].ToString() + nettempbin[21].ToString() + nettempbin[22].ToString() + nettempbin[23].ToString();
                netaddress_bin_oct4.Content = nettempbin[24].ToString() + nettempbin[25].ToString() + nettempbin[26].ToString() + nettempbin[27].ToString() + nettempbin[28].ToString() + nettempbin[29].ToString() + nettempbin[30].ToString() + nettempbin[31].ToString();
                netaddress_dec_oct1.Content = binToDec(netaddress_bin_oct1.Content.ToString()).ToString();
                netaddress_dec_oct2.Content = binToDec(netaddress_bin_oct2.Content.ToString()).ToString();
                netaddress_dec_oct3.Content = binToDec(netaddress_bin_oct3.Content.ToString()).ToString();
                netaddress_dec_oct4.Content = binToDec(netaddress_bin_oct4.Content.ToString()).ToString();
            }
        }

        void setBroadcast()
        {
            if ( broadcast_dec_oct1 != null & broadcast_dec_oct2 != null & broadcast_dec_oct3 != null & broadcast_dec_oct4 != null & broadcast_bin_oct1 != null & broadcast_bin_oct2 != null & broadcast_bin_oct3 != null & broadcast_bin_oct4 != null)
            {

                bool[] temp = broadcastCounter(binToBoolArr(ipv4_bin_1oct, ipv4_bin_2oct, ipv4_bin_3oct, ipv4_bin_4oct), (binToBoolArr(mask_bin_1oct, mask_bin_2oct, mask_bin_3oct, mask_bin_4oct)));
                string nettempbin = boolArrToBin(temp);
                broadcast_bin_oct1.Content = nettempbin[0].ToString() + nettempbin[1].ToString() + nettempbin[2].ToString() + nettempbin[3].ToString() + nettempbin[4].ToString() + nettempbin[5].ToString() + nettempbin[6].ToString() + nettempbin[7].ToString();
                broadcast_bin_oct2.Content = nettempbin[8].ToString() + nettempbin[9].ToString() + nettempbin[10].ToString() + nettempbin[11].ToString() + nettempbin[12].ToString() + nettempbin[13].ToString() + nettempbin[14].ToString() + nettempbin[15].ToString();
                broadcast_bin_oct3.Content = nettempbin[16].ToString() + nettempbin[17].ToString() + nettempbin[18].ToString() + nettempbin[19].ToString() + nettempbin[20].ToString() + nettempbin[21].ToString() + nettempbin[22].ToString() + nettempbin[23].ToString();
                broadcast_bin_oct4.Content = nettempbin[24].ToString() + nettempbin[25].ToString() + nettempbin[26].ToString() + nettempbin[27].ToString() + nettempbin[28].ToString() + nettempbin[29].ToString() + nettempbin[30].ToString() + nettempbin[31].ToString();
                broadcast_dec_oct1.Content = binToDec(broadcast_bin_oct1.Content.ToString()).ToString();
                broadcast_dec_oct2.Content = binToDec(broadcast_bin_oct2.Content.ToString()).ToString();
                broadcast_dec_oct3.Content = binToDec(broadcast_bin_oct3.Content.ToString()).ToString();
                broadcast_dec_oct4.Content = binToDec(broadcast_bin_oct4.Content.ToString()).ToString();
            }
        }

        void setFirstHost()
        {
            if (minpool != null & firsthost_dec_oct1 != null & firsthost_dec_oct2 != null & firsthost_dec_oct3 != null & firsthost_dec_oct4 != null & firsthost_bin_oct1 != null & firsthost_bin_oct2 != null & firsthost_bin_oct3 != null & firsthost_bin_oct4 != null)
            {

                string nettempbin = firstUsableAddressCounter(binToBoolArr(ipv4_bin_1oct, ipv4_bin_2oct, ipv4_bin_3oct, ipv4_bin_4oct), (binToBoolArr(mask_bin_1oct, mask_bin_2oct, mask_bin_3oct, mask_bin_4oct)));
                
                firsthost_bin_oct1.Content = nettempbin[0].ToString() + nettempbin[1].ToString() + nettempbin[2].ToString() + nettempbin[3].ToString() + nettempbin[4].ToString() + nettempbin[5].ToString() + nettempbin[6].ToString() + nettempbin[7].ToString();
                firsthost_bin_oct2.Content = nettempbin[8].ToString() + nettempbin[9].ToString() + nettempbin[10].ToString() + nettempbin[11].ToString() + nettempbin[12].ToString() + nettempbin[13].ToString() + nettempbin[14].ToString() + nettempbin[15].ToString();
                firsthost_bin_oct3.Content = nettempbin[16].ToString() + nettempbin[17].ToString() + nettempbin[18].ToString() + nettempbin[19].ToString() + nettempbin[20].ToString() + nettempbin[21].ToString() + nettempbin[22].ToString() + nettempbin[23].ToString();
                firsthost_bin_oct4.Content = nettempbin[24].ToString() + nettempbin[25].ToString() + nettempbin[26].ToString() + nettempbin[27].ToString() + nettempbin[28].ToString() + nettempbin[29].ToString() + nettempbin[30].ToString() + nettempbin[31].ToString();
                firsthost_dec_oct1.Content = binToDec(firsthost_bin_oct1.Content.ToString()).ToString();
                firsthost_dec_oct2.Content = binToDec(firsthost_bin_oct2.Content.ToString()).ToString();
                firsthost_dec_oct3.Content = binToDec(firsthost_bin_oct3.Content.ToString()).ToString();
                firsthost_dec_oct4.Content = binToDec(firsthost_bin_oct4.Content.ToString()).ToString();
                minpool.Content = firsthost_dec_oct1.Content.ToString()+"." + firsthost_dec_oct2.Content.ToString() + "." + firsthost_dec_oct3.Content.ToString() + "." + firsthost_dec_oct4.Content.ToString();
            }
        }
        void setLastHost()
        {
            if (maxpool != null & lasthost_dec_oct1 != null & lasthost_dec_oct2 != null & lasthost_dec_oct3 != null & lasthost_dec_oct4 != null & lasthost_bin_oct1 != null & lasthost_bin_oct2 != null & lasthost_bin_oct3 != null & lasthost_bin_oct4 != null)
            {

                string nettempbin = lastUsableAddressCount(binToBoolArr(ipv4_bin_1oct, ipv4_bin_2oct, ipv4_bin_3oct, ipv4_bin_4oct), (binToBoolArr(mask_bin_1oct, mask_bin_2oct, mask_bin_3oct, mask_bin_4oct)));

                lasthost_bin_oct1.Content = nettempbin[0].ToString() + nettempbin[1].ToString() + nettempbin[2].ToString() + nettempbin[3].ToString() + nettempbin[4].ToString() + nettempbin[5].ToString() + nettempbin[6].ToString() + nettempbin[7].ToString();
                lasthost_bin_oct2.Content = nettempbin[8].ToString() + nettempbin[9].ToString() + nettempbin[10].ToString() + nettempbin[11].ToString() + nettempbin[12].ToString() + nettempbin[13].ToString() + nettempbin[14].ToString() + nettempbin[15].ToString();
                lasthost_bin_oct3.Content = nettempbin[16].ToString() + nettempbin[17].ToString() + nettempbin[18].ToString() + nettempbin[19].ToString() + nettempbin[20].ToString() + nettempbin[21].ToString() + nettempbin[22].ToString() + nettempbin[23].ToString();
                lasthost_bin_oct4.Content = nettempbin[24].ToString() + nettempbin[25].ToString() + nettempbin[26].ToString() + nettempbin[27].ToString() + nettempbin[28].ToString() + nettempbin[29].ToString() + nettempbin[30].ToString() + nettempbin[31].ToString();
                lasthost_dec_oct1.Content = binToDec(lasthost_bin_oct1.Content.ToString()).ToString();
                lasthost_dec_oct2.Content = binToDec(lasthost_bin_oct2.Content.ToString()).ToString();
                lasthost_dec_oct3.Content = binToDec(lasthost_bin_oct3.Content.ToString()).ToString();
                lasthost_dec_oct4.Content = binToDec(lasthost_bin_oct4.Content.ToString()).ToString();
                maxpool.Content = lasthost_dec_oct1.Content.ToString() + "." + lasthost_dec_oct2.Content.ToString() + "." + lasthost_dec_oct3.Content.ToString() + "." + lasthost_dec_oct4.Content.ToString();
            }
        }

        void setMaskError()
        {
            if (errorLbl != null)
            {
                bool flag = false;
                bool[] mask = binToBoolArr(mask_bin_1oct, mask_bin_2oct, mask_bin_3oct, mask_bin_4oct);
                foreach (bool a in mask)
                {
                    if (!a)
                    {
                        if (!flag) flag = true;
                        
                    }
                    else if (flag & a)
                    {
                        errorLbl.Content = "Error in subnet mask";
                        errorLbl.Visibility = Visibility.Visible;
                        break;
                    }
                    else errorLbl.Visibility = Visibility.Hidden;
                }
            }
        }

        void setAll()
        {
            setNetAddress();
            setBroadcast();
            setHostNumber();
            setHostSubnetCounter();
            setFirstHost();
            setLastHost();
            setMaskError();

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
            ipv4_dec_1oct.Text = decimalCut(ipv4_dec_1oct.Text);
            ipv4_dec_1oct.SelectionStart = ipv4_dec_1oct.Text.Length;

            if (null != ipv4_bin_1oct)
            {
                ipv4_bin_1oct.Text = decToBin(ipv4_dec_1oct.Text);
            }
            try
            {
                setAll();
            }
            catch { }

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
            ipv4_dec_2oct.Text = decimalCut(ipv4_dec_2oct.Text);
            ipv4_dec_2oct.SelectionStart = ipv4_dec_2oct.Text.Length;

            if (null != ipv4_bin_2oct)
            {
                ipv4_bin_2oct.Text = decToBin(ipv4_dec_2oct.Text);
            }
            try
            {
                setAll();
            }
            catch { }
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
            ipv4_dec_3oct.Text = decimalCut(ipv4_dec_3oct.Text);
            ipv4_dec_3oct.SelectionStart = ipv4_dec_3oct.Text.Length;

            if (null != ipv4_bin_3oct)
            {
                ipv4_bin_3oct.Text = decToBin(ipv4_dec_3oct.Text);
            }
            try
            {
                setAll();
            }
            catch { }
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
            ipv4_dec_4oct.Text = decimalCut(ipv4_dec_4oct.Text);
            ipv4_dec_4oct.SelectionStart = ipv4_dec_4oct.Text.Length;

            if (null != ipv4_bin_4oct)
            {
                ipv4_bin_4oct.Text = decToBin(ipv4_dec_4oct.Text);
            }
            try
            {
                setAll();
            }
            catch { }
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
            mask_dec_1oct.Text = decimalCut(mask_dec_1oct.Text);
            mask_dec_1oct.SelectionStart = mask_dec_1oct.Text.Length;

            if (null != mask_bin_1oct)
            {
                mask_bin_1oct.Text = decToBin(mask_dec_1oct.Text);
            }
            try
            {

                setAll();
            }
            catch { }
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
            mask_dec_2oct.Text = decimalCut(mask_dec_2oct.Text);
            mask_dec_2oct.SelectionStart = mask_dec_2oct.Text.Length;

            if (null != mask_bin_2oct)
            {
                mask_bin_2oct.Text = decToBin(mask_dec_2oct.Text);
            }
            try
            {
                setAll();
            }
            catch { }
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
            mask_dec_3oct.Text = decimalCut(mask_dec_3oct.Text);
            mask_dec_3oct.SelectionStart = mask_dec_3oct.Text.Length;

            if (null != mask_bin_3oct)
            {
                mask_bin_3oct.Text = decToBin(mask_dec_3oct.Text);
            }
            try
            {
                setAll();
            }
            catch { }
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
            mask_dec_4oct.Text = decimalCut(mask_dec_4oct.Text);
            mask_dec_4oct.SelectionStart = mask_dec_4oct.Text.Length;

            if (null != mask_bin_4oct)
            {
                mask_bin_4oct.Text = decToBin(mask_dec_4oct.Text);
            }
            try
            {
               setAll();
            }
            catch { }
        }

        private void mask_dec_slash_TextChanged(object sender, TextChangedEventArgs e)
        {

            mask_dec_slash.Text = maskDecimalCut(mask_dec_slash.Text);
            mask_dec_slash.SelectionStart = mask_dec_slash.Text.Length;
            if (null != mask_dec_1oct & null != mask_dec_2oct & null != mask_dec_3oct & null != mask_dec_4oct)
            {
                switch (mask_dec_slash.Text)
                {
                    case "0":
                        mask_dec_1oct.Text = maskTypes[0, 0];
                        mask_dec_2oct.Text = maskTypes[0, 1];
                        mask_dec_3oct.Text = maskTypes[0, 2];
                        mask_dec_4oct.Text = maskTypes[0, 3];
                        break;
                    case "1":
                        mask_dec_1oct.Text = maskTypes[1, 0];
                        mask_dec_2oct.Text = maskTypes[1, 1];
                        mask_dec_3oct.Text = maskTypes[1, 2];
                        mask_dec_4oct.Text = maskTypes[1, 3];
                        break;
                    case "2":
                        mask_dec_1oct.Text = maskTypes[2, 0];
                        mask_dec_2oct.Text = maskTypes[2, 1];
                        mask_dec_3oct.Text = maskTypes[2, 2];
                        mask_dec_4oct.Text = maskTypes[2, 3];
                        break;
                    case "3":
                        mask_dec_1oct.Text = maskTypes[3, 0];
                        mask_dec_2oct.Text = maskTypes[3, 1];
                        mask_dec_3oct.Text = maskTypes[3, 2];
                        mask_dec_4oct.Text = maskTypes[3, 3];
                        break;
                    case "4":
                        mask_dec_1oct.Text = maskTypes[4, 0];
                        mask_dec_2oct.Text = maskTypes[4, 1];
                        mask_dec_3oct.Text = maskTypes[4, 2];
                        mask_dec_4oct.Text = maskTypes[4, 3];
                        break;
                    case "5":
                        mask_dec_1oct.Text = maskTypes[5, 0];
                        mask_dec_2oct.Text = maskTypes[5, 1];
                        mask_dec_3oct.Text = maskTypes[5, 2];
                        mask_dec_4oct.Text = maskTypes[5, 3];
                        break;
                    case "6":
                        mask_dec_1oct.Text = maskTypes[6, 0];
                        mask_dec_2oct.Text = maskTypes[6, 1];
                        mask_dec_3oct.Text = maskTypes[6, 2];
                        mask_dec_4oct.Text = maskTypes[6, 3];
                        break;
                    case "7":
                        mask_dec_1oct.Text = maskTypes[7, 0];
                        mask_dec_2oct.Text = maskTypes[7, 1];
                        mask_dec_3oct.Text = maskTypes[7, 2];
                        mask_dec_4oct.Text = maskTypes[7, 3];
                        break;
                    case "8":
                        mask_dec_1oct.Text = maskTypes[8, 0];
                        mask_dec_2oct.Text = maskTypes[8, 1];
                        mask_dec_3oct.Text = maskTypes[8, 2];
                        mask_dec_4oct.Text = maskTypes[8, 3];
                        break;
                    case "9":
                        mask_dec_1oct.Text = maskTypes[9, 0];
                        mask_dec_2oct.Text = maskTypes[9, 1];
                        mask_dec_3oct.Text = maskTypes[9, 2];
                        mask_dec_4oct.Text = maskTypes[9, 3];
                        break;
                    case "10":
                        mask_dec_1oct.Text = maskTypes[10, 0];
                        mask_dec_2oct.Text = maskTypes[10, 1];
                        mask_dec_3oct.Text = maskTypes[10, 2];
                        mask_dec_4oct.Text = maskTypes[10, 3];
                        break;
                    case "11":
                        mask_dec_1oct.Text = maskTypes[11, 0];
                        mask_dec_2oct.Text = maskTypes[11, 1];
                        mask_dec_3oct.Text = maskTypes[11, 2];
                        mask_dec_4oct.Text = maskTypes[11, 3];
                        break;
                    case "12":
                        mask_dec_1oct.Text = maskTypes[12, 0];
                        mask_dec_2oct.Text = maskTypes[12, 1];
                        mask_dec_3oct.Text = maskTypes[12, 2];
                        mask_dec_4oct.Text = maskTypes[12, 3];
                        break;
                    case "13":
                        mask_dec_1oct.Text = maskTypes[13, 0];
                        mask_dec_2oct.Text = maskTypes[13, 1];
                        mask_dec_3oct.Text = maskTypes[13, 2];
                        mask_dec_4oct.Text = maskTypes[13, 3];
                        break;
                    case "14":
                        mask_dec_1oct.Text = maskTypes[14, 0];
                        mask_dec_2oct.Text = maskTypes[14, 1];
                        mask_dec_3oct.Text = maskTypes[14, 2];
                        mask_dec_4oct.Text = maskTypes[14, 3];
                        break;
                    case "15":
                        mask_dec_1oct.Text = maskTypes[15, 0];
                        mask_dec_2oct.Text = maskTypes[15, 1];
                        mask_dec_3oct.Text = maskTypes[15, 2];
                        mask_dec_4oct.Text = maskTypes[15, 3];
                        break;
                    case "16":
                        mask_dec_1oct.Text = maskTypes[16, 0];
                        mask_dec_2oct.Text = maskTypes[16, 1];
                        mask_dec_3oct.Text = maskTypes[16, 2];
                        mask_dec_4oct.Text = maskTypes[16, 3];
                        break;
                    case "17":
                        mask_dec_1oct.Text = maskTypes[17, 0];
                        mask_dec_2oct.Text = maskTypes[17, 1];
                        mask_dec_3oct.Text = maskTypes[17, 2];
                        mask_dec_4oct.Text = maskTypes[17, 3];
                        break;
                    case "18":
                        mask_dec_1oct.Text = maskTypes[18, 0];
                        mask_dec_2oct.Text = maskTypes[18, 1];
                        mask_dec_3oct.Text = maskTypes[18, 2];
                        mask_dec_4oct.Text = maskTypes[18, 3];
                        break;
                    case "19":
                        mask_dec_1oct.Text = maskTypes[19, 0];
                        mask_dec_2oct.Text = maskTypes[19, 1];
                        mask_dec_3oct.Text = maskTypes[19, 2];
                        mask_dec_4oct.Text = maskTypes[19, 3];
                        break;
                    case "20":
                        mask_dec_1oct.Text = maskTypes[20, 0];
                        mask_dec_2oct.Text = maskTypes[20, 1];
                        mask_dec_3oct.Text = maskTypes[20, 2];
                        mask_dec_4oct.Text = maskTypes[20, 3];
                        break;
                    case "21":
                        mask_dec_1oct.Text = maskTypes[21, 0];
                        mask_dec_2oct.Text = maskTypes[21, 1];
                        mask_dec_3oct.Text = maskTypes[21, 2];
                        mask_dec_4oct.Text = maskTypes[21, 3];
                        break;
                    case "22":
                        mask_dec_1oct.Text = maskTypes[22, 0];
                        mask_dec_2oct.Text = maskTypes[22, 1];
                        mask_dec_3oct.Text = maskTypes[22, 2];
                        mask_dec_4oct.Text = maskTypes[22, 3];
                        break;
                    case "23":
                        mask_dec_1oct.Text = maskTypes[23, 0];
                        mask_dec_2oct.Text = maskTypes[23, 1];
                        mask_dec_3oct.Text = maskTypes[23, 2];
                        mask_dec_4oct.Text = maskTypes[23, 3];
                        break;
                    case "24":
                        mask_dec_1oct.Text = maskTypes[24, 0];
                        mask_dec_2oct.Text = maskTypes[24, 1];
                        mask_dec_3oct.Text = maskTypes[24, 2];
                        mask_dec_4oct.Text = maskTypes[24, 3];
                        break;
                    case "25":
                        mask_dec_1oct.Text = maskTypes[25, 0];
                        mask_dec_2oct.Text = maskTypes[25, 1];
                        mask_dec_3oct.Text = maskTypes[25, 2];
                        mask_dec_4oct.Text = maskTypes[25, 3];
                        break;
                    case "26":
                        mask_dec_1oct.Text = maskTypes[26, 0];
                        mask_dec_2oct.Text = maskTypes[26, 1];
                        mask_dec_3oct.Text = maskTypes[26, 2];
                        mask_dec_4oct.Text = maskTypes[26, 3];
                        break;
                    case "27":
                        mask_dec_1oct.Text = maskTypes[27, 0];
                        mask_dec_2oct.Text = maskTypes[27, 1];
                        mask_dec_3oct.Text = maskTypes[27, 2];
                        mask_dec_4oct.Text = maskTypes[27, 3];
                        break;
                    case "28":
                        mask_dec_1oct.Text = maskTypes[28, 0];
                        mask_dec_2oct.Text = maskTypes[28, 1];
                        mask_dec_3oct.Text = maskTypes[28, 2];
                        mask_dec_4oct.Text = maskTypes[28, 3];
                        break;
                    case "29":
                        mask_dec_1oct.Text = maskTypes[29, 0];
                        mask_dec_2oct.Text = maskTypes[29, 1];
                        mask_dec_3oct.Text = maskTypes[29, 2];
                        mask_dec_4oct.Text = maskTypes[29, 3];
                        break;
                    case "30":
                        mask_dec_1oct.Text = maskTypes[30, 0];
                        mask_dec_2oct.Text = maskTypes[30, 1];
                        mask_dec_3oct.Text = maskTypes[30, 2];
                        mask_dec_4oct.Text = maskTypes[30, 3];
                        break;
                    case "31":
                        mask_dec_1oct.Text = maskTypes[31, 0];
                        mask_dec_2oct.Text = maskTypes[31, 1];
                        mask_dec_3oct.Text = maskTypes[31, 2];
                        mask_dec_4oct.Text = maskTypes[31, 3];
                        break;
                    case "32":
                        mask_dec_1oct.Text = maskTypes[32, 0];
                        mask_dec_2oct.Text = maskTypes[32, 1];
                        mask_dec_3oct.Text = maskTypes[32, 2];
                        mask_dec_4oct.Text = maskTypes[32, 3];
                        break;
                }
            }
        }

        private void mask_dec_slash_GotFocus(object sender, RoutedEventArgs e)
        {
            setSelection(mask_dec_slash);
        }

        private void mask_dec_slash_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            setSelection(mask_dec_slash);
        }

        private void mask_dec_slash_GotMouseCapture(object sender, MouseEventArgs e)
        {
            setSelection(mask_dec_slash);
        }

        private void mask_dec_1oct_LostFocus(object sender, RoutedEventArgs e)
        {
            setMask(mask_dec_1oct);
        }

        private void mask_dec_2oct_LostFocus(object sender, RoutedEventArgs e)
        {
            setMask(mask_dec_2oct);
        }

        private void mask_dec_3oct_LostFocus(object sender, RoutedEventArgs e)
        {
            setMask(mask_dec_3oct);
        }

        private void mask_dec_4oct_LostFocus(object sender, RoutedEventArgs e)
        {
            setMask(mask_dec_4oct);
        }
    }
}
