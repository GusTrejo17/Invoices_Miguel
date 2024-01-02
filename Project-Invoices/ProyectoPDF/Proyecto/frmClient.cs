using Proyecto.Capa_Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class frmClient : Form
    {
        private string operation = string.Empty;
        private int idCustomer;

        private Conexion conexion;
        public frmClient()
        {
            InitializeComponent();
            conexion = new Conexion();
            CargarDatosClientes();
        }

        private void CargarDatosClientes()
        {
            try
            {
                conexion.OpenConnection();

                // Crear un comando SQL que llama al procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CustomerProcess", conexion.GetSqlConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro @action
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar, 50).Value = "ALL";
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 250).Value = "";

                    // Crear un SqlDataAdapter y DataSet como antes
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");

                    // Asignar el DataTable del DataSet como origen de datos para el DataGridView
                    dgvClient.DataSource = dataSet.Tables["Customers"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.OpenConnection();
                // Crear un comando SQL que llama al procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CustomerProcess", conexion.GetSqlConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros @action y @param1
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar, 50).Value = "SEARCH";
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 250).Value = txtSearch.Text;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 250).Value = "";


                    // Crear un SqlDataAdapter y DataSet como antes
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");

                    // Asignar el DataTable del DataSet como origen de datos para el DataGridView
                    dgvClient.DataSource = dataSet.Tables["Customers"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        private async void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Espera 500 milisegundos antes de realizar la búsqueda
                await Task.Delay(500);

                conexion.OpenConnection();

                // Crear un comando SQL que llama al procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CustomerProcess", conexion.GetSqlConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros @action y @param1
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar, 50).Value = "SEARCH";
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 250).Value = txtSearch.Text;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 250).Value = "";

                    // Crear un SqlDataAdapter y DataSet como antes
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");

                    // Asignar el DataTable del DataSet como origen de datos para el DataGridView
                    dgvClient.DataSource = dataSet.Tables["Customers"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.OpenConnection();
                // Crear un comando SQL que llama al procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CustomerProcess", conexion.GetSqlConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros @action y @param1
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar, 50).Value = "ADD";
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 250).Value = txtCustomer.Text;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 250).Value = txtName.Text;
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 250).Value = txtAddress.Text;
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 250).Value = txtCity.Text;
                    cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 250).Value = "";

                    // Parámetro de retorno
                    SqlParameter returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    // Obtener el valor de retorno
                    int returnValue = (int)returnParameter.Value;

                    // Comprobar si la operación en la base de datos fue exitosa
                    if (returnValue == 0)
                    {
                        // Operación exitosa
                        updateList();
                        MessageBox.Show("¡El Cliente se guardó con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Operación no exitosa
                        MessageBox.Show("Error al agregar el cliente. Código de error: " + returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        private void updateList()
        {
            try
            {
                conexion.OpenConnection();

                // Crear un comando SQL que llama al procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CustomerProcess", conexion.GetSqlConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro @action
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar, 50).Value = "ALL";
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 250).Value = "";

                    // Crear un SqlDataAdapter y DataSet como antes
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "Customers");

                    // Asignar el DataTable del DataSet como origen de datos para el DataGridView
                    dgvClient.DataSource = dataSet.Tables["Customers"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
        private void dgvClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(operation == "")
            {
                MessageBox.Show("First you must select the operation to be performed", "Choose operation", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            // Verifica si el clic se realizó en una fila válida
            if (operation == "DELETE")
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvClient.Rows[e.RowIndex];

                    // Muestra un cuadro de diálogo con botones Delete, Update y Cancel
                    DialogResult result = MessageBox.Show("Do you want to delete this Customer?", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Verifica si el usuario hizo clic en "Yes"
                    if (result == DialogResult.Yes)
                    {
                        // Obtiene el valor de la columna Id_Customer
                        if (row.Cells["Id_Customer"].Value != null && int.TryParse(row.Cells["Id_Customer"].Value.ToString(), out int customerId))
                        {
                            // Llama a la función deleteCustomer con el Id_Customer como parámetro
                            deleteCustomer(customerId);
                        }
                        else
                        {
                            // Maneja el caso en el que no se pueda obtener el Id_Customer
                            MessageBox.Show("Error: Unable to get Customer Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            } else if(operation == "UPDATE")
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvClient.Rows[e.RowIndex];

                    // Obtiene el valor de la columna Id_Customer
                    if (row.Cells["Id_Customer"].Value != null && int.TryParse(row.Cells["Id_Customer"].Value.ToString(), out int customerId))
                    {
                        // Obtiene los valores de las columnas Customer, Name, Address y CityState
                        string customerValue = row.Cells["Customer"].Value?.ToString() ?? string.Empty;
                        string nameValue = row.Cells["Name"].Value?.ToString() ?? string.Empty;
                        string addressValue = row.Cells["Address"].Value?.ToString() ?? string.Empty;
                        string cityStateValue = row.Cells["CityState"].Value?.ToString() ?? string.Empty;

                        // Llama a la función updateCustomer con los valores como parámetros
                        updateCustomer(customerId, customerValue, nameValue, addressValue, cityStateValue);
                    }
                    else
                    {
                        // Maneja el caso en el que no se pueda obtener el Id_Customer
                        MessageBox.Show("Error: Unable to get Customer Data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void deleteCustomer(int customerId)
        {
            try
            {
                conexion.OpenConnection();
                // Crear un comando SQL que llama al procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("CustomerProcess", conexion.GetSqlConnection()))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros @action y @param1
                    cmd.Parameters.Add("@action", SqlDbType.NVarChar, 50).Value = "DELETE";
                    cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 250).Value = customerId;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 250).Value = "";
                    cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 250).Value = "";

                    // Parámetro de retorno
                    SqlParameter returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    // Obtener el valor de retorno
                    int returnValue = (int)returnParameter.Value;

                    // Comprobar si la operación en la base de datos fue exitosa
                    if (returnValue == 0)
                    {
                        // Operación exitosa
                        updateList();
                        MessageBox.Show("¡El Cliente se elimino con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Operación no exitosa
                        MessageBox.Show("Error al eliminar el cliente. Código de error: " + returnValue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }


        // Función para actualizar un cliente
        private void updateCustomer(int customerId, string customerValue, string nameValue, string addressValue, string cityStateValue)
        {
            idCustomer = customerId;
            txtCustomer.Text = customerValue;
            txtName.Text = nameValue;
            txtAddress.Text = addressValue;
            txtCity.Text = cityStateValue;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (operation == "UPDATE")
            {
                MessageBox.Show("You have already selected the update operation, you only have to choose the customer to Update.", "Update Customer", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                operation = "UPDATE";
                MessageBox.Show("Select the Customer you want to update Customer", "Update Customer", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void btnExecUpdate_Click(object sender, EventArgs e)
        {
            if(operation == "UPDATE")
            {
                try
                {
                    conexion.OpenConnection();
                    // Crear un comando SQL que llama al procedimiento almacenado
                    using (SqlCommand cmd = new SqlCommand("CustomerProcess", conexion.GetSqlConnection()))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros @action y @param1
                        cmd.Parameters.Add("@action", SqlDbType.NVarChar, 50).Value = "UPDATE";
                        cmd.Parameters.Add("@param1", SqlDbType.NVarChar, 250).Value = idCustomer;
                        cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 250).Value = txtCustomer.Text;
                        cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 250).Value = txtName.Text;
                        cmd.Parameters.Add("@param4", SqlDbType.NVarChar, 250).Value = txtAddress.Text;
                        cmd.Parameters.Add("@param5", SqlDbType.NVarChar, 250).Value = txtCity.Text;

                        // Parámetro de retorno
                        SqlParameter returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        // Ejecutar el procedimiento almacenado
                        cmd.ExecuteNonQuery();

                        // Obtener el valor de retorno
                        int returnValue = (int)returnParameter.Value;

                        // Comprobar si la operación en la base de datos fue exitosa
                        if (returnValue == 0)
                        {
                            // Operación exitosa
                            updateList();
                            MessageBox.Show("¡El Cliente se elimino con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cleanData();
                        }
                        else
                        {
                            // Operación no exitosa
                            MessageBox.Show("Error al eliminar el cliente. Código de error: " + returnValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al realizar la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.CloseConnection();
                }
            }
        }

        private void cleanData()
        {
            txtCustomer.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(operation == "DELETE")
            {
                MessageBox.Show("You have already selected the delete operation, you only have to choose the customer to delete.", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                operation = "DELETE";
                MessageBox.Show("Select the Customer you want to delete Customer", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
    }
}
