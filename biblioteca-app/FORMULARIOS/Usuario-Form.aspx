<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario-Form.aspx.cs" Inherits="biblioteca_app.Usuario_Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Usuarios</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #5a67d8;
            min-height: 100vh;
            padding: 20px;
        }

        .container {
            max-width: 900px;
            margin: 0 auto;
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
            overflow: hidden;
        }

        .header {
            background: #5a67d8;
            color: white;
            padding: 30px;
            text-align: center;
        }

        .header h1 {
            font-size: 28px;
            font-weight: 600;
            margin-bottom: 5px;
        }

        .header p {
            font-size: 14px;
            opacity: 0.9;
        }

        .form-content {
            padding: 40px;
        }

        .form-title {
            font-size: 24px;
            font-weight: 600;
            color: #333;
            margin-bottom: 30px;
            text-align: center;
        }

        .form-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 25px;
        }

        .form-group {
            margin-bottom: 25px;
        }

        .form-group label {
            display: block;
            margin-bottom: 8px;
            color: #333;
            font-weight: 500;
            font-size: 14px;
        }

        .form-group .required {
            color: #e74c3c;
            margin-left: 3px;
        }

        .form-control {
            width: 100%;
            padding: 12px 15px;
            border: 2px solid #e0e0e0;
            border-radius: 8px;
            font-size: 14px;
            transition: all 0.3s ease;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .form-control:focus {
            outline: none;
            border-color: #5a67d8;
            box-shadow: 0 0 0 3px rgba(90, 103, 216, 0.1);
        }

        .checkbox-group {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .checkbox-group input[type="checkbox"] {
            width: 20px;
            height: 20px;
            cursor: pointer;
        }

        .button-group {
            display: flex;
            gap: 15px;
            margin-top: 30px;
            flex-wrap: wrap;
        }

        .btn {
            padding: 12px 30px;
            border: none;
            border-radius: 8px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .btn-primary {
            background: #5a67d8;
            color: white;
            flex: 1;
        }

        .btn-primary:hover {
            background: #4c51bf;
            transform: translateY(-2px);
            box-shadow: 0 5px 20px rgba(90, 103, 216, 0.4);
        }

        .grid-container {
            margin-top: 30px;
            border-radius: 8px;
            overflow: hidden;
            border: 2px solid #e0e0e0;
        }

        .grid-view {
            width: 100%;
            border-collapse: collapse;
        }

        .grid-view th {
            background: #5a67d8;
            color: white;
            padding: 15px;
            text-align: left;
            font-weight: 600;
            font-size: 13px;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }

        .grid-view td {
            padding: 12px 15px;
            border-bottom: 1px solid #e0e0e0;
            font-size: 14px;
        }

        .grid-view tr:hover {
            background: #f8f9fa;
        }

        .grid-view a {
            color: #5a67d8;
            text-decoration: none;
            margin: 0 5px;
            font-weight: 500;
        }

        .grid-view a:hover {
            text-decoration: underline;
        }

        @media (max-width: 768px) {
            .form-row {
                grid-template-columns: 1fr;
            }

            .form-content {
                padding: 20px;
            }

            .button-group {
                flex-direction: column;
            }

            .btn {
                width: 100%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h1>Gestión de Usuarios</h1>
                <p>Administración de usuarios del sistema de biblioteca</p>
            </div>
            
            <div class="form-content">
                <h2 class="form-title">Usuarios</h2>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtNombre">Nombre <span class="required">*</span></label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del usuario" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" 
                            ErrorMessage="El nombre es obligatorio" 
                            ForeColor="#e74c3c" 
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtApellido">Apellido <span class="required">*</span></label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido del usuario" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" 
                            ControlToValidate="txtApellido" 
                            ErrorMessage="El apellido es obligatorio" 
                            ForeColor="#e74c3c" 
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtEmail">Email <span class="required">*</span></label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="ejemplo@correo.com" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                            ControlToValidate="txtEmail" 
                            ErrorMessage="El email es obligatorio" 
                            ForeColor="#e74c3c" 
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                            ControlToValidate="txtEmail" 
                            ErrorMessage="Formato de email inválido" 
                            ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" 
                            ForeColor="#e74c3c" 
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtTelefono">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Número de teléfono" MaxLength="20"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtDireccion">Dirección</label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Dirección completa" MaxLength="200"></asp:TextBox>
                </div>

                <div class="form-row">
                    <div class="form-group">
                        <label for="ddlRol">Rol <span class="required">*</span></label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRol" runat="server" 
                            ControlToValidate="ddlRol" 
                            InitialValue="0"
                            ErrorMessage="Debe seleccionar un rol" 
                            ForeColor="#e74c3c" 
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="chkActivo">Estado</label>
                        <div class="checkbox-group">
                            <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                            <label for="chkActivo" style="margin: 0;">Usuario Activo</label>
                        </div>
                    </div>
                </div>

                <div class="button-group">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>

                <div class="grid-container">
                    <asp:GridView ID="gvUsuarios" runat="server" CssClass="grid-view" AutoGenerateColumns="False" 
                        DataKeyNames="id_usuario" OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="id_usuario" HeaderText="ID" ReadOnly="True" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="email" HeaderText="Email" />
                            <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="nombre_rol" HeaderText="Rol" />
                            <asp:CheckBoxField DataField="activo" HeaderText="Activo" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" HeaderText="Acción" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
