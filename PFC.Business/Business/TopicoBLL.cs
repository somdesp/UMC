using PFC.DAO;
using PFC.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace PFC.Business
{
    public class TopicoBLL
    {

        TopicoDAO daoTopico = new TopicoDAO();
        TemaDAO daoTema = new TemaDAO();
        UsuarioBLL bllUsuario = new UsuarioBLL();
        AvaliacaoBLL avaliacaobll = new AvaliacaoBLL();
        ArquivoBLL arquivoBll = new ArquivoBLL();

        #region Listar Topicos Filhos
        public async Task<List<Topico>> ListarTopicoFilho(Topico topico)
        {

            List<Topico> listTopicos = new List<Topico>();
            listTopicos = await daoTopico.ListarTopicoFilho(topico.Id);

            for (int i = 0; i < listTopicos.Count(); i++)
            {
                listTopicos[i].Anexos.id_topico = listTopicos[i].Id;
               // listTopicos[i].Anexos =await arquivoBll.RecuperarArqTopico(listTopicos[i].Anexos);
               // listTopicos[i].usuario =await bllUsuario.ConsultaUsuarioInt(listTopicos[i].usuario);
               // listTopicos[i].Tema =await daoTema.ListarTemaTopico(listTopicos[i].Tema.Id);
                listTopicos[i].avaliacao.idTopico = listTopicos[i].Id;
                listTopicos[i].avaliacao =await avaliacaobll.consultaAvaliacao(listTopicos[i].avaliacao, topico.avaliacao.idUsuario);
            }

            return listTopicos;
        }
        #endregion

        #region Listar Topicos Pai
        public async Task<List<Topico>> ListarTopico()
        {
            List<Topico> listTopicos = new List<Topico>();

            listTopicos = await daoTopico.ListarTopico();

            //for (int i = 0; i < listTopicos.Count(); i++)
            //{
            //    listTopicos[i].usuario =await bllUsuario.ConsultaUsuarioInt(listTopicos[i].usuario);
            //    //listTopicos[i].Resposta = ListarTopicoFilho(listTopicos[i]);
            //}

            return listTopicos;
        }
        #endregion

        #region Listar Topicos Pai conforme Pesquisa do Usuario
        public async Task<List<Topico>> ListarTopico(string pesquisa)
        {
            List<Topico> listTopicos = new List<Topico>();

            listTopicos = await daoTopico.ListarTopicoPesquisa(pesquisa);

            for (int i = 0; i < listTopicos.Count(); i++)
            {
                listTopicos[i].usuario =await bllUsuario.ConsultaUsuarioInt(listTopicos[i].usuario);
                listTopicos[i].Resposta = await ListarTopicoFilho(listTopicos[i]);
            }

            return listTopicos;
        }
        #endregion

        #region Detalha Topico
        public async Task<Topico> DetalheTopico(Topico topico)
        {

            AvaliacaoBLL avaliacaobll = new AvaliacaoBLL();
            int usuarioLogado = topico.avaliacao.idUsuario;
            topico = await daoTopico.DetalheTopico(topico);
            topico.avaliacao =await avaliacaobll.consultarAvaliacaoCurtir(topico,usuarioLogado);
            topico.usuario =await bllUsuario.ConsultaUsuarioInt(topico.usuario);
            topico.Tema = await daoTema.ConsultaTema(topico.Tema);
            topico.avaliacao.idUsuario = usuarioLogado;
            topico.Resposta = await ListarTopicoFilho(topico);
            

            return topico;

        }
        #endregion

        #region Adicona Resposta
        public bool AdicionarPosts(Topico post)
        {
            post.TopicoFilho.Status.Id = 1;
            daoTopico.UpdateDataTopico(post);
            return daoTopico.AdicionarPosts(post);
        }
        #endregion

        #region Valida se topico escolhido existe

        public async Task<bool> ValTopico(Topico topico)
        {

            topico = await daoTopico.ValTopico(topico);

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

        public async Task<bool> AdicionarTopico(Topico topico)
        {
            topico.Status.Id = 1;
            topico.Tema = await daoTema.ConsultaTema(topico.Tema.Id);
            return daoTopico.AdicionarTopico(topico);
        }


        #endregion

        #region FechaTopico
        public bool FechaTopico(Topico topico)
        {
            //await EnviarEmail(topico);
            topico.Titulo ="[RESOLVIDO] "+ topico.Titulo;
            return daoTopico.FechaTopico(topico);
        }
        #endregion

        #region Enviar Email

        public async Task EnviarEmail(Topico topico)
        {

            List<string> email = new List<string>(); 
            for (int i = 0; i < topico.Resposta.Count; i++)
            {
                email.Add(topico.Resposta[i].usuario.Email);
            }
            try
            {
                email = email.Distinct().ToList();

                for (int i = 0; i < email.Count; i++)
                {

                    using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("pfc8.umc@gmail.com", "UMC@2018"),
                        EnableSsl = true
                    })

                    {
                        client.Send(email[i], email[i], "Forum UMC", "O Topico http://www.mehelpehml.tk/Topico/TopicoSelecionado?topicoId=" + topico.Id + " Foi Fechado");
                    }

                }
            }
            catch (System.Exception)
            {

                throw;
            }

              
           

        }
        #endregion


        #region RemoverResposta
        public bool RemoverResposta(Denuncia topico)
        {
            //await EnviarEmail(topico);
            return daoTopico.RemoverResposta(topico);
        }
        #endregion



    }
}
