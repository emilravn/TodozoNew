﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Todozo
{
    public partial class HomePage : Form
    {

        //lists used to store information from database 
        List<ListContainer> containerLists = new List<ListContainer>();
        List<TaskContainer> contaionerTasks = new List<TaskContainer>(); 

        //variable that stores information about the list which is selected 
        public static ListContainer listButtonPressed;



        public HomePage()
        {
            InitializeComponent();
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanelTask_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TopPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ListButton_Click(object sender, System.EventArgs e)
        {

        }

        //code that runs when the application starts 
        private void HomePage_Load(object sender, EventArgs e)
        {
            UpdateLists();
            //ViewTask testTask = new ViewTask();
           // flowLayoutPanelTask.Controls.Add(testTask);
        }


        


        private void button1_Click(object sender, EventArgs e)
        {


        }


        #region Buttons

        //create list button
        private void CreateListButton_Click(object sender, EventArgs e)
        {
            CreateListPage popup = new CreateListPage();
            DialogResult dialogresult = popup.ShowDialog();
            popup.Dispose();

            UpdateLists();
        }

        //create task button 
        private void CreateTaskButton_Click(object sender, EventArgs e)
        {
            CreateTaskPage popup = new CreateTaskPage();
            DialogResult dialogresult = popup.ShowDialog();
            popup.Dispose();

            UpdateTasks(listButtonPressed);
        } 

        //eventhandler that executes if a list i clicked on 
        void List_Click(object sender, EventArgs e)
        {
            foreach (ListContainer i in containerLists)
            {
                if (i.ListClicked == true)
                {
                    UpdateTasks(i);
                    listButtonPressed = i;
                    i.ListClicked = false;
                }
            }
        }

        #endregion

        #region Update functions 

        //Function that clears the Container list and Flowlayoutpanel, and updates it accordingly to the data in the database 
        public void UpdateLists()
        {


            DataAccess db = new DataAccess();
            containerLists.Clear();
            flowLayoutPanelList.Controls.Clear();

            foreach (List i in db.GetLists())
            {
                containerLists.Add(new ListContainer(i));
            }

            foreach (ListContainer i in containerLists)
            {
                flowLayoutPanelList.Controls.Add(i);
            }

            foreach (ListContainer i in containerLists)
            {

                i.name.Click += new System.EventHandler(List_Click);

            }
            //Potentionally instead make a function that checks if the container class is already there, then dont add it again 
        }


        //function that clears the ContaionerTasks list and updates it accordingly to the data in the database
        public void UpdateTasks(ListContainer list)
        {
            DataAccess db = new DataAccess();
            contaionerTasks.Clear();
            flowLayoutPanelTask.Controls.Clear();

            foreach (Task i in db.GetTasks(list.ListID))
            {
                contaionerTasks.Add(new TaskContainer(i));
            }

            foreach (TaskContainer i in contaionerTasks)
            {
                flowLayoutPanelTask.Controls.Add(i); 
                i.viewTask.Click += new System.EventHandler(viewTask_Click);
            }




            //code that creates a new eventhandler for the edit button
            
        } 


            void viewTask_Click(object sender, EventArgs e)
            {
                
                

                foreach (TaskContainer i in contaionerTasks)
                {
                    if (i.TaskClicked == true)
                    { 
                        flowLayoutPanelTask.Controls.Clear();
                        ViewTask viewTask = new ViewTask(i);
                    flowLayoutPanelTask.Controls.Add(viewTask);
                        
                        
                    }
                }


            }





        #endregion

    }
}
