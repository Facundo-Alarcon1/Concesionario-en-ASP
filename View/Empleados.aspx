<%@ Page Title="Gestion de Empleados" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Empleados" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Encabezado -->
    <div class="mb-8 border-l-4 border-amber-500 pl-4">
        <h2 class="text-3xl font-bold text-slate-900 tracking-tight">N&oacute;mina de Empleados</h2>
        <p class="text-sm text-slate-500 mt-1">Administra los accesos y el personal de la concesionaria.</p>
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-8">
        <!-- Panel Nuevo Empleado -->
        <div class="bg-slate-100/50 rounded-xl border border-slate-200 p-6 h-fit shadow-sm relative overflow-hidden">
            <div class="absolute top-0 left-0 w-full h-1 bg-amber-500"></div>
            <h3 class="text-sm font-bold text-slate-800 uppercase tracking-wider mb-4 border-b border-slate-200 pb-2 flex items-center gap-2">
                <svg class="w-4 h-4 text-amber-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z"></path></svg>
                Registrar Empleado
            </h3>
            
            <div class="space-y-4">
                <asp:HiddenField ID="hfEmpleadoId" runat="server" />
                
                <div>
                    <label class="block text-xs font-semibold text-slate-600 uppercase mb-1">Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input-modern" placeholder="Ej: Juan" required="true" />
                </div>
                <div>
                    <label class="block text-xs font-semibold text-slate-600 uppercase mb-1">Apellido</label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="input-modern" placeholder="Ej: P&eacute;rez" required="true" />
                </div>
                <div>
                    <label class="block text-xs font-semibold text-slate-600 uppercase mb-1">Puesto</label>
                    <asp:DropDownList ID="ddlPuesto" runat="server" CssClass="input-modern">
                        <asp:ListItem Text="Gerente" Value="Gerente"></asp:ListItem>
                        <asp:ListItem Text="Vendedor" Value="Vendedor"></asp:ListItem>
                        <asp:ListItem Text="Mecanico" Value="Mecanico"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="pt-4">
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-xs font-medium block mb-2" />
                    <div class="flex gap-2">
                        <asp:Button ID="btnGuardar" runat="server" CssClass="btn-primary-modern flex-1" Text="Guardar" OnClick="btnGuardar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btn-secondary-modern px-4" Text="Cancelar" OnClick="btnCancelar_Click" Visible="false" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Tabla de Empleados -->
        <div class="lg:col-span-2">
            <div class="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden">
                <div class="p-4 border-b border-slate-200 bg-slate-50/50 flex justify-between items-center">
                    <h3 class="font-semibold text-slate-800">Listado Activo</h3>
                    <span class="text-xs font-medium bg-amber-100 text-amber-800 px-2 py-1 rounded-full">Confidencial</span>
                </div>
                
                <div class="overflow-x-auto p-4">
                    <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_empleado" 
                        CssClass="gridview-modern w-full text-left text-sm" OnRowCommand="gvEmpleados_RowCommand" GridLines="None" BorderStyle="None">
                        
                        <Columns>
                            <asp:BoundField DataField="ID_empleado" HeaderText="ID" ReadOnly="true" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="Puesto" HeaderText="Puesto" />
                            
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <div class="flex gap-3 items-center">
                                        <asp:LinkButton runat="server" CommandName="SelectEdit" CommandArgument='<%# Container.DataItemIndex %>' CssClass="text-blue-500 hover:text-blue-700 transition-colors" ToolTip="Editar">
                                            <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z"></path></svg>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" CommandName="DeleteEmp" CommandArgument='<%# Container.DataItemIndex %>' CssClass="text-rose-500 hover:text-rose-700 transition-colors" ToolTip="Eliminar" OnClientClick="return confirm('¿Eliminar empleado?');">
                                            <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"></path></svg>
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
