import React, { useContext } from "react";
import { BrowserRouter as Router, Switch } from "react-router-dom";

import { AuthContext } from "../auth/auth-context";
import RutaPrivada from "./RutaPrivada";
import RutaPublica from "./RutaPublica";

import RutaTablero from "./RutaTablero";
import Login from "../views/Login";

const RutaRecibo = () => {
  const { user } = useContext(AuthContext);

  return (
    <Router>
      <div>
        <Switch>
          <RutaPublica
            exact
            path="/login"
            component={Login}
            isAuthenticated={user.logged}
          />

          <RutaPrivada
            path="/"
            component={RutaTablero}
            isAuthenticated={user.logged}
          />
        </Switch>
      </div>
    </Router>
  );
};

export default RutaRecibo;
