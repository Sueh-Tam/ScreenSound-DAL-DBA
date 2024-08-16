using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound_OFC.Banco;
using ScreenSound_OFC.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndPointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });


        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {

            var artista = dal.RecuperarPor(
                a => a.Nome.ToUpper().Equals(
                    nome.ToUpper()
                )
            );
            if (artista is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(artista);
        });
        /*
         * Create Artista, recebe o JSON no seguinte formato
         * {
            "nome": "Nome do artista/banda",
            "bio":"bio da banda/artista",
            "FotoPerfil": "link para a foto"
            }
        */
        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var artista = new Artista(artistaRequest.nome,artistaRequest.bio, artistaRequest.FotoPerfil);
            dal.Adicionar(artista);

            Results.Ok(artista);

        });
        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.RecuperarPor(a => a.Id == id);
            if (artista is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(artista);
            return Results.NoContent();
        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) => {
            var artistaAAtualizar = dal.RecuperarPor(a => a.Id == artistaRequestEdit.Id);
            if (artistaAAtualizar is null)
            {
                return Results.NotFound();
            }
            artistaAAtualizar.Nome = artistaRequestEdit.nome;
            artistaAAtualizar.Bio = artistaRequestEdit.bio;
            dal.Atualizar(artistaAAtualizar);
            return Results.Ok();
        });

    }
}
