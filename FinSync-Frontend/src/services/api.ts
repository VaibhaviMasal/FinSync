import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7210/api", // your backend
});

export default api;