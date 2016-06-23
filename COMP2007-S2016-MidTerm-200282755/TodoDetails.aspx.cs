using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm_200282755.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_MidTerm_200282755
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodo();
                if (TodoCompleteCheckBox.Checked == true)
                {
                    toDoComplete.Text = "Done!";
                    toDoComplete.Visible = true;
                }
                else
                {
                    toDoComplete.Visible = false;
                }
            }
            if (TodoCompleteCheckBox.Checked == true)
            {
                toDoComplete.Text = "Done!";
                toDoComplete.Visible = true;
            }
            else
            {
                toDoComplete.Visible = false;
            }
        }


        /**
         * <summary>
         * This event handler gets the to-do to edit, or nothing if adding a new one
         * </summary>
         * 
         * @method GetTodo
         * @returns {void}
         */
        protected void GetTodo()
        {
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            //Connect to database and fill in the todo list item's details on the page
            using (TodoConnection db = new TodoConnection())
            {
                Todo updatedTodo = (from todo in db.Todos
                                          where todo.TodoID == TodoID
                                          select todo).FirstOrDefault();

                if (updatedTodo != null)
                {
                    TodoNameTextBox.Text = updatedTodo.TodoName;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                    if(updatedTodo.Completed == true)
                    {
                        TodoCompleteCheckBox.Checked = true;
                    }
                    else
                    {
                        TodoCompleteCheckBox.Checked = false;
                    }
                }
            }
        }

        /**
         * <summary>
         * This event handler sends the user back to the list if they cancel
         * </summary>
         * 
         * @method  CancelButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TodoList.aspx");
        }

        /**
         * <summary>
         * This event handler saves the todo
         * </summary>
         * 
         * @method  SaveButton_Click
         * @param {object} sender
         * @param {EventArgs} e
         * @returns {void}
         */
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // connect to EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // use the student model to save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                //IF adding a new student, run this, else skip it
                if (Request.QueryString.Count > 0) //Our URL HAS a student id
                {
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    newTodo = (from todo in db.Todos
                                  where todo.TodoID == TodoID
                                  select todo).FirstOrDefault();
                }

                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;
                if (TodoCompleteCheckBox.Checked == true)
                {
                    newTodo.Completed = true;
                }
                else
                {
                    newTodo.Completed = false;
                }

                //Only add if new student
                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // run insert in DB
                db.SaveChanges();

                // redirect to the updated students page
                Response.Redirect("~/TodoList.aspx");
            }
        }

        protected void TodoCompleteCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}