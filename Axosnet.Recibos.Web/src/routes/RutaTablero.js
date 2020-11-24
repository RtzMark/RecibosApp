import React, { useState } from "react";
import { Switch, Route, Redirect } from "react-router-dom";

import Navbar from "../components/Navbar/Navbar";
import { Error } from "../components/Errorbar/Error";

import Recibos from "../views/Recibos";
import Usuarios from "../views/Usuarios";
import Home from "../views/Home";

const RutaTablero = () => {
  const [error, setError] = useState();

  const ocultarError = () => {
    setError(null);
  };

  return (
    <>
      <Navbar />
      <Error mensaje={error} esconderError={ocultarError} />
      <div>
        <Switch>
          <Route path="/Inicio" render={(props) => <Home />} default />
          <Route
            path="/recibos"
            render={(props) => <Recibos setError={setError} />}
          />
          <Route
            path="/usuarios"
            render={(props) => <Usuarios setError={setError} />}
          />
          <Redirect to="/Inicio" />
        </Switch>
      </div>
    </>
  );
};

export default RutaTablero;
