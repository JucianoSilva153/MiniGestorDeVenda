using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_de_Vendas
{
    public partial class Form1 : Form
    {

        string[] vCod_Produtov = new string[100];
        string[] vNome_Produto = new string[100];
        decimal[] vPreco_Produto = new decimal[100];
        string[] vlocalizacao = new string[100];
        string[] vFornecedor = new string[100];
        DateTime[] vDataUltCompra = new DateTime[100];
        decimal[] vValorUltCompra = new decimal[100];

        bool teveerro;

        string modo;

        public Form1()
        {
            InitializeComponent();
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Carregamento
            StreamReader Leitor = new StreamReader("c:/temp/BD_produtos.txt");

            int i = 0;


            while (!Leitor.EndOfStream)
            {
                string reg = Leitor.ReadLine();
                i++;
                vCod_Produtov[i] = reg.Substring(0, 10);
                vNome_Produto[i] = reg.Substring(10, 20);
                vPreco_Produto[i] = Convert.ToDecimal(reg.Substring(30, 14));
                vlocalizacao[i] = reg.Substring(44, 10);
                vFornecedor[i] = reg.Substring(54, 15);
                vDataUltCompra[i] = Convert.ToDateTime(reg.Substring(69, 10));
                vValorUltCompra[i] = Convert.ToDecimal(reg.Substring(79, 14));


                dgv_produto.Rows.Add(vCod_Produtov[i], vNome_Produto[i], vFornecedor[i]);
            }
            Leitor.Close();

            //Carregamento

            //Desabilitar TextBox's


            desabilitacampos();
            habilitaBTN();

            //Desabilitar TextBox's



            //incialização da variavel modo

            modo = "";
            toolStripStatusLabel1.Text = "";
        }


        private void desabilitacampos()
        {
            mtb_cod_prod.Enabled = false;
            txt_nome_produto.Enabled = false;
          
            txt_preco_produto.Enabled = false;
            txt_valor_compra.Enabled = false;
            txt_loc_produto.Enabled = false;
            txt_fornecedor.Enabled = false;
            dtp_dataUlt_comp.Enabled = false;
        }

        private void habilitacampos()
        {
            mtb_cod_prod.Enabled = true;
            txt_nome_produto.Enabled = true;
          
            txt_preco_produto.Enabled = true;
            txt_valor_compra.Enabled = true;
            txt_loc_produto.Enabled = true;
            txt_fornecedor.Enabled = true;
            dtp_dataUlt_comp.Enabled = true;
            mtb_cod_prod.Focus();
        }


        private void dgv_produto_SelectionChanged(object sender, EventArgs e)
        {
            CarregaCampos();
        }



        private void CarregaCampos()
        {
            int linhadgv = dgv_produto.CurrentRow.Index + 1;

            if( vCod_Produtov[linhadgv] != null) { 
            mtb_cod_prod.Text = vCod_Produtov[linhadgv].Trim();
            txt_nome_produto.Text = vNome_Produto[linhadgv].Trim();
            txt_preco_produto.Text = Convert.ToString(vPreco_Produto[linhadgv]).Trim();
            txt_loc_produto.Text = vlocalizacao[linhadgv].Trim();
            txt_fornecedor.Text = vFornecedor[linhadgv].Trim();
            dtp_dataUlt_comp.Value = vDataUltCompra[linhadgv];
            txt_valor_compra.Text = Convert.ToString(vValorUltCompra[linhadgv]).Trim();
            }
        }



        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

        }


        private void criticaCampo()
        {

            //Verificando se o valor de venda esta correto(não deve ter letras, apenas numero)
            string nova = "";
            string a = "";
            for (int i = 0; i < txt_preco_produto.TextLength - 1; i++)
            {
                a = txt_preco_produto.Text.Substring(i, 1);

                if (!(a != "1" && a != "2" && a != "3" && a != "4" && a != "5" && a != "6" && a != "7" && a != "8" && a != "9" && a != "0" && a != ","))
                {
                    nova = nova + a;
                }
                else
                {
                    MessageBox.Show("Valor de Venda Incorreto");
                    teveerro = true;
                    return;
                }
            }
            //Verificando se o valor de venda esta correto(não deve ter letras, apenas numero)





            //verificando se o valor de venda é < que o preco de compra
            if (Convert.ToDecimal(txt_preco_produto.Text) < Convert.ToDecimal(txt_valor_compra.Text))
            {
                MessageBox.Show("Valor de Venda não pode ser menos que Valor de compra", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                teveerro = true;
                return;
            }
            //verificando se o valor de venda é < que o preco de compra






            //Verificando se a Localização do produto está correcta
            txt_loc_produto.Text = txt_loc_produto.Text.ToUpper();

            if (!(txt_loc_produto.Text.Contains("C")) || !(txt_loc_produto.Text.Contains("F")) || !(txt_loc_produto.Text.Contains("P")))
            {
                MessageBox.Show("Insira a Localização Correcta");
                teveerro = true;
                return;
            }
            //Verificando se a Localização do produto está correcta



        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {



        }

        private void txt_loc_produto_TextChanged(object sender, EventArgs e)
        {
            //txt_loc_produto.Text = txt_loc_produto.Text.ToUpper();
        }

        private void txt_preco_produto_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_alterar_Click(object sender, EventArgs e)
        {
            habilitacampos();
            desabilitaBTN();
            
            modo = "A";
            toolStripStatusLabel1.Text = "Modo de Actualização";
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            teveerro = false;
            criticaCampo();

            if (teveerro)
            {
                return;
            }

            //Gravação de Dados 

                

            //Modo de Atualização

            if (modo == "A")
            {

               //Adicionando espaços nos itens que não têm espaço no arquivo.
                int linhadgv = dgv_produto.CurrentRow.Index + 1;
                vCod_Produtov[linhadgv] = String.Format("{0}          ", mtb_cod_prod.Text).Substring(0, 10);
                vNome_Produto[linhadgv] = String.Format("{0}                    ", txt_nome_produto.Text).Substring(0, 20);
                vPreco_Produto[linhadgv] = Convert.ToDecimal(txt_preco_produto.Text);
                vlocalizacao[linhadgv] = String.Format("{0}          ", txt_loc_produto.Text).Substring(0, 10);
                vFornecedor[linhadgv] = String.Format("{0}               ", txt_fornecedor.Text).Substring(0, 15);
                vDataUltCompra[linhadgv] = Convert.ToDateTime(dtp_dataUlt_comp.Text);
                vValorUltCompra[linhadgv] = Convert.ToDecimal(txt_valor_compra.Text);
                modo = "";
                toolStripStatusLabel1.Text = "Dados Alterados Com Sucesso!";
                desabilitacampos();
                atualizagrid();
            }


            //Modo de Atualização




            //Modo de Inclusão


            if (modo == "I")
            {

                int linhadgv = dgv_produto.Rows.Count + 1;

                vCod_Produtov[linhadgv] = String.Format("{0}          ", mtb_cod_prod.Text).Substring(0, 10);
                vNome_Produto[linhadgv] = String.Format("{0}                    ", txt_nome_produto.Text).Substring(0, 20);
                vPreco_Produto[linhadgv] = Convert.ToDecimal(txt_preco_produto.Text);
                vlocalizacao[linhadgv] = String.Format("{0}          ", txt_loc_produto.Text).Substring(0, 10);
                vFornecedor[linhadgv] = String.Format("{0}               ", txt_fornecedor.Text).Substring(0, 15);
                vDataUltCompra[linhadgv] = Convert.ToDateTime(dtp_dataUlt_comp.Text);
                vValorUltCompra[linhadgv] = Convert.ToDecimal(txt_valor_compra.Text);
                atualizagrid();


                modo = "";
                toolStripStatusLabel1.Text = String.Format("Dados Adicionado Com Sucesso! Registro Nº:{0}", linhadgv);
                desabilitacampos();


            }



            //Modo de Inclusão


            //Modo de Exclusão

            if (modo == "E")
            {

                int linhadgv = dgv_produto.CurrentRow.Index + 1;

                for (int i = 1; i <= vCod_Produtov.Length - 2; i++)
                {
                    if (linhadgv <= i)
                    {
                        vCod_Produtov[i] = vCod_Produtov[i + 1];
                        vNome_Produto[i] = vNome_Produto[i + 1];
                        vPreco_Produto[i] = vPreco_Produto[i + 1];
                        vlocalizacao[i] = vlocalizacao[i + 1];
                        vFornecedor[i] = vFornecedor[i + 1];
                        vDataUltCompra[i] = vDataUltCompra[i + 1];
                        vValorUltCompra[i] = vValorUltCompra[i + 1];
                    }

                   
          
                }



                modo = "";
                toolStripStatusLabel1.Text = String.Format("Dados Excluidos Com Sucesso!");

                CarregaCampos();
                atualizagrid();
            }



            //Modo de Exclusão




            //Gravação de dados

            habilitaBTN();
            gravacao();

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            desabilitacampos();
            CarregaCampos();
            habilitaBTN();

            modo = "";

            toolStripStatusLabel1.Text = "";
        }

        private void atualizagrid()
        {
            dgv_produto.Rows.Clear();

            for (int i = 1; i <= vCod_Produtov.Length - 1; i++)
            {
                if (vCod_Produtov[i] != null)
                {
                    dgv_produto.Rows.Add(vCod_Produtov[i], vNome_Produto[i], vFornecedor[i]);

                }
                

            }
        }

        private void btn_novo_Click(object sender, EventArgs e)
        {
            habilitacampos();
            desabilitaBTN();

            modo = "I";
            toolStripStatusLabel1.Text = "Modo de Inclusão";


            mtb_cod_prod.Text = "";
            txt_nome_produto.Text = "";
            txt_preco_produto.Text = "";
            txt_loc_produto.Text = "";
            txt_fornecedor.Text = "";
            dtp_dataUlt_comp.Value = System.DateTime.Now;
            txt_valor_compra.Text = "";
        }

        private void panel_lateral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            desabilitaBTN();
            modo = "E";
            toolStripStatusLabel1.Text = "Modo de Exclusão";
            MessageBox.Show(String.Format("Para confirmar A Exclusão de '{0}' clique em [Guardar]", txt_nome_produto.Text), "Eliminação de Dados", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void habilitaBTN()
        {
            btn_novo.Enabled = true;
            btn_eliminar.Enabled = true;
            btn_alterar.Enabled = true;


            btn_guardar.Enabled = false;
            btn_cancelar.Enabled = false;
        }

        private void desabilitaBTN()
        {
            btn_novo.Enabled = false;
            btn_eliminar.Enabled = false;
            btn_alterar.Enabled = false;


            btn_guardar.Enabled = true;
            btn_cancelar.Enabled = true;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            panel_lateral.Width = 132;
            bunifuImageButton4.Visible = true;

            bunifuImageButton2.Visible = false;
            Point Y = new Point();

            Y.Y = 57;
            Y.X = 138;

            panel1.Location = Y;


            panel1.Location.Y.GetType();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            panel_lateral.Width = 39;
            bunifuImageButton2.Visible = true;
            bunifuImageButton4.Visible = false;
            Point Y = new Point();

            Y.Y = 57;
            Y.X = 94;

            panel1.Location = Y;


            panel1.Location.Y.GetType();

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gravacao();
        }

        private void gravacao()
        {
            StreamWriter vgravador = new StreamWriter("c:/temp/BD_produtos.txt");


            for (int i = 1; i <= vCod_Produtov.Length -1; i++)
            {
                if (vCod_Produtov[i] != null)
                {
                    string linhaDados ="";
                                                                         
                    linhaDados += vCod_Produtov[i];
                    linhaDados += vNome_Produto[i];
                    linhaDados += String.Format("              {0}", Convert.ToString(vPreco_Produto[i])).Substring(Convert.ToString(vPreco_Produto[i]).Length, 14);
                    linhaDados += vlocalizacao[i];
                    linhaDados += vFornecedor[i];
                    linhaDados += Convert.ToString(vDataUltCompra[i]).Substring(0, 10);
                    linhaDados += String.Format("              {0}", Convert.ToString(vValorUltCompra[i])).Substring(Convert.ToString(vValorUltCompra[i]).Length, 14); 
                    vgravador.WriteLine(linhaDados);
                }
            }

            vgravador.Close();
        }

        private void btn_pesquisa_Click(object sender, EventArgs e)
        {
            for( int p=1; p <= vCod_Produtov.Length -1; p++)
            {

                if(vCod_Produtov[p] == null)
                {
                    MessageBox.Show("Produto não encontrado, verifique se escreveu corretamente!", "Pesquisa de Produto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                   
                }
                if (txt_pesquisa.Text == vCod_Produtov[p].Trim().ToLower() || vNome_Produto[p].Trim().ToLower().Contains(txt_pesquisa.Text.ToLower()))
                {
                    dgv_produto[0, p - 1].Selected = true;

                    CarregaCampos();
                   
                   
                   
                    
                    //mtb_cod_prod.Text = vCod_Produtov[p].Trim();
                    //txt_nome_produto.Text = vNome_Produto[p].Trim();
                    //txt_preco_produto.Text = Convert.ToString(vPreco_Produto[p]).Trim();
                    //txt_loc_produto.Text = vlocalizacao[p].Trim();
                    //txt_fornecedor.Text = vFornecedor[p].Trim();
                    //dtp_dataUlt_comp.Value = vDataUltCompra[p];
                    //txt_valor_compra.Text = Convert.ToString(vValorUltCompra[p]).Trim();

                    return;
                }


                
            }
        }
    }
}
