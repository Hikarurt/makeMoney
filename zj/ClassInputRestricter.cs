using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace zj
{
    class ClassInputRestricter
    {

        public static bool IsInteger(string inputString)
        {

            return Regex.IsMatch(inputString,@"^([+-]?)\d*$");
        }

        public static bool IsUnInteger(string inputString)
        {

            return Regex.IsMatch(inputString, @"^[0-9]*$");
        }


        public static bool IsValidDecimal(string inputString)
        {
            return Regex.IsMatch(inputString, @"^([-]{0,1})(\d*)([.]{0,1})(\d{0,2})$");
        }

        public static bool LenCheck(string inputString,int minLen,int maxLen)
        {
            if (inputString.Length >= minLen && inputString.Length <= maxLen)
                return true;
            else
                return false;
        }

        public static void CheckInput(object sender,KeyPressEventArgs e)
        {
            string tbContent=((TextBox)sender).Text;
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '-' && e.KeyChar !='.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                if(tbContent.Contains('.'))
                    e.Handled = true;
            }
            if (e.KeyChar == '-')
            {
                if (tbContent.Contains('-'))
                    e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                SendKeys.Send("{TAB}");
            }
        }

        public static void CheckNum(object sender)
        {
            if (!ClassInputRestricter.IsUnInteger(((TextBox)sender).Text.Trim()))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Substring(0, ((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).Select(((TextBox)sender).SelectionStart, ((TextBox)sender).Text.Length);
            }
        }


        public static void CheckChange(object sender)
        {

            if (!ClassInputRestricter.IsValidDecimal(((TextBox)sender).Text.Trim().Replace(",", "")))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Substring(0, ((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).Select(((TextBox)sender).SelectionStart, ((TextBox)sender).Text.Length);
            }
        }
        //判断是否有效数字，并进行会计计数
        public static void CheckFormat(object sender)
        {
            string tempContent = ((TextBox)sender).Text.Trim();
            ((TextBox)sender).Text = tempContent.Replace(",", "");
            if (tempContent == "" || tempContent == "-")
            {
                ((TextBox)sender).Text = "0.00";
            }
            else
            {
                double tempResult = double.Parse(tempContent);
                string strResult = tempResult.ToString("n");
                ((TextBox)sender).Text = strResult;
            }
        }
        public static void CheckNull(object sender)
        {
            string tempContent = ((TextBox)sender).Text.Trim();
            if (tempContent == "")
            {
                ((TextBox)sender).Text = "0";
            }
        }


        public static void EnterToTab(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
