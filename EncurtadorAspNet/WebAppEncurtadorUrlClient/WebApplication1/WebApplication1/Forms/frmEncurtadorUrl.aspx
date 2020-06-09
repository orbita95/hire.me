<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEncurtadorUrl.aspx.cs" Inherits="WebApplication1.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>

        .content-box {
            margin-top: 20px;
            margin-left:2em;
        }

    </style>

    <div class="content-box">
        <div class="row">
            <div class="col-md-4">
                <label>URL </label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="urlPadrao_tb" TextMode="Url"></asp:TextBox>
            </div>
        
        </div>
        <div class="row">
            <div class="col-md-4">
                <label>Alias (Opcional) </label>
            </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="alias_tb"  ></asp:TextBox>
        </div>
    </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Button runat="server" OnClick="Unnamed_Click1" Text="Encurtar" />
            </div>
            
        </div>

        

        <hr />

        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" ID="shortUrl_lbl"></asp:Label>
            </div>
            
            <div class="col-md-4">
                <asp:Label runat="server" ID="alias_lbl"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" ID="tempoOperacao_lbl"></asp:Label>
            </div>
            
        </div>

        <hr />

        <div class="row">
            <div class="col-md-4">
                <label>Alias (ou ShortUrl) </label>
            </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="alias_se_tb"  ></asp:TextBox>
        </div>

        <div class="row">
            
            <div class="col-md-4">
                <asp:Button runat="server" Text="Buscar URL" OnClick="Unnamed_Click" />
            </div>
            
        </div>

        <hr />

        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" ID="urlOriginal_lbl"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4">
                <asp:Label runat="server" ID="error_lbl"></asp:Label>
            </div>
        </div>

    </div>


    
</asp:Content>
