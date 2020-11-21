import React, { useState } from "react";
import Main from "../components/Main/Main";

const Login = ({ login }) => {
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
      login(datosLogin.email, datosLogin.clave);
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
            <button type="submit" className="Form__submit">
              Login
            </button>
          </form>
        </div>
      </div>
    </Main>
  );
};

export default Login;
