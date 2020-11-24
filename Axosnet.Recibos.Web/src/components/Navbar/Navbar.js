import React, { useContext } from "react";
import { Link, useHistory } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSignOutAlt } from "@fortawesome/free-solid-svg-icons";

import { AuthContext } from "../../auth/auth-context";
import { types } from "../../types/types";

import { eliminarToken } from "../../auth/auth-helpers";

import "./Navbar.css";

const Navbar = () => {
  const {
    user: { email },
    dispatch,
  } = useContext(AuthContext);
  const history = useHistory();

  const handleLogout = () => {
    history.replace("/login");

    eliminarToken();
    dispatch({
      type: types.logout,
    });
  };

  return (
    <nav className="Nav">
      <ul className="Nav__links">
        <li>
          <Link className="Nav__link" to="/">
            Inicio
          </Link>
        </li>
        <li className="Nav__link-margin-left">
          <Link className="Nav__link" to="/recibos">
            Recibos
          </Link>
        </li>
        <li className="Nav__link-margin-left">
          <Link className="Nav__link" to="/usuarios">
            Usuarios
          </Link>
        </li>
        <li className="Nav__link-push">
          <Link className="Nav__link" to="/">
            {email}
          </Link>
        </li>
        <li className="Nav__link-margin-left">
          <button className="Nav__link" onClick={handleLogout}>
            <FontAwesomeIcon icon={faSignOutAlt} />
          </button>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
