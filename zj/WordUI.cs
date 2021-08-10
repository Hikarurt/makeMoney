using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace zj
{
    class WordUI
    {
        public void openWord(String strfilename)
        {
            object objOpt = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Application wapp = new Microsoft.Office.Interop.Word.Application();
            wapp.Visible = true;
            object fileName = strfilename;
            object isread = false;
            object isvisible = true;
            object miss = System.Reflection.Missing.Value;
            wapp.Documents.Open(ref fileName, ref miss, ref isread, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref isvisible, ref miss, ref miss, ref miss, ref miss);


            //Microsoft.Office.Interop.Word._Application xApp = new ApplicationClass();
            
            /*Microsoft.Office.Interop.Word._Document oWord;
            Microsoft.Office.Interop.Word._Application oDoc;
            oWord = new ApplicationClass();
            xApp.Visible = true;
            object fileName = @strfilename;
            oDoc = oWord.Documents.Open();
            */

        }
    }
}
