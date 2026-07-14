<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Home" MaintainScrollPositionOnPostBack="true" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Encabezado -->
    <div class="mb-8 border-l-4 border-amber-500 pl-4">
        <h2 class="text-3xl font-bold text-slate-900 tracking-tight">Panel Principal</h2>
        <p class="text-sm text-slate-500 mt-1">Visi&oacute;n general y accesos r&aacute;pidos del sistema.</p>
    </div>

    <!-- Cards de Acceso Rápido -->
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        
        <a href="/View/Auto.aspx" class="bg-white rounded-xl shadow-sm border border-slate-200 p-6 flex flex-col items-center text-center hover:border-slate-300 hover:shadow-md transition-all text-decoration-none group">
            <div class="w-12 h-12 bg-blue-50 text-blue-600 rounded-lg flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
                <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 17h2c.6 0 1-.4 1-1v-3c0-.9-.7-1.7-1.5-1.9C18.7 10.6 16 10 16 10s-1.3-1.4-2.2-2.3c-.5-.4-1.1-.7-1.8-.7H5c-.6 0-1.1.4-1.4 1l-1.4 2.9A3.7 3.7 0 002 12.5V16c0 .6.4 1 1 1h2m14 0a2 2 0 11-4 0 2 2 0 014 0zM7 17a2 2 0 11-4 0 2 2 0 014 0z"></path></svg>
            </div>
            <h3 class="font-semibold text-slate-800">Autos</h3>
            <p class="text-xs text-slate-500 mt-1">Inventario y operaciones</p>
        </a>

        <a href="/View/Servicios.aspx" class="bg-white rounded-xl shadow-sm border border-slate-200 p-6 flex flex-col items-center text-center hover:border-slate-300 hover:shadow-md transition-all text-decoration-none group">
            <div class="w-12 h-12 bg-emerald-50 text-emerald-600 rounded-lg flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
                <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10.325 4.317c.426-1.756 2.924-1.756 3.35 0a1.724 1.724 0 002.573 1.066c1.543-.94 3.31.826 2.37 2.37a1.724 1.724 0 001.065 2.572c1.756.426 1.756 2.924 0 3.35a1.724 1.724 0 00-1.066 2.573c.94 1.543-.826 3.31-2.37 2.37a1.724 1.724 0 00-2.572 1.065c-.426 1.756-2.924 1.756-3.35 0a1.724 1.724 0 00-2.573-1.066c-1.543.94-3.31-.826-2.37-2.37a1.724 1.724 0 00-1.065-2.572c-1.756-.426-1.756-2.924 0-3.35a1.724 1.724 0 001.066-2.573c-.94-1.543.826-3.31 2.37-2.37.996.608 2.296.07 2.572-1.065z"></path><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"></path></svg>
            </div>
            <h3 class="font-semibold text-slate-800">Servicios</h3>
            <p class="text-xs text-slate-500 mt-1">Mantenimientos y taller</p>
        </a>

        <a href="/View/Comprobantes.aspx" class="bg-white rounded-xl shadow-sm border border-slate-200 p-6 flex flex-col items-center text-center hover:border-slate-300 hover:shadow-md transition-all text-decoration-none group">
            <div class="w-12 h-12 bg-amber-50 text-amber-600 rounded-lg flex items-center justify-center mb-4 group-hover:scale-110 transition-transform">
                <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"></path></svg>
            </div>
            <h3 class="font-semibold text-slate-800">Comprobantes</h3>
            <p class="text-xs text-slate-500 mt-1">Registro de operaciones</p>
        </a>

        <a href="/View/Empleados.aspx" class="group bg-white rounded-xl shadow-sm border border-slate-200 p-6 hover:shadow-md hover:border-amber-400 transition-all text-decoration-none">
            <div class="flex items-center justify-between mb-4">
                <div class="w-12 h-12 bg-amber-50 text-amber-600 rounded-lg flex items-center justify-center group-hover:bg-amber-100 transition-colors">
                    <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z"></path></svg>
                </div>
                <span class="text-xs font-semibold text-amber-600 bg-amber-100 px-2 py-1 rounded-full uppercase tracking-wide">Activo</span>
            </div>
            <h3 class="text-lg font-bold text-slate-800 mb-2">Empleados</h3>
            <p class="text-sm text-slate-500">Gesti&oacute;n de RRHH y accesos del sistema.</p>
        </a>
        
    </div>

</asp:Content>
