namespace ApiLogin.Servicio
{
    public interface IAuthServicio
    {
        string Autenticar(string nombreUsuario, string contrasena);
    }

}
