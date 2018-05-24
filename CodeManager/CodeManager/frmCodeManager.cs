using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SANWA;

namespace CodeManager
{
    public partial class frmCodeManager : Form
    {
        public frmCodeManager()
        {
            InitializeComponent();
        }

        private DataTable dtCode;
        private DataTable dtCategory;
        private string[] strColumsn;
        private string[] strCategory;

        private void frmCodeManager_Load(object sender, EventArgs e)
        {
            SANWA.Utility.Controlling controlling;
            SANWA.Utility.ContainerSet containerSet;

            try
            {
                controlling = new SANWA.Utility.Controlling();
                containerSet = new SANWA.Utility.ContainerSet();
                dtCode = new DataTable();
                dtCategory = new DataTable();

                strColumsn = Properties.Settings.Default.Code_Columns.Cast<string>().ToArray();
                strCategory = Properties.Settings.Default.Category.Cast<string>().ToArray();

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml"))
                {
                    containerSet.TableFormatting(ref dtCode, System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml");
                }
                else
                {
                    containerSet.TableFormatting(ref dtCode, "Table", strCategory);
                }
                containerSet.TableFormatting(ref dtCategory, "Table", new string[] { "ID","NAME" });

                foreach (string item in strCategory)
                {
                    if (string.IsNullOrEmpty(item.Trim()))
                    {
                        continue;
                    }

                    dtCategory.Rows.Add(item.Split(','));
                }

                controlling.ListBox_ItemChange(ref lsbQueryCondition, dtCategory.Select(), "ID", "NAME");
                controlling.ListBox_ItemChange(ref lsbEditorCategoryID, dtCategory.Select(), "ID", "NAME");

                lsbQueryCondition.SelectedIndex = -1;
                lsbEditorCategoryID.SelectedIndex = -1;
                txbEditorCategoryName.Text = string.Empty;
                txbQueryCategoryName.Text = string.Empty;

                dgvCode.DataSource = dtCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                controlling = null;
                containerSet = null;
            }
        }

        private void btnEditorCancel_Click(object sender, EventArgs e)
        {
            cmdEditorCodeType.Text = string.Empty;
            txbEditorCodeID.Text = string.Empty;
            txbEditorCodeName.Text = string.Empty;
            txbEditorCodeCause.Text = string.Empty;
            txbEditorCodeCauseEnglish.Text = string.Empty;
        }

        private void btnEditorSave_Click(object sender, EventArgs e)
        {
            if (lsbEditorCategoryID.SelectedIndex < 0)
            {
                return;
            }

            if (cmdEditorCodeType.Text.Equals(string.Empty))
            {
                return;
            }

            if (txbEditorCodeID.Text.Equals(string.Empty))
            {
                return;
            }

            DataRow drTemp;
            DataView dvTemp;

            try
            {
                dvTemp = new DataView(dtCode);
                dvTemp.RowFilter = "Category_ID = '" + lsbEditorCategoryID.Text + "' AND Code_Type = '" + cmdEditorCodeType.Text + "' AND Code_ID = '" + txbEditorCodeID.Text + "'";

                if (dvTemp.ToTable().Rows.Count > 0)
                {
                    MessageBox.Show("Already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                }
                else
                {
                    drTemp = dtCode.NewRow();

                    drTemp["Category_ID"] = lsbEditorCategoryID.Text;
                    drTemp["Category_Description"] = lsbEditorCategoryID.SelectedValue.ToString();
                    drTemp["Code_Name"] = txbEditorCodeName.Text.Trim();
                    drTemp["Code_Type"] = cmdEditorCodeType.Text.Trim();
                    drTemp["Code_ID"] = txbEditorCodeID.Text.Trim();
                    drTemp["Code_Cause"] = txbEditorCodeCause.Text.Trim();
                    drTemp["Code_Cause_English"] = txbEditorCodeCauseEnglish.Text.Trim();

                    dtCode.Rows.Add(drTemp);
                    dtCode.WriteXml(System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml");
                    dtCode.Clear();
                    dtCode.ReadXml(System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml");

                    MessageBox.Show("Save Done.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                    lsbQueryCondition_Click(lsbEditorCategoryID, e);
                    lsbQueryCondition_Click(lsbQueryCondition, e);
                    btnEditorCancel_Click(this, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                drTemp = null;
                dvTemp = null;
            }
        }

        private void lsbQueryCondition_Click(object sender, EventArgs e)
        {
            SANWA.Utility.Controlling controlling;
            DataView dvTemp;
            DataRow[] drsTemp;
            ArrayList alTemp;

            try
            {
                if (((ListBox)sender).SelectedIndex >= 0)
                {
                    switch (((ListBox)sender).Name)
                    {
                        case "lsbQueryCondition":
                            txbQueryCategoryName.Text = ((ListBox)sender).SelectedValue.ToString();

                            if (dgvCode.DataSource != null)
                            {
                                dvTemp = new DataView(dtCode);
                                dvTemp.RowFilter = "Category_ID = '" + ((ListBox)sender).Text + "'";
                                dgvCode.DataSource = dvTemp.ToTable();
                            }

                            break;

                        case "lsbEditorCategoryID":
                            txbEditorCategoryName.Text = ((ListBox)sender).SelectedValue.ToString();
                            controlling = new SANWA.Utility.Controlling();

                            // * Code Type Add
                            alTemp = new ArrayList();
                            drsTemp = dtCode.DefaultView.ToTable(true, new string[] { "Category_ID", "Code_Type" }).Select("Category_ID = '" + ((ListBox)sender).Text + "'");

                            foreach (DataRow dr in drsTemp)
                            {
                                alTemp.Add(dr["Code_Type"].ToString());
                            }

                            controlling.ComboBox_ItemChange(ref cmdEditorCodeType, (string[])alTemp.ToArray(typeof(string)));
                            controlling.ListBox_ItemChange(ref lsbEditorCodeType, (string[])alTemp.ToArray(typeof(string)));

                            lsbEditorCodeList.DataSource = null;
                            txbEditorCodeListName.Text = string.Empty;

                            break;

                        case "lsbEditorCodeType":
                            // * Code List Add
                            controlling = new SANWA.Utility.Controlling();
                            drsTemp = dtCode.DefaultView.ToTable(true, new string[] { "Category_ID", "Code_Type", "Code_ID", "Code_Name" }).Select("Category_ID = '" + lsbEditorCategoryID.Text + "' AND Code_Type = '" + lsbEditorCodeType.Text + "'");
                            controlling.ListBox_ItemChange(ref lsbEditorCodeList, drsTemp, "Code_ID", "Code_Name");
                            lsbEditorCodeList.SelectedIndex = -1;
                            txbEditorCodeListName.Text = string.Empty;

                            break;

                        case "lsbEditorCodeList":
                            txbEditorCodeListName.Text = ((ListBox)sender).SelectedValue.ToString();

                            break;
                    }
                }
                else
                {
                    txbQueryCategoryName.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
            finally
            {
                controlling = null;
            }
        }

        private void lsbQueryCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void lsbEditorCodeList_DoubleClick(object sender, EventArgs e)
        {
            DataView dvTemp;

            try
            {
                if (dgvCode.DataSource != null)
                {
                    dvTemp = new DataView(dtCode);
                    dvTemp.RowFilter = "Category_ID = '" + lsbEditorCategoryID.Text + "' AND Code_Type = '" + lsbEditorCodeType.Text + "' AND Code_ID = '" + lsbEditorCodeList.Text + "' AND Code_Name = '" + txbEditorCodeListName.Text + "'";

                    if (dvTemp.ToTable().Rows.Count > 0)
                    {
                        cmdEditorCodeType.Text = dvTemp.ToTable().Rows[0]["Code_Type"].ToString();
                        txbEditorCodeName.Text = dvTemp.ToTable().Rows[0]["Code_Name"].ToString();
                        txbEditorCodeID.Text = dvTemp.ToTable().Rows[0]["Code_ID"].ToString();
                        txbEditorCodeCause.Text = dvTemp.ToTable().Rows[0]["Code_Cause"].ToString();
                        txbEditorCodeCauseEnglish.Text = dvTemp.ToTable().Rows[0]["Code_Cause_English"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }

        private void lsbEditorCodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEditorCancel_Click(this, e);
        }

        private void btnEditorDelete_Click(object sender, EventArgs e)
        {
            if (lsbEditorCategoryID.SelectedIndex < 0)
            {
                return;
            }

            if (cmdEditorCodeType.Text.Equals(string.Empty))
            {
                return;
            }

            if (txbEditorCodeID.Text.Equals(string.Empty))
            {
                return;
            }

            DataRow[] drsTemp;

            try
            {
                drsTemp = dtCode.Select("Category_ID = '" + lsbEditorCategoryID.Text + "' AND Code_Type = '" + cmdEditorCodeType.Text + "' AND Code_ID = '" + lsbEditorCodeList.Text + "' AND Code_Name = '" + txbEditorCodeListName.Text + "'");

                if (drsTemp.Length > 0)
                {
                    foreach (DataRow dr in drsTemp)
                    {
                        dtCode.Rows.Remove(dr);
                    }

                    dtCode.WriteXml(System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml");
                    dtCode.Clear();
                    dtCode.ReadXml(System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml");

                    MessageBox.Show("Save Done.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);

                    lsbQueryCondition_Click(lsbEditorCategoryID, e);
                    lsbQueryCondition_Click(lsbQueryCondition, e);
                    btnEditorCancel_Click(this, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            }
        }
    }
}
