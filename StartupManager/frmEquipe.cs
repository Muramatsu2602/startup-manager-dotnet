﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartupManager
{
    public partial class frmEquipe : MaterialSkin.Controls.MaterialForm
    {
        private Int64 idProjeto;
        private UsuarioProjeto usuarioProjeto;
        private Projeto projeto;
        private ModelProjeto modelProjeto;
        private ModelUsuarioProjeto model = new ModelUsuarioProjeto();
        public frmEquipe(Int64 idProjeto)
        {
            this.idProjeto = idProjeto;
            InitializeComponent();
            modelProjeto = new ModelProjeto();
            projeto = modelProjeto.BuscaId(idProjeto);
            lblNomeProjeto.Text = projeto.Nome;

            CarregaGrid();
            CarregaUsuarios();
        }

        private void CarregaGrid()
        {
            dgvEquipe.DataSource = model.listarTodos(idProjeto);
        }

        private void CarregaUsuarios()
        {
            ModelUsuario mu = new ModelUsuario();
            cmbColaboradores.DataSource = mu.listarTodos();
            cmbColaboradores.ValueMember = "ID";
            cmbColaboradores.DisplayMember = "NOME";
        }

        private void frmEquipe_Load(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvEquipe.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dgvEquipe.CurrentRow.Cells[0].Value.ToString());
                DialogResult dr = MessageBox.Show("Deseja realmente EXCLUIR ?", "StartUp Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Yes)
                {
                    //model.Excluir(id);
                    CarregaGrid();
                }
            }
            else
            {
                MessageBox.Show("Selecione apenas um registro !!");
            }
        }

        private void btnAdiciona_Click(object sender, EventArgs e)
        {

            try
            {
                usuarioProjeto = new UsuarioProjeto();
                usuarioProjeto.Id_projeto = idProjeto;
                usuarioProjeto.Id_usuario = Int64.Parse(cmbColaboradores.SelectedValue.ToString());
                model.Inserir(usuarioProjeto);

                MessageBox.Show("Colaborador adicionado com Sucesso!!");
                CarregaGrid();
            }
            catch (Exception er)
            {
                MessageBox.Show("Erro em adicionar colaborador!Mais detalhes \n" + er.Message);

            }

        }
    }
}