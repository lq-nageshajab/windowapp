using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Validate
{
    public static class SQLResults
    {
        public static DataSet GetDbResult()
        {
            string scrapeName = ConfigurationManager.AppSettings["scrapeFileName"];
            //burni query
            string[] burninColumns = ConfigurationManager.AppSettings["burnInColumnsToPick"].Split(new string[] { "," }, StringSplitOptions.None);
            string burninQuery = ConfigurationManager.AppSettings["burninQuery"];
            DataSet ds = Common.GetDbResult(burninQuery);
            Console.WriteLine("\n********************burn in result ******************");
            Common.ConvertDataTableToString(ds.Tables[0], burninColumns);

            //sandbox query
            string[] sandboxColumns = ConfigurationManager.AppSettings["sandboxColumnstoPick"].Split(new string[] { "," }, StringSplitOptions.None);
            string sandboxQuery = ConfigurationManager.AppSettings["sandboxQuery"];
            ds = Common.GetDbResult(sandboxQuery);
            Console.WriteLine("\n********************sandbox result ******************");
            Common.ConvertDataTableToString(ds.Tables[0], sandboxColumns);

            //last success data - sandbox - RowChangeReason <> Removed
            Console.WriteLine("\n*******last success data - sandbox - RowChangeReason <> Removed*******");
            string[] ScrapedRowColumns = ConfigurationManager.AppSettings["ScrapedRowColumns"].Split(new string[] { "," }, StringSplitOptions.None);
            string successDataQuery = string.Format("SELECT TOP 10 {0} FROM Sandbox.Jobs.dbo.DimJobInfo WHERE ScrapeFileName LIKE '@scrapename%' AND RowIsCurrent ='Y' AND RowChangeReason <> 'Removed' ORDER BY JobInfoKey DESC", ConfigurationManager.AppSettings["ScrapedRowColumns"]);
            ds = Common.GetDbResult(successDataQuery);
            string datatable = Common.ConvertDataTableToString(ds.Tables[0], ScrapedRowColumns);

            //last success data - sandbox - RowChangeReason = Removed
            Console.WriteLine("\n*******last success data - sandbox - RowChangeReason = Removed*******");
            successDataQuery = string.Format("SELECT TOP 10 {0} FROM Sandbox.Jobs.dbo.DimJobInfo WHERE ScrapeFileName LIKE '@scrapename%' AND RowIsCurrent ='Y' AND RowChangeReason = 'Removed' ORDER BY JobInfoKey DESC", ConfigurationManager.AppSettings["ScrapedRowColumns"]);
            ds = Common.GetDbResult(successDataQuery);
            datatable = Common.ConvertDataTableToString(ds.Tables[0], ScrapedRowColumns);

            //last success data - prod - RowChangeReason <> Removed
            Console.WriteLine("\n*******last success data - prod - RowChangeReason <> Removed*******");
            successDataQuery = string.Format("SELECT TOP 10 {0} FROM prod.Jobs.dbo.DimJobInfo WHERE ScrapeFileName LIKE '@scrapename%' AND RowIsCurrent ='Y' AND RowChangeReason <> 'Removed' ORDER BY JobInfoKey DESC", ConfigurationManager.AppSettings["ScrapedRowColumns"]);
            ds = Common.GetDbResult(successDataQuery);
            datatable = Common.ConvertDataTableToString(ds.Tables[0], ScrapedRowColumns);

            //last success data - prod - RowChangeReason = Removed
            Console.WriteLine("\n*******last success data - prod - RowChangeReason = Removed*******");
            successDataQuery = string.Format("SELECT TOP 10 {0} FROM prod.Jobs.dbo.DimJobInfo WHERE ScrapeFileName LIKE '@scrapename%' AND RowIsCurrent ='Y' AND RowChangeReason = 'Removed' ORDER BY JobInfoKey DESC", ConfigurationManager.AppSettings["ScrapedRowColumns"]);
            ds = Common.GetDbResult(successDataQuery);
            datatable = Common.ConvertDataTableToString(ds.Tables[0], ScrapedRowColumns);
            return ds;
        }
    }
}
