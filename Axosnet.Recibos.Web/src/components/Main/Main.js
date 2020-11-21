import React from "react";
import "./Main.css";

const Main = ({ children, center }) => {
  const clases = `Main ${center ? "Main--center" : ""}`;

  return <main className={clases}>{children}</main>;
};

export default Main;
