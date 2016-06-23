using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_MidTerm_200282755.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_MidTerm_200282755
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetTodos();
            }
        }

        protected void GetTodos()
        {
            // connect to EF DB
            using (TodoConnection db = new TodoConnection())
            {
                //Sort string
                //string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                // query the Students table using EF and LINQ
                var Todos = (from allTodos in db.Todos
                                select allTodos);

                //bind the result to the GridView
                TodoGridView.DataSource = Todos.AsQueryable().ToList();
                TodoGridView.DataBind();
            }

        }


        /**
         * <summary>
         * This event handler deletes a to-do list item from the DB using the EF
         * </summary>
         * 
         * @method TodoGridView_RowDeleting
         * @param {object} sender
         * @param {GridViewDeleteEventArgs} e
         * @returns {void}
         */
        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Store what row was clicked
            int selectedRow = e.RowIndex;

            //Get the selected Student ID using the grid's data key
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            //Use ef to find student in DB and remove them
            using (TodoConnection db = new TodoConnection())
            {
                //Create student class object to store query for the student to delete
                Todo deletedTodo = (from todos in db.Todos
                                          where todos.TodoID == TodoID
                                          select todos).FirstOrDefault();
                //Remove student
                db.Todos.Remove(deletedTodo);

                //Save DB changes
                db.SaveChanges();

                //Refresh DB
                this.GetTodos();
            }

        }

        /**
         * <summary>
         * This event handler allows pagination to occur on the to-do page
         * </summary>
         * 
         * @method TodoGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */
        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set page #
            TodoGridView.PageIndex = e.NewPageIndex;

            //Refresh grid
            this.GetTodos();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //Refresh grid
            this.GetTodos();
        }

        protected void todoCompleted_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox todoCompleted = (CheckBox)sender;
            GridViewRow row = (GridViewRow)todoCompleted.NamingContainer;
            int rowIndex = Convert.ToInt32(row.RowIndex);
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[rowIndex].Values["TodoID"]);
            bool status = todoCompleted.Checked;

            using (TodoConnection db = new TodoConnection())
            {
                //Create student class object to store query for the student to delete
                Todo changedTodo = (from todos in db.Todos
                                    where todos.TodoID == TodoID
                                    select todos).FirstOrDefault();
                if(status == true)
                {
                    changedTodo.Completed = true;
                }
                else
                {
                    changedTodo.Completed = false;
                }

                //Save DB changes
                db.SaveChanges();

                //Refresh DB
                this.GetTodos();
            }

        }
    }
}