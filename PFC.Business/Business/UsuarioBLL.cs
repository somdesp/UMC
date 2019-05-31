using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using PFC.DAO;
using PFC.Model;

namespace PFC.Business
{
    public class UsuarioBLL
    {

        UsuarioDAO daoUsuario = new UsuarioDAO();
        ArquivoBLL arquivoDao = new ArquivoBLL();
        AutorizacoesBLL permissaoBll = new AutorizacoesBLL();

        #region Inativar Usuario
        public bool InativarUsuario(Usuario usuario)
        {
            return daoUsuario.InativarUsuario(usuario);
        }
        #endregion

        #region Adicionar Usuario
        public bool AdicionarUsuario(Usuario usuario)
        {
            arquivoDao.AnexoArquivos(usuario.UploadArquivo);
            return daoUsuario.AdicionarUsuario(usuario);
        }
        #endregion

        #region Atualiza Usuario
        public bool AtualizarUsuario(Usuario usuario)
        {
            return daoUsuario.AtualizarUsuario(usuario);
        }
        #endregion

        public bool AtualizarSenha(Usuario usuario)
        {
            return daoUsuario.AtualizarSenha(usuario);
        }

        #region Listar Usuarios

        //public List<Usuario> ListarUsuarios()
        //{
        //    List<Usuario> usuarios = new List<Usuario>();
        //    usuarios = daoUsuario.ListarUsuarios();

        //    for (int i = 0; i < usuarios.Count(); i++)
        //    {
        //        //usuarios[i].Sexo = daoUsuario.ConsultaUsuarioInt(usuarios[i].Sexo.Id);
        //        //usuarios[i].Semestre = daoTema.ListarTemaTopico(usuarios[i].Tema.Id);
        //        //usuarios[i].Curso = daoTema.ListarTemaTopico(usuarios[i].Tema.Id);
        //    }


        //}

        #endregion

        #region Consulta Usuario ID

        public async Task<Usuario> ConsultaUsuarioInt(Usuario UsuarioId)
        {
            Usuario usuario = new Usuario();
            CursoDAO curso = new CursoDAO();
            GeneroDAO genero = new GeneroDAO();
            SemestreDAO semestre = new SemestreDAO();
            usuario = await daoUsuario.ConsultaUsuarioInt(UsuarioId);
            usuario.Curso =await curso.BuscaPorID(usuario.Curso.Id);
            usuario.Semestre =await semestre.BuscaPorID(usuario.Semestre.Id);
            usuario.Sexo = await genero.BuscaPorID(usuario.Sexo.Id);
            usuario.UploadArquivo = await arquivoDao.RecuperarImagem(usuario.UploadArquivo);
            usuario.Auth = await permissaoBll.ReturnAutPorID(usuario.Auth);
            return usuario;
        }


        #endregion

        #region Pesquisar Usuario camada BLL
        public async Task<List<Usuario>> ConsultaUsuario()
        {
            UsuarioDAO usuario = new UsuarioDAO();
            Cache cache = new Cache();

            var objetoCache = (List<Usuario>)HttpContext.Current.Cache["listandoUsuario"];
            if (objetoCache == null)
            {
                objetoCache = await usuario.PesquisarUsuario();
                HttpContext.Current.Cache.Insert("listandoUsuario", objetoCache, null, DateTime.Now.AddMinutes(2), Cache.NoSlidingExpiration);
            }

            return objetoCache;

        }
        #endregion

        #region Pesquisar Usuario Master/ADM
        public async Task<List<Usuario>> ConsultaUsuarioMasterADM()
        { 
            return await daoUsuario.ConsultaUsuarioMastersADM();
        }
        #endregion







    }


}
