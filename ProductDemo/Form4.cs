using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ProductDemo
{
    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form4()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
        }

        private DataSet GetStudents()
        {
            da = new SqlDataAdapter("select * from student", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds,"stud");
            return ds;
        }

        private void ClearFormFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtPer.Clear();
            comboBox1.SelectedIndex = -1;
        }   
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetStudents();
                DataRow row = ds.Tables["stud"].NewRow();
                row["name"] = txtName.Text;
                row["percentage"] = txtPer.Text;
                row["branch"] = comboBox1.Text;
                ds.Tables["stud"].Rows.Add(row);
                int result = da.Update(ds.Tables["stud"]);
                if (result >= 1)
                {
                    MessageBox.Show("Student Record Inserted Successfully");
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
            ds = GetStudents();
            dataGridView1.DataSource = ds.Tables["stud"];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetStudents();
                DataRow row = ds.Tables["stud"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["percentage"] = txtPer.Text;
                    row["branch"] = comboBox1.Text;
                    
                    int result = da.Update(ds.Tables["stud"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Student Record Updated Successfully");
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
                ds = GetStudents();
                DataRow row = ds.Tables["stud"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtPer.Text = row["percentage"].ToString();
                    comboBox1.Text = row["branch"].ToString();
                }
                else
                {
                    MessageBox.Show("Student Record Not Found");
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
                ds = GetStudents();
                DataRow row= ds.Tables["stud"].Rows.Find(txtId.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["stud"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("Student Record Deleted Successfully");
                        ClearFormFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
