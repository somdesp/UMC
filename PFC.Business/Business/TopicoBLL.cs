using PFC.DAO;
using PFC.Model;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;
using Microsoft.AspNet.Identity;


namespace PFC.Business.Business
{
    public class TopicoBLL
    {

        TopicoDAO daoTopico = new TopicoDAO();
        TemaDAO daoTema = new TemaDAO();
        UsuarioBLL bllUsuario = new UsuarioBLL();
        AvaliacaoBLL avaliacaobll = new AvaliacaoBLL();

        #region Listar Topicos Filhos
        public List<Topico> ListarTopicoFilho(Topico topico)
        {

            List<Topico> listTopicos = new List<Topico>();
            listTopicos = daoTopico.ListarTopicoFilho(topico.Id);

            for (int i = 0; i < listTopicos.Count(); i++)
            {
                listTopicos[i].usuario = bllUsuario.ConsultaUsuarioInt(listTopicos[i].usuario);
                listTopicos[i].Tema = daoTema.ListarTemaTopico(listTopicos[i].Tema.Id);
                listTopicos[i].avaliacao.idTopico = listTopicos[i].Id;
                listTopicos[i].avaliacao = avaliacaobll.consultaAvaliacao(listTopicos[i].avaliacao, topico.avaliacao.idUsuario);
            }

            return listTopicos;
        }
        #endregion

        #region Listar Topicos Pai
        public List<Topico> ListarTopico()
        {
            List<Topico> listTopicos = new List<Topico>();

            listTopicos = daoTopico.ListarTopico();

            for (int i = 0; i < listTopicos.Count(); i++)
            {
                listTopicos[i].usuario = bllUsuario.ConsultaUsuarioInt(listTopicos[i].usuario);
                //listTopicos[i].Resposta = ListarTopicoFilho(listTopicos[i]);
            }

            return listTopicos;
        }
        #endregion

        #region Listar Topicos Pai conforme Pesquisa do Usuario
        public List<Topico> ListarTopico(string pesquisa)
        {
            List<Topico> listTopicos = new List<Topico>();

            listTopicos = daoTopico.ListarTopicoPesquisa(pesquisa);

            for (int i = 0; i < listTopicos.Count(); i++)
            {
                listTopicos[i].usuario = bllUsuario.ConsultaUsuarioInt(listTopicos[i].usuario);
                listTopicos[i].Resposta = ListarTopicoFilho(listTopicos[i]);
            }

            return listTopicos;
        }
        #endregion

        #region Detalha Topico
        public Topico DetalheTopico(Topico topico)
        {
            AvaliacaoBLL avaliacaobll = new AvaliacaoBLL();
            int usuarioLogado = topico.avaliacao.idUsuario;
            topico = daoTopico.DetalheTopico(topico);
            topico.avaliacao = avaliacaobll.consultarAvaliacaoCurtir(topico,usuarioLogado);
            topico.usuario = bllUsuario.ConsultaUsuarioInt(topico.usuario);
            topico.Tema = daoTema.ConsultaTema(topico.Tema);
            topico.Resposta = ListarTopicoFilho(topico);
            

            return topico;

        }
        #endregion

        #region Adicona Resposta
        public bool AdicionarPosts(Topico post)
        {
            daoTopico.UpdateDataTopico(post);
            return daoTopico.AdicionarPosts(post);
        }
        #endregion

        #region Valida se topico escolhido existe

        public bool ValTopico(Topico topico)
        {

            topico = daoTopico.ValTopico(topico);

            if (topico.Id != 0)
            {
                return true;
            }
            else
            {
                return false;

            }

        }


        #endregion

        #region Novo Topico

        public bool AdicionarTopico(Topico topico)
        {
            topico.Status.Id = 1;
            topico.Tema = daoTema.ConsultaTema(topico.Tema.Id);
            return daoTopico.AdicionarTopico(topico);
        }


        #endregion

        #region FechaTopico
        public bool FechaTopico(Topico topico)
        {
          //  EnviarEmail(topico);
            return daoTopico.FechaTopico(topico);
        }
        #endregion

        #region Enviar Email

        public void EnviarEmail(Topico topico)
        {
           List<Usuario> listUsuario = new List<Usuario>();
            List<Usuario> nlistUsuario = new List<Usuario>();
            List<string> email = new List<string>(); 
            for (int i = 0; i < topico.Resposta.Count; i++)
            {
                email.Add(topico.Resposta[i].usuario.Email);
            }

            email = email.Distinct().ToList();

            for (int i = 0; i < email.Count; i++)
            {

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("pfc8.umc@gmail.com", "UMC@2018"),
                    EnableSsl = true
                })

                {
                    client.Send(email[i], email[i], "Forum UMC", "O Topico http://localhost:52005/Topico/TopicoSelecionado?topicoId=" + topico.Id + " Foi Fechado");
                }

            }
              
           

        }
        #endregion



    }
}
