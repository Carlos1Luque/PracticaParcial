public class Auto
{
    private int id; 
    private string patente = string.Empty;
    private bool vendido; 
    private Usuario? usuario; 
    public int Id {get{return id;} set {id = value;}}
    public string Patente{get{return patente;} set{patente = value;}}
    public bool Vendido{get{return vendido;} set{vendido = value;}}
    public Usuario? Usuario{get{return usuario;} set{usuario = value;}}
    public Auto()
{
}

    public Auto(int id, string patente, bool vendido, Usuario? usuario)
    {
        this.id = id;
        this.patente = patente;
        this.vendido = vendido;
        this.usuario = usuario;
    }
}