using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Add_Lost_and_Found_Menu : Form
    {
        private Panel homePanelReference;

        public Add_Lost_and_Found_Menu(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            Lost_And_Found lostAndFound = new Lost_And_Found(homePanelReference);
            ConfigureForm(lostAndFound);
            lostAndFound.Show();
        }

        private void SubmitBT_Click(object sender, EventArgs e)
        {
            Add_Found_Item addFoundItem = new Add_Found_Item(homePanelReference);
            ConfigureForm(addFoundItem);
            addFoundItem.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Add_Lost_and_Found addLostAndFound = new Add_Lost_and_Found(homePanelReference);
            ConfigureForm(addLostAndFound);
            addLostAndFound.Show();
        }

        private void ConfigureForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(form);
        }
    }
}
