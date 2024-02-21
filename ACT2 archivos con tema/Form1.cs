using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACT2_archivos_con_tema
{
    public partial class Form1 : Form
    {
        private const string SequentialFilePath = "sequential.txt";
        private const string DirectAccessFilePath = "directAccess.bin";
        private const string IndexedFilePath = "indexed.txt";
        // Agrega estos controles en tu formulario de Windows Forms
        private TextBox txtId;
        private TextBox txtName;
        private TextBox txtSalary;
     

        public Form1()
        {
            InitializeComponent();
            // Inicialización de controles para la entrada del usuario
            txtId = new TextBox();
            txtId.Location = new Point(10, 10);
            this.Controls.Add(txtId);

            txtName = new TextBox();
            txtName.Location = new Point(10, 40);
            this.Controls.Add(txtName);

            txtSalary = new TextBox();
            txtSalary.Location = new Point(10, 70);
            this.Controls.Add(txtSalary);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSequential_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                int salary = int.Parse(txtSalary.Text);

                WriteSequentialFile(new Employee { Id = id, Name = name, Salary = salary });
                MessageBox.Show("Datos escritos correctamente en el archivo secuencial.");

                ReadSequentialFile();
                MessageBox.Show("Datos leídos correctamente del archivo secuencial.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en operaciones de archivo secuencial: {ex.Message}");
            }
        }
        private void WriteSequentialFile(Employee employee)
        {
            using (StreamWriter writer = new StreamWriter(SequentialFilePath, true))
            {
                writer.WriteLine($"{employee.Id},{employee.Name},{employee.Salary}");
            }
        }

        private void ReadSequentialFile()
        {
            textBoxOutput.Text = "";  // Limpia el contenido anterior en el cuadro de texto

            using (StreamReader reader = new StreamReader(SequentialFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Agrega cada línea al cuadro de texto
                    textBoxOutput.AppendText(line + Environment.NewLine);
                }
            }
        }

        private void btnDirectAccess_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                int salary = int.Parse(txtSalary.Text);

                WriteDirectAccessFile(new Employee { Id = id, Name = name, Salary = salary });
                MessageBox.Show("Datos escritos correctamente en el archivo de acceso directo.");

                ReadDirectAccessFile();
                MessageBox.Show("Datos leídos correctamente del archivo de acceso directo.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en operaciones de archivo de acceso directo: {ex.Message}");
            }
        }
        private void WriteDirectAccessFile(Employee employee)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(DirectAccessFilePath, FileMode.Append)))
            {
                byte[] recordBytes = StructureToByteArray(employee);
                writer.Write(recordBytes);
            }
        }

        private void ReadDirectAccessFile()
        {
            textBoxOutput.Text = "";  // Limpia el contenido anterior en el cuadro de texto

            List<Employee> employees = new List<Employee>();

            using (BinaryReader reader = new BinaryReader(File.Open(DirectAccessFilePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    byte[] recordBytes = reader.ReadBytes(Marshal.SizeOf(typeof(Employee)));
                    Employee employee = ByteArrayToStructure<Employee>(recordBytes);
                    employees.Add(employee);
                }
            }

            // Agrega cada empleado al cuadro de texto
            foreach (var employee in employees)
            {
                textBoxOutput.AppendText($"{employee.Id}: {employee.Name}, {employee.Salary}" + Environment.NewLine);
            }
        }

        private void btnIndexed_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                int salary = int.Parse(txtSalary.Text);

                WriteIndexedFile(new Employee { Id = id, Name = name, Salary = salary });
                MessageBox.Show("Datos escritos correctamente en el archivo indexado.");

                ReadIndexedFile();
                MessageBox.Show("Datos leídos correctamente del archivo indexado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en operaciones de archivo indexado: {ex.Message}");
            }
        }
        private void WriteIndexedFile(Employee employee)
        {
            using (StreamWriter writer = new StreamWriter(IndexedFilePath, true))
            {
                writer.WriteLine($"{employee.Id},{employee.Name},{employee.Salary}");
            }
        }

        private void ReadIndexedFile()
        {
            textBoxOutput.Text = "";  // Limpia el contenido anterior en el cuadro de texto

            Dictionary<int, Employee> indexedData = new Dictionary<int, Employee>();

            using (StreamReader reader = new StreamReader(IndexedFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int key))
                    {
                        Employee employee = new Employee
                        {
                            Id = key,
                            Name = parts[1],
                            Salary = int.Parse(parts[2])
                        };
                        indexedData[key] = employee;
                    }
                }
            }

            // Ordena el diccionario por clave antes de mostrarlo
            var sortedData = indexedData.OrderBy(pair => pair.Key);

            // Agrega cada entrada ordenada del diccionario al cuadro de texto
            foreach (var entry in sortedData)
            {
                textBoxOutput.AppendText($"{entry.Value.Id}: {entry.Value.Name}, {entry.Value.Salary}" + Environment.NewLine);
            }
        }

        // Métodos auxiliares y estructuras
        private static byte[] StructureToByteArray<T>(T structure) where T : struct
        {
            int size = Marshal.SizeOf(structure);
            byte[] array = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.StructureToPtr(structure, ptr, false);
                Marshal.Copy(ptr, array, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return array;
        }

        private static T ByteArrayToStructure<T>(byte[] array) where T : struct
        {
            T structure;
            int size = Marshal.SizeOf(typeof(T));

            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(array, 0, ptr, size);
                structure = (T)Marshal.PtrToStructure(ptr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return structure;
        }

        // Estructura para archivos de acceso directo
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct Employee
        {
            public int Id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string Name;
            public int Salary;
        }   
    }
}
