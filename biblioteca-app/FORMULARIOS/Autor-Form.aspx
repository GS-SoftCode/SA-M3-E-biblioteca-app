<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Autor-Form.aspx.cs" Inherits="biblioteca_app.Autor_Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Gestión de Autores</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #4A90E2;
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
            background: #4A90E2;
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
            border-color: #4A90E2;
            box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.1);
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
            background: #4A90E2;
            color: white;
            flex: 1;
        }

        .btn-primary:hover {
            background: #357ABD;
            transform: translateY(-2px);
            box-shadow: 0 5px 20px rgba(74, 144, 226, 0.4);
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
            background: #4A90E2;
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
            color: #4A90E2;
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
                <h1>Gestión de Autores</h1>
                <p>Administración de autores del sistema de biblioteca</p>
            </div>
            
            <div class="form-content">
                <h2 class="form-title">Autores</h2>

                <div class="form-row">
                    <div class="form-group">
                        <label for="txtNombre">Nombre <span class="required">*</span></label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del autor" MaxLength="100"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                            ControlToValidate="txtNombre" 
                            ErrorMessage="El nombre es obligatorio" 
                            ForeColor="#e74c3c" 
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group">
                        <label for="txtApellido">Apellido <span class="required">*</span></label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido del autor" MaxLength="100"></asp:TextBox>
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
                        <label for="txtNacionalidad">Nacionalidad</label>
                        <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control" placeholder="País de origen" MaxLength="50"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label for="txtFechaNacimiento">Fecha de Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="button-group">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                </div>

                <div class="grid-container">
                    <asp:GridView ID="gvAutores" runat="server" CssClass="grid-view" AutoGenerateColumns="False" 
                        DataKeyNames="id_autor" OnSelectedIndexChanged="gvAutores_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="id_autor" HeaderText="ID" ReadOnly="True" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="nacionalidad" HeaderText="Nacionalidad" />
                            <asp:BoundField DataField="fecha_nacimiento" HeaderText="Fecha Nacimiento" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" HeaderText="Acción" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
