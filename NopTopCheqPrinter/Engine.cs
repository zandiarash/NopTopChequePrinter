using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NopTopCheqPrinter
{
    static class Engine
    {


        public static string cMonth(string s)
        {
            var res = "";
            switch (s)
            {
                case "01":
                    {
                        res = "فروردين";
                        break;
                    }
                case "02":
                    {
                        res = "ارديبهشت";
                        break;
                    }
                case "03":
                    {
                        res = "خرداد";
                        break;
                    }
                case "04":
                    {
                        res = "تير";
                        break;
                    }
                case "05":
                    {
                        res = "مرداد";
                        break;
                    }
                case "06":
                    {
                        res = "شهريور";
                        break;
                    }
                case "07":
                    {
                        res = "مهر";
                        break;
                    }
                case "08":
                    {
                        res = "آبان";
                        break;
                    }
                case "09":
                    {
                        res = "آذر";
                        break;
                    }
                case "10":
                    {
                        res = "دي";
                        break;
                    }
                case "11":
                    {
                        res = "بهمن";
                        break;
                    }
                case "12":
                    {
                        res = "اسفند";
                        break;
                    }
            }
            return res;
        }
        public static string GET_Number_To_PersianString(string TXT)
        {
            string RET = " ", STRVA = " ";
            string[] MainStr = STR_To_Int(TXT);
            int Q = 0;
            for (int i = MainStr.Length - 1; i >= 0; i--)
            {
                STRVA = " ";
                if (RET != " " && RET != null)
                    STRVA = " و ";
                RET = Convert_STR(GETCountStr(MainStr[i]), Q) + STRVA + RET;
                Q++;
            }
            if (RET == " " || RET == null || RET == "  ")
                RET = "صفر";
            return RET.Trim();
        }

        private static string[] STR_To_Int(string STR)
        {
            STR = GETCountStr(STR);
            string[] RET = new string[STR.Length / 3];
            int Q = 0;
            for (int I = 0; I < STR.Length; I += 3)
            {
                RET[Q] = STR.Substring(I, 3);
                Q++;
            }
            return RET;
        }

        private static string GETCountStr(string STR)
        {
            string RET = STR;
            int LEN = (STR.Length / 3 + 1) * 3 - STR.Length;
            if (LEN < 3)
            {
                for (int i = 0; i < LEN; i++)
                {
                    RET = "0" + RET;
                }
            }
            if (RET == "")
                return "000";
            return RET;
        }

        private static string Convert_STR(string INT, int Count)
        {
            string RET = "";
            //یک صد
            if (Count == 0)
            {
                if (INT.Substring(1, 1) == "1" && INT.Substring(2, 1) != "0")
                {
                    RET = GET_Number(3, Convert.ToInt32(INT.Substring(0, 1)), " ") + GET_Number(1, Convert.ToInt32(INT.Substring(2, 1)), "");
                }
                else
                {
                    string STR = GET_Number(0, Convert.ToInt32(INT.Substring(2, 1)), "");
                    RET = GET_Number(3, Convert.ToInt32(INT.Substring(0, 1)), GET_Number(2, Convert.ToInt32(INT.Substring(1, 1)), "") + STR) + GET_Number(2, Convert.ToInt32(INT.Substring(1, 1)), STR) + GET_Number(0, Convert.ToInt32(INT.Substring(2, 1)), "");
                }
            }
            //هزار
            else if (Count == 1)
            {
                RET = Convert_STR(INT, 0);
                RET += " هزار";
            }
            //میلیون
            else if (Count == 2)
            {
                RET = Convert_STR(INT, 0);
                RET += " میلیون";
            }
            //میلیارد
            else if (Count == 3)
            {
                RET = Convert_STR(INT, 0);
                RET += " میلیارد";
            }
            //میلیارد
            else if (Count == 4)
            {
                RET = Convert_STR(INT, 0);
                RET += " تیلیارد";
            }
            //میلیارد
            else if (Count == 5)
            {
                RET = Convert_STR(INT, 0);
                RET += " بیلیارد";
            }
            else
            {
                RET = Convert_STR(INT, 0);
                RET += Count.ToString();
            }
            return RET;
        }

        private static string GET_Number(int Count, int Number, string VA)
        {
            string RET = "";

            if (VA != "" && VA != null)
            {
                VA = " و ";
            }
            if (Count == 0 || Count == 1)
            {
                bool IsDah = Convert.ToBoolean(Count);
                string[] MySTR = new string[10];
                MySTR[1] = IsDah ? "یازده" : "یک" + VA;
                MySTR[2] = IsDah ? "دوازده" : "دو" + VA;
                MySTR[3] = IsDah ? "سیزده" : "سه" + VA;
                MySTR[4] = IsDah ? "چهارده" : "چهار" + VA;
                MySTR[5] = IsDah ? "پانزده" : "پنج" + VA;
                MySTR[6] = IsDah ? "شانزده" : "شش" + VA;
                MySTR[7] = IsDah ? "هفده" : "هفت" + VA;
                MySTR[8] = IsDah ? "هجده" : "هشت" + VA;
                MySTR[9] = IsDah ? "نوزده" : "نه" + VA;
                return MySTR[Number];
            }
            else if (Count == 2)
            {
                string[] MySTR = new string[10];
                MySTR[1] = "ده";
                MySTR[2] = "بیست" + VA;
                MySTR[3] = "سی" + VA;
                MySTR[4] = "چهل" + VA;
                MySTR[5] = "پنجاه" + VA;
                MySTR[6] = "شصت" + VA;
                MySTR[7] = "هفتاد" + VA;
                MySTR[8] = "هشتاد" + VA;
                MySTR[9] = "نود" + VA;
                return MySTR[Number];
            }
            else if (Count == 3)
            {
                string[] MySTR = new string[10];
                MySTR[1] = "یکصد" + VA;
                MySTR[2] = "دویست" + VA;
                MySTR[3] = "سیصد" + VA;
                MySTR[4] = "چهارصد" + VA;
                MySTR[5] = "پانصد" + VA;
                MySTR[6] = "ششصد" + VA;
                MySTR[7] = "هفتصد" + VA;
                MySTR[8] = "هشتصد" + VA;
                MySTR[9] = "نهصد" + VA;
                return MySTR[Number];
            }
            return RET;
        }
    }
    public class EngineNew
    {
        private static string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        public static string GetIntPart(string str)
        {
            return new String(str.Where(Char.IsDigit).ToArray());
        }
        private static string getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }
        public static string num2str(string snum)
        {
            string stotal = "";
            if (snum == "") return "صفر";
            if (snum == "0")
            {
                return yakan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int L = snum.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(snum.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }
}
