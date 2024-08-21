using System;
using System.Windows.Forms;
using EasyModbus;  // Importa EasyModbus

namespace ModbusClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Dirección IP y puerto del PLC
                string plcIpAddress = "192.168.0.1"; // Aqui va la IP del PLC,hay que cambiarla.
                int port = 502; // Puerto Modbus TCP/IP

                // Crea cliente Modbus
                ModbusClient modbusClient = new ModbusClient(plcIpAddress, port);

                // Conecta al PLC
                modbusClient.Connect();

                // Lee el registro que envia el PLC
                int startingAddress = 0; // Dirección inicial del registro
                int[] registers = modbusClient.ReadHoldingRegisters(startingAddress, 1);

                // Mostrar el valor recibido
                MessageBox.Show($"Valor recibido: {registers[0]}", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Desconectar
                modbusClient.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Crear y configurar el botón de conexión
            Button btnConnect = new Button();
            btnConnect.Text = "Conectar";
            btnConnect.Location = new System.Drawing.Point(100, 100);
            btnConnect.Click += new EventHandler(btnConnect_Click);

            // Agregar el botón al formulario
            this.Controls.Add(btnConnect);
        }
    }
}
