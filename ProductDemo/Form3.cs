using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProductDemo
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;

        public Form3()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

        }
        // to fetch the data at app side & load in DataSet
        private DataSet GetEmployees()
        {
            da = new SqlDataAdapter("select * from employeee", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            // track DataSet & generate query & assign to da object
            ds = new DataSet();
            da.Fill(ds, "emp");
            // emp is a table name given to DataSet table
            return ds;
        }
        private void ClearFormFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtSal.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                ds = GetEmployees();
                //need to add new object in the DataSet
                DataRow row = ds.Tables["emp"].NewRow();
                row["name"] = txtName.Text;
                row["email"] = txtEmail.Text;
                row["salary"] = txtSal.Text;
                // attach or load the row to the DataSet
                ds.Tables["emp"].Rows.Add(row);
                // reflect the changes to the DB
                int result = da.Update(ds.Tables["emp"]);
                if (result >= 1)
                {
                    MessageBox.Show("Record Inserted Successfully");
                    ClearFormFields();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ds = GetEmployees();
            dataGridView1.DataSource = ds.Tables["emp"];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                // find the object in the DataSet
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["email"] = txtEmail.Text;
                    row["salary"] = txtSal.Text;
                    // reflect the changes to the DB
                    int result = da.Update(ds.Tables["emp"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record Updated Successfully");
                        ClearFormFields();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtEmail.Text = row["email"].ToString();
                    txtSal.Text = row["salary"].ToString();
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                // find the object in the DataSet
                DataRow row = ds.Tables["emp"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["emp"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Record Deleted Successfully");
                        ClearFormFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found For Mentioned Id");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
