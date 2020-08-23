using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Validate
{
    public static class Common
    {
        public static XElement RipFile { get; set; }
        public static string Filename { get; set; }
        public static ArrayList ErrorList;

        public static string ShortFileName()
        {
            string str = Common.Filename.Split('\\')[Common.Filename.Split('\\').Length - 1];
            return str.Replace(".rip", "");
        }
        public enum ElementType
        {
            Content = 0,
            Template = 1
        }
        public static XElement FindElement(string elementToFind, ElementType elementType = ElementType.Content)
        {
            var element = RipFile.Descendants("Name")
                  .Where(e => e.Value == elementToFind && e.Parent.Name == elementType.ToString())
                  .FirstOrDefault();

            return element;
        }
        public static DataSet GetDbResult(string commandText)
        {
            string scrapename = ConfigurationManager.AppSettings["scrapeFileName"];
            DataSet dataSet = new DataSet();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["burnin"].ToString();
                conn.Open();
                using (SqlCommand command = new SqlCommand(commandText, conn))
                {
                    if (commandText.ToLower().Contains("like"))
                        command.CommandText = commandText.ToLower().Replace("@scrapename", scrapename);
                    else
                        command.Parameters.AddWithValue("scrapename", scrapename);

                    //SqlDataReader reader = command.ExecuteReader();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataSet);
                }
            }
            return dataSet;
        }
        public static string ConvertDataRowToJson(DataTable dt, string[] columns, int noOfRowsToConvert)
        {
            string returnval = string.Empty;
            string columnName;
            for (int i = 0; i < noOfRowsToConvert; i++)
            {
                returnval = "{";
                foreach (string str in columns)
                {
                    columnName = str.ToLower().Trim();
                    returnval += @"""" + str + @""":""" + dt.Rows[i][columnName] + @""",";
                }
                returnval = returnval.Substring(0, returnval.Length - 1) + "},";
            }
            return returnval.Substring(0, returnval.Length - 1);
        }
        public static int getMaxLengthFromDataColumn(DataTable dt, string columnName)
        {
            columnName = columnName.ToLower().Trim();
            int rowindex = 0;
            int length = dt.Rows[0][columnName].ToString().Length;
            do
            {
                if (length < dt.Rows[rowindex][columnName].ToString().Length)
                    length = dt.Rows[rowindex][columnName].ToString().Length;

                rowindex += 1;
            } while (rowindex < 9 && rowindex < dt.Rows.Count);

            return length;
        }
        public static string ConvertDataTableToString(DataTable dt, string[] columns,int noOfRowsToPrint=3)
        {
            if (dt.Rows.Count == 0)
                return string.Empty;

            string returnval = string.Empty;
            int[] columnLengths = new int[columns.Length];

            int columnIndex = 0;
            int columnLength;
            foreach (string columnName in columns)
            {
                columnLength= getMaxLengthFromDataColumn(dt, columnName);
                if (columnLength < columnName.Length)
                    columnLength = columnName.Length;
                columnLength += 3;
                columnLengths[columnIndex] = columnLength;
                returnval += string.Format("{0,-" + columnLength.ToString() + "}", columnName.ToLower().Trim());
                
                columnIndex += 1;
            }
            //print all table column headers
            Console.WriteLine(returnval);

            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                columnIndex = 0;
                returnval = string.Empty;
                foreach (string str in columns)
                {
                    returnval += string.Format("{0,-" + columnLengths[columnIndex].ToString() + "}", dt.Rows[rowIndex][str.ToLower().Trim()]);
                    columnIndex += 1;
                }
                //returnval += Environment.NewLine;
                Console.WriteLine(returnval);
                if (rowIndex >= noOfRowsToPrint-1)
                    break;
            }
            return returnval;
        }
    }
}
