﻿using Finanzas_Generico.Entidades;
using Finanzas_Generico.Manager;
using System;
using System.Configuration;
using System.Windows;

namespace Finanzas_Generico.Vistas
{
    /// <summary>
    /// Interaction logic for ActualizarCliente.xaml
    /// </summary>
    public partial class ActualizarCliente : Window
    {
        public ActualizarCliente()
        {
            InitializeComponent();
            this.Title = string.Format(ConfigurationManager.AppSettings["formatoTitulos"].ToString(), Conexion.Utilidades.Usuario, "Actualizar Cliente");
            txt_Identificacion.Focus();
        }

        public ActualizarCliente(String IdCliente)
        {
            InitializeComponent();

            AdministrarPersona ap = new AdministrarPersona();
            Persona person = new Persona();
            person = ap.ConsultarPersona(IdCliente);

            txt_Identificacion.Text = IdCliente;
            txt_Nombres.Text = person.nombres;
            txt_Apellidos.Text = person.apellidos;
            txt_Direccion.Text = person.direccion;
            txt_Correo.Text = person.correo;
            txt_Telefono.Text = person.telefono;
            cb_Estado.SelectedIndex = Convert.ToInt32(person.estado);
            txt_Observaciones.Text = person.observaciones;

            btn_Buscar.IsEnabled = false;
            txt_Identificacion.IsEnabled = false;
            txt_Nombres.Focus();
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            this.Close();
        }

        private void btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            AdministrarPersona ap = new AdministrarPersona();
            Persona per = new Persona();
            per = ap.ConsultarPersona(txt_Identificacion.Text);

            txt_Nombres.Text = per.nombres;
            txt_Apellidos.Text = per.apellidos;
            txt_Direccion.Text = per.direccion;
            txt_Correo.Text = per.correo;
            txt_Telefono.Text = per.telefono;
            cb_Estado.SelectedIndex = Convert.ToInt32(per.estado);
            txt_Observaciones.Text = per.observaciones;

            btn_Buscar.IsEnabled = false;
            txt_Identificacion.IsEnabled = false;
        }

        private void btn_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            int capturarBinario;
            Persona p = new Persona();
            p.identificacion = txt_Identificacion.Text;
            p.nombres = txt_Nombres.Text;
            p.apellidos = txt_Apellidos.Text;
            p.direccion = txt_Direccion.Text;
            p.correo = txt_Correo.Text;
            p.telefono = txt_Telefono.Text;
            p.estado = Convert.ToString(cb_Estado.SelectedIndex);
            p.observaciones = txt_Observaciones.Text;
            p.usuarioModifica = int.Parse(Conexion.Utilidades.IdUsuario);
            capturarBinario = AdministrarPersona.ActualizarPersona(p);

            if (capturarBinario == 1)
            {
                LimpiarCampos();
                MessageBox.Show("Perona actualizada.");
            }
            else {
                MessageBox.Show("Error al actualizar");
            }
        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
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

            btn_Buscar.IsEnabled = true;
            txt_Identificacion.IsEnabled = true;
            txt_Identificacion.Focus();
        }
    }
}