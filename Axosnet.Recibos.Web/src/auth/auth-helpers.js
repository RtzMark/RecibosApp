const TOKEN_KEY = "RECIBOS_TOKEN";

export const setToken = (token) => {
  localStorage.setItem(TOKEN_KEY, token);
};

export const getToken = () => {
  return localStorage.getItem(TOKEN_KEY);
};

export const eliminarToken = () => {
  localStorage.removeItem(TOKEN_KEY);
};
