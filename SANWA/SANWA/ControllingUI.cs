using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace SANWA.Utility
{
    public class Controlling
    {
        enum UIList
        {
            ComboBox = 0,
            ListBox = 1,
        }

        public void ListBox_ItemChange(ref ListBox lsb, string[] items)
        {
            object obj = lsb;

            try
            {
                UI_ItemChange(ref obj, UIList.ListBox, items);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void ListBox_ItemChange(ref ListBox lsb, DataRow[] drs, string DisplayMember, string ValueMember)
        {
            object obj = lsb;

            try
            {
                UI_ItemChange(ref obj, UIList.ListBox, drs, DisplayMember, ValueMember);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void ComboBox_ItemChange(ref ComboBox cmb, string[] items)
        {
            object obj = cmb;

            try
            {
                UI_ItemChange(ref obj, UIList.ComboBox, items);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void ComboBox_ItemChange(ref ComboBox cmb, DataRow [] drs, string DisplayMember, string ValueMember)
        {
            object obj = cmb;

            try
            {
                UI_ItemChange(ref obj, UIList.ComboBox, drs, DisplayMember, ValueMember);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void UI_ItemChange(ref object obj, UIList uIList, string[] items)
        {
            try
            {
                switch (uIList)
                {
                    case UIList.ComboBox:
                        ((ComboBox)obj).Items.Clear();
                        //((ComboBox)obj).Items.Add(string.Empty);
                        ((ComboBox)obj).Items.AddRange(items);

                        break;

                    case UIList.ListBox:
                        ((ListBox)obj).Items.Clear();
                        //((ListBox)obj).Items.Add(string.Empty);
                        ((ListBox)obj).Items.AddRange(items);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void UI_ItemChange(ref object obj, UIList uIList, DataRow[] drs, string DisplayMember, string ValueMember)
        {
            DataSet dsTemp;

            try
            {
                dsTemp = new DataSet();
                dsTemp.Merge(drs);

                switch (uIList)
                {

                    case UIList.ComboBox:

                        if (dsTemp.Tables.Count == 0 || dsTemp.Tables[0].Rows.Count == 0)
                        {
                            ((ComboBox)obj).DataSource = null;
                        }
                        else
                        {
                            ((ComboBox)obj).DataSource = dsTemp.Tables[0];
                            ((ComboBox)obj).DisplayMember = DisplayMember;
                            ((ComboBox)obj).ValueMember = ValueMember;
                        }

                        break;

                    case UIList.ListBox:

                        if (dsTemp.Tables.Count == 0 || dsTemp.Tables[0].Rows.Count == 0)
                        {
                            ((ListBox)obj).DataSource = null;
                        }
                        else
                        {
                            ((ListBox)obj).DataSource = dsTemp.Tables[0];
                            ((ListBox)obj).DisplayMember = DisplayMember;
                            ((ListBox)obj).ValueMember = ValueMember;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
