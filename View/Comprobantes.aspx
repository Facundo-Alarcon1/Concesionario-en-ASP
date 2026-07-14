<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comprobantes.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Comprobantes" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostBack="true" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="flex flex-col md:flex-row justify-between items-start md:items-end mb-6 gap-4">
        <div>
            <h2 class="text-2xl font-bold text-slate-900">Historial Financiero</h2>
            <p class="text-sm text-slate-500 mt-1">Auditor&iacute;a de compras, ventas y movimientos.</p>
        </div>
        
        <div class="flex items-center gap-3 w-full md:w-auto">
            <asp:DropDownList ID="ddlTipoFiltro" runat="server" CssClass="input-modern md:w-64">
                <asp:ListItem Text="Todos los comprobantes" Value=""></asp:ListItem>
                <asp:ListItem Text="Solo Compras (Ingresos)" Value="compra"></asp:ListItem>
                <asp:ListItem Text="Solo Ventas (Egresos)" Value="venta"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnFiltrar" runat="server" CssClass="btn-primary-modern" Text="Filtrar" OnClick="btnFiltrar_Click" />
        </div>
    </div>

    <!-- Contenedor Tabla -->
    <div class="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden">
        <div class="overflow-x-auto w-full">
            <asp:GridView ID="gvComprobantes" runat="server" AutoGenerateColumns="false" 
                CssClass="gridview-modern w-full text-left text-sm" GridLines="None" BorderStyle="None">
                
                <HeaderStyle CssClass="bg-slate-50 text-slate-500 font-semibold uppercase tracking-wider text-xs border-b border-slate-200" />
                <RowStyle CssClass="bg-white border-b border-slate-100 hover:bg-slate-50 transition-colors text-slate-700" />
                <AlternatingRowStyle CssClass="bg-slate-50 border-b border-slate-100 hover:bg-slate-100 transition-colors text-slate-700" />
                
                <Columns>
                    <asp:BoundField DataField="ID_comprobante" HeaderText="Cod" />
                    
                    <asp:TemplateField HeaderText="Operaci&oacute;n">
                        <ItemTemplate>
                            <div class="flex items-center gap-2">
                                <svg class='w-4 h-4 <%# Eval("Tipo").ToString().ToLower() == "venta" ? "text-emerald-500" : "text-blue-500" %>' fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d='<%# Eval("Tipo").ToString().ToLower() == "venta" ? "M13 7h8m0 0v8m0-8l-8 8-4-4-6 6" : "M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z" %>'></path></svg>
                                <span class="font-medium text-slate-800 capitalize"><%# Eval("Tipo") %></span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="FechaHora" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="ID_auto" HeaderText="ID Auto" />
                    <asp:BoundField DataField="ID_empleado" HeaderText="Empleado" />
                    
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="inline-flex items-center px-2 py-0.5 rounded text-[11px] font-semibold uppercase tracking-wider <%# Eval("Estado").ToString().ToLower() == "vendido" ? "bg-emerald-100 text-emerald-800" : "bg-amber-100 text-amber-800" %>">
                                <%# Eval("Estado") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Observaciones" HeaderText="Detalle" />
                    <asp:BoundField DataField="Precio" HeaderText="Monto" DataFormatString="${0:N2}" HtmlEncode="false" ItemStyle-CssClass="font-semibold text-slate-900" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
