using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SANWA;

namespace Instruction_editor
{
    public partial class frmInstructionEditor : Form
    {
        public frmInstructionEditor()
        {
            InitializeComponent();
        }

        private string[] strsEquipmentType;
        private string[] strsCommandType;
        private string[] strsEquipment_Supplier;
        private string[] strsCommand_Parameter_Columns;

        private DataTable dtParameterList;
        private DataTable dtCode;

        private string strCommandFormat = string.Empty;
        private string strEqpAssembly = string.Empty;

        private void frmInstructionEditor_Load(object sender, EventArgs e)
        {
            SANWA.Utility.Controlling controlling;
            SANWA.Utility.ContainerSet container;
            DataView dvTemp;

            try
            {
                controlling = new SANWA.Utility.Controlling();
                container = new SANWA.Utility.ContainerSet();


                strsEquipmentType = Properties.Settings.Default.EquipmentType.Cast<string>().ToArray();
                strsCommandType = Properties.Settings.Default.CommandType.Cast<string>().ToArray();
                strsEquipment_Supplier = Properties.Settings.Default.Equipment_Supplier.Cast<string>().ToArray();


                strsCommand_Parameter_Columns = Properties.Settings.Default.Command_Parameter_Columns.Cast<string>().ToArray();

                //controlling.ListBox_ItemChange(ref lsbCreateCommandType, strsCommandType);
                //controlling.ListBox_ItemChange(ref lsbCreateEqpSupplier, strsEquipment_Supplier);
                //controlling.ListBox_ItemChange(ref lsbCreateEqpType, strsEquipmentType);

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "ParameterList.xml"))
                {
                    container.TableFormatting(ref dtParameterList, System.AppDomain.CurrentDomain.BaseDirectory + "ParameterList.xml");
                }
                else
                {
                    container.TableFormatting(ref dtParameterList, "Table", strsCommand_Parameter_Columns);
                }

                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml"))
                {
                    container.TableFormatting(ref dtCode, System.AppDomain.CurrentDomain.BaseDirectory + "Code.xml");
                }

                if (dtCode.Rows.Count > 0)
                {
                    dvTemp = new DataView(dtCode);
                    dvTemp.RowFilter = "Category_ID = 'System'";
                    controlling.ListBox_ItemChange(ref lsbCreateEqpType, dvTemp.ToTable().Copy().Select("Code_Type = 'EquipmentType'"), "Code_ID", "Code_ID");
                    lsbCreateEqpType.SelectedIndex = -1;

                    controlling.ListBox_ItemChange(ref lsbCreateEqpSupplier, dvTemp.ToTable().Copy().Select("Code_Type = 'EquipmentSupplier'"), "Code_ID", "Code_ID");
                    lsbCreateEqpSupplier.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            finally
            {
                controlling = null;
                container = null;
            }
        }

        private void lsbCreateEqpType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void lsbCreateCommand_Click(object sender, EventArgs e)
        {
            SANWA.Utility.Controlling controlling;

            try
            {
                lsbCreateCommandParameter.DataSource = null;

                if (dtParameterList.Rows.Count == 0)
                {
                    nudParameterOrder.Value = 1;
                    return;
                }

                controlling = new SANWA.Utility.Controlling();

                var query = ( from a in dtParameterList.AsEnumerable()
                             where a.Field<string>("Equipment_Type") == lsbCreateEqpType.Text 
                                && a.Field<string>("Equipment_Supplier") == lsbCreateEqpSupplier.Text
                                && a.Field<string>("Command_Type") == lsbCreateCommandType.Text
                                && a.Field<string>("Command_ID") == lsbCreateCommand.Text
                            select a).ToList();

                if (query.Count > 0)
                {
                    controlling.ListBox_ItemChange(ref lsbCreateCommandParameter, query.CopyToDataTable().Select(), "Parameter_ID", "Parameter_ID");
                    nudParameterOrder.Value = Convert.ToInt32(query.CopyToDataTable().Rows[query.CopyToDataTable().Rows.Count - 1]["Parameter_Order"].ToString()) + 1;
                    lsbCreateCommandParameter.SelectedIndex = -1;
                }
                else
                {
                    nudParameterOrder.Value = 1;
                }

                txbParameterID.Text = string.Empty;
                txbParameterDescription.Text = string.Empty;
                nudMAXValue.Value = 0;
                nudMINValue.Value = 0;
                nudValueLength.Value = 0;
                nudDefaultValue.Value = 0;
                chbIsFill.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnInstructionSave_Click(object sender, EventArgs e)
        {
            if (lsbCreateEqpType.SelectedIndex < 0 ||
                lsbCreateEqpSupplier.SelectedIndex < 0 ||
                lsbCreateCommandType.SelectedIndex < 0 ||
                lsbCreateCommand.SelectedIndex < 0 
                )
            {
                MessageBox.Show(@"Missing condition ""Equipment Type"" ""Supplier"" ""Command Type"" ""Command"".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if (txbParameterID.Text.Trim().Equals(string.Empty))
            {
                //MessageBox.Show("Missing Parameter ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                //return;
                txbParameterID.Text = "Null";
            }

            if (!(txbParameterID.Text.Trim().Equals("Null")) && (nudMAXValue.Value == nudMINValue.Value))
            {
                MessageBox.Show("Max value = Min value.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if (!(txbParameterID.Text.Trim().Equals("Null")) && ((nudDefaultValue.Value > nudMAXValue.Value) || (nudDefaultValue.Value < nudMINValue.Value)))
            {
                MessageBox.Show("Default value > Max value or Default value < Min value.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if (strCommandFormat.Equals(string.Empty))
            {
                MessageBox.Show("Command format error.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            DataRow drTemp;
            DataRow[] drsTemp;
            DataTable dtTemp;
            Int32 itCommandParameterCount = 0;
            Int32 itCommandCount = 0;
            SANWA.Utility.ContainerSet containerSet;
            DataView dvTemp;

            try
            {
                containerSet = new SANWA.Utility.ContainerSet();

                drsTemp = dtParameterList.Select("Equipment_Type = '" + lsbCreateEqpType.Text + "' AND " +
                                                 "Equipment_Supplier = '" + lsbCreateEqpSupplier.Text + "' AND " +
                                                 "Command_Type = '" + lsbCreateCommandType.Text + "' AND " +
                                                 "Command_ID = '" + lsbCreateCommand.Text + "' AND " +
                                                 "Parameter_ID = '" + txbParameterID.Text + "'"
                                                 );

                if (drsTemp.Length > 0)
                {
                    MessageBox.Show("Repeat Parameter.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                drsTemp = dtParameterList.Select("Equipment_Type = '" + lsbCreateEqpType.Text + "' AND " +
                                                 "Equipment_Supplier = '" + lsbCreateEqpSupplier.Text + "' AND " +
                                                 "Command_Type = '" + lsbCreateCommandType.Text + "' AND " +
                                                 "Command_ID = '" + lsbCreateCommand.Text + "' AND " +
                                                 "Parameter_Order = '" + nudParameterOrder.Value.ToString() + "' AND " +
                                                 "Parameter_ID = '" + txbParameterID.Text + "'"
                                                 );

                if (drsTemp.Length > 0)
                {
                    drsTemp[0]["Parameter_Description"] = txbParameterDescription.Text.Trim();

                    if (rdbEditorParameterData.Checked || txbParameterID.Text.Equals("Null"))
                    {
                        drsTemp[0]["Data_Value"] = txbParameterDescription.Text.Trim();
                        drsTemp[0]["Min_Value"] = string.Empty;
                        drsTemp[0]["Max_Value"] = string.Empty;
                        drsTemp[0]["Default_Value"] = string.Empty;
                        drsTemp[0]["Values_length"] = string.Empty;
                        drsTemp[0]["Is_Fill"] = string.Empty;
                    }
                    else if (rdbEditorParameterValue.Checked)
                    {
                        drsTemp[0]["Data_Value"] = string.Empty;
                        drsTemp[0]["Min_Value"] = nudMINValue.Value.ToString();
                        drsTemp[0]["Max_Value"] = nudMAXValue.Value.ToString();
                        drsTemp[0]["Default_Value"] = nudDefaultValue.Value.ToString();
                        drsTemp[0]["Values_length"] = nudValueLength.Value.ToString();
                        drsTemp[0]["Is_Fill"] = chbIsFill.Checked ? "Y" : "N";
                    }
                }
                else
                {
                    itCommandParameterCount = dtParameterList.Select("Equipment_Type = '" + lsbCreateEqpType.Text + "' AND " +
                                                                     "Equipment_Supplier = '" + lsbCreateEqpSupplier.Text + "' AND " +
                                                                     "Command_Type = '" + lsbCreateCommandType.Text + "' AND " +
                                                                     "Command_ID = '" + lsbCreateCommand.Text + "'"
                                                                     ).Length;

                    dvTemp = new DataView(dtParameterList);
                    dvTemp.RowFilter = "Command_Type = '" + lsbCreateCommandType.Text + "'";
                    dtTemp = dvTemp.ToTable(true, new string[] { "Equipment_Type", "Equipment_Supplier", "Command_Type", "Command_ID", "Command_Order" });

                    if (dtTemp.Rows.Count == 0 || Convert.ToInt32(dtTemp.Compute("max(Command_Order)", string.Empty)) == 0)
                    {
                        itCommandCount = 1;
                    }
                    else if (dtTemp.Select("Command_ID = '" + lsbCreateCommand.Text + "'").Length == 0)
                    {
                        itCommandCount = Convert.ToInt32(dtTemp.Compute("max(Command_Order)", string.Empty)) + 1;
                    }
                    else
                    {
                        itCommandCount = Convert.ToInt32(dtTemp.Compute("max(Command_Order)", string.Empty));
                    }


                    drTemp = dtParameterList.NewRow();
                    drTemp["Equipment_Type"] = lsbCreateEqpType.Text;
                    drTemp["Equipment_Supplier"] = lsbCreateEqpSupplier.Text;
                    drTemp["Command_Type"] = lsbCreateCommandType.Text;
                    drTemp["Command_ID"] = lsbCreateCommand.Text;

                    switch (lsbCreateEqpSupplier.Text.ToUpper())
                    {
                        case "SANWA":
                            drTemp["Command_Format"] = string.Format(strCommandFormat, "{0}", "{1}", lsbCreateCommandType.Text, lsbCreateCommand.Text);
                            break;

                        case "KAWASAKI":
                            //drTemp["Command_Format"] = string.Format(strCommandFormat, "{0}", "{1}", lsbCreateCommandType.Text, lsbCreateCommand.Text.PadRight(5, '_'));
                            break;

                        case "TDK":
                            drTemp["Command_Format"] = string.Format(strCommandFormat, lsbCreateCommandType.Text, lsbCreateCommand.Text);
                            break;
                    }

                    drTemp["Command_Name"] = lsbCreateCommand.SelectedValue;
                    drTemp["Command_Order"] = itCommandCount.ToString("D2");
                    drTemp["Parameters_Amount"] = (itCommandParameterCount + 1).ToString("D2");
                    drTemp["Parameter_Order"] = Convert.ToInt32(nudParameterOrder.Value).ToString("D2");

                    drTemp["Parameter_ID"] = txbParameterID.Text.Trim();
                    drTemp["Parameter_Description"] = txbParameterDescription.Text.Trim();

                    if (rdbEditorParameterData.Checked || txbParameterID.Text.Equals("Null"))
                    {
                        drTemp["Data_Value"] = txbParameterDescription.Text.Trim();
                        drTemp["Min_Value"] = string.Empty;
                        drTemp["Max_Value"] = string.Empty;
                        drTemp["Default_Value"] = string.Empty;
                        drTemp["Values_length"] = string.Empty;
                        drTemp["Is_Fill"] = string.Empty;
                    }
                    else if (rdbEditorParameterValue.Checked)
                    {
                        drTemp["Data_Value"] = string.Empty;
                        drTemp["Min_Value"] = nudMINValue.Value.ToString();
                        drTemp["Max_Value"] = nudMAXValue.Value.ToString();
                        drTemp["Default_Value"] = nudDefaultValue.Value.ToString();
                        drTemp["Values_length"] = nudValueLength.Value.ToString();
                        drTemp["Is_Fill"] = chbIsFill.Checked ? "Y" : "N";
                    }
                    else
                    {
                        drTemp["Data_Value"] = string.Empty;
                        drTemp["Min_Value"] = string.Empty;
                        drTemp["Max_Value"] = string.Empty;
                        drTemp["Default_Value"] = string.Empty;
                        drTemp["Values_length"] = string.Empty;
                        drTemp["Is_Fill"] = string.Empty;
                    }

                    drTemp["Action_Function"] = cmbCommandAction.Text;
                    dtParameterList.Rows.Add(drTemp);
                }

                dtParameterList.WriteXml(System.AppDomain.CurrentDomain.BaseDirectory + "ParameterList.xml");
                containerSet.TableFormatting(ref dtParameterList, System.AppDomain.CurrentDomain.BaseDirectory + "ParameterList.xml");

                txbParameterID.Text = string.Empty;
                txbParameterDescription.Text = string.Empty;
                nudMAXValue.Value = 0;
                nudMINValue.Value = 0;
                nudValueLength.Value = 0;
                nudDefaultValue.Value = 0;
                chbIsFill.Checked = false;

                MessageBox.Show("Save Done.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                lsbCreateEqpType_Click(this, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void lsbCreateCommandParameter_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                var query = (from a in dtParameterList.AsEnumerable()
                             where a.Field<string>("Equipment_Type") == lsbCreateEqpType.Text
                                && a.Field<string>("Equipment_Supplier") == lsbCreateEqpSupplier.Text
                                && a.Field<string>("Command_Type") == lsbCreateCommandType.Text
                                && a.Field<string>("Command_ID") == lsbCreateCommand.Text
                                && a.Field<string>("Parameter_ID") == lsbCreateCommandParameter.Text
                             select a).ToList();

                if (query.Count > 0)
                {
                    if (query[0]["Data_Value"].ToString().Length > 0)
                    {
                        rdbEditorParameterData.Checked = true;
                        txbEditorParameterData.Text = query[0]["Data_Value"].ToString();
                    }
                    else if (query[0]["Min_Value"].ToString().Length > 0)
                    {
                        rdbEditorParameterValue.Checked = true;
                        nudMAXValue.Value = Convert.ToInt32(query[0]["Max_Value"].ToString());
                        nudMINValue.Value = Convert.ToInt32(query[0]["Min_Value"].ToString());
                        nudValueLength.Value = Convert.ToInt32(query[0]["Values_length"].ToString());
                        nudDefaultValue.Value = Convert.ToInt32(query[0]["Default_Value"].ToString());
                        chbIsFill.Checked = query[0]["Is_Fill"].ToString() == "Y" ? true : false;
                    }

                    nudParameterOrder.Value = Convert.ToInt32(query[0]["Parameter_Order"].ToString());
                    txbParameterID.Text = query[0]["Parameter_ID"].ToString();
                    txbParameterDescription.Text = query[0]["Parameter_Description"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void rdbEditorParameterData_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                switch (((RadioButton)sender).Name)
                {
                    case "rdbEditorParameterData":
                        palEditorData.Enabled = true;
                        palEditorValue.Enabled = false;
                        chbIsFill.Enabled = false;
                        break;

                    case "rdbEditorParameterValue":
                        palEditorData.Enabled = false;
                        palEditorValue.Enabled = true;
                        chbIsFill.Enabled = true;
                        break;
                }

                txbEditorParameterData.Text = string.Empty;
                nudMAXValue.Value = 0;
                nudMINValue.Value = 0;
                nudValueLength.Value = 0;
                nudDefaultValue.Value = 0;
                chbIsFill.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void nudMINValue_Enter(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Value.ToString().Length);
        }

        private void lsbCreateEqpType_Click(object sender, EventArgs e)
        {
            if (lsbCreateEqpType.SelectedIndex < 0)
            {
                lsbCreateCommandType.DataSource = null;
                return;
            }

            if (lsbCreateEqpSupplier.SelectedIndex < 0)
            {
                lsbCreateCommandType.DataSource = null;
                return;
            }

            SANWA.Utility.Controlling controlling;
            DataRow[] drsTemp;
            DataRow[] drsCode;
            DataView dvTemp;

            try
            {
                controlling = new SANWA.Utility.Controlling();

                if (strEqpAssembly.Equals(""))
                {
                    dvTemp = new DataView(dtCode);
                    dvTemp.RowFilter = "Category_ID = '" + lsbCreateEqpSupplier.SelectedValue + "." + lsbCreateEqpType.SelectedValue + "'";
                    controlling.ListBox_ItemChange(ref lsbCreateCommandType, dvTemp.ToTable(true, new string[] { "Code_Type" }).Copy().Select(""), "Code_Type", "Code_Type");
                    lsbCreateCommandType.SelectedIndex = -1;
                    strEqpAssembly = lsbCreateEqpSupplier.SelectedValue + "." + lsbCreateEqpType.SelectedValue;
                    lsbCreateCommand.DataSource = null;
                    return;
                }
                else if (strEqpAssembly != lsbCreateEqpSupplier.SelectedValue + "." + lsbCreateEqpType.SelectedValue)
                {
                    dvTemp = new DataView(dtCode);
                    dvTemp.RowFilter = "Category_ID = '" + lsbCreateEqpSupplier.SelectedValue + "." + lsbCreateEqpType.SelectedValue + "'";
                    controlling.ListBox_ItemChange(ref lsbCreateCommandType, dvTemp.ToTable(true, new string[] { "Code_Type" }).Copy().Select(""), "Code_Type", "Code_Type");
                    lsbCreateCommandType.SelectedIndex = -1;
                    strEqpAssembly = lsbCreateEqpSupplier.SelectedValue + "." + lsbCreateEqpType.SelectedValue;
                    lsbCreateCommand.DataSource = null;
                    return;
                }


                if (lsbCreateCommandType.SelectedIndex < 0)
                {
                    return;
                }

                if (lsbCreateCommandType.DataSource != null && lsbCreateCommandType.SelectedIndex >= 0)
                {
                    if (dtCode.Rows.Count > 0)
                    {
                        drsTemp = dtCode.DefaultView.ToTable(true, new string[] { "Category_ID", "Code_Type", "Code_ID", "Code_Name" }).Select("Category_ID = '" + lsbCreateEqpSupplier.Text + "." + lsbCreateEqpType.Text + "' AND Code_Type = '" + lsbCreateCommandType.Text + "'");

                        controlling.ListBox_ItemChange(ref lsbCreateCommand, drsTemp, "Code_ID", "Code_Name");

                        lsbCreateCommand.SelectedIndex = -1;
                        lsbCreateCommandParameter.DataSource = null;


                        drsCode = dtCode.DefaultView.ToTable(true, new string[] { "Category_ID", "Code_Type", "Code_ID", "Code_Name" }).Select("Category_ID = 'System' AND Code_Type = '" + string.Format("{0}.Command.Format", lsbCreateEqpSupplier.Text) + "'");

                        if (drsCode.Length > 0)
                        {
                            strCommandFormat = drsCode[0]["Code_ID"].ToString();
                        }
                    }
                    else
                    {
                        if (((DataTable)lsbCreateCommand.DataSource) != null)
                        {
                            ((DataTable)lsbCreateCommand.DataSource).Clear();
                            ((DataTable)lsbCreateCommandParameter.DataSource).Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception Message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SANWA.Utility.Encoder encoder = new SANWA.Utility.Encoder("TDK");

            encoder.LoadPort.Indicator(SANWA.Utility.EncoderLoadPort.CommandType.Finish, SANWA.Utility.EncoderLoadPort.IndicatorType.Status01, SANWA.Utility.EncoderLoadPort.IndicatorStatus.Flashes);

        }
    }
}
