import React from "react";
import { Modal } from "@material-ui/core";

import "../../styles/Forms.css";
import "./Modals.css";

import { ErrorModal } from "../../components/Errorbar/Error";

const ModalRecibo = ({
  titulo,
  datosRecibo,
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
          name="proveedor"
          placeholder="Proveedor"
          className="Form__field"
          autoFocus
          required
          onChange={handleInputChange}
          value={datosRecibo.proveedor || ""}
        />

        <input
          type="number"
          name="monto"
          placeholder="Monto"
          className="Form__field"
          required
          onChange={handleInputChange}
          value={datosRecibo.monto || ""}
        />

        <input
          type="text"
          name="moneda"
          placeholder="Moneda"
          className="Form__field"
          required
          onChange={handleInputChange}
          value={datosRecibo.moneda || ""}
        />

        <input
          type="date"
          name="fecha"
          placeholder="Fecha"
          className="Form__field"
          required
          onChange={handleInputChange}
          value={datosRecibo.fecha || ""}
        />

        <input
          type="text"
          name="comentario"
          placeholder="Comentario"
          className="Form__field"
          onChange={handleInputChange}
          value={datosRecibo.comentario || ""}
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

export default ModalRecibo;
