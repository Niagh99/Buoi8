using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diem
{
    
    
    public partial class Form1 : Form
    {
        private BindingSource bindingSource;
        public Form1()
        {
            InitializeComponent();
            bindingSource = new BindingSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           using(Model1 dbContext= new Model1())
            {
                var students = dbContext.Students.ToList();
                bindingSource.DataSource = students;
                dataGridView1.DataSource = bindingSource;
                bindingNavigator1.BindingSource = bindingSource;
            }
            textBox1.DataBindings.Add("Text", bindingSource, "StudentID");
            textBox2.DataBindings.Add("Text", bindingSource, "FullName");
            textBox3.DataBindings.Add("Text", bindingSource, "ĐTB");
            textBox4.DataBindings.Add("Text", bindingSource, "FacultyID");
           bindingSource.ResetBindings(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                try
                {
                    if (!int.TryParse(textBox1.Text, out int studentID))
                    {
                        MessageBox.Show("Vui lòng nhập một StudentID hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dbContext.Students.Any(s => s.StudentID == studentID))
                    {
                        MessageBox.Show("StudentID đã tồn tại. Vui lòng nhập StudentID khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var newStudent = new Student
                    {
                        StudentID = studentID, 
                        FullName = textBox2.Text,
                        ĐTB = double.TryParse(textBox3.Text, out double avgScore) ? avgScore : 0,
                        FacultyID = int.TryParse(textBox4.Text, out int facultyId) ? (int?)facultyId : null
                    };

                    dbContext.Students.Add(newStudent);
                    dbContext.SaveChanges();

                    bindingSource.DataSource = dbContext.Students.ToList();
                    bindingSource.ResetBindings(false);

                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi thêm sinh viên: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                Student currentEmloyee = (Student)bindingSource.Current;
                Student emloyeeToDelete = dbContext.Students.Find(currentEmloyee.StudentID);
                if ((emloyeeToDelete != null))
                {
                    dbContext.Students.Remove(emloyeeToDelete);
                    dbContext.SaveChanges();
                    bindingSource.DataSource = dbContext.Students.ToList();
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                try
                {
                    // Lấy sinh viên đang được chọn từ BindingSource
                    Student currentStudent = bindingSource.Current as Student;
                    if (currentStudent != null)
                    {
                        // Tìm sinh viên trong dbContext bằng StudentID
                        Student studentToUpdate = dbContext.Students.Find(currentStudent.StudentID);
                        if (studentToUpdate != null)
                        {
                            // Cập nhật thuộc tính của sinh viên từ dữ liệu nhập vào, không thay đổi StudentID
                            studentToUpdate.FullName = textBox2.Text;
                            studentToUpdate.ĐTB = double.TryParse(textBox3.Text, out double avgScore) ? avgScore : 0;
                            studentToUpdate.FacultyID = int.TryParse(textBox4.Text, out int facultyId) ? (int?)facultyId : null;

                            // Lưu thay đổi vào cơ sở dữ liệu
                            dbContext.SaveChanges();

                            // Cập nhật lại dữ liệu cho BindingSource và DataGridView
                            bindingSource.DataSource = dbContext.Students.ToList();
                            bindingSource.ResetBindings(false);

                            MessageBox.Show("Sửa thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có sinh viên nào được chọn để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi sửa sinh viên: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            bindingSource.MoveNext();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource.MoveNext();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bindingSource.MovePrevious();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                toolStripButton1_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Delete))
            {
                button2_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
