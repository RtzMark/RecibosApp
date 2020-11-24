import Axios from "axios";
import { getToken, eliminarToken } from "../auth/auth-helpers";

Axios.defaults.baseURL = "https://recibos-api.azurewebsites.net/api";

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
    ({ response }) => {
      if (response.status === 401) {
        eliminarToken();
        window.location = "/login";
      } else {
        return response.data;
      }
    }
  );
};

const requestGenerico = {
  get: (url) => Axios.get(url),
  post: (url, body) => Axios.post(url, body),
  put: (url, body) => Axios.put(url, body),
  delete: (url) => Axios.delete(url),
};

export default requestGenerico;
