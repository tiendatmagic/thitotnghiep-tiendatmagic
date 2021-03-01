using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using Quanlybangdia.Class;

namespace Quanlybangdia
{
    public partial class frmBangdia : Form
    {
        DataTable tblCL;
        public frmBangdia()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtmabangdia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã băng đĩa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmabangdia.Focus();
                return;
            }
            if (txtsoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsoluong.Focus();
                return;
            }

            if (txttenbangdia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên băng đĩa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttenbangdia.Focus();
                return;
            }
            sql = "Select MaBD From BangDia where MaBD='" + txtmabangdia.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã băng đĩa đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtmabangdia.Focus();
                return;
            }

            sql = "INSERT INTO BangDia VALUES('" +
              txtmabangdia.Text + "',N'" + txttenbangdia.Text + "','" + txtsoluong.Text + "')";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmabangdia.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtsoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txttenbangdia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            sql = ("UPDATE BangDia SET Tenbangdia=N'" +
              txttenbangdia.Text.ToString() +
              "' WHERE MaBD='" + txtmabangdia.Text + "'" +
              "UPDATE BangDia SET Soluong=N'" +
              txtsoluong.Text.ToString() +
              "' WHERE MaBD='" + txtmabangdia.Text + "'");

            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmabangdia.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            sql = "DELETE BangDia WHERE MaBD=N'" + txtmabangdia.Text + "'";
            Class.Functions.RunSqlDel(sql);
            LoadDataGridView();
            ResetValue();

        }

        private void frmDMChatlieu_Load(object sender, EventArgs e)
        {
            Functions.Connect();
            txtmabangdia.Enabled = true;

            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM BangDia";
            tblCL = Class.Functions.GetDataToTable(sql);
            DataGridView.DataSource = tblCL;
            DataGridView.Columns[0].HeaderText = "Mã băng đĩa";
            DataGridView.Columns[1].HeaderText = "Tên băng đĩa";
            DataGridView.Columns[2].HeaderText = "Số lượng";
            DataGridView.Columns[0].Width = 100;
            DataGridView.Columns[1].Width = 250;
            DataGridView.Columns[2].Width = 100;

            DataGridView.AllowUserToAddRows = false;
            DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValue()
        {
            txtmabangdia.Text = "";
            txttenbangdia.Text = "";
            txtsoluong.Text = "";
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmabangdia.Focus();
                return;
            }
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtmabangdia.Text = DataGridView.CurrentRow.Cells["Machatlieu"].Value.ToString();
            txtsoluong.Text = DataGridView.CurrentRow.Cells["Tenchatlieu"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmabangdia.Text = DataGridView.CurrentRow.Cells["MaBD"].Value.ToString();
            txttenbangdia.Text = DataGridView.CurrentRow.Cells["Tenbangdia"].Value.ToString();
            txtsoluong.Text = DataGridView.CurrentRow.Cells["Soluong"].Value.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}