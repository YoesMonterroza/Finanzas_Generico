﻿using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using System;
using System.Configuration;
using System.Windows;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for IngresarCliente.xaml
    /// </summary>
    public partial class IngresarCliente : Window
    {
        public IngresarCliente()
        {
            InitializeComponent();
            this.Title = string.Format(ConfigurationManager.AppSettings["formatoTitulos"].ToString(), Conexion.Utilidades.Usuario, "Ingresar Cliente");
            cb_Estado.SelectedIndex = 0;
            txt_Identificacion.Focus();
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LimpiarCampos()
        {
            txt_Identificacion.Text = "";
            txt_Nombres.Text = "";
            txt_Apellidos.Text = "";
            txt_Direccion.Text = "";
            txt_Correo.Text = "";
            txt_Telefono.Text = "";
            cb_Estado.SelectedIndex = 0;
            txt_Observaciones.Text = "";
            txt_Identificacion.Focus();
        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            int capturarBinario;
            Persona p = new Persona();
            p.identificacion = txt_Identificacion.Text;
            p.nombres = txt_Nombres.Text;
            p.apellidos = txt_Apellidos.Text;
            p.telefono = txt_Telefono.Text;
            p.correo = txt_Correo.Text;
            p.direccion = txt_Direccion.Text;
            p.estado = Convert.ToString(cb_Estado.SelectedIndex);
            p.observaciones = txt_Observaciones.Text;
            p.usuarioModifica = int.Parse(Conexion.Utilidades.IdUsuario);
            capturarBinario = AdministrarPersona.InsertarPersona(p);
            LimpiarCampos();

            if (capturarBinario == 1)
                MessageBox.Show("Cliente guardado.");
            else
                MessageBox.Show("Error al guardar");
        }
    }
}