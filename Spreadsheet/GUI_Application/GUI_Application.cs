/// <summary>
///   Authors: Samuel Hancock, Hyrum Schenk
///   
///   We, Hyrum Schenk and Samuel Hancock, certify that I wrote this code from scratch and did not copy it in part or whole 
///   (with the Exception of the examples provided in the lecture notes) from  
///   another source.  All references used in the completion of the assignment are cited in my README file.
///   
/// File Contents
/// <para>
///     This GUI provides a way to interact with the 
///     Spreadsheet class; to enter values to cells, evalute
///     formulas based on values in cells and view the contents and values of cells.
///   </para>
/// 
/// </summary>


using System;
using System.Windows.Forms;


namespace SpreadsheetGUI
{

    class Spreadsheet_Window : ApplicationContext
    {
        
        private int formCount = 0;

        
        private static Spreadsheet_Window appContext;

        public static Spreadsheet_Window getAppContext()
        {
            if (appContext == null)
            {
                appContext = new Spreadsheet_Window();
            }
            return appContext;
        }

       
        private Spreadsheet_Window()
        {
        }

        
        public void RunForm(Form form)
        {
            formCount++;

            form.FormClosed += (o, e) => { if (--formCount <= 0) ExitThread(); };

            form.Show();
        }

    }

    class GUI_Application
    {


        
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Spreadsheet_Window appContext = Spreadsheet_Window.getAppContext();
            appContext.RunForm(new SimpleSpreadsheetGUI());
            Application.Run(appContext);

        }
    }
}
