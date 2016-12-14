using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Aras;
using Aras.IOM;
using ArasInnovatorService.Common;

using ParameterTest.GarmentStyleService;
using ParameterTest.FabricPartService;

using ParameterTest.ESBService;


namespace ParameterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        private void button1_Click(object sender, EventArgs e)
        {
            //m_Connection = IomFactory.CreateHttpServerConnection("http://localhost/WesChenServer", "InnovatorDB", "admin", Innovator.ScalcMD5("innovator"));
            //m_Innovator = IomFactory.CreateInnovator(m_Connection);

            //Item l_newItem = m_Innovator.newItem("My Part", "Get");
            //SelectionFilter l_sample = SelectionFilter.CreateLeaf("Cost", "EQ", "100");
            //Item l_searchItem = ParseSelectionFilter.ParseSelection(l_sample, l_newItem);

            //textBox2.Text = l_sample.ToString();
            //textBox1.Text = l_searchItem.ToString();

            //m_Connection.Logout();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //m_Connection = IomFactory.CreateHttpServerConnection("http://localhost/WesChenServer", "InnovatorDB", "admin", Innovator.ScalcMD5("innovator"));
            //m_Innovator = IomFactory.CreateInnovator(m_Connection);

            //Item l_newItem = m_Innovator.newItem("My Part", "Get");
            //SelectionFilter l_sample = SelectionFilter.CreateAndFilter(
            //    new SelectionFilter[]{
            //        SelectionFilter.CreateLeaf("A", "EQ", "1"),
            //        SelectionFilter.CreateLeaf("B", "NEQ", "2"),
            //        SelectionFilter.CreateLeaf("C", "GT", "3")
            //    }
            //);
            //Item l_searchItem = ParseSelectionFilter.ParseSelection(l_sample, l_newItem);

            //textBox1.Text = l_searchItem.ToString();
            //textBox2.Text = l_sample.ToString();

            //m_Connection.Logout();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //m_Connection = IomFactory.CreateHttpServerConnection("http://localhost/WesChenServer", "InnovatorDB", "admin", Innovator.ScalcMD5("innovator"));
            //m_Innovator = IomFactory.CreateInnovator(m_Connection);

            //Item l_newItem = m_Innovator.newItem("My Part", "Get");
            //SelectionFilter l_sample = SelectionFilter.CreateOrFilter(
            //    new SelectionFilter[]{
            //        SelectionFilter.CreateAndFilter(
            //            new SelectionFilter[]{
            //                SelectionFilter.CreateLeaf("A", "EQ", "1"),
            //                SelectionFilter.CreateLeaf("B", "NEQ", "2"),
            //                SelectionFilter.CreateLeaf("C", "GT", "3")
            //            }
            //        ),
            //        SelectionFilter.CreateLeaf("D", "STE", "4")
            //    }
            //);
            //Item l_searchItem = ParseSelectionFilter.ParseSelection(l_sample, l_newItem);

            //textBox1.Text = l_searchItem.ToString();
            //textBox2.Text = l_sample.ToString();

            //m_Connection.Logout();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //m_Connection = IomFactory.CreateHttpServerConnection("http://localhost/WesChenServer", "InnovatorDB", "admin", Innovator.ScalcMD5("innovator"));
            //m_Innovator = IomFactory.CreateInnovator(m_Connection);

            //Item l_newItem = m_Innovator.newItem("My Part", "Get");
            //SelectionFilter l_sample = SelectionFilter.CreateOrFilter(
            //    new SelectionFilter[]{
            //        SelectionFilter.CreateAndFilter(
            //            new SelectionFilter[]{
            //                SelectionFilter.CreateLeaf("A", "EQ", "1"),
            //                SelectionFilter.CreateLeaf("B", "NEQ", "2"),
            //                SelectionFilter.CreateLeaf("C", "GT", "3")
            //            }
            //        ),
            //         SelectionFilter.CreateAndFilter(
            //            new SelectionFilter[]{
            //                SelectionFilter.CreateLeaf("D", "STE", "4"),
            //                SelectionFilter.CreateLeaf("E", "GTE", "5"),
            //            }
            //        ),SelectionFilter.CreateAndFilter(
            //            new SelectionFilter[]{
            //                SelectionFilter.CreateLeaf("F","EQ","100"),
            //                SelectionFilter.CreateLeaf("G","EQ","200")
            //            }
            //        )
            //    }
            //);
            //Item l_searchItem = ParseSelectionFilter.ParseSelection(l_sample, l_newItem);

            //textBox1.Text = l_searchItem.ToString();
            //textBox2.Text = l_sample.ToString();

            //m_Connection.Logout();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FabricPartManagementWSClient l_serviceClient = new FabricPartManagementWSClient();
            FabricPartClass l_searchClass = new FabricPartClass();
            l_searchClass.SelectionFilter = l_serviceClient.CreateLeaf("ITEM_NUMBER", "EQ", "HL201506789");

            FabricPartClass l_return = l_serviceClient.getFabricPartList(l_searchClass);
 

        }

        private void button7_Click(object sender, EventArgs e)
        {
            GarmentStyleManagementWSClient l_serviceClient = new GarmentStyleManagementWSClient();
            GarmentStyleClass l_searchClass = new GarmentStyleClass();
            l_searchClass.DisplayPageIndex = 1;
            l_searchClass.DisplayPageSize = 20;

            l_searchClass.SelectionFilter = l_serviceClient.CreateLeaf("ITEM_NUMBER", "EQ", "PYE-15DSFB001ET");

            GarmentStyleClass l_return = l_serviceClient.getGarmentStyleList(l_searchClass);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ESBService.portTypeClient l_client = new portTypeClient();
            Product[] l_get = l_client.GetProductOp(null, 1, 20);


        }
    }
}
