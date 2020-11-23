import React, { useReducer, useEffect } from "react";

import HttpCliente, { initAxiosInterceptar } from "./services/HttpCliente";
import { getToken } from "./auth/auth-helpers";
import { AuthContext } from "./auth/auth-context";
import AuthReducer from "./auth/auth-reducer";
import { types } from "./types/types";

import RutaRecibo from "./routes/RutaRecibo";

initAxiosInterceptar();

const cargarUsuario = () => {
  if (!getToken()) {
    return { logged: false };
  }

  return { logged: true };
};

const RecibosApp = () => {
  const [user, dispatch] = useReducer(AuthReducer, {}, cargarUsuario);

  useEffect(() => {
    const cargarUsuario = async () => {
      if (user.logged) {
        try {
          const { error, datos } = await HttpCliente.get("Usuario");

          if (error) {
            dispatch({
              type: types.logout,
            });
          } else {
            dispatch({
              type: types.login,
              payload: {
                email: datos.email,
              },
            });
          }
        } catch (error) {
          dispatch({
            type: types.logout,
          });
        }
      }
    };

    cargarUsuario();
  }, [user.logged]);

  return (
    <AuthContext.Provider value={{ user, dispatch }}>
      <RutaRecibo />
    </AuthContext.Provider>
  );
};

export default RecibosApp;
