import React from "react";
import Navbar from "../components/Navbar/Navbar";
import { Switch, Route, Redirect } from "react-router-dom";
import Recibos from "../views/Recibos/Recibos";
import Usuarios from "../views/Usuarios/Usuarios";

const RutaTablero = () => {
  return (
    <>
      <Navbar />
      <div>
        <Switch>
          <Route exact path="/recibos" component={Recibos} />
          <Route exact path="/usuarios" component={Usuarios} />
          <Redirect to="/recibos" />
        </Switch>
      </div>
    </>
  );
};

export default RutaTablero;
