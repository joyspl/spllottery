using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class CheckDigitCalculation
    {
        public string GenCheckDigit(string LotteryNoWithId)
        {
            StringBuilder strFinalNo = new StringBuilder();
            string LotteryNo = LotteryNoWithId;// "1430AA0123456";
            int NumericValue = 0;
            int LotteryLen = LotteryNo.Length;
            string AlphaValue = Regex.Replace(LotteryNo, @"[^A-Z]+", String.Empty);// 99
            foreach (char Chr in AlphaValue)
            {
                NumericValue = Convert.ToInt16(NumericValue.ToString() + NumericValueForAlphabet(Chr.ToString()).ToString());
            }
            if (NumericValue > 0)
            {
                LotteryNo = LotteryNo.Replace(AlphaValue, NumericValue.ToString());//1430990123456
            }
            string WeightNo = "9731";
            Int16 WeightCnt = Convert.ToInt16((LotteryNo.Length / WeightNo.Length) + 1);
            for (int iCnt = 0; iCnt < WeightCnt; iCnt++)
            {
                WeightNo = WeightNo + WeightNo;
            }
            WeightNo = WeightNo.Substring(0, LotteryNo.Length);//9731973197319

            int[] FnLNo = new int[WeightNo.Length];
            for (int i = 0; i < WeightNo.Length; i++)
            {
                FnLNo[i] = Convert.ToInt16(WeightNo[i].ToString()) * Convert.ToInt16(LotteryNo[i].ToString());
            }
            //FnLNo[i]==9 , 28, 9, 0, 81, 63, 0,1,18,21,12,5,54
            for (int i = 0; i < FnLNo.Length; i++)
            {
                int MultiplyWeight = 0;
                for (int j = 0; j < FnLNo[i].ToString().Length; j++)
                {
                    MultiplyWeight = MultiplyWeight + Convert.ToInt16(FnLNo[i].ToString()[j].ToString());
                }
                FnLNo[i] = MultiplyWeight;

            }
            //FnLNo[i]==9 , 10, 9, 0, 9, 9, 0,1,9,3,3,5,9
            int sumOfWeight = 0;
            for (int i = 0; i < FnLNo.Length; i++)
            {
                sumOfWeight = Convert.ToInt16(sumOfWeight) + Convert.ToInt16(FnLNo[i]);
            }
            //sumOfWeight==76
            int RemainderOfDigit = sumOfWeight % LotteryNo.Length; //11
            int remaningCheckDigit = LotteryNo.Length - RemainderOfDigit;

            return remaningCheckDigit.ToString();
        }
        public int NumericValueForAlphabet(string AlphaValue)
        {
            AlphaValue = AlphaValue.ToUpper();
            int NumericValue = 0;
            if (AlphaValue == "A" || AlphaValue == "J" || AlphaValue == "S")
            {
                NumericValue = 9;
            }
            else if (AlphaValue == "B" || AlphaValue == "K" || AlphaValue == "T")
            {
                NumericValue = 8;
            }
            else if (AlphaValue == "C" || AlphaValue == "L" || AlphaValue == "U")
            {
                NumericValue = 7;
            }
            else if (AlphaValue == "D" || AlphaValue == "M" || AlphaValue == "V")
            {
                NumericValue = 6;
            }
            else if (AlphaValue == "E" || AlphaValue == "N" || AlphaValue == "W")
            {
                NumericValue = 5;
            }
            else if (AlphaValue == "F" || AlphaValue == "O" || AlphaValue == "X")
            {
                NumericValue = 4;
            }
            else if (AlphaValue == "G" || AlphaValue == "P" || AlphaValue == "Y")
            {
                NumericValue = 3;
            }
            else if (AlphaValue == "H" || AlphaValue == "Q" || AlphaValue == "Z")
            {
                NumericValue = 2;
            }
            else if (AlphaValue == "I" || AlphaValue == "R")
            {
                NumericValue = 1;
            }
            return NumericValue;
        }
    }
}
