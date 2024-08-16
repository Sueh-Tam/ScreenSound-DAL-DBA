﻿namespace ScreenSound_OFC.Modelos;

public class Musica
{
    public Musica()
    {

    }
    public Musica(string nome, int artistaId, int anoLancamento)
    {
        Nome = nome;
        AnoLancamento = anoLancamento;
        ArtistaId = artistaId;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public int? ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }


    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");

    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";

    }
}