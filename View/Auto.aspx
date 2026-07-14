<%@ Page Title="Autos" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Auto.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Autos" MaintainScrollPositionOnPostBack="true" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="flex justify-between items-end mb-6">
        <div>
            <h2 class="text-2xl font-bold text-slate-900">Cat&aacute;logo de Autos</h2>
            <p class="text-sm text-slate-500 mt-1">Gestiona el inventario, busca modelos y registra nuevas unidades.</p>
        </div>
        <asp:Label ID="lblUsuario" runat="server" CssClass="text-xs font-semibold text-slate-600 bg-white border border-slate-200 px-3 py-1.5 rounded-lg shadow-sm" />
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-8">
        <!-- Panel Buscar -->
        <div class="bg-slate-100/50 rounded-xl border border-slate-200 p-6 h-fit shadow-sm relative overflow-hidden">
            <div class="absolute top-0 left-0 w-full h-1 bg-amber-500"></div>
            <h3 class="text-sm font-bold text-slate-800 uppercase tracking-wider mb-4 border-b border-slate-200 pb-2 flex items-center gap-2">
                <svg class="w-4 h-4 text-amber-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
                Filtros de B&uacute;squeda
            </h3>
            
            <div class="space-y-4">
                <div>
                    <label class="block text-xs font-semibold text-slate-600 mb-1.5">T&eacute;rmino clave</label>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="input-modern" placeholder="Marca, modelo, patente..." />
                </div>
                
                <div class="flex gap-2 pt-2">
                    <asp:Button ID="btnBuscar" runat="server" CssClass="flex-1 btn-primary-modern" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnVerTodos" runat="server" CssClass="flex-1 btn-secondary-modern" Text="Limpiar" OnClick="btnVerTodos_Click" />
                </div>
            </div>
        </div>

        <!-- Panel Agregar -->
        <asp:Panel ID="pnlAgregarAuto" runat="server" CssClass="lg:col-span-2 bg-slate-100/50 rounded-xl border border-slate-200 p-6 h-fit shadow-sm">
            <div class="flex justify-between items-center border-b border-slate-200 pb-2 mb-4">
                <h3 class="text-sm font-bold text-slate-800 uppercase tracking-wider flex items-center gap-2">
                    <svg class="w-4 h-4 text-slate-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4v16m8-8H4"></path></svg>
                    Nuevo Auto
                </h3>
                <span class="text-[10px] font-bold text-indigo-700 bg-indigo-100 px-2 py-0.5 rounded uppercase tracking-wider">Solo Gerente</span>
            </div>
            
            <asp:HiddenField ID="hfAutoId" runat="server" />
            <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4 mb-5">
                <div>
                    <label class="block text-xs font-semibold text-slate-600 mb-1.5">Marca</label>
                    <asp:TextBox ID="txtMarca" runat="server" CssClass="input-modern" />
                </div>
                <div>
                    <label class="block text-xs font-semibold text-slate-600 mb-1.5">Modelo</label>
                    <asp:TextBox ID="txtModelo" runat="server" CssClass="input-modern" />
                </div>
                <div>
                    <label class="block text-xs font-semibold text-slate-600 mb-1.5">Color</label>
                    <asp:TextBox ID="txtColor" runat="server" CssClass="input-modern" />
                </div>
                <div>
                    <label class="block text-xs font-semibold text-slate-600 mb-1.5">Patente</label>
                    <asp:TextBox ID="txtPatente" runat="server" CssClass="input-modern" />
                </div>
                <div>
                    <label class="block text-xs font-semibold text-slate-600 mb-1.5">A&ntilde;o</label>
                    <asp:TextBox ID="txtAnio" runat="server" CssClass="input-modern" />
                </div>
                <div>
                    <label class="block text-xs font-semibold text-slate-600 mb-1.5">Precio ($)</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="input-modern" />
                </div>
            </div>
            
            <div class="flex justify-between items-center border-t border-slate-200 pt-4">
                <asp:Label ID="lblAgregarResultado" runat="server" CssClass="text-xs font-medium" />
                <asp:Button ID="btnAgregar" runat="server" CssClass="btn-primary-modern px-6" Text="Guardar Auto" OnClick="btnAgregar_Click" />
            </div>
        </asp:Panel>
    </div>

    <!-- Tabla -->
    <div class="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden mb-4">
        <div class="overflow-x-auto w-full">
            <asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_auto" CssClass="gridview-modern w-full text-left text-sm"
                OnRowEditing="gvAutos_RowEditing"
                OnRowUpdating="gvAutos_RowUpdating"
                OnRowCancelingEdit="gvAutos_RowCancelingEdit"
                OnRowDeleting="gvAutos_RowDeleting"
                OnRowCommand="gvAutos_RowCommand"
                GridLines="None"
                BorderStyle="None">
                <HeaderStyle CssClass="bg-slate-50 text-slate-500 font-semibold uppercase tracking-wider text-xs border-b border-slate-200" />
                <RowStyle CssClass="bg-white border-b border-slate-100 hover:bg-slate-50 transition-colors text-slate-700" />
                <AlternatingRowStyle CssClass="bg-slate-50 border-b border-slate-100 hover:bg-slate-100 transition-colors text-slate-700" />
                
                <Columns>
                    <asp:BoundField DataField="ID_auto" HeaderText="ID" ReadOnly="true" />
                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="Patente" HeaderText="Patente" />
                    <asp:BoundField DataField="Anio" HeaderText="A&ntilde;o" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="${0:N2}" HtmlEncode="false" ItemStyle-CssClass="font-medium" />
                    
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="inline-flex items-center px-2 py-0.5 rounded text-[11px] font-semibold uppercase tracking-wider <%# Eval("Estado").ToString().ToLower() == "disponible" ? "bg-emerald-100 text-emerald-800" : "bg-slate-100 text-slate-600" %>">
                                <%# Eval("Estado") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowEditButton="True" EditText="Editar" UpdateText="Guardar" CancelText="Cancelar" ControlStyle-CssClass="text-indigo-600 hover:text-indigo-900 text-xs font-medium mr-2" />
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="flex gap-3 items-center">
                                <asp:LinkButton runat="server" CommandName="Delete" CssClass="text-rose-500 hover:text-rose-700 transition-colors" ToolTip="Eliminar" OnClientClick="return confirm('Eliminar auto?');">
                                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"></path></svg>
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" CommandName="Vender" CommandArgument='<%# Container.DataItemIndex %>' CssClass="text-emerald-500 hover:text-emerald-700 transition-colors" ToolTip="Vender" Visible='<%# Eval("Estado").ToString().ToLower() == "disponible" %>'>
                                    <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"></path></svg>
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    
    <div class="text-right">
        <asp:Label ID="lblGridMensaje" runat="server" CssClass="text-rose-500 font-medium text-xs bg-rose-50 px-3 py-1 rounded" />
    </div>

</asp:Content>
