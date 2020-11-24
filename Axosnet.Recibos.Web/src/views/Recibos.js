import React, { useState, useEffect } from "react";

import MaterialTable from "material-table";
import Moment from "moment";

import Main from "../components/Main/Main";
import Loading from "../components/Loading/Loading";
import ModalRecibo from "../components/Modals/ModalRecibo";

import {
  columnas,
  ObtenerRecibosAync,
  ObtenerReciboAync,
  AgregarReciboAync,
  EditarReciboAync,
  EliminarReciboAync,
} from "../actions/RecibosAction";

const Recibos = ({ mostrarError }) => {
  const [loading, setLoading] = useState(false);
  const [listadoRecibo, setListadoRecibo] = useState([]);
  const [estatusModal, setEstatusModal] = useState(false);
  const [tituloModal, setTituloModal] = useState("");
  const [datosRecibo, setdatosRecibo] = useState({
    id: 0,
    proveedor: "",
    monto: "",
    moneda: "",
    fecha: "",
    comentario: "",
  });
  const [errorModal, setErrorModal] = useState();
  const [accionRecibo, setAccionRecibo] = useState(true);

  const ObtenerRecibos = async () => {
    setLoading(true);
    const { error, mensaje, datos } = await ObtenerRecibosAync();

    if (error) {
      mostrarError(mensaje);
      setListadoRecibo(null);
    } else {
      setListadoRecibo(datos);
    }
    setLoading(false);
  };

  useEffect(() => {
    ObtenerRecibos();
  }, []);

  const handleInputChange = (e) => {
    setdatosRecibo({
      ...datosRecibo,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (accionRecibo) {
      agregarRecibo(datosRecibo);
    } else {
      editarRecibo(datosRecibo);
    }
  };

  const abrirCerrarModal = () => {
    setEstatusModal(!estatusModal);
  };

  const abrirModalAgregar = () => {
    setTituloModal("Agregar Recibo");
    setAccionRecibo(true);
    setdatosRecibo({
      id: 0,
      proveedor: "",
      monto: "",
      moneda: "",
      fecha: "",
      comentario: "",
    });
    setEstatusModal(!estatusModal);
  };

  const abrirModalEditar = (datos) => {
    setTituloModal("Editar Recibo");
    setAccionRecibo(false);
    cargarDatosRecibo(datos.id);
  };

  const ocultarErrorModal = () => {
    setErrorModal(null);
  };

  const cargarDatosRecibo = (id) => {
    ObtenerReciboAync(id).then(({ error, mensaje, datos }) => {
      if (error) {
        mostrarError(mensaje);
      } else {
        setEstatusModal(!estatusModal);
        datos.fecha = Moment(datos.fecha).format("YYYY-MM-DD");
        setdatosRecibo(datos);
      }
    });
  };

  const agregarRecibo = (recibo) => {
    AgregarReciboAync(recibo).then(({ error, mensaje, datos }) => {
      if (error) {
        setErrorModal(mensaje);
      } else {
        setEstatusModal(!estatusModal);
        ObtenerRecibos();
      }
    });
  };

  const editarRecibo = (recibo) => {
    EditarReciboAync(recibo).then(({ error, mensaje, datos }) => {
      if (error) {
        setErrorModal(mensaje);
      } else {
        setEstatusModal(!estatusModal);
        ObtenerRecibos();
      }
    });
  };

  const eliminarRecibo = (id) => {
    EliminarReciboAync(id).then(({ error, mensaje, datos }) => {
      if (error) {
        mostrarError(mensaje);
      } else {
        setdatosRecibo(datos);
        ObtenerRecibos();
      }
    });
  };

  if (loading) {
    return (
      <Main>
        <Loading />
      </Main>
    );
  }

  return (
    <Main center>
      <button className="Form__button" onClick={abrirModalAgregar}>
        Agregar
      </button>
      <MaterialTable
        title="Recibos"
        columns={columnas}
        data={listadoRecibo}
        actions={[
          {
            icon: "edit",
            tooltip: "Editar recibo",
            onClick: (event, rowData) => abrirModalEditar(rowData),
          },
          {
            icon: "delete",
            tooltip: "Eliminar recibo",
            onClick: (event, rowData) => eliminarRecibo(rowData.id),
          },
        ]}
        options={{ actionsColumnIndex: -1 }}
        localization={{ header: { actions: "Acción" } }}
      />

      <ModalRecibo
        titulo={tituloModal}
        datosRecibo={datosRecibo}
        handleSubmit={handleSubmit}
        handleInputChange={handleInputChange}
        estatusModal={estatusModal}
        abrirCerrarModal={abrirCerrarModal}
        errorModal={errorModal}
        ocultarErrorModal={ocultarErrorModal}
      />
    </Main>
  );
};

export default Recibos;
