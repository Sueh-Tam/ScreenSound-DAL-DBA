using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound_OFC.Banco;
using ScreenSound_OFC.Modelos;

namespace ScreenSound.API.Endpoints;

public static class MusicasExtensions
{
    public static void AddEndPointsMusicas(this WebApplication app)
    {
        #region Get all musicas
        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            return Results.Ok(dal.Listar());
        });
        #endregion
        #region Get one musica
        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (musica is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(musica);

        });
        #endregion
        #region Post one musica
        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
        {
            var musica = new Musica(musicaRequest.nome, musicaRequest.ArtistaId, musicaRequest.anoLancamento);
            dal.Adicionar(musica);
            Results.Ok(musica);
        });
        #endregion
        #region Put one musica
        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequestEdit) => {
            var musicaAAtualizar = dal.RecuperarPor(a => a.Id == musicaRequestEdit.Id);
            if (musicaAAtualizar is null)
            {
                return Results.NotFound();
            }
            musicaAAtualizar.Nome = musicaRequestEdit.nome;
            musicaAAtualizar.AnoLancamento = musicaRequestEdit.anoLancamento;

            dal.Atualizar(musicaAAtualizar);
            return Results.Ok();
        });
        #endregion
        #region Delete one Musica
        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            var musica = dal.RecuperarPor(a => a.Id == id);
            if (musica is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(musica);
            return Results.NoContent();
        });
        #endregion
    }
}
