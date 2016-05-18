using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Overlay
{
    class Textc
    {
        public String text;
        public String font;
        public float size=1;
        public String color="";
        public Textc(string text1, string type1)
        {
            text = text1;
            font = type1;
        }
        public Textc transform( int num)
        {

            Textc t1 = new Textc("", "");
            
            
            Textc t2 = new Textc("", "");
            Textc t3 = new Textc("", "");
            string pattern = @"(.*)\[b\](.+?)\[\/b\](.*)";
    //        regExp = @"\[i\](.+?)\[\/i\]";
  //          regExp = @"\[u\](.+?)\[\/u\]";
//regExp =@"\[size=([^\]]+)\]([^\]]+)\[\/size\]";
  //          regExp =@"\[color=([^\]]+)\]([^\]]+)\[\/color\]";
            //@\[(\w)\](.+?)\[\/\1\]




  //  strTextToReplace = regExp.Replace(strTextToReplace, "<b>$1</b>"); 

            foreach (Match match in Regex.Matches(text, pattern, RegexOptions.IgnoreCase))
            {
                t1.text = match.Groups[1].Value;
                t2.text = match.Groups[2].Value;
                t3.text = match.Groups[3].Value;
            }


            t1.font = font;
            t3.font = font;


            t2.font = "bold";




            if(num==1)
            return t1;
            if(num==2)
                return t2;
            else return t3;
                        

        }



    }

}




