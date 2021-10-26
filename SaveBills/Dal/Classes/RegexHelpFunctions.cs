using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dal.Classes
{
    public static class RegexHelpFunctions
    {
        public const string EMPTY_LINES_1 = @"\n\r\n";
        public const string EMPTY_LINES_2 = @"\n\n";
        public const string DATE_PATTERN = @"(\d{2}\s?[/\.-]{1}\s?\d{1,2}\s?[/\.-]{1}\s?\d{2,4})";
        public const string DATE_TIME_PATTERN =  "("+DATE_PATTERN + @"\s*\d{2}:\d{2}:\d{2} ) | ( \s*\d{2}:\d{2}:\d{2}" + DATE_PATTERN+ ")";
        public const string CONCAT_DETAILS_PATTERN = @"((טלפון) | (פקס) | (טל) | (נייד) [\r\s:-]*\d{2,3}-?\d*)| (מייל .*@)";
        public const string BILL_NUM_PATTERN = @"(חשבונית\s*מס'?\s*/\s*) | ('מספר\s*(קבלה)|(חשבונית)) ";
        public const string PRIVATE_OR_PUBLIC_CAMPANY_PATTERN = @"((ח[\.. ']?פ[\. '] ?) | (ע[.\. ']?מ[\. '] ?) | (ע[.\. ']?פ[\. '] ?))[\s\.:]*\d{4}";
        public const string SUM_FOR_PAY_PATTERN = @"((מע.?מ) | ( סה.?כ ) | ( תשלום ) | ( סכום)){1}[:\s\n\t\r₪]*" + Product.PRICE_PATTERN;
        public const string WORDS_TO_REMOVE_PATTERN = @"(תאריך) | (כרטיס אשראי) | (תשלום) | (תשלומים) | (כרטיס) | ( תשלום ) | ( סכום) | (תאריך תפוגה) | (צ'ק) | (מע.?מ) | (אחריות) | (אישור)";
        public const string DISCOUND_PATTERN = @"(הנחה)?.*-" + Product.PRICE_PATTERN;
        
        public static string RemoveEmptyLines(this string str)
        {

            return str.ReplacePattern(EMPTY_LINES_1,"\n").ReplacePattern(EMPTY_LINES_2,"\n").ReplacePattern(EMPTY_LINES_2,"\n");
        }
        public static string RemoveLinesContains(this string str, string pattern)
        {
            if(str =="" ||pattern ==null ||pattern == "")
            {
                return str;
            }
            Regex rgx = new Regex(pattern,RegexOptions.IgnorePatternWhitespace);
            var results =rgx.Matches(str);
            foreach (Match res in results)
            {
                int ind = str.IndexOf(res.Value);
                if (ind != -1)
                {
                    int lineBegin = Math.Max(0,str.LastIndexOf('\n', ind));
                    int lineEnd = str.IndexOf('\n', ind + res.Length);
                    if (lineEnd == -1)
                        lineEnd = str.Length - 1;
                    str = str.Substring(0, lineBegin) + str.Substring(lineEnd, str.Length - lineEnd - 1);
                }
            }
            return str;
        }
        public static string RemovePatternIgnoreWhiteSpaces(this string str, string pattern)
        {
            if (str == "" || pattern == null || pattern == "")
            {
                return str;
            }
            Regex rgx = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            var results = rgx.Matches(str);
            foreach (Match res in results)
            {
                int ind = str.IndexOf(res.Value);
                if (ind != -1)
                {
                        str = str.Substring(0, ind) + str.Substring(ind + res.Length);
                }
            }
            return str;
        }

        public static string RemovePattern(this string str, string pattern)
        {
            if (str == "" || pattern == null || pattern == "")
            {
                return str;
            }
            Regex rgx = new Regex(pattern);
            var results = rgx.Matches(str);
            foreach (Match res in results)
            {
                int ind = str.IndexOf(res.Value);
                if (ind != -1)
                {
                    str = str.Substring(0, ind) + str.Substring(ind + res.Length);
                }
            }
            return str;
        }

        public static string ReplacePattern (this string str, string pattern ,string replace)
        {
            if (str == "" || pattern == null || pattern == "")
            {
                return str;
            }
            Regex rgx = new Regex(pattern);
            var results = rgx.Matches(str);
            foreach (Match res in results)
            {
                int ind = str.IndexOf(res.Value);
                if (ind != -1)
                {
                    str = str.Substring(0, ind) +replace+ str.Substring(ind + res.Length);
                }
            }
            return str;
        }
    }
}
