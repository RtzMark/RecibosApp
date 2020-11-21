import React, { useState, useEffect } from "react";
import Navbar from "./components/Navbar/Navbar";
import Login from "./views/Login";
import Axios from "axios";
import {
  setToken,
  getToken,
  eliminarToken,
  initAxiosInterceptar,
} from "./Helpers/auth-helpers";

initAxiosInterceptar();

const RecibosApp = () => {
  const [usuario, setUsuario] = useState(null);
  const [cargandoUsuario, setCargandoUsuario] = useState(true);

  useEffect(() => {
    const cargarUsuario = async () => {
      if (!getToken()) {
        setCargandoUsuario(false);
        return;
      }

      try {
        const { error, mensaje, datos } = await Axios.get(
          "http://localhost:52234/api/Usuario"
        );
        setUsuario(datos);
      } catch (error) {}
    };

    cargarUsuario();
  }, []);

  const login = async (email, clave) => {
    const { error, mensaje, datos } = await Axios.post(
      "http://localhost:52234/api/Acceso/Login",
      {
        email,
        clave,
      }
    );

    if (!error) {
      setUsuario(datos.usuario);
      setToken(datos.token);
    }
  };

  const logout = async () => {
    setUsuario(null);
    eliminarToken();
  };

  return (
    <div>
      <Navbar />
      <Login login={login} />
      <div>{JSON.stringify(usuario)}</div>
    </div>
  );
};

export default RecibosApp;
