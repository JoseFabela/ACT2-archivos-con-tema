using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ACT2_archivos_con_tema.Form1;

namespace ACT2_archivos_con_tema
{
    public partial class Form1 : Form
    {

        private readonly AgendaContactos agenda = new AgendaContactos();

        public Form1()
        {
            InitializeComponent();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var nuevoContacto = new AgendaContactos.Contacto
            {
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Correo = txtCorreo.Text,
                Direccion = txtDireccion.Text
            };

            // Agregar utilizando archivos secuenciales
            agenda.AgregarContactoSecuencial(nuevoContacto);

            // Agregar utilizando archivos secuenciales indexados
            agenda.AgregarContactoSecuencialIndexado(nuevoContacto);

            // Agregar utilizando archivos de acceso directo
            agenda.AgregarContactoAccesoDirecto(nuevoContacto);

            LimpiarCampos();
            MessageBox.Show("Contacto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            MostrarContactos("Secuencial", agenda.ObtenerContactosSecuencial());
            MostrarContactos("Secuencial Indexado", agenda.ObtenerContactosSecuencialIndexado());
            MostrarContactos("Acceso Directo", agenda.ObtenerContactosAccesoDirecto());

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreBusqueda = txtBuscar.Text;
            if (!string.IsNullOrEmpty(nombreBusqueda))
            {
                BuscarContacto("Secuencial Indexado", nombreBusqueda, agenda.BuscarContactoSecuencialIndexado);
                BuscarContacto("Acceso Directo", nombreBusqueda, agenda.BuscarContactoAccesoDirecto);
            }
        }
        private void MostrarContactos(string tipo, List<AgendaContactos.Contacto> contactos)
        {
            listBoxContactos.Items.Add($"Contactos ({tipo}):");

            // Ordenar la lista según el tipo de acceso
            switch (tipo)
            {
                case "Secuencial":
                    contactos = agenda.OrdenarContactosPorNombre(contactos);
                    break;
                case "Secuencial Indexado":
                    contactos = agenda.OrdenarContactosPorTelefono(contactos);
                    break;
                case "Acceso Directo":
                    contactos = agenda.OrdenarContactosPorCorreo(contactos);
                    break;
                default:
                    break;
            }

            foreach (var contacto in contactos)
            {
                listBoxContactos.Items.Add($"{contacto.Nombre}, {contacto.Telefono}, {contacto.Correo}, {contacto.Direccion}");
            }
            listBoxContactos.Items.Add("-------------------------");
        }







        private void BuscarContacto(string tipo, string nombre, Func<string, AgendaContactos.Contacto> buscarFunc)
        {
            var encontrado = buscarFunc(nombre);
            if (encontrado != null)
            {
                MessageBox.Show($"Contacto encontrado ({tipo}): \n" +
                                $"Nombre: {encontrado.Nombre}\n" +
                                $"Teléfono: {encontrado.Telefono}\n" +
                                $"Correo: {encontrado.Correo}\n" +
                                $"Dirección: {encontrado.Direccion}",
                                "Información del Contacto",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Contacto no encontrado ({tipo}).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
        }
    }
}
[Serializable]
public class AgendaContactos
{

    private const string SecuencialFilePath = "secuencial.txt";
    private const string SecuencialIndexadoFilePath = "secuencial_indexado.txt";
    private const string AccesoDirectoFilePath = "acceso_directo.dat";

    [Serializable]
    public class Contacto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }

    public void AgregarContactoSecuencial(Contacto nuevoContacto)
    {
        using (StreamWriter writer = new StreamWriter(SecuencialFilePath, true))
        {
            writer.WriteLine($"{nuevoContacto.Nombre},{nuevoContacto.Telefono},{nuevoContacto.Correo},{nuevoContacto.Direccion}");
        }
    }

    public List<Contacto> ObtenerContactosSecuencial()
    {
        List<Contacto> contactos = new List<Contacto>();

        if (File.Exists(SecuencialFilePath))
        {
            using (StreamReader reader = new StreamReader(SecuencialFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        contactos.Add(new Contacto
                        {
                            Nombre = parts[0],
                            Telefono = parts[1],
                            Correo = parts[2],
                            Direccion = parts[3]
                        });
                    }
                }
            }
        }

        return contactos;
    }

    public void AgregarContactoSecuencialIndexado(Contacto nuevoContacto)
    {
        // Implementar lógica para archivos secuenciales indexados
        using (BinaryWriter writer = new BinaryWriter(new FileStream(SecuencialIndexadoFilePath, FileMode.Append)))
        {
            // Escribe los datos del nuevo contacto en el archivo
            writer.Write(nuevoContacto.Nombre);
            writer.Write(nuevoContacto.Telefono);
            writer.Write(nuevoContacto.Correo);
            writer.Write(nuevoContacto.Direccion);
        }
    }

    public List<Contacto> ObtenerContactosSecuencialIndexado()
    {
        List<Contacto> contactos = new List<Contacto>();

        using (BinaryReader reader = new BinaryReader(new FileStream(SecuencialIndexadoFilePath, FileMode.OpenOrCreate)))
        {
            // Leer los datos del archivo y construir la lista de contactos
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string nombre = reader.ReadString();
                string telefono = reader.ReadString();
                string correo = reader.ReadString();
                string direccion = reader.ReadString();

                contactos.Add(new Contacto
                {
                    Nombre = nombre,
                    Telefono = telefono,
                    Correo = correo,
                    Direccion = direccion
                });
            }
        }

        return contactos;
    }

    public void AgregarContactoAccesoDirecto(Contacto nuevoContacto)
    {
        // Implementar lógica para archivos de acceso directo
        using (BinaryWriter writer = new BinaryWriter(new FileStream(AccesoDirectoFilePath, FileMode.Append)))
        {
            // Escribe los datos del nuevo contacto en el archivo binario
            writer.Write(nuevoContacto.Nombre);
            writer.Write(nuevoContacto.Telefono);
            writer.Write(nuevoContacto.Correo);
            writer.Write(nuevoContacto.Direccion);
        }
    }

    public List<Contacto> ObtenerContactosAccesoDirecto()
    {
        List<Contacto> contactos = new List<Contacto>();

        using (BinaryReader reader = new BinaryReader(new FileStream(AccesoDirectoFilePath, FileMode.OpenOrCreate)))
        {
            // Leer los datos del archivo binario y construir la lista de contactos
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string nombre = reader.ReadString();
                string telefono = reader.ReadString();
                string correo = reader.ReadString();
                string direccion = reader.ReadString();

                contactos.Add(new Contacto
                {
                    Nombre = nombre,
                    Telefono = telefono,
                    Correo = correo,
                    Direccion = direccion
                });
            }
        }

        return contactos;
    }

    public Contacto BuscarContactoSecuencialIndexado(string nombre)
    {
        // Implementar lógica para buscar contacto en archivos secuenciales indexados
        using (BinaryReader reader = new BinaryReader(new FileStream(SecuencialIndexadoFilePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string currentNombre = reader.ReadString();
                string telefono = reader.ReadString();
                string correo = reader.ReadString();
                string direccion = reader.ReadString();

                if (currentNombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return new Contacto
                    {
                        Nombre = currentNombre,
                        Telefono = telefono,
                        Correo = correo,
                        Direccion = direccion
                    };
                }
            }
        }

        return null;
    }

    public Contacto BuscarContactoAccesoDirecto(string nombre)
    {
        // Implementar lógica para buscar contacto en archivos de acceso directo
        using (BinaryReader reader = new BinaryReader(new FileStream(AccesoDirectoFilePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string currentNombre = reader.ReadString();
                string telefono = reader.ReadString();
                string correo = reader.ReadString();
                string direccion = reader.ReadString();

                if (currentNombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return new Contacto
                    {
                        Nombre = currentNombre,
                        Telefono = telefono,
                        Correo = correo,
                        Direccion = direccion
                    };
                }
            }
        }

        return null;
    }
    // Agrega estos métodos a la clase AgendaContactos

    public List<Contacto> OrdenarContactosPorNombre(List<Contacto> contactos)
    {
        return contactos.OrderBy(c => c.Nombre).ToList();
    }

    public List<Contacto> OrdenarContactosPorTelefono(List<Contacto> contactos)
    {
        return contactos.OrderBy(c => c.Telefono).ToList();
    }

    public List<Contacto> OrdenarContactosPorCorreo(List<Contacto> contactos)
    {
        return contactos.OrderBy(c => c.Correo).ToList();
    }

}