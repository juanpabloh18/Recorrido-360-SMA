using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Usuario
{
    public string nombreCompleto;
    public string correo;
    public string password;
}

[System.Serializable]
public class ListaUsuarios
{
    public List<Usuario> usuarios = new List<Usuario>();
}
