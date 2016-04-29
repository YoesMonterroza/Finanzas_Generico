using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows;
using System.Diagnostics;
using System.Globalization;

namespace Finanzas_Generico.Entidades
{
    class GenerarPdf
    {
        public static void GenerarPdfFactura(Persona per, List<ListaProductoVenta> listPv, Venta vt, string codigoFactura = null)
        {
            //Se defime la fecha actual y se formatea
            DateTime dFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string sCapturaFecha = dFechaActual.ToString("d", DateTimeFormatInfo.InvariantInfo);

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"D:\prueba.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Factura de venta");
            doc.AddCreator("Luna Turqueza");

            // Abrimos el archivo
            doc.Open();

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Luna turqueza"));
            doc.Add(Chunk.NEWLINE);

            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            #region  Creación tabla datos cliente            
            PdfPTable tblDatosCliente = new PdfPTable(4);
            tblDatosCliente.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell columna1 = new PdfPCell(new Phrase("", _standardFont));
            columna1.BorderWidth = 0;
            columna1.BorderWidthBottom = 0.75f;

            PdfPCell columna2 = new PdfPCell(new Phrase("", _standardFont));
            columna2.BorderWidth = 0;
            columna2.BorderWidthBottom = 0.75f;

            PdfPCell columna3 = new PdfPCell(new Phrase("", _standardFont));
            columna3.BorderWidth = 0;
            columna3.BorderWidthBottom = 0.75f;

            PdfPCell columna4 = new PdfPCell(new Phrase("", _standardFont));
            columna4.BorderWidth = 0;
            columna4.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblDatosCliente.AddCell(columna1);
            tblDatosCliente.AddCell(columna2);
            tblDatosCliente.AddCell(columna3);
            tblDatosCliente.AddCell(columna4);


            // Llenamos la tabla con información
            columna1 = new PdfPCell(new Phrase("Fecha", _standardFont));
            columna1.BorderWidth = 0;
            columna2 = new PdfPCell(new Phrase(sCapturaFecha, _standardFont));
            columna2.BorderWidth = 0;
            columna3 = new PdfPCell(new Phrase("Factura de venta", _standardFont));
            columna3.BorderWidth = 0;
            columna4 = new PdfPCell(new Phrase(codigoFactura, _standardFont));
            columna4.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblDatosCliente.AddCell(columna1);
            tblDatosCliente.AddCell(columna2);
            tblDatosCliente.AddCell(columna3);
            tblDatosCliente.AddCell(columna4);

            // Llenamos la tabla con información
            columna1 = new PdfPCell(new Phrase("Nombre", _standardFont));
            columna1.BorderWidth = 0;
            columna2 = new PdfPCell(new Phrase(per.nombres, _standardFont));
            columna2.BorderWidth = 0;
            columna3 = new PdfPCell(new Phrase("Identificación", _standardFont));
            columna3.BorderWidth = 0;
            columna4 = new PdfPCell(new Phrase(per.identificacion, _standardFont));
            columna4.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblDatosCliente.AddCell(columna1);
            tblDatosCliente.AddCell(columna2);
            tblDatosCliente.AddCell(columna3);
            tblDatosCliente.AddCell(columna4);

            // Llenamos la tabla con información
            columna1 = new PdfPCell(new Phrase("Dirección", _standardFont));
            columna1.BorderWidth = 0;
            columna2 = new PdfPCell(new Phrase(per.direccion, _standardFont));
            columna2.BorderWidth = 0;
            columna3 = new PdfPCell(new Phrase("Telefono", _standardFont));
            columna3.BorderWidth = 0;
            columna4 = new PdfPCell(new Phrase(per.telefono, _standardFont));
            columna4.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblDatosCliente.AddCell(columna1);
            tblDatosCliente.AddCell(columna2);
            tblDatosCliente.AddCell(columna3);
            tblDatosCliente.AddCell(columna4);

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblDatosCliente);
            doc.Add(new Chunk(" "));
            #endregion
            #region Creación de la tabla productos en venta
            PdfPTable tblDatosProductos = new PdfPTable(5);
            tblDatosProductos.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell codigo = new PdfPCell(new Phrase("Código", _standardFont));
            codigo.BorderWidth = 0;
            codigo.BorderWidthBottom = 0.75f;

            PdfPCell nombre = new PdfPCell(new Phrase("Nombre", _standardFont));
            nombre.BorderWidth = 0;
            nombre.BorderWidthBottom = 0.75f;

            PdfPCell cantidad = new PdfPCell(new Phrase("Cantidad", _standardFont));
            cantidad.BorderWidth = 0;
            cantidad.BorderWidthBottom = 0.75f;

            PdfPCell valor = new PdfPCell(new Phrase("Valor", _standardFont));
            valor.BorderWidth = 0;
            valor.BorderWidthBottom = 0.75f;

            PdfPCell valorTotal = new PdfPCell(new Phrase("Valor Total", _standardFont));
            valorTotal.BorderWidth = 0;
            valorTotal.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblDatosProductos.AddCell(codigo);
            tblDatosProductos.AddCell(nombre);
            tblDatosProductos.AddCell(cantidad);
            tblDatosProductos.AddCell(valor);
            tblDatosProductos.AddCell(valorTotal);

            foreach (var pro in listPv)
            {
                codigo = new PdfPCell(new Phrase(pro.codigo, _standardFont));
                codigo.BorderWidth = 0;

                nombre = new PdfPCell(new Phrase(pro.nombre, _standardFont));
                nombre.BorderWidth = 0;

                cantidad = new PdfPCell(new Phrase(pro.cantidad.ToString(), _standardFont));
                cantidad.BorderWidth = 0;

                valor = new PdfPCell(new Phrase(pro.precio.ToString(), _standardFont));
                valor.BorderWidth = 0;

                valorTotal = new PdfPCell(new Phrase(pro.precioTotal.ToString(), _standardFont));
                valorTotal.BorderWidth = 0;

                // Añadimos las celdas a la tabla
                tblDatosProductos.AddCell(codigo);
                tblDatosProductos.AddCell(nombre);
                tblDatosProductos.AddCell(cantidad);
                tblDatosProductos.AddCell(valor);
                tblDatosProductos.AddCell(valorTotal);
            }

            doc.Add(tblDatosProductos);
            doc.Add(new Chunk(" "));
            #endregion
            #region valores factura
            PdfPTable tblprecios = new PdfPTable(3);
            tblprecios.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell subTotal = new PdfPCell(new Phrase("Sub - Total", _standardFont));
            subTotal.BorderWidth = 0;
            subTotal.BorderWidthBottom = 0.75f;

            PdfPCell descuento = new PdfPCell(new Phrase("Descuento", _standardFont));
            descuento.BorderWidth = 0;
            descuento.BorderWidthBottom = 0.75f;

            PdfPCell total = new PdfPCell(new Phrase("Total", _standardFont));
            total.BorderWidth = 0;
            total.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblprecios.AddCell(subTotal);
            tblprecios.AddCell(descuento);
            tblprecios.AddCell(total);

            decimal dSubtotal = vt.subTotal;
            decimal dDescuento = vt.descuento;
            decimal dTotal = vt.total;

            subTotal = new PdfPCell(new Phrase(dSubtotal.ToString(), _standardFont));
            subTotal.BorderWidth = 0;

            descuento = new PdfPCell(new Phrase(dDescuento.ToString(), _standardFont));
            descuento.BorderWidth = 0;

            total = new PdfPCell(new Phrase(dTotal.ToString(), _standardFont));
            total.BorderWidth = 0;

            tblprecios.AddCell(subTotal);
            tblprecios.AddCell(descuento);
            tblprecios.AddCell(total);

            doc.Add(tblprecios);
            doc.Add(new Chunk(" "));
            #endregion
            #region Observaciones
            PdfPTable tblObservaciones = new PdfPTable(1);
            tblObservaciones.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell observacion = new PdfPCell(new Phrase("Observación", _standardFont));
            observacion.BorderWidth = 0;
            observacion.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblObservaciones.AddCell(observacion);

            observacion = new PdfPCell(new Phrase(vt.observacion, _standardFont));
            observacion.BorderWidth = 0;

            tblObservaciones.AddCell(observacion);

            doc.Add(tblObservaciones);
            #endregion
            doc.Close();
            writer.Close();
                        
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "D:\\prueba.pdf";
            proc.Start();
            proc.Close();

        }
        
    }
}
