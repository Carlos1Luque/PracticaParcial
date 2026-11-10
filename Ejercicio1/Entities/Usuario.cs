public class Usuario
{
    private int id; 
    private string nombre;
    private string apellido; 
    private bool estado; 
    public int Id {get{return id;} set{id = value;}}
    public string Nombre{get{return nombre;} set{nombre = value;}}
    public string Apellido{get{return apellido;} set{apellido = value;}}
    public bool Estado{get{return estado;} set{estado = value;}}
    public Usuario(int id, string nombre, string apellido, bool estado)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.estado = estado; 
    }
}