<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ElasticGrid.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="InfoLabel" runat="server" Text="Отладка" ToolTip="Информационная метка"></asp:Label>
        <br />
        <asp:GridView ID="InfoGridView" runat="server" Height="220px"  Width="100%" HorizontalAlign="Center">
        </asp:GridView>
        <br />
        <asp:Button ID="DebugButton" runat="server" OnClick="DebugButtonClick" Text="Отладка" />
    
    </div>
    </form>
</body>
</html>
