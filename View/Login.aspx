<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ConcesionarioWEBFORM1111.Login" MasterPageFile="~/Site1.Master" MaintainScrollPositionOnPostBack="true" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="min-h-[70vh] flex items-center justify-center">
        <!-- Clean Login Card -->
        <div class="bg-white rounded-2xl shadow-sm border border-slate-200 p-8 w-full max-w-md">
            
            <div class="text-center mb-8">
                <div class="w-14 h-14 bg-slate-900 text-amber-400 rounded-2xl flex items-center justify-center mx-auto mb-5 shadow-lg shadow-slate-900/20">
                    <svg class="w-7 h-7" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z"></path></svg>
                </div>
                <h2 class="text-2xl font-bold text-slate-900 tracking-tight">Acceso Privado</h2>
                <p class="text-slate-500 text-sm mt-1">Ingresa tus credenciales para continuar</p>
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="block mb-6 text-sm font-medium text-rose-600 bg-rose-50 border border-rose-100 px-4 py-3 rounded-lg text-center empty:hidden"></asp:Label>
            
            <div class="space-y-5">
                <div>
                    <label class="block text-sm font-semibold text-slate-700 mb-1.5">Usuario / Legajo</label>
                    <asp:TextBox ID="txtUsuario" runat="server" placeholder="ej. usuario123" CssClass="input-modern"></asp:TextBox>
                </div>
                
                <div>
                    <label class="block text-sm font-semibold text-slate-700 mb-1.5">Contrase&ntilde;a</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="********" CssClass="input-modern"></asp:TextBox>
                </div>

                <div class="pt-2">
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesi&oacute;n" OnClick="btnLogin_Click" CssClass="w-full btn-primary-modern py-2.5" />
                </div>
            </div>

        </div>
    </div>
</asp:Content>
