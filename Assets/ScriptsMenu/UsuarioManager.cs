using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UsuarioManager : MonoBehaviour
{
    private string filePath;
    private ListaUsuarios listaUsuarios;

    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "usuarios.json");
        CargarUsuarios();
    }

    public bool CrearUsuario(string nombre, string correo, string password)
    {
        if (listaUsuarios == null)
            listaUsuarios = new ListaUsuarios();

        if (listaUsuarios.usuarios.Exists(u => u.correo == correo))
        {
            Debug.Log("El correo ya está registrado");
            return false;
        }

        Usuario nuevo = new Usuario
        {
            nombreCompleto = nombre,
            correo = correo,
            password = password
        };

        listaUsuarios.usuarios.Add(nuevo);
        GuardarUsuarios();
        Debug.Log("Usuario registrado correctamente");
        return true;
    }

    public bool IniciarSesion(string correo, string password)
    {
        if (listaUsuarios == null) return false;

        Usuario u = listaUsuarios.usuarios.Find(u => u.correo == correo && u.password == password);
        return u != null;
    }

    private void GuardarUsuarios()
    {
        string json = JsonUtility.ToJson(listaUsuarios, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Usuarios guardados en: " + filePath);
    }

    private void CargarUsuarios()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            listaUsuarios = JsonUtility.FromJson<ListaUsuarios>(json);

            if (listaUsuarios == null)
                listaUsuarios = new ListaUsuarios();
        }
        else
        {
            listaUsuarios = new ListaUsuarios();
        }
    }
}
