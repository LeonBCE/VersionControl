using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {

        BindingList<User> users = new BindingList<User>();

        public Form1()
        {
            InitializeComponent();

            lblLastName.Text = Resource1.LastName;
            //lblFirstName.Text = Resource1.FirstName;
            btnAdd.Text = Resource1.Add;
            btnWrite.Text = Resource1.Write;

            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtLastName.Text,
                //FirstName = txtFirstName.Text
            };
            users.Add(u);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "Users.txt";
            sfd.Filter = "Text File | .txt";
            //sfd.InitialDirectory = default;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.OpenFile());

                for (int i = 0; i < users.Count; i++)
                {
                    sw.WriteLine(users.ToString());
                }

                sw.Dispose();
                sw.Close();
            }

        }
    }
}
