import React from "react";
import "./Navbar.css";

const Navbar = () => {
  return (
    <nav className="Nav">
      <ul className="Nav__links">
        <li>
          <a href="/" className="Nav__link">
            Recibos
          </a>
        </li>
      </ul>
    </nav>
  );
};

export default Navbar;
