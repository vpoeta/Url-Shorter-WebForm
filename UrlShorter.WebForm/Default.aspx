<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>URL SHORTER SERVICES</h1>
        <p class="lead">Create Short Links in quick and simple way</p>
        <p><asp:TextBox ID="txtLink" runat="server" placeholder="    Insert link" cssclass="txtbox"/></p>
        <asp:RequiredFieldValidator forecolor="Red" runat="server" id="reqLink" controltovalidate="txtLink" 
            Errormessage="You should insert the link to convert!" SetFocusOnError="True"/>
        <asp:CustomValidator ID="CustomLinkValidator" forecolor="Red" runat="server" OnServerValidate="CustomLinkValidate"  ControlToValidate="txtLink"   
            ErrorMessage="You should insert a valid link" SetFocusOnError="True"></asp:CustomValidator>
        <asp:HyperLink ID="linkresult" runat="server" Font-Size="Large" Font-Italic="true" Font-Bold="true">
        </asp:HyperLink>
        <asp:label id="result" runat="server" />
        <br /><br />
        <p><asp:Button runat="server" Text="Submit &raquo;" OnClick="Submit_Click" class="btn btn-primary btn-lg"/></p>
    </div>
</asp:Content>
