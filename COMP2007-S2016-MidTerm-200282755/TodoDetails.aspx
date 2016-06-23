<%--Author: Emma Hilborn - 200282755
Date: June 23, 2016
Version: 1
Description: A page allowing a user to edit/alter a to-do (e.g. change name or description) --%>

<%@ Page Title="Todo Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoDetails.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200282755.TodoDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <h1>Todo Details</h1>
                <h5>Todo name and notes are required - marking as complete is optional.</h5>
                <br />
                <div class="form-group">
                    <label class="control-label" for="TodoNameTextBox">Todo Name</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNameTextBox" placeholder="Todo Name" required="true" MaxLength="50"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label" for="TodoNotesTextBox">Todo Notes</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="TodoNotesTextBox" placeholder="Todo Notes" required="true" MaxLength="50"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:CheckBox runat="server" ID="TodoCompleteCheckBox" AutoPostBack="true" OnCheckedChanged="TodoCompleteCheckBox_CheckedChanged" />
                    <label class="control-label" for="TodoCompleteCheckBox">Completed</label>
                    <asp:Label ID="toDoComplete" runat="server" Display="Dynamic" Text=""></asp:Label>
                </div>
                <div class="text-right">
                    <asp:Button Text="Cancel" ID="CancelButton" runat="server" CssClass="btn btn-warning btn-lg" UseSubmitBehavior="false" CausesValidation="false" OnClick="CancelButton_Click" />
                    <asp:Button Text="Save" ID="SaveButton" runat="server" CssClass="btn btn-primary btn-lg" OnClick="SaveButton_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
