import React, { useState, useContext } from "react";
import Axios from "axios";

import { AuthContext } from "../auth/auth-context";
import { initAxiosInterceptar } from "../auth/auth-helpers";
import { types } from "../types/types";

import Main from "../components/Main/Main";

initAxiosInterceptar();

const Login = () => {
  const { dispatch } = useContext(AuthContext);
  const [datosLogin, setDatosLogin] = useState({
    email: "",
    clave: "",
  });

  const handleInputChange = (e) => {
    setDatosLogin({
      ...datosLogin,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const { error, mensaje, datos } = await Axios.post(
        "http://localhost:52234/api/Acceso/Login",
        datosLogin
      );

      if (!error) {
        dispatch({
          type: types.login,
          payload: {
            email: datos.usuario.email,
          },
        });
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <Main center>
      <div className="FormContainer">
        <h1 className="Form__titulo">Recibos App</h1>
        <div>
          <form onSubmit={handleSubmit}>
            <input
              type="email"
              name="email"
              placeholder="Email"
              className="Form__field"
              required
              onChange={handleInputChange}
              value={datosLogin.email}
            />
            <input
              type="password"
              name="clave"
              placeholder="Contraseña"
              className="Form__field"
              required
              onChange={handleInputChange}
              value={datosLogin.clave}
            />
            <button type="submit" className="Form__submit Login__Button">
              Login
            </button>
          </form>
        </div>
      </div>
    </Main>
  );
};

export default Login;
