<%@ Page Title="Servicios" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Servicios" MaintainScrollPositionOnPostBack="true" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="mb-6 flex justify-between items-end">
        <div>
            <h2 class="text-2xl font-bold text-slate-900">Servicios y Mantenimiento</h2>
            <p class="text-sm text-slate-500 mt-1">Programa y administra las tareas del taller mec&aacute;nico.</p>
        </div>
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
        
        <!-- Contenedor Agregar -->
        <div class="lg:col-span-1">
            <div class="bg-slate-100/50 rounded-xl border border-slate-200 p-6 shadow-sm">
                <h3 class="text-sm font-bold text-slate-800 uppercase tracking-wider mb-4 border-b border-slate-200 pb-2 flex items-center gap-2">
                    <svg class="w-4 h-4 text-slate-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 6v6m0 0v6m0-6h6m-6 0H6"></path></svg>
                    Nuevo Servicio
                </h3>
                
                <div class="space-y-4">
                    <div>
                        <label class="block text-xs font-semibold text-slate-600 mb-1.5">Descripci&oacute;n</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-modern" />
                    </div>
                    
                    <div>
                        <label class="block text-xs font-semibold text-slate-600 mb-1.5">Mec&aacute;nico / Empleado</label>
                        <asp:DropDownList ID="ddlEmpleados" runat="server" CssClass="input-modern" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>

                    <div>
                        <label class="block text-xs font-semibold text-slate-600 mb-1.5">Fecha Programada</label>
                        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="input-modern" />
                    </div>
                    
                    <div class="pt-2">
                        <asp:Button ID="btnAgregar" runat="server" CssClass="w-full btn-primary-modern" Text="Crear Servicio" OnClick="btnAgregar_Click" />
                    </div>
                    
                    <asp:Label ID="lblMensaje" runat="server" CssClass="block text-xs font-medium text-center mt-2" />
                </div>
            </div>
        </div>

        <!-- Contenedor Tabla -->
        <div class="lg:col-span-2">
            <div class="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden">
                <div class="px-6 py-4 border-b border-slate-200 bg-white">
                    <h3 class="text-sm font-bold text-slate-800">Agenda de Trabajos</h3>
                </div>
                
                <div class="overflow-x-auto w-full">
                    <asp:GridView ID="gvServicios" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_servicio" 
                        CssClass="gridview-modern w-full text-left text-sm" OnRowCommand="gvServicios_RowCommand" GridLines="None" BorderStyle="None">
                        <HeaderStyle CssClass="bg-slate-50 text-slate-500 font-semibold uppercase tracking-wider text-xs border-b border-slate-200" />
                        <RowStyle CssClass="bg-white border-b border-slate-100 hover:bg-slate-50 transition-colors text-slate-700" />
                        <AlternatingRowStyle CssClass="bg-slate-50 border-b border-slate-100 hover:bg-slate-100 transition-colors text-slate-700" />
                        
                        <Columns>
                            <asp:BoundField DataField="ID_servicio" HeaderText="ID" />
                            <asp:BoundField DataField="Descripcion" HeaderText="Descripci&oacute;n" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="whitespace-nowrap" />
                            
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <span class="inline-flex items-center px-2 py-0.5 rounded text-[11px] font-semibold uppercase tracking-wider <%# Eval("Estado").ToString().ToUpper() == "PENDIENTE" || Eval("Estado").ToString().ToUpper() == "EN PROCESO" ? "bg-amber-100 text-amber-800" : "bg-emerald-100 text-emerald-800" %>">
                                        <%# Eval("Estado") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="NombreEmpleado" HeaderText="Responsable" />
                            
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <div class="flex gap-3 justify-end items-center px-2">
                                        <asp:LinkButton runat="server" CommandName="Realizado" CommandArgument='<%# Container.DataItemIndex %>' CssClass="text-emerald-500 hover:text-emerald-700 transition-colors" ToolTip="Marcar como Completado">
                                            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"></path></svg>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="text-rose-500 hover:text-rose-700 transition-colors" ToolTip="Eliminar">
                                            <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"></path></svg>
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
