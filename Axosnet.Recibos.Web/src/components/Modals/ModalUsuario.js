import React from "react";
import { Modal } from "@material-ui/core";

import "../../styles/Forms.css";
import "./Modals.css";

import { ErrorModal } from "../../components/Errorbar/Error";

const ModalUsuario = ({
  titulo,
  datosUsuario,
  handleSubmit,
  handleInputChange,
  estatusModal,
  abrirCerrarModal,
  errorModal,
  ocultarErrorModal,
}) => {
  return (
    <Modal open={estatusModal} onClose={abrirCerrarModal}>
      <form className="ModalContainer">
        <h1 className="Form__titulo">{titulo}</h1>
        <ErrorModal mensaje={errorModal} esconderError={ocultarErrorModal} />
        <hr />
        <input
          type="text"
          name="nombre"
          placeholder="Nombre usuario"
          className="Form__field"
          autoFocus
          required
          onChange={handleInputChange}
          value={datosUsuario.nombre || ""}
        />

        <input
          type="email"
          name="email"
          placeholder="Email"
          className="Form__field"
          required
          onChange={handleInputChange}
          value={datosUsuario.email || ""}
        />

        <input
          type="password"
          name="clave"
          placeholder="Contraseña"
          className="Form__field"
          onChange={handleInputChange}
          value={datosUsuario.clave || ""}
        />
        <hr />

        <div>
          <button
            type="submit"
            className="Modal__button"
            onClick={handleSubmit}
          >
            Guardar
          </button>
          <button
            type="button"
            className="Modal__button__cancel"
            onClick={abrirCerrarModal}
          >
            Cancelar
          </button>
        </div>
      </form>
    </Modal>
  );
};

export default ModalUsuario;
