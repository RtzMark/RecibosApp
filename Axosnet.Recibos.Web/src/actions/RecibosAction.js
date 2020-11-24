import HttpCliente from "../services/HttpCliente";

const RUTA_SERVICIO = "Recibos";

export const columnas = [
  {
    title: "Proveedor",
    field: "proveedor",
  },
  {
    title: "Monto",
    field: "monto",
    type: "currency",
  },
  {
    title: "Moneda",
    field: "moneda",
  },
  {
    title: "Fecha",
    field: "fecha",
    type: "date",
  },
  {
    title: "Comentario",
    field: "comentario",
  },
];

export const datos = [
  {
    proveedor: "Ejemplo provedor",
    monto: 100,
    moneda: "Ejemplo moneda",
    fecha: "2020-11-22",
    comentario: "Sin comentarios",
  },
  {
    proveedor: "Ejemplo segundo provedor",
    monto: 250,
    moneda: "Ejemplo moneda",
    fecha: "2020-11-22",
    comentario: "Sin comentarios",
  },
];

export const ObtenerRecibosAync = async () => {
  return await HttpCliente.get(RUTA_SERVICIO);
};

export const ObtenerReciboAync = async (id) => {
  return await HttpCliente.get(`${RUTA_SERVICIO}/${id}`);
};

export const AgregarReciboAync = async (recibo) => {
  return await HttpCliente.post(RUTA_SERVICIO, recibo);
};

export const EditarReciboAync = async (recibo) => {
  return await HttpCliente.put(RUTA_SERVICIO, recibo);
};

export const EliminarReciboAync = async (id) => {
  return await HttpCliente.delete(`${RUTA_SERVICIO}/${id}`);
};
