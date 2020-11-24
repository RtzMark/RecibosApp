import React, { useState, useEffect } from "react";

import MaterialTable from "material-table";

import Main from "../components/Main/Main";
import Loading from "../components/Loading/Loading";
import ModalUsuario from "../components/Modals/ModalUsuario";

import {
  columnas,
  ObtenerUsuariosAync,
  ObtenerUsuarioAync,
  AgregarUsuarioAync,
  EditarUsuarioAync,
  EliminarUsuarioAync,
} from "../actions/UsuariosAction";

const Usuarios = ({ setError }) => {
  const [loading, setLoading] = useState(false);
  const [listadoUsuario, setListadoUsuario] = useState([]);
  const [estatusModal, setEstatusModal] = useState(false);
  const [tituloModal, setTituloModal] = useState("");
  const [datosUsuario, setDatosUsuario] = useState({
    id: "00000000-0000-0000-0000-000000000000",
    proveedor: "",
    monto: "",
    moneda: "",
    fecha: "",
    comentario: "",
  });
  const [errorModal, setErrorModal] = useState();
  const [accionUsuario, setAccionUsuario] = useState(true);

  const ObtenerUsuarios = async () => {
    setLoading(true);
    const { error, mensaje, datos } = await ObtenerUsuariosAync();

    if (error) {
      setError(mensaje);
      setListadoUsuario(null);
    } else {
      setListadoUsuario(datos);
    }
    setLoading(false);
  };

  useEffect(() => {
    ObtenerUsuarios();
  }, []);

  const handleInputChange = (e) => {
    setDatosUsuario({
      ...datosUsuario,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (accionUsuario) {
      agregarUsuario(datosUsuario);
    } else {
      editarUsuario(datosUsuario);
    }
  };

  const abrirCerrarModal = () => {
    setEstatusModal(!estatusModal);
  };

  const abrirModalAgregar = () => {
    setTituloModal("Agregar Usuario");
    setAccionUsuario(true);
    setDatosUsuario({
      id: "00000000-0000-0000-0000-000000000000",
      nombre: "",
      email: "",
      clave: "",
    });
    setEstatusModal(!estatusModal);
  };

  const abrirModalEditar = (datos) => {
    setTituloModal("Editar Usuario");
    setAccionUsuario(false);
    cargardatosUsuario(datos.id);
  };

  const ocultarErrorModal = () => {
    setErrorModal(null);
  };

  const cargardatosUsuario = (id) => {
    ObtenerUsuarioAync(id).then(({ error, mensaje, datos }) => {
      if (error) {
        setError(mensaje);
      } else {
        setEstatusModal(!estatusModal);
        setDatosUsuario(datos);
      }
    });
  };

  const agregarUsuario = (Usuario) => {
    AgregarUsuarioAync(Usuario).then(({ error, mensaje, datos }) => {
      console.log(mensaje);
      if (error) {
        setErrorModal(mensaje);
      } else {
        setEstatusModal(!estatusModal);
        ObtenerUsuarios();
      }
    });
  };

  const editarUsuario = (Usuario) => {
    EditarUsuarioAync(Usuario).then(({ error, mensaje, datos }) => {
      if (error) {
        setErrorModal(mensaje);
      } else {
        setEstatusModal(!estatusModal);
        ObtenerUsuarios();
      }
    });
  };

  const eliminarUsuario = (id) => {
    EliminarUsuarioAync(id).then(({ error, mensaje, datos }) => {
      if (error) {
        setError(mensaje);
      } else {
        setDatosUsuario(datos);
        ObtenerUsuarios();
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
        title="Usuarios"
        columns={columnas}
        data={listadoUsuario}
        actions={[
          {
            icon: "edit",
            tooltip: "Editar Usuario",
            onClick: (event, rowData) => abrirModalEditar(rowData),
          },
          {
            icon: "delete",
            tooltip: "Eliminar Usuario",
            onClick: (event, rowData) => eliminarUsuario(rowData.id),
          },
        ]}
        options={{ actionsColumnIndex: -1 }}
        localization={{ header: { actions: "Acción" } }}
      />

      <ModalUsuario
        titulo={tituloModal}
        datosUsuario={datosUsuario}
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

export default Usuarios;
