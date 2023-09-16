using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using trabalho_DAD_livro.Connection;
using trabalho_DAD_livro.Controller;
using trabalho_DAD_livro.model;

namespace trabalho_DAD_livro
{
    public partial class Form1 : Form
    {
        bool novo;
        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable livros;
        SqlDataReader tabLivro;
        DataRow[] linhaAtual;

        int posicao = 0;
        public Form1()
        {
            InitializeComponent();
            novo = true;
            carregarTabela();
            limpaCampos();
            TotalRegistros();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            
            if (novo)
            {
                LivroModel livro = new LivroModel();
                livro.Titulo = txtTitulo.Text;
                livro.Autor = txtAutor.Text;
                livro.Editora = txtEditora.Text;
                livro.Valor = decimal.Parse(txtValor.Text);
                livro.Isbn = txtIsbn.Text;
                

                LivroController livroController = new LivroController();
                livroController.inserirDados(livro);

            }
            else
            {

                
                
                LivroModel livro = new LivroModel();
                livro.Id = Int32.Parse(txtId.Text);
                livro.Titulo = txtTitulo.Text;
                livro.Autor = txtAutor.Text;
                livro.Editora = txtEditora.Text;
                livro.Valor = decimal.Parse(txtValor.Text);
                livro.Isbn = txtIsbn.Text;

                LivroController livroController = new LivroController();
                livroController.editarDados(livro);

            }
            carregarTabela();
            TotalRegistros();
            limpaCampos();

        }

        public void carregarTabela()
        {
            string strSql = "SELECT * FROM livro order by titulo";

            ConexaoBanco conexaoBanco = new ConexaoBanco();
            connection = conexaoBanco.conectaSqlServer();



            cmd = new SqlCommand(strSql, connection);

            connection.Open();
            
            cmd.CommandType = CommandType.Text;

            da = new SqlDataAdapter(cmd);

            livros = new DataTable();

            da.Fill(livros);

            tabelaLivros.DataSource = livros;

            linhaAtual = livros.Select();


            if (linhaAtual.Length == 0)
            {
                MessageBox.Show("Não Existem Registros!");
            }
            else
            {
                txtId.Text = linhaAtual[0]["Id"].ToString();
                txtTitulo.Text = linhaAtual[0]["titulo"].ToString();
                txtEditora.Text = linhaAtual[0]["editora"].ToString();
                txtAutor.Text = linhaAtual[0]["autor"].ToString();
                txtValor.Text = linhaAtual[0]["valor"].ToString();
                txtIsbn.Text = linhaAtual[0]["isbn"].ToString();
               
            }

        }

        private void limpaCampos()
        {
            txtId.Text = "";
            txtTitulo.Text = "";
            txtEditora.Text = "";
            txtAutor.Text = "";
            txtValor.Text = "";
            txtIsbn.Text = "";
            
        }

        void TotalRegistros()
        {
            
            string sqlBuscarId = "select count(id) as total from livro";
            ConexaoBanco conexaoBanco = new ConexaoBanco();
            connection = conexaoBanco.conectaSqlServer();
            cmd = new SqlCommand(sqlBuscarId, connection);

            cmd.Parameters.AddWithValue("@id", txtBuscar.Text + "%");
            cmd.CommandType = CommandType.Text;

            connection.Open();
            string total = "";
            try
            {
                tabLivro = cmd.ExecuteReader();
                if (tabLivro.Read())
                {
                    total = (tabLivro[0].ToString());

                }
                else
                {
                    MessageBox.Show("livro não Encontrado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Buscar!!!");
            }

            finally
            {
                connection.Close();
            }

            lblTotalRegistro.Text = total;


        }
        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;//ativa o botão novo
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = true;
            txtTitulo.Enabled = false;
            txtEditora.Enabled = false;
            txtAutor.Enabled = false;
            txtValor.Enabled = false;
            txtIsbn.Enabled = false;


            limpaCampos();
            
        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            limpaCampos();

            tsbNovo.Enabled = false;
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = false;
            txtTitulo.Enabled = true;
            txtEditora.Enabled = true;
            txtAutor.Enabled = true;
            txtValor.Enabled = true;
            txtIsbn.Enabled = true;
        }

         private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            


            
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            string sqlBuscarId = "select * from livro where id = @id";
            ConexaoBanco conexaoBanco = new ConexaoBanco();
            connection = conexaoBanco.conectaSqlServer();
            cmd = new SqlCommand(sqlBuscarId, connection);


            cmd.Parameters.AddWithValue("@id", txtBuscar.Text);
            cmd.CommandType = CommandType.Text;

            connection.Open();

            da = new SqlDataAdapter(cmd);
            livros = new DataTable();
            da.Fill(livros);
            tabelaLivros.DataSource = livros;
            try
            {
                tabLivro = cmd.ExecuteReader();
                if (tabLivro.Read())
                {
                    txtId.Text = tabLivro[0].ToString();
                    txtTitulo.Text = tabLivro[1].ToString();
                    txtEditora.Text = tabLivro[3].ToString();
                    txtValor.Text =  tabLivro[4].ToString();
                    txtAutor.Text = tabLivro[2].ToString();
                    txtIsbn.Text = tabLivro[5].ToString();

                    tsbNovo.Enabled = false;
                    tsbSalvar.Enabled = true;
                    tsbCancelar.Enabled = true;
                    tsbExcluir.Enabled = true;

                    txtTitulo.Enabled = true;
                    txtEditora.Enabled = true;
                    txtAutor.Enabled = true;
                    txtValor.Enabled = true;
                    txtIsbn.Enabled = true;
                    txtTitulo.Focus();
                    novo = false;
                }
                else
                {
                    MessageBox.Show("livro não Encontrado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Buscar!!!");
            }

            finally
            {
                connection.Close();
            }
            txtBuscar.Text = string.Empty;

          
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            LivroController cc = new LivroController();
            cc.apagarDados(Int32.Parse(txtId.Text));

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            btnBuscar.Enabled = true;
            txtTitulo.Enabled = false;
            txtEditora.Enabled = false;
            txtAutor.Enabled = false;
            txtValor.Enabled = false;
            txtIsbn.Enabled = false;

            carregarTabela();
            TotalRegistros();
            limpaCampos();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {

            FrmRelatorio frm = new FrmRelatorio(livros);
            frm.ShowDialog();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foto = dialog.FileName.ToString();
                pictureBox1.ImageLocation = foto;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalRegistro_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
    }

