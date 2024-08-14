using ScreenSound_OFC.Modelos;
using ScreenSound_OFC.Banco;

namespace ScreenSound_OFC.Menus;

internal class MenuSair : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
