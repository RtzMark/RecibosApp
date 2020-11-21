import Axios from "axios";

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

export const initAxiosInterceptar = () => {
  Axios.interceptors.request.use((config) => {
    const token = getToken();

    if (token) {
      config.headers.Authorization = `bearer ${token}`;
    }

    return config;
  });

  Axios.interceptors.response.use(
    ({ data }) => data,
    (error) => {
      if (error.response.status === 401) {
        eliminarToken();
        window.location = "/login";
      } else {
        return Promise.reject(error);
      }
    }
  );
};
