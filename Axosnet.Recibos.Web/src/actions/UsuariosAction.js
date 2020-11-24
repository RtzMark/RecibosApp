import HttpCliente from "../services/HttpCliente";

const RUTA_SERVICIO = "Usuarios";

export const columnas = [
  {
    title: "Nombre",
    field: "nombre",
  },
  {
    title: "Email",
    field: "email",
  },
];

export const ObtenerUsuariosAync = async () => {
  return await HttpCliente.get(RUTA_SERVICIO);
};

export const ObtenerUsuarioAync = async (id) => {
  return await HttpCliente.get(`${RUTA_SERVICIO}/${id}`);
};

export const AgregarUsuarioAync = async (usuario) => {
  return await HttpCliente.post(RUTA_SERVICIO, usuario);
};

export const EditarUsuarioAync = async (usuario) => {
  return await HttpCliente.put(RUTA_SERVICIO, usuario);
};

export const EliminarUsuarioAync = async (id) => {
  return await HttpCliente.delete(`${RUTA_SERVICIO}/${id}`);
};
