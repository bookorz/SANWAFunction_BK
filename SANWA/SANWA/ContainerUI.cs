using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace SANWA.Utility
{
    public class ContainerSet
    {
        public enum DataTableMoveRow
        {
            Up,
            Down
        }

        public void TableFormatting(ref DataTable dt, string TableName, string[] Columns)
        {
            try
            {
                if (dt == null)
                {
                    dt = new DataTable();
                }
                else
                {
                    dt.Clear();
                }

                dt.TableName = TableName;
                for (int i = 0; i < Columns.Length; i++)
                {
                    dt.Columns.Add(Columns[i].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void TableFormatting(ref DataTable dt, string XMLPath)
        {
            DataSet dsTemp;

            try
            {
                if (dt == null)
                {
                    dt = new DataTable();
                }
                else
                {
                    dt.Dispose();
                    dt = new DataTable();
                }

                dsTemp = new DataSet();
                dsTemp.ReadXml(XMLPath);
                dt = dsTemp.Tables[0].Copy();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public string StringFormat(string Format, string[] Param)
        {
            string strCommand = string.Empty;

            try
            {
                strCommand = string.Format(Format, Param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommand;
        }

        public string StringFormat(DataRow[] drs, string[] Param)
        {
            string strCommand = string.Empty;
            string strCommandFormat = string.Empty;

            try
            {
                for (int i = 0; i < drs.Length; i++)
                {
                    if (i == 0 && drs[i]["Parameter_ID"].ToString() != "Null")
                    {
                        strCommandFormat = ":";
                    }

                    strCommandFormat = strCommandFormat + "{" + i.ToString() + "},";
                }

                strCommand = StringFormat(strCommandFormat.Substring(0, strCommandFormat.Length -1), Param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return strCommand;
        }

        public int MoveRow(ref DataTable dtTemp, DataRow row, DataTableMoveRow direction)
        {
            DataRow oldRow = row;
            DataRow newRow = dtTemp.NewRow();

            newRow.ItemArray = oldRow.ItemArray;

            int oldRowIndex = dtTemp.Rows.IndexOf(row);

            if (direction == DataTableMoveRow.Down)
            {
                int newRowIndex = oldRowIndex + 1;

                if (oldRowIndex < (dtTemp.Rows.Count))
                {
                    dtTemp.Rows.Remove(oldRow);
                    dtTemp.Rows.InsertAt(newRow, newRowIndex);
                    return dtTemp.Rows.IndexOf(newRow);
                }
            }

            if (direction == DataTableMoveRow.Up)
            {
                int newRowIndex = oldRowIndex - 1;

                if (oldRowIndex > 0)
                {
                    dtTemp.Rows.Remove(oldRow);
                    dtTemp.Rows.InsertAt(newRow, newRowIndex);
                    return dtTemp.Rows.IndexOf(newRow);
                }
            }

            return 0;
        }
    }
}
