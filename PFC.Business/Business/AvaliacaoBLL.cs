using PFC.DAO;
using PFC.Model;

namespace PFC.Business.Business
{
    public class AvaliacaoBLL
    {
        #region Inserir novo ponto no banco de dados comunicação direta com a DAO
        public Avaliacao inserirPontos(Avaliacao avaliacao)
        {
            AvaliacaoDAO avaliacaoDAO = new AvaliacaoDAO();
            Avaliacao resultado=new Avaliacao();
            float pontosUsuario = avaliacao.pontos * (float)0.2;
            if (avaliacaoDAO.consultaAvaliacaoID(avaliacao, avaliacao.idUsuario)> 0)
            {
                if (pontosUsuario != avaliacaoDAO.consultaAvaliarpontos(avaliacao, avaliacao.idUsuario))
                {
                    // realiza atualização da nota se caso os pontos que encontrar for diferente
                    resultado = avaliacaoDAO.AtualizarPonto(avaliacao,pontosUsuario);
                    resultado.pontos = converterPontosEstrelas(resultado.pontos);
                }
                
            }
            else
            {
               // Se caso o tópico não foi respondido Inserir pontos 
                resultado = avaliacaoDAO.InserirPonto(avaliacao,pontosUsuario);
                resultado.pontos = converterPontosEstrelas(resultado.pontos);
            }

             return resultado;
        }
        #endregion



        #region Inserir novo ponto no banco de dados comunicação direta com a DAO
        public Avaliacao inserirPontosLikeDeslike(Avaliacao avaliacao)
        {
            AvaliacaoDAO avaliacaoDAO = new AvaliacaoDAO();
            Avaliacao resultado = new Avaliacao();
            double pontosUsuario = avaliacao.pontos;
            // verifica se existe a avaliação no banco e dados
            if (avaliacaoDAO.consultaAvaliacaoID(avaliacao, avaliacao.idUsuario) > 0)
            {
                if (pontosUsuario != avaliacaoDAO.consultaAvaliarpontos(avaliacao, avaliacao.idUsuario))
                {
                    // realiza atualização da nota se caso os pontos que encontrar for diferente
                    resultado = avaliacaoDAO.AtualizarPontoLikeDeslike(avaliacao, pontosUsuario);

                }

            }
            else
            {
                // Se caso o tópico não foi respondido Inserir pontos 
                resultado = avaliacaoDAO.InserirPontoLikeDeslike(avaliacao, pontosUsuario);
            }

            return resultado;
        }
        #endregion








        #region método BLL para ligar a classe DAO onde é possivel deletar um id Avaliacao
        public bool ApagarAvaliacao(Avaliacao avaliacao)
        {
            AvaliacaoDAO avaliacaoDAO = new AvaliacaoDAO();
            bool resposta = avaliacaoDAO.DeletarAvaliacao(avaliacao);
            return resposta;
        }
        #endregion

        #region consultar avaliacao média e pontos
        public Avaliacao consultaAvaliacao(Avaliacao avalicao,int idUsuario)
        {
            AvaliacaoDAO avaliacaoDAO = new AvaliacaoDAO();
            avalicao.pontos = converterPontosEstrelas(avaliacaoDAO.consultaAvaliarpontos(avalicao, idUsuario));
            avalicao.mediaPontos = converterPontosEstrelas(avaliacaoDAO.consultaMediaAvaliacao(avalicao));
            return avalicao;
        }
        #endregion

        public float converterPontosEstrelas(float pontos)
        {
            float conversao = pontos * 10;
            conversao = conversao / 2;
            return conversao;
        }

        #region consultar pontos respondido e quantidade 
        public Avaliacao consultarAvaliacaoCurtir(Topico topico,int idUsuarioLogado)
        {
            
            AvaliacaoDAO avaliacaoDao = new AvaliacaoDAO();
            Avaliacao avaliacao = new Avaliacao();
            avaliacao.pontos = avaliacaoDao.consultaLikeDeslike(topico,idUsuarioLogado);
            avaliacao.contarLike = avaliacaoDao.consultaLike(topico, idUsuarioLogado);
            avaliacao.contarDeslike = avaliacaoDao.consultaDeslike(topico, idUsuarioLogado);
            return avaliacao;


        }
        #endregion





    }
}
