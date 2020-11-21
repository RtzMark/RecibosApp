import React, { useReducer, useEffect } from "react";

import RutaRecibo from "./routes/RutaRecibo";

import { AuthContext } from "./auth/auth-context";
import AuthReducer from "./auth/auth-reducer";

const init = () => {
  return JSON.parse(localStorage.getItem("user")) || { logged: false };
};

const RecibosApp = () => {
  const [user, dispatch] = useReducer(AuthReducer, {}, init);

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
