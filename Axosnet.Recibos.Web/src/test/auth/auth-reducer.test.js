import AuthReducer from "../../auth/auth-reducer";
import { types } from "../../types/types";

describe("Pruebas en auth-reducer", () => {
  test("debe de retornar el estado por defecto", () => {
    const state = AuthReducer({ logged: false }, {});
    expect(state).toEqual({ logged: false });
  });
  test("debe de autentucar y colocar el name del usuario", () => {
    const accion = {
      type: types.login,
      payload: {
        name: "Marco",
      },
    };

    const state = AuthReducer({ logged: false }, accion);
    expect(state).toEqual({ logged: true, name: "Marco" });
  });
  test("debe de borrar el name del usuario y logger en false", () => {
    const accion = {
      type: types.logout,
    };

    const state = AuthReducer({ logged: true, name: "Marco" }, accion);
    expect(state).toEqual({ logged: false });
  });
});
