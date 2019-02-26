using PFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFC.DAO;
using System.Threading.Tasks;

namespace PFC.Business
{
    public class RankBLL
    {
        #region Listar Usuarios Hank soma/Media
        public List<Usuario> ListarUsuariosHank()
        {
            List<Usuario> mediausuario = new List<Usuario>();
            List<Usuario> somausuario = new List<Usuario>();
            List<Usuario> totalUsuario = new List<Usuario>();

            RankingDAO dao = new RankingDAO();
            mediausuario = dao.ListarMédiaUsuario();
            somausuario = dao.ListarSomaUsuario();



            for (int media = 0; media < mediausuario.Count; media++)
            {
                int y = 0;

                for (int soma = 0; soma < somausuario.Count; soma++)
                {


                    if (mediausuario[media].avaliacao.idUsuario == somausuario[soma].avaliacao.idUsuario)
                    {
                        y = 1;
                        Usuario usuario = new Usuario();
                        usuario.Nome = mediausuario[media].Nome;
                        usuario.avaliacao.idUsuario = mediausuario[media].avaliacao.idUsuario;
                        usuario.avaliacao.pontos = mediausuario[media].avaliacao.mediaPontos + somausuario[soma].avaliacao.pontos;

                        totalUsuario.Add(usuario);
                    }
                    else
                    {
                        int x = 0;
                        for (int media2 = 0; media2 < mediausuario.Count; media2++)
                        {
                            if (mediausuario[media2].avaliacao.idUsuario == somausuario[soma].avaliacao.idUsuario)
                            {
                                x = 1;
                            }
                        }

                        if (x == 0)
                        {
                            Usuario usuario = new Usuario();
                            usuario.Nome = somausuario[soma].Nome;
                            usuario.avaliacao.idUsuario = somausuario[soma].avaliacao.idUsuario;
                            usuario.avaliacao.pontos = somausuario[soma].avaliacao.pontos;

                            totalUsuario.Add(usuario);
                        }
                    }

                }

                if (y == 0)
                {
                    Usuario usuario = new Usuario();
                    usuario.Nome = mediausuario[media].Nome;
                    usuario.avaliacao.idUsuario = mediausuario[media].avaliacao.idUsuario;
                    usuario.avaliacao.pontos = mediausuario[media].avaliacao.mediaPontos;

                    totalUsuario.Add(usuario);
                }
            }
            return totalUsuario;
        }
        #endregion

        #region Consulta na tabela rank  diario  
        public List<Usuario> ListarRank()
        {
            RankingDAO dao = new RankingDAO();
            List<Usuario> listarRanking = new List<Usuario>();
            listarRanking = dao.ListandoTabelaRank();
            return listarRanking;
        }

        #endregion
    }

}

