import React, { useReducer, useEffect } from "react";

import { initAxiosInterceptar } from "./services/HttpCliente";
import { getToken } from "./auth/auth-helpers";
import { AuthContext } from "./auth/auth-context";
import AuthReducer from "./auth/auth-reducer";

import RutaRecibo from "./routes/RutaRecibo";

initAxiosInterceptar();

const cargarUsuario = () => {
  return getToken() && localStorage.getItem("user")
    ? JSON.parse(localStorage.getItem("user"))
    : { logged: false };
};

const RecibosApp = () => {
  const [user, dispatch] = useReducer(AuthReducer, {}, cargarUsuario);

  useEffect(() => {
    localStorage.setItem("user", JSON.stringify(user));
  }, [user]);

  return (
    <AuthContext.Provider value={{ user, dispatch }}>
      <RutaRecibo />
    </AuthContext.Provider>
  );
};

export default RecibosApp;
